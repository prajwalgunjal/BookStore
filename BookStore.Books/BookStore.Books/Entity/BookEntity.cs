using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Books.Entity
{
    public class BookEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookID { get; set; }

        [Required]
        //[DataType(DataType.Text)]
        public string BookName { get; set; }
        [Required]
        //[DataType(DataType.Text)]
        public string Description { get; set; }
        [Required]
        //[DataType(DataType.Text)]
        public string Author { get; set; }

        [Required]
        //[Range(1, 100000)]
        public int Quantity { get; set; }

        [Required]
        //[Range(1.0, 1000.0)]
        public float Discount { get; set; }

        [Required]
        //[Range(1.0, 100000.0)]
        public float Price { get; set; }
    }
}
