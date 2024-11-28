using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class UserLogins
    {
        public UserLogins()
        {

        }

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        
    }
}
