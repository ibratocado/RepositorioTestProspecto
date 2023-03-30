using APIProspecto.DTO;

namespace APIProspecto.Service.Interfaces
{
    public interface IProspectoService
    {
        Task<List<ProspectoRespon>> GetStateFull(int state);
        Task<ProspectoRespon> GetById(string id);
        Task<Guid> InsertProspecto(ProspectoRequest model);
        Task<bool> GetExistRFC(string RFC);
        void UpdateStateByProspeto(ProspestoUpdateStatusRespon request);
    }
}
