using SQLite;
using System;
using MaxLengthAttribute = SQLite.MaxLengthAttribute;

namespace ProductsCRUD.Models.Products
{
    public class Product
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
