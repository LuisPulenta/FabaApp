namespace FabaApp.Common.Models
{
    public class RecipeDetailResponse
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }
}