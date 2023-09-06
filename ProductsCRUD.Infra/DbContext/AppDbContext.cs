using SQLite;
using System;
using System.IO;

namespace ProductsCRUD.DbContext
{
    public class AppDbContext : IDisposable
    {
        private readonly SQLiteConnection _connection;
        private readonly SQLiteAsyncConnection _asyncConnection;

        public AppDbContext()
        {
            string dbPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Market.db");
            _connection = new SQLiteConnection(dbPath);
            _asyncConnection = new SQLiteAsyncConnection(dbPath);
        }

        public SQLiteConnection Connection => _connection;
        public SQLiteAsyncConnection AsyncConnection => _asyncConnection;

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
