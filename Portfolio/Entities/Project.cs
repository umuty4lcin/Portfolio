using System.ComponentModel.DataAnnotations;

namespace Portfolio.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Proje başlığı zorunludur.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Açıklama zorunludur.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "GitHub adresi zorunludur.")]
        [Url(ErrorMessage = "Geçerli bir adres girin (https://...).")]
        public string GithubUrl { get; set; }

        public string? CoverImageUrl { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Kategori seçiniz.")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public bool IsActive { get; set; } = true;
    }
}