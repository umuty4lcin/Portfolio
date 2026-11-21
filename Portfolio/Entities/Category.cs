using System.ComponentModel.DataAnnotations;

namespace Portfolio.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Kategori adı zorunludur.")]
        public string CategoryName { get; set; }

        public List<Project> Projects { get; set; } = new();
    }
}