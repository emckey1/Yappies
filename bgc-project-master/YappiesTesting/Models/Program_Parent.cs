using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YappiesTesting.Models
{
    public class Program_Parent : Auditable
    {

        [Display(Name = "Parent")]
        [Required(ErrorMessage = "Parent is required.")]
        public int ParentID { get; set; }

        [Display(Name = "Program")]
        [Required(ErrorMessage = "Program is required.")]
        public int ProgramID { get; set; }

        public virtual Parent Parent { get; set; }
        public virtual Program Program { get; set; }
    }
}
