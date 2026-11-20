using System.ComponentModel.DataAnnotations;

namespace Portfolio.Entities
{
    public class SkillGroup
    {
        public int SkillGroupId { get; set; }

        [Required(ErrorMessage = "Grup adı zorunludur.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Açıklama zorunludur.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "İkon zorunludur.")]
        public string IconClass { get; set; }     // örn. bi-hdd-network

        [Required(ErrorMessage = "Renk seçimi zorunludur.")]
        public string ColorClass { get; set; }    // örn. bg-icon-purple

        public bool IsActive { get; set; } = true;

        public List<Skill> Skills { get; set; } = new();
    }
}
