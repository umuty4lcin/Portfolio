namespace Portfolio.Entities
{
    public class CvFile
    {
        public int CvFileId { get; set; }
        public string FilePath { get; set; }          // /files/cv/xxxx.pdf
        public string OriginalFileName { get; set; }  // Umut-Yalcin-CV.pdf
        public DateTime UploadedAt { get; set; } = DateTime.Now;
    }
}
