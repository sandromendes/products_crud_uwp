using SQLite;
using System;
using System.IO;

namespace ProductsCRUD.DbContext
{
    public class AppDbContext : IDisposable
    {
        private readonly SQLiteConnection _connection;

        public AppDbContext()
        {
            string dbPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Market.db");
            _connection = new SQLiteConnection(dbPath);
        }

        public SQLiteConnection Connection => _connection;

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
