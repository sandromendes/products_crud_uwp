using System;
using System.ComponentModel.DataAnnotations;
using ProductsCRUD.Domain.Models.Products;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace ProductsCRUD.Domain.Models.Categories
{
    public class Category : IEntity
    {
        [PrimaryKey]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [ForeignKey(typeof(Product))]
        public string ProductId { get; set; }

        [ManyToOne]
        public Product Product { get; set; }

    }
}
