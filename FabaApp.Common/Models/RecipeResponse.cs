using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabaApp.Common.Models
{
    public class RecipeResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DischargeDate { get; set; }
        public DateTime RecipeDate { get; set; }
        public int SocialWorkId { get; set; }
        public SocialWorkResponse SocialWork { get; set; }
        public string UserId { get; set; }

        public string Foto1 { get; set; }
        public string Foto2 { get; set; }
        public string Foto3 { get; set; }
        public string Foto4 { get; set; }
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
        public DateTime StateDate { get; set; }

        public ICollection<RecipeDetailResponse> RecipeDetails { get; set; }

        public int? CantItems { get; set; }

        public int? CantFotos { get; set; }
    }
}
