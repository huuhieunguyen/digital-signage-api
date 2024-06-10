namespace CMS.DTOs.ContentItemDtos
{
    public class FileDownloadResult
    {
        public Stream Stream { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }
}
