using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Departments
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MapIcon { get; set; }
        public bool Deleted { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public Employees Employees { get; set; }
    }
}
