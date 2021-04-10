using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Employees
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(60)]
        public string FirstName { get; set; }
        [MaxLength(60)]
        public string LastName { get; set; }
        [Required]
        public int Salary { get; set; }
        [Required]
        [MaxLength(20)]
        public string Title { get; set; }
    }
}

