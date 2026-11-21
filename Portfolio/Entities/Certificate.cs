using System.ComponentModel.DataAnnotations;

namespace Portfolio.Entities
{
    public class Certificate
    {
        public int CertificateId { get; set; }

        [Required(ErrorMessage = "Sertifika adı zorunludur.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Veren kurum zorunludur.")]
        public string Issuer { get; set; }

        [Range(1990, 2100, ErrorMessage = "Geçerli bir yıl girin.")]
        public int Year { get; set; } = DateTime.Today.Year;

        [Url(ErrorMessage = "Geçerli bir adres girin (https://...).")]
        public string? CertificateUrl { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
