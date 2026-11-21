using System.ComponentModel.DataAnnotations;

namespace Portfolio.Entities
{
    public class HeroSection
    {
        public int HeroSectionId { get; set; }

        [Required(ErrorMessage = "Rozet metni zorunludur.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Ad soyad zorunludur.")]
        public string SubTitle { get; set; }

        [Required(ErrorMessage = "Açıklama zorunludur.")]
        public string Description { get; set; }

        public string? ImageUrl { get; set; }
    }
}
