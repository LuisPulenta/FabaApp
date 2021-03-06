﻿using FabaApp.Common.Models;
using FabaApp.Web.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FabaApp.Web.Helpers
{
    public interface IConverterHelper
    {

        LabResponse ToLabResponse(LabEntity lab);

        UserResponse ToUserResponse(UserEntity user);

        SocialWorkResponse ToSocialWorkResponse(SocialWorkEntity socialWork);

        CodeResponse ToCodeResponse(CodeEntity code);

        List<SocialWorkResponse> ToSocialWorkResponse(List<SocialWorkEntity> socialWorkEntity);

        List<CodeResponse> ToCodeResponse(List<CodeEntity> code);
        
        

        Task<RecipeResponse> ToRecipeResponse(RecipeEntity recipe);

        RecipeDetailResponse ToRecipeDetailResponse(RecipeDetailEntity recipeDetail);
    }
}
