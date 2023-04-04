using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Models
{
    [Table("Categorys")]
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên loại sản phẩm không được để trống!")]
        [StringLength(255)]
        public string Name { get; set; }
        public string Slug { get; set; }
        public int? ParentId { get; set; }
        public int? Orders { get; set; }
        [Required(ErrorMessage ="không được để trống!")]
        public string MetaDesc { get; set; }
        [Required(ErrorMessage = "không được để trống!")]
        public string MetaKey { get; set; }
        public int? Create_By { get; set;}
        public DateTime? CreateAt { get; set; }
        public int? Update_By { get; set; }
        public DateTime? UpdateAt { get; set;}
        public int Status { get; set; }
    }
}
