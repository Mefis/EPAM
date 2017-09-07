namespace OhSnapDAL.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [Required]
        public int UserID { get; set; }

        [Required]
        public string UserLogin { get; set; }

        [Required]
        public string UserPassword { get; set; }

        public byte[] UserPasswordHash { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int RoleID { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
    }
}
