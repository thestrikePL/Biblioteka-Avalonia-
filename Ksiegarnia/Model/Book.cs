using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksiegarnia.Model
{
    public class Book
    {
        [Key]
        public int Id { get; set; }


        [Required]
        public string Title { get; set; }
        public string Author { get; set; }



        [ForeignKey("Owner")]
        public int OwnerId { get; set; }
        public virtual User Owner { get; set; }



        [ForeignKey("Category")]
        public int CategoryId { get; set; }



        public virtual Category Category { get; set; }



        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
