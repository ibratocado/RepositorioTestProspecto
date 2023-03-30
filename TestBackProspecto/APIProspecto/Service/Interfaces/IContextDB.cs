using System.Data.SqlClient;

namespace APIProspecto.Service.Interfaces
{
    public interface IContextDB
    {
        SqlConnection OpenConection();
        SqlConnection CloseConection();
    }
}
