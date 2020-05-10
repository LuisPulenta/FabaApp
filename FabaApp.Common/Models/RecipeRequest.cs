using System;

namespace FabaApp.Common.Models
{
    public class RecipeRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DischargeDate { get; set; }
        public DateTime RecipeDate { get; set; }
        public int SocialWorkId { get; set; }
        public byte[] PictureArray1 { get; set; }
        public byte[] PictureArray2 { get; set; }
        public byte[] PictureArray3 { get; set; }
        public byte[] PictureArray4 { get; set; }
        public bool Flag1 { get; set; }
        public bool Flag2 { get; set; }
        public bool Flag3 { get; set; }
        public bool Flag4 { get; set; }
        public string State { get; set; }
        public DateTime StateDate { get; set; }
    }
}
