using APIProspecto.DTO;
using ModelDatabase;

namespace APIProspecto.Service.Interfaces
{
    public interface IDocumentsService
    {
        Task<List<Documents>> GetByProspecto(string prospectoId);
        Task<string> InsertByPospecto(DocumentsRequest request);
    }
}
