using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Entities
{
    public class Experience
    {
        public int ExperienceId { get; set; }

        [Required(ErrorMessage = "Pozisyon zorunludur.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Şirket adı zorunludur.")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Açıklama zorunludur.")]
        public string Description { get; set; }      // her satır bir madde

        [Required(ErrorMessage = "Alt başlık zorunludur.")]
        public string SideTitle { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Today;

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }       // boş = hâlâ çalışıyor

        [Required(ErrorMessage = "Çalışma türü zorunludur.")]
        public string WorkType { get; set; }

        public string? Technologies { get; set; }    // virgülle ayrılmış: C#, SQL Server

        public bool IsActive { get; set; } = true;

        [NotMapped]
        public string DurationText =>
            StartDate.Year + " – " + (EndDate.HasValue ? EndDate.Value.Year.ToString() : "Günümüz");
    }
}