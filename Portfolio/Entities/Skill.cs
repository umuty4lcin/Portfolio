using System.ComponentModel.DataAnnotations;

namespace Portfolio.Entities
{
    public class Skill
    {
        public int SkillId { get; set; }

        [Required(ErrorMessage = "Yetenek adı zorunludur.")]
        public string Name { get; set; }

        [Range(0, 100, ErrorMessage = "Seviye 0 ile 100 arasında olmalıdır.")]
        public int Level { get; set; } = 80;

        public bool IsActive { get; set; } = true;

        public int SkillGroupId { get; set; }
        public SkillGroup? SkillGroup { get; set; }
    }
}
