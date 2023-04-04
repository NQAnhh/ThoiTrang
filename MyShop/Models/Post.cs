using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Models
{
    [Table("Posts")]
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public int? TopicId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Slug { get; set; }
        [Required]
        public string Detail { get; set; }
        public string Img { get; set; }
        public string PostType { get; set; }
        [Required]
        public string MetaKey { get; set; }
        public string MetaDesc { get; set; }
        public int? Create_By { get; set; }
        public DateTime? CreateAt { get; set;}
        public int? Update_By { get; set; }
        public DateTime? UpdateAt { get;set;}
        public int Status { get; set; }

    }
}
