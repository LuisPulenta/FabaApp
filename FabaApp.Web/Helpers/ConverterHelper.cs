using FabaApp.Common.Models;
using FabaApp.Web.Data;
using FabaApp.Web.Data.Entities;
using System.Collections.Generic;

namespace FabaApp.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _context;


        public ConverterHelper(DataContext context)
        {
            _context = context;
        }

        public LabResponse ToLabResponse(LabEntity lab)
        {
            return new LabResponse
            {
                Active = lab.Active,
                Address = lab.Address,
                Contact = lab.Contact,
                CUIT = lab.CUIT,
                DischargeDate = lab.DischargeDate,
                Email = lab.Email,
                Id = lab.Id,
                Name = lab.Name,
                Phone = lab.Phone
            };
        }

        public SocialWorkResponse ToSocialWorkResponse(SocialWorkEntity socialWork)
        {
            return new SocialWorkResponse
            {
                Id = socialWork.Id,
                Name = socialWork.Name,
                Active = socialWork.Active,

            };
        }

        public List<SocialWorkResponse> ToSocialWorkResponse(List<SocialWorkEntity> socialWorksEntities)
        {
            List<SocialWorkResponse> list = new List<SocialWorkResponse>();
            foreach (SocialWorkEntity socialWorkEntity in socialWorksEntities)
            {
                list.Add(ToSocialWorkResponse(socialWorkEntity));
            }

            return list;
        }

        public CodeResponse ToCodeResponse(CodeEntity code)
        {
            return new CodeResponse
            {
                Active = code.Active,
                Code = code.Code,
                Description = code.Description,
                Id = code.Id,
                SocialWork = ToSocialWorkResponse(code.SocialWork)
            };
        }

        public List<CodeResponse> ToCodeResponse(List<CodeEntity> codes)
        {
            List<CodeResponse> list = new List<CodeResponse>();
            foreach (CodeEntity CodeEntity in codes)
            {
                list.Add(ToCodeResponse(CodeEntity));
            }
            return list;
        }


        public UserResponse ToUserResponse(UserEntity user)
        {

            return new UserResponse
            {
                Active = user.Active,
                DischargeDate = user.DischargeDate,
                Document = user.Document,
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                Lab = ToLabResponse(user.Lab),
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                UserType = user.UserType
            };
        }


        public RecipeResponse ToRecipeResponse(RecipeEntity recipe)
        {

            return new RecipeResponse
            {
                DischargeDate = recipe.DischargeDate,
                Flag1 = recipe.Flag1,
                Flag2 = recipe.Flag2,
                Flag3 = recipe.Flag3,
                Flag4 = recipe.Flag4,
                Name = recipe.Name,
                RecipeDate = recipe.RecipeDate,
                State = recipe.State,
                StateDate = recipe.StateDate,
                Foto1=recipe.Foto1,
                Foto2 = recipe.Foto2,
                Foto3 = recipe.Foto3,
                Foto4 = recipe.Foto4,
                Id=recipe.Id,
                SocialWorkId=recipe.SocialWork.Id,
                
            };
        }

        public RecipeDetailResponse ToRecipeDetailResponse(RecipeDetailEntity recipeDetail)
        {

            return new RecipeDetailResponse
            {
                Code = recipeDetail.Code,
                Description = recipeDetail.Description,
                Quantity = recipeDetail.Quantity,
                RecipeId= recipeDetail.Recipe.Id,
                Id= recipeDetail.Id,
            };
        }


    }
}