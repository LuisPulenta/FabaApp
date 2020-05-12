using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FabaApp.Web.Data;
using FabaApp.Web.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using FabaApp.Common.Models;
using FabaApp.Web.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace FabaApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RecipeDetailsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public RecipeDetailsController(DataContext context, IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
        }



        [HttpPost]
        public async Task<IActionResult> PostRecipeDetail([FromBody] RecipeDetailRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var recipeDetail = new RecipeDetailEntity
            {
                Code=request.Code,
                Description = request.Description,
                Quantity = request.Quantity,
                Recipe= await _context.Recipes.FindAsync(request.RecipeId),
            };

            _context.RecipeDetails.Add(recipeDetail);
            await _context.SaveChangesAsync();
            return Ok(_converterHelper.ToRecipeDetailResponse(recipeDetail));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipeDetail([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var recipeDetail = await _context.RecipeDetails
                .FirstOrDefaultAsync(p => p.Id == id);
            if (recipeDetail == null)
            {
                return this.NotFound();
            }

            _context.RecipeDetails.Remove(recipeDetail);
            await _context.SaveChangesAsync();
            return Ok("J");
        }

    }
}