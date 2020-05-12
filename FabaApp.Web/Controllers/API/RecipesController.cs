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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FabaApp.Common.Models;
using System.IO;
using FabaApp.Common.Helpers;
using FabaApp.Web.Helpers;

namespace FabaApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RecipesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public RecipesController(DataContext context,IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
        }

        // GET: api/Recipes
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetRecipes([FromRoute] string id)

        [HttpGet("{id}")]
      
        public async Task<IActionResult> GetRecipes([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var recipes = await _context.Recipes
                .Include(m => m.SocialWork)
                .Include(u => u.User)
                .ThenInclude(l => l.Lab)
                .Include(g => g.RecipeDetails)
                .Where(u => u.User.Id == id)
                .OrderBy(t => t.RecipeDate)
                .ToListAsync();

            if (recipes == null)
            {
                return BadRequest("No hay Recetas para este Usuario.");
            }


            List<RecipeResponse> recipeResponses = new List<RecipeResponse>();

            foreach (RecipeEntity recipe in recipes)
            {
                RecipeResponse recipeResponse = await _converterHelper.ToRecipeResponse(recipe);
                recipeResponses.Add(recipeResponse);
            }

            return Ok(recipeResponses);
        }

        // PUT: api/Recipes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipeEntity([FromRoute] int id, [FromBody] RecipeEntity recipeEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != recipeEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(recipeEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Recipes
       
        [HttpPost]
        public async Task<IActionResult> PostRecipe([FromBody] RecipeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Foto1
            var imageUrl1 = string.Empty;
            if (request.PictureArray1 != null && request.PictureArray1.Length > 0)
            {
                var stream = new MemoryStream(request.PictureArray1);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "wwwroot\\images\\Recipes";
                var fullPath = $"~/images/Recipes/{file}";
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    imageUrl1 = fullPath;
                }
            }
            //Foto2
            var imageUrl2 = string.Empty;
            if (request.PictureArray2 != null && request.PictureArray2.Length > 0)
            {
                var stream = new MemoryStream(request.PictureArray2);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "wwwroot\\images\\Recipes";
                var fullPath = $"~/images/Recipes/{file}";
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    imageUrl2 = fullPath;
                }
            }
            //Foto3
            var imageUrl3 = string.Empty;
            if (request.PictureArray3 != null && request.PictureArray3.Length > 0)
            {
                var stream = new MemoryStream(request.PictureArray3);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "wwwroot\\images\\Recipes";
                var fullPath = $"~/images/Recipes/{file}";
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    imageUrl3 = fullPath;
                }
            }
            //Foto4
            var imageUrl4 = string.Empty;
            if (request.PictureArray4 != null && request.PictureArray4.Length > 0)
            {
                var stream = new MemoryStream(request.PictureArray4);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "wwwroot\\images\\Recipes";
                var fullPath = $"~/images/Recipes/{file}";
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    imageUrl4 = fullPath;
                }
            }

            var recipe = new RecipeEntity
            {
                DischargeDate=request.DischargeDate,
                Flag1 = request.Flag1,
                Flag2 = request.Flag2,
                Flag3 = request.Flag3,
                Flag4 = request.Flag4,
                Foto1= imageUrl1,
                Foto2 = imageUrl2,
                Foto3 = imageUrl3,
                Foto4 = imageUrl4,
                Name=request.Name,
                RecipeDate = request.RecipeDate,
                State = request.State,
                StateDate = request.StateDate,
                SocialWork=await _context.SocialWorks.FindAsync(request.SocialWorkId),
                User= await _context.Users.FindAsync(request.UserId),
            };

            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            var responseFinal = new RecipeRequest
            {
                Id = recipe.Id,
                Name = recipe.Name,
                DischargeDate = recipe.DischargeDate,
                RecipeDate = recipe.RecipeDate,
                SocialWorkId = recipe.SocialWork.Id,
                UserId = recipe.User.Id,
                Flag1 = recipe.Flag1,
                Flag2 = recipe.Flag2,
                Flag3 = recipe.Flag3,
                Flag4 = recipe.Flag4,
                State = recipe.State,
                StateDate = recipe.StateDate,
            };
            return Ok(responseFinal);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var recipe = await _context.Recipes
                .Include(p => p.RecipeDetails)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (recipe == null)
            {
                return this.NotFound();
            }



            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return Ok("Receta borrada");
        }

        private bool RecipeEntityExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }
    }
}