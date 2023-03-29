using ModelDatabase;

namespace APIProspecto.Service.Interfaces
{
    public interface IStatusService
    {
        Task<List<StatusSolicitud>> GetFull();
    }
}
