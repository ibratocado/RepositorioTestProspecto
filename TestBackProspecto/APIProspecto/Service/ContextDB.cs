using APIProspecto.Service.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace APIProspecto.Service
{
    public class ContextDB : IContextDB
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection conexion;

        public ContextDB(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile(
                "appsettings.json", optional: false);

            _configuration = builder.Build();
            string connectionString = _configuration.GetConnectionString("DB_TestProspecto").ToString();

            conexion = new SqlConnection(connectionString);
            _configuration = configuration;
        }

        public SqlConnection CloseConection()
        {
            if (conexion.State == ConnectionState.Open)
                conexion.Close();
            return conexion;
        }

        public SqlConnection OpenConection()
        {
            if (conexion.State == ConnectionState.Closed)
                conexion.Open();
            return conexion;
        }
    }
}
