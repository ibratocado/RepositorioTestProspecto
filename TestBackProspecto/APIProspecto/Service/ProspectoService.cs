using APIProspecto.DTO;
using APIProspecto.Service.Interfaces;
using ModelDatabase;
using System.Data;
using System.Data.SqlClient;

namespace APIProspecto.Service
{
    public class ProspectoService : IProspectoService
    {
        private readonly IContextDB _contextDB;
        private SqlDataReader? read;
        private SqlCommand command;
        public ProspectoService(IContextDB contextDB)
        {
            _contextDB = contextDB;
            command = new SqlCommand();
        }

        public async Task<ProspectoRespon> GetById(string id)
        {
            var task = await Task<ProspectoRespon>.Factory.StartNew(() =>
            {
                command.Connection = _contextDB.OpenConection();
                command.CommandText = "GetProspectoForId";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                read = command.ExecuteReader();
                //command.ExecuteNonQuery();


                var model = new ProspectoRespon();
                while (read.Read())
                {
                    model.Id = read.GetString(0);
                    model.Nombre = read.GetString(1);
                    model.PrimerApellido = read.GetString(2);
                    model.SegundoApellido = read.GetString(3);
                    model.Calle = read.GetString(4);
                    model.Numero = read.GetInt32(5);
                    model.Colonia = read.GetString(6);
                    model.CodigoPostal = read.GetString(7);
                    model.Telefono = read.GetString(8);
                    model.RFC = read.GetString(9);
                    model.Status = new StatusSolicitud
                    {
                        Id = read.GetInt32(10),
                        NameStatus = read.GetString(11)
                    };

                }
                command.Parameters.Clear();
                command.Connection = _contextDB.CloseConection();
                return model;
            });

            return task;
        }

        public async Task<bool> GetExistRFC(string RFC)
        {
            
            var task = await Task<bool>.Factory.StartNew(() =>
            {
                command.Connection = _contextDB.OpenConection();
                command.CommandText = "GetExistRFC";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@RFC", RFC);
                read = command.ExecuteReader();


                var model = new List<ProspectoRespon>();
                while (read.Read())
                {
                    if (read.GetString(0) == RFC)
                        return true;
                }
                command.Parameters.Clear();
                command.Connection = _contextDB.CloseConection();
                return false;
            });

            return task;
        }

        public async Task<List<ProspectoRespon>> GetStateFull(int state)
        {
            
            var task = await Task<List<ProspectoRespon>>.Factory.StartNew(() =>
            {
                command.Connection = _contextDB.OpenConection();
                command.CommandText = "GetProspectosForState";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Status", state);
                read = command.ExecuteReader();


                var model = new List<ProspectoRespon>();
                while (read.Read())
                {
                    ProspectoRespon temp = new ProspectoRespon()
                    {
                        Id = read.GetString(0),
                        Nombre = read.GetString(1),
                        PrimerApellido = read.GetString(2),
                        SegundoApellido = read.GetString(3),
                        Calle = read.GetString(4),
                        Numero = read.GetInt32(5),
                        Colonia = read.GetString(6),
                        CodigoPostal = read.GetString(7),
                        Telefono = read.GetString(8),
                        RFC = read.GetString(9),
                        Status = new StatusSolicitud
                        {
                            Id = read.GetInt32(10),
                            NameStatus = read.GetString(11)
                        }

                    };
                    model.Add(temp);
                }
                command.Parameters.Clear();
                command.Connection = _contextDB.CloseConection();
                return model;
            });

            return task;
        }

        public async Task<Guid> InsertProspecto(ProspectoRequest model)
        {
            
            var task = await Task<Guid>.Factory.StartNew(() =>
            {
                var id = Guid.NewGuid();
                command.Connection = _contextDB.OpenConection();
                command.CommandText = "InsertProspecto";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Nombre", model.Nombre);
                command.Parameters.AddWithValue("@PrimerApellido", model.PrimerApellido);
                command.Parameters.AddWithValue("@SegundoApellido", model.SegundoApellido);
                command.Parameters.AddWithValue("@Calle", model.Calle);
                command.Parameters.AddWithValue("@Numero", model.Numero);
                command.Parameters.AddWithValue("@Colonia", model.Colonia);
                command.Parameters.AddWithValue("@CodigoPostal", model.CodigoPostal);
                command.Parameters.AddWithValue("@Telefono", model.Telefono);
                command.Parameters.AddWithValue("@RFC", model.RFC);
                command.Parameters.AddWithValue("@IdSolicitud", Guid.NewGuid());

                command.ExecuteNonQuery();
                command.Parameters.Clear();
                command.Connection = _contextDB.CloseConection();

                return id;
            });

            return task;
        }

        public async void UpdateStateByProspeto(ProspestoUpdateStatusRespon request)
        {
            await Task.Factory.StartNew(() => 
            {
                if (request.Id == null)
                    return;

                var exits = ProspectoExist(request.Id);
                if(exits)
                {
                    command.Connection = _contextDB.OpenConection();
                    command.CommandText = "UpdateStateProspecto";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", request.Id);
                    command.Parameters.AddWithValue("@State", request.Status);
                    command.Parameters.AddWithValue("@Obs", request.Observations);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    command.Connection = _contextDB.CloseConection();
                }
            });
        }

        private bool ProspectoExist(string id)
        {
            command.Connection = _contextDB.OpenConection();
            command.CommandText = "GetExistProspecto";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);
            read = command.ExecuteReader();

            while (read.Read())
            {
                if (read.GetString(0).Length > 0)
                {
                    command.Parameters.Clear();
                    command.Connection = _contextDB.CloseConection();
                    return true;
                }
            }
            command.Parameters.Clear();
            command.Connection = _contextDB.CloseConection();

            return false;

        }
    }
}
