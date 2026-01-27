using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;


namespace BlogDataLibrary.Database
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public List<T> LoadData<T, U>(
            string sqlStatement,
            U parameters,
            string connectionStringName,
            bool isStoredProcedure)
        {
            string connectionString = _config.GetConnectionString(connectionStringName)
                ?? throw new InvalidOperationException("Connection string not found.");

            CommandType commandType = isStoredProcedure
                ? CommandType.StoredProcedure
                : CommandType.Text;

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<T>(
                    sqlStatement,
                    parameters,
                    commandType: commandType).ToList();
            }
        }

        public void SaveData<T>(
            string sqlStatement,
            T parameters,
            string connectionStringName,
            bool isStoredProcedure)
        {
            string connectionString = _config.GetConnectionString(connectionStringName)
                ?? throw new InvalidOperationException("Connection string not found.");

            CommandType commandType = isStoredProcedure
                ? CommandType.StoredProcedure
                : CommandType.Text;

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(
                    sqlStatement,
                    parameters,
                    commandType: commandType);
            }
        }
    }
}
