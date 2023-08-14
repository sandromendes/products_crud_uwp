using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using MaxLengthAttribute = SQLite.MaxLengthAttribute;

namespace ProductsCRUD.Models
{
    public class Product
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        public double Price { get; set; }
        public byte[] Image { get; set; }
    }
}
