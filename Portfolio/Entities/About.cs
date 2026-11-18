using System.ComponentModel.DataAnnotations;

namespace Portfolio.Entities
{
    public class About
    {
        public int AboutId { get; set; }
        [Required(ErrorMessage = "Ad Soyad zorunludur.")]
        public string NameSurname { get; set; }
        [Required(ErrorMessage = "İş Başlığı zorunludur.")]
        public string JobTitle { get; set; }
        [Required(ErrorMessage = "Adres zorunludur.")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Uzmanlık zorunludur.")]
        public string Expert { get; set; }
        [Required(ErrorMessage = "E-posta zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta girin.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Açıklama zorunludur.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Çalışmaya açık olma durumu zorunludur.")]
        public string OpenToWork { get; set; }
    }
}
