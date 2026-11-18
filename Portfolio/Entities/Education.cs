using System.ComponentModel.DataAnnotations;

namespace Portfolio.Entities
{
    public class Education
    {
        public int EducationId { get; set; }
        [Required(ErrorMessage = "Okul/kurum adı zorunludur.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Bölüm/program zorunludur.")]
        public string SubTitle { get; set; }
        [Required(ErrorMessage = "Açıklama zorunludur.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Dönem zorunludur.")]
        public string Duration { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
