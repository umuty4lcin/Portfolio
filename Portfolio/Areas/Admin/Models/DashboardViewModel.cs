using Portfolio.Entities;

namespace Portfolio.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        // İstatistik kartları
        public int TotalMessages { get; set; }
        public int UnreadMessages { get; set; }
        public int TotalProjects { get; set; }
        public int ActiveProjects { get; set; }
        public int TotalCategories { get; set; }

        // Hesaplanan alanlar
        public int ReadMessages => TotalMessages - UnreadMessages;
        public int ReadPercent => TotalMessages > 0 ? (int)Math.Round(ReadMessages * 100.0 / TotalMessages) : 0;
        public int UnreadPercent => TotalMessages > 0 ? (int)Math.Round(UnreadMessages * 100.0 / TotalMessages) : 0;

        // Listeler
        public List<Message> LastMessages { get; set; } = new();
        public List<Project> LastProjects { get; set; } = new();
        public List<CategoryStat> CategoryStats { get; set; } = new();
    }

    public class CategoryStat
    {
        public string CategoryName { get; set; } = "";
        public int ProjectCount { get; set; }
    }
}