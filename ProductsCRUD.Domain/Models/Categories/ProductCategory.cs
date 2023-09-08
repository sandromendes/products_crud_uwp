using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProductsCRUD.Domain.Models.Products;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace ProductsCRUD.Domain.Models.Categories
{
    public class ProductCategory : IEntity
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


        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public IList<Product> Products { get; set; }
    }
}
