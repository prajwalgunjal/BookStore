using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Order.Entity
{
    public class OrderEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public int Quantity { get; set; }
        public int BookID { get; set; }

        [NotMapped]
        public float OrderAmount { get; set; }
        [NotMapped]
        public BookEntity Book { get; set; }

        [NotMapped]
        public UserEntity User { get; set; }
    }

}
