using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;

namespace ExploreDotNET8
{
    public class DataContextDapper
    {
    
    private readonly IConfiguration _config;
    public DataContextDapper(IConfiguration config)
    {
        _config = config;
    }

    public IEnumerable<T> LoadData<T>(string sql)
    {
        IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        return dbConnection.Query<T>(sql);
    }

    public T LoadDataSingle<T>(string sql)
    {
    using (IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
    {
        dbConnection.Open(); // Open the connection explicitly

        // Use InvariantCulture for the command to ensure globalization-invariant mode
        return dbConnection.QuerySingle<T>(new CommandDefinition(sql));
    }
    }

    }
}