using System.ComponentModel.DataAnnotations;

namespace Portfolio.Entities
{
    public class Message
    {
        public int MessageId { get; set; }
        [Required(ErrorMessage = "Ad soyad zorunludur.")]
        [StringLength(100, ErrorMessage = "En fazla 100 karakter olabilir.")]
        public string NameSurname { get; set; }

        [Required(ErrorMessage = "E-posta zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta girin.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Konu zorunludur.")]
        [StringLength(150, ErrorMessage = "En fazla 150 karakter olabilir.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Mesaj zorunludur.")]
        [StringLength(2000, MinimumLength = 10, ErrorMessage = "Mesaj 10-2000 karakter arasında olmalı.")]
        public string MessageDetail { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsRead { get; set; }
    }
}
