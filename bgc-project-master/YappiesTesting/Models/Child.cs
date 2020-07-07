using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YappiesTesting.Models
{
    public class Child
    {
        public Child ()
        {
            this.Programs = new HashSet<Program_Child>();
        }

        public int ID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Child Requires a First Name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        public int ParentID { get; set; }
        public virtual Parent Parent { get; set; }

        public virtual ICollection<Program_Child> Programs { get; set; }
    }
}
