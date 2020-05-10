namespace FabaApp.Common.Models
{
    public class CodeResponse
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public bool Active { get; set; }

        public int Qty { get; set; }

        public SocialWorkResponse SocialWork { get; set; }
    }
}