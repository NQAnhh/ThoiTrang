using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyShop.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public int CatId { get; set; }
        public int SupplierId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Slug { get; set; }
        [Required]
        public string Detail { get; set; }

        public int? Create_By { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? Update_By { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int Status { get; set; }
        public string Img { get; set; }
    }
}
