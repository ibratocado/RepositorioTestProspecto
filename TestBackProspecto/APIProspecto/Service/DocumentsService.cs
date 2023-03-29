using APIProspecto.DTO;
using APIProspecto.Service.Interfaces;
using ModelDatabase;
using System.Data;
using System.Data.SqlClient;

namespace APIProspecto.Service
{
    public class DocumentsService : IDocumentsService
    {
        private readonly IContextDB _contextDB;
        private readonly IWebHostEnvironment _env;
        private SqlDataReader? read;
        private readonly SqlCommand command;

        public DocumentsService(IContextDB contextDB, IWebHostEnvironment env)
        {
            _contextDB = contextDB;
            command = new SqlCommand();
            _env = env;
        }

        public async Task<List<Documents>> GetByProspecto(string prospectoId)
        {
            
            var task = await Task<List<Documents>>.Factory.StartNew(() => {
                command.Connection = _contextDB.OpenConection();
                command.CommandText = "GetDocumentsByProspecto";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdProspecto", prospectoId);
                read = command.ExecuteReader();
                

                var model = new List<Documents>();
                while (read.Read())
                {
                    var temp = new Documents()
                    {
                        Id = read.GetString(0),
                        IdProspecto = read.GetString(1),
                        NameDocument = read.GetString(2),
                        Link = read.GetString(3)
                    };

                    model.Add(temp);
                }
                command.Parameters.Clear();
                command.Connection = _contextDB.CloseConection();
                return model;
            });

            return task;
        }

        public async Task<string> InsertByPospecto(DocumentsRequest request)
        {
            int i = 0;
            var task = await Task<bool>.Factory.StartNew(() => 
            {
                var respon = true;
                while(respon)
                {
                    var item = request.DocumentData[i];
                    var model = DocumentDataAndSave(item);

                    if (model == null)
                        return false;
                    
                    model.IdProspecto = request.ProspectoId;
                    respon = AddDocumentsDB(model);
                    i++;
                }
                return respon;
            });

            if (!task)
            {
                return "Seprodujo error aparteir del docmuento: " + (i + 1);
            }

            return "Agregados con exito";
        }

        private bool AddDocumentsDB(Documents model)
        {

            try
            {
                command.Connection = _contextDB.OpenConection();
                command.CommandText = "InsertDocumentsByProspecto";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", Guid.NewGuid());
                command.Parameters.AddWithValue("@IdProspecto", model.IdProspecto); 
                command.Parameters.AddWithValue("@Name", model.NameDocument);
                command.Parameters.AddWithValue("@Link", model.Link);
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                command.Connection = _contextDB.CloseConection();

                return true;

            }
            catch (Exception)
            {

                return false;
            }

        }

        private Documents DocumentDataAndSave(IFormFile document)
        {
            Documents model;
            try
            {
                var routeBase = Path.Combine(_env.WebRootPath, "Archivos");
                Console.WriteLine(routeBase);

                if (!Directory.Exists(routeBase))
                {
                    Directory.CreateDirectory(routeBase);
                }
                var nameDoc = $"{Guid.NewGuid()}{Path.GetExtension(document.FileName)}";
                var pathroot = Path.Combine(routeBase, nameDoc);

                using (var stream = System.IO.File.Create(pathroot))
                {
                    document.OpenReadStream().CopyToAsync(stream);
                }

                model = new Documents()
                {
                    NameDocument = document.FileName,
                    Link = nameDoc
                };

                return model;
            }
            catch (Exception)
            {
                model = new Documents();
                return model;
            }
        }
    }
}
