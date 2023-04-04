using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Models
{
    [Table("Contacts")]
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Tiêu đề không để rỗng!")]
        [StringLength(255)]
        public string Title { get; set; }

        public int Status { get; set; }
       
    }
}
