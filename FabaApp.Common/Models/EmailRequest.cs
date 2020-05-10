using System.ComponentModel.DataAnnotations;

namespace FabaApp.Common.Models
{
    public class EmailRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}