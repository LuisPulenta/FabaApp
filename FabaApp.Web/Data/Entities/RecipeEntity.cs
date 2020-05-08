using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FabaApp.Web.Data.Entities
{
    public class RecipeEntity
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Name { get; set; }

        //public User User { get; set; }

        [Display(Name = "Fecha Alta")]
        public DateTime DischargeDate { get; set; }

        SocialWorkEntity SocialWork { get; set; }

        public string Foto1 { get; set; }
        public string Foto2 { get; set; }
        public string Foto3 { get; set; }
        public string Foto4 { get; set; }

        public ICollection<RecipeDetailEntity> RecipeDetails { get; set; }

        public string Foto1FullPath => string.IsNullOrEmpty(Foto1)
           ? "noimage"//null
           : $"http://keypress.serveftp.net:88/FabaAppApi{Foto1.Substring(1)}";

        public string Foto2FullPath => string.IsNullOrEmpty(Foto2)
           ? "noimage"//null
           : $"http://keypress.serveftp.net:88/FabaAppApi{Foto2.Substring(1)}";

        public string Foto3FullPath => string.IsNullOrEmpty(Foto3)
           ? "noimage"//null
           : $"http://keypress.serveftp.net:88/FabaAppApi{Foto3.Substring(1)}";

        public string Foto4FullPath => string.IsNullOrEmpty(Foto4)
           ? "noimage"//null
           : $"http://keypress.serveftp.net:88/FabaAppApi{Foto4.Substring(1)}";

        public bool Flag1 { get; set; }
        public bool Flag2 { get; set; }
        public bool Flag3 { get; set; }
        public bool Flag4 { get; set; }

        public string State { get; set; }

    }
}
