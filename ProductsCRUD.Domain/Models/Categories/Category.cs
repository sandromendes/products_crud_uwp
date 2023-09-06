using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ProductsCRUD.Models.Products;

namespace ProductsCRUD.Models.Categories
{
    public class Category : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [InverseProperty("Category")]
        public ICollection<Product> Produtos { get; set; }
        string IEntity.Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
