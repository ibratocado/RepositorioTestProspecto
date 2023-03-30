using APIProspecto.Service.Interfaces;
using ModelDatabase;
using System.Data;
using System.Data.SqlClient;

namespace APIProspecto.Service
{
    public class StatusService : IStatusService
    {
        private readonly IContextDB _contextDB;
        private SqlDataReader read;
        private readonly SqlCommand command;
        public StatusService(IContextDB contextDB)
        {
            _contextDB = contextDB;
            command = new SqlCommand();
        }

        public async Task<List<StatusSolicitud>> GetFull()
        {
            command.Connection = _contextDB.OpenConection();
            var task = await Task<List<StatusSolicitud>>.Factory.StartNew(() => {
                command.CommandText = "GetFullStatusSolicitud";
                command.CommandType = CommandType.StoredProcedure;
                read = command.ExecuteReader();

                var list = new List<StatusSolicitud>();

                while (read.Read())
                {
                    StatusSolicitud model = new StatusSolicitud()
                    {
                        Id = read.GetInt32(0),
                        NameStatus = read.GetString(1)
                    };

                    list.Add(model);
                }
                _contextDB.CloseConection();
                return list;
            });

            return task;
        }
    }
}
