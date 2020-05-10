using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using FabaApp.Common.Enums;
using FabaApp.Common.Models;
using FabaApp.Web.Data;
using FabaApp.Web.Data.Entities;
using FabaApp.Web.Helpers;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

namespace Soccer.Web.Controllers.API
{
    [Route("api/[Controller]")]
    public class AccountController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;
        private readonly IMailHelper _mailHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;

        public AccountController(
            DataContext dataContext,
            IUserHelper userHelper,
            IMailHelper mailHelper,
            IImageHelper imageHelper,
            IConverterHelper converterHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
            _mailHelper = mailHelper;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
        }

        

        [HttpPost]
        [Route("RecoverPassword")]
        public async Task<IActionResult> RecoverPassword([FromBody] EmailRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "Bad request"
                });
            }

            var user = await _userHelper.GetUserAsync(request.Email);
            if (user == null)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "Este mail no pertenece a ningún Usuario."
                });
            }

            var myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action("ResetPassword", "Account", new { token = myToken }, protocol: HttpContext.Request.Scheme);
            _mailHelper.SendMail(request.Email, "Password Reset", $"<h1>Recover Password</h1>" +
                $"Para resetear el Password haga clic en este link: </br></br>" +
                $"<a href = \"{link}\">Resetear Password</a>");

            return Ok(new Response
            {
                IsSuccess = true,
                Message = "Se le envió un mail con instrucciones para resetear el Password."
            });
        }

       
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "Bad request",
                    Result = ModelState
                });
            }


            UserEntity user = await _userHelper.GetUserAsync(request.Email);
            if (user == null)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "Usuario no encontrado"
                });
            }

            IdentityResult result = await _userHelper.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = result.Errors.FirstOrDefault().Description
                });
            }

            return Ok(new Response
            {
                IsSuccess = true,
                Message = "El password fue cambiado con éxito."
            });
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("GetUserByEmail")]
        public async Task<IActionResult> GetUserByEmail([FromBody] EmailRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }




            UserEntity userEntity = await _userHelper.GetUserAsync(request.Email);
            if (userEntity == null)
            {
                return NotFound("Este Usuario no existe.");
            }

            var user = _dataContext.Users
                .Include (l=>l.Lab)
                .FirstOrDefault(o => o.Id == userEntity.Id);



            //return Ok(_converterHelper.ToPlayerResponse(_converterHelper.ToPlayer(userEntity)));

            return Ok(_converterHelper.ToUserResponse(user));
        }


    }
}
