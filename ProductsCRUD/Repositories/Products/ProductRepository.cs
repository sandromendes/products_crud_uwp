﻿using ProductsCRUD.DbContext;
using ProductsCRUD.Models.Products;
using SQLite;
using System;
using System.Collections.Generic;

namespace ProductsCRUD.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly SQLiteConnection database;

        public ProductRepository(AppDbContext appDbContext)
        {
            database = appDbContext.Connection;
            database.CreateTable<Product>();
        }

        public void AddProduct(Product newProduct)
        {
            var productDb = GetProductById(newProduct.Id);
            if (productDb == null)
            {
                newProduct.Id = Guid.NewGuid().ToString();
                database.Insert(newProduct);
            }
        }

        public List<Product> GetProducts()
        {
            return database.Table<Product>().ToList();
        }

        public Product GetProductById(string id)
        {
            return database.Table<Product>().Where(a => a.Id == id).FirstOrDefault();
        }

        public void UpdateProduct(Product updatedProduct)
        {
            var productDb = GetProductById(updatedProduct.Id);
            if (productDb != null)
                database.Update(updatedProduct);
        }

        public void DeleteProduct(string id)
        {
            var product = GetProductById(id);
            if(product != null)
                database.Delete(product);
        }
    }
}