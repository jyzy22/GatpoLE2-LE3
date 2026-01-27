using BlogDataLibrary.Database;

namespace BlogDataLibrary.Data
{
    public class SqlData
    {
        private readonly ISqlDataAccess _db;
        private const string connectionStringName = "BlogDB";

        public SqlData(ISqlDataAccess db)
        {
            _db = db;
        }
    }
}