using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FabaApp.Web.Data.Entities
{
    public class CodeEntity
    {
        public int Id { get; set; }

        [Display(Name = "Código")]
        [MaxLength(20, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Code { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(250, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Description { get; set; }

        [Display(Name = "Activo")]
        public bool Active { get; set; }

        public SocialWorkEntity SocialWork { get; set; }

    }
}
