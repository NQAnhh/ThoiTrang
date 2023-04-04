using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Models
{
    [Table("Sliders")]
    public class Slider
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Link { get; set; }
        [Required]
        public string Img { get; set; }
        [Required]
        public string Position { get; set; }
        public int? Create_By { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? Update_By { get; set; }
        public DateTime? UpdateAt { get; set;}
        public int Status { get; set; }
    }
}
