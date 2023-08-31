using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Order.Entity
{
    public class WishListEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WishListID { get; set; }
        [Required]
        //[RegularExpression("^[0-9]{1,}$")]
        public int BookID { get; set; }
        [Required]
        //[RegularExpression("^[0-9]{1,}$")]
        public int UserID { get; set; }
        [NotMapped]
        public BookEntity Book { get; set; }
        [NotMapped]
        public UserEntity User { get; set; }
    }
}
