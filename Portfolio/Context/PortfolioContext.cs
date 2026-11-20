using Microsoft.EntityFrameworkCore;
using Portfolio.Entities;

namespace Portfolio.Context
{
    public class PortfolioContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=PortfolioDBNight;integrated security=true;trustservercertificate=true");
        }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<HeroSection> HeroSections { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet <Skill> Skills { get; set; }
        public DbSet <SkillGroup> SkillGroups { get; set; }
    }
}
