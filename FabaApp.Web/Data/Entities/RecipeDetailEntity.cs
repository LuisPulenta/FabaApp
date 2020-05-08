using System;
using System.ComponentModel.DataAnnotations;

namespace FabaApp.Web.Data.Entities
{
    public class RecipeDetailEntity
    {
        public int Id { get; set; }

        public RecipeEntity Recipe { get; set; }

        [Display(Name = "Código")]
        [MaxLength(20, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Code { get; set; }


        [Display(Name = "Descripción")]
        [MaxLength(150, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Description { get; set; }

        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public int Quantity { get; set; }

    }
}
