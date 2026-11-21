using System.ComponentModel.DataAnnotations;

namespace Portfolio.Entities
{
    public class WorkflowStep
    {
        public int WorkflowStepId { get; set; }

        [Required(ErrorMessage = "Adım başlığı zorunludur.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Açıklama zorunludur.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "İkon zorunludur.")]
        public string IconClass { get; set; }     // örn. bi-search

        [Range(1, 99, ErrorMessage = "Sıra 1 ile 99 arasında olmalıdır.")]
        public int SortOrder { get; set; } = 1;

        public bool IsActive { get; set; } = true;
    }
}
