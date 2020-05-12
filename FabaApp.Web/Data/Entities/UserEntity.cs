using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using FabaApp.Common.Enums;
using System;
using System.Collections.Generic;

namespace FabaApp.Web.Data.Entities
{
    public class UserEntity : IdentityUser
    {
        [Display(Name = "Documento")]
        [MaxLength(20, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Document { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string FirstName { get; set; }

        [Display(Name = "Apellido")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string LastName { get; set; }

        [Display(Name = "Tipo Usuario")]
        public UserType UserType { get; set; }

        [Display(Name = "Usuario")]
        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "Usuario y Documento")]
        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public LabEntity Lab { get; set; }

        [Display(Name = "Fecha Alta")]
        public DateTime DischargeDate { get; set; }

        [Display(Name = "Activo")]
        public bool Active { get; set; }

        public ICollection<RecipeEntity> Recipes { get; set; }
    }
}