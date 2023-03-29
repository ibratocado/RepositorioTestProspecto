namespace APIProspecto.DTO
{
    public class DocumentsRequest
    {
        public List<IFormFile> DocumentData { get; set; }
        public string ProspectoId { get; set; }

        public DocumentsRequest()
        {
            DocumentData = new List<IFormFile>();
        }
    }
}
