using ProductsCRUD.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsCRUD.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SQLiteConnection database;

        public ProductRepository()
        {
            // Configure o caminho e o nome do arquivo do banco de dados SQLite
            string dbPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Products.db");
            database = new SQLiteConnection(dbPath);
            database.CreateTable<Product>();
        }

        public void AddProduct(Product newProduct)
        {
            var productDb = GetProductById(newProduct.Id);
            if (productDb == null)
                database.Insert(newProduct);
        }

        public List<Product> GetProducts()
        {
            return database.Table<Product>().ToList();
        }

        public Product GetProductById(int id)
        {
            return database.Table<Product>().Where(a => a.Id == id).FirstOrDefault();
        }

        public void UpdateProduct(Product updatedProduct)
        {
            var productDb = GetProductById(updatedProduct.Id);
            if (productDb != null)
                database.Update(updatedProduct);
        }

        public void DeleteProduct(int id)
        {
            var product = GetProductById(id);
            if(product != null)
                database.Delete(product);
        }
    }
}
