using SQLite;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using MaxLengthAttribute = SQLite.MaxLengthAttribute;
using ProductsCRUD.Models.Categories;

namespace ProductsCRUD.Models.Products
{
    public class Product : IEntity
    {
        [PrimaryKey]
        public string Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        public double Price { get; set; }
        public byte[] Image { get; set; }
    }
}
