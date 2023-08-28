﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Admin.Entity
{
    public class AdminEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdminId { get; set; }



        [Required(ErrorMessage = "FirstName {0} is required")]
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "length is too short...Minimum length is 3 character,Maximum length is 50")]
        public string FirstName { get; set; }




        [Required(ErrorMessage = "LastName {0} is required")]
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "length is too short...Minimum length is 3 character,Maximum length is 50")]
        public string LastName { get; set; }





        [Required(ErrorMessage = "Email {0} is required")]
        [EmailAddress]
        public string Email { get; set; }




        [Required(ErrorMessage = "Password {0} is required")]
        [DataType(DataType.Password)]
        [PasswordPropertyText]
        public string Password { get; set; }


    }
}
