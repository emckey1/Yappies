using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YappiesTesting.Models
{
    public class Program_Child
    {
        [Display(Name = "Child")]
        [Required(ErrorMessage = "Child is required.")]
        public int ChildID { get; set; }

        [Display(Name = "Program")]
        [Required(ErrorMessage = "Program is required.")]
        public int ProgramID { get; set; }

        public virtual Child Child { get; set; }
        public virtual Program Program { get; set; }
    }
}
