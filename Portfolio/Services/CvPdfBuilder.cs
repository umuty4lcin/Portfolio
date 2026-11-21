using Portfolio.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Portfolio.Services
{
    public static class CvPdfBuilder
    {
        public static byte[] Build(
            About about,
            Contact? contact,
            List<Education> educations,
            List<Experience> experiences,
            List<SkillGroup> skillGroups,
            List<Project> projects)
        {
            var accent = Colors.Indigo.Medium;

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(40);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Content().Column(col =>
                    {
                        col.Spacing(12);

                        // --- Başlık ---
                        col.Item().Column(header =>
                        {
                            header.Item().Text(about.NameSurname).FontSize(26).Bold();
                            header.Item().Text(about.JobTitle).FontSize(13).FontColor(accent);

                            var iletisim = new List<string>();
                            if (!string.IsNullOrWhiteSpace(about.Email)) iletisim.Add(about.Email);
                            if (!string.IsNullOrWhiteSpace(about.Location)) iletisim.Add(about.Location);
                            if (!string.IsNullOrWhiteSpace(contact?.Linkedin)) iletisim.Add(contact!.Linkedin);
                            if (!string.IsNullOrWhiteSpace(contact?.Github)) iletisim.Add(contact!.Github);

                            header.Item().PaddingTop(4).Text(string.Join("  |  ", iletisim))
                                .FontSize(9).FontColor(Colors.Grey.Darken1);
                        });

                        col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

                        // --- Hakkımda ---
                        if (!string.IsNullOrWhiteSpace(about.Description))
                        {
                            col.Item().Element(c => Baslik(c, "HAKKIMDA", accent));
                            col.Item().Text(about.Description.Replace("\r", ""));
                        }

                        // --- Deneyim ---
                        if (experiences.Any())
                        {
                            col.Item().Element(c => Baslik(c, "İŞ DENEYİMİ", accent));
                            foreach (var e in experiences)
                            {
                                col.Item().Column(item =>
                                {
                                    item.Item().Row(row =>
                                    {
                                        row.RelativeItem().Text(t =>
                                        {
                                            t.Span(e.Title).Bold();
                                            t.Span(" — " + e.CompanyName);
                                        });
                                        row.AutoItem().Text(e.DurationText).FontColor(Colors.Grey.Darken1);
                                    });

                                    var maddeler = e.Description.Split('\n',
                                        StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                                    foreach (var madde in maddeler)
                                        item.Item().PaddingLeft(8).Text("• " + madde);

                                    if (!string.IsNullOrWhiteSpace(e.Technologies))
                                        item.Item().PaddingTop(2).Text("Teknolojiler: " + e.Technologies)
                                            .FontSize(9).Italic().FontColor(Colors.Grey.Darken1);
                                });
                            }
                        }
                        // --- Projeler ---
                        if (projects.Any())
                        {
                            col.Item().Element(c => Baslik(c, "PROJELER", accent));
                            foreach (var p in projects)
                            {
                                col.Item().Column(item =>
                                {
                                    item.Item().Row(row =>
                                    {
                                        row.RelativeItem().Text(p.Title).Bold();
                                        if (p.Category != null)
                                            row.AutoItem().Text(p.Category.CategoryName).FontColor(Colors.Grey.Darken1);
                                    });

                                    if (!string.IsNullOrWhiteSpace(p.Description))
                                        item.Item().Text(p.Description).FontSize(9);

                                    if (!string.IsNullOrWhiteSpace(p.GithubUrl))
                                        item.Item().Text(p.GithubUrl).FontSize(9).FontColor(accent);
                                });
                            }
                        }
                        // --- Eğitim ---
                        if (educations.Any())
                        {
                            col.Item().Element(c => Baslik(c, "EĞİTİM", accent));
                            foreach (var ed in educations)
                            {
                                col.Item().Column(item =>
                                {
                                    item.Item().Row(row =>
                                    {
                                        row.RelativeItem().Text(ed.Title).Bold();
                                        row.AutoItem().Text(ed.Duration).FontColor(Colors.Grey.Darken1);
                                    });
                                    item.Item().Text(ed.SubTitle).FontColor(accent);
                                    item.Item().Text(ed.Description).FontSize(9);
                                });
                            }
                        }

                        // --- Yetenekler ---
                        if (skillGroups.Any())
                        {
                            col.Item().Element(c => Baslik(c, "YETENEKLER", accent));
                            foreach (var g in skillGroups)
                            {
                                col.Item().Text(t =>
                                {
                                    t.Span(g.Title + ": ").Bold();
                                    t.Span(string.Join(", ", g.Skills.Select(s => s.Name)));
                                });
                            }
                        }
                    });
                });
            }).GeneratePdf();
        }

        private static void Baslik(IContainer container, string text, string color)
        {
            container.PaddingTop(4).Text(text).FontSize(11).Bold().FontColor(color);
        }
    }
}