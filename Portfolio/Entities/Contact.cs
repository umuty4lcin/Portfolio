using System.ComponentModel.DataAnnotations;
namespace Portfolio.Entities
{
    public class Contact
    {
        public int ContactId { get; set; }
        [Required(ErrorMessage = "Açıklama zorunludur.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "E-posta zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta girin.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "LinkedIn adresi zorunludur.")]
        [Url(ErrorMessage = "Geçerli bir adres girin (https://...).")]
        public string Linkedin { get; set; }

        [Required(ErrorMessage = "Konum zorunludur.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "GitHub adresi zorunludur.")]
        [Url(ErrorMessage = "Geçerli bir adres girin (https://...).")]
        public string Github { get; set; }

        [Required(ErrorMessage = "Alt açıklama zorunludur.")]
        public string SubDescription { get; set; }
    }
}
