using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FabaApp.Web.Data.Entities
{
    public class SocialWorkEntity
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Name { get; set; }

        //public User User { get; set; }

        [Display(Name = "Fecha Alta")]
        public DateTime DischargeDate { get; set; }

        [Display(Name = "Activo")]
        public bool Active { get; set; }

        public ICollection<CodeEntity> Codes { get; set; }
    }
}