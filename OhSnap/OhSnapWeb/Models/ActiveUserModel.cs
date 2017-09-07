namespace OhSnapWeb.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ActiveUserModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}