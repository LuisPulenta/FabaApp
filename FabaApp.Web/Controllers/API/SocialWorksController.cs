using FabaApp.Web.Data;
using FabaApp.Web.Data.Entities;
using FabaApp.Web.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FabaApp.Web.Controllers.API
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class SocialWorksController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public SocialWorksController(DataContext context, IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
        }

        // GET: api/SocialWorks
        [HttpGet]
        public async Task<IActionResult> GetSocialWorks()
        {
        
            List<SocialWorkEntity> socialWorks = await _context.SocialWorks
                .Where(s => s.Active)
                .OrderBy(s => s.Name)
                .ToListAsync();
            return Ok(_converterHelper.ToSocialWorkResponse(socialWorks));
        }
    }
}