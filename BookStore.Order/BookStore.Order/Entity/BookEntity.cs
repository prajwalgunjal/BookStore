using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Order.Entity
{
    public class BookEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookID { get; set; }

        [Required]

        public string BookName { get; set; }
        [Required]

        public string Description { get; set; }
        [Required]

        public string Author { get; set; }

        [Required]
        
        public int Quantity { get; set; }

        [Required]

        public float Discount { get; set; }

        [Required]
 
        public float Price { get; set; }
    }
}
