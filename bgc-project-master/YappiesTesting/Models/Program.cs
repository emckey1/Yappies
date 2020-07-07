using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace YappiesTesting.Models
{
    public class Program : Auditable
    {
        public Program()
        {
            this.Parents = new HashSet<Program_Parent>();
            this.Activities = new HashSet<Activity>();
            this.Announcements = new HashSet<Announcement>();
            this.Program_Children = new HashSet<Program_Child>();
        }

        public int ID { get; set; }

        [Display(Name = "Program Name")]
        [Required(ErrorMessage = "Program requires a name.")]
        [StringLength(500)]
        public string ProgramName { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Program requires a description.")]
        [StringLength(1000000)]
        public string ProgramDescription { get; set; }

        [Display(Name = "Join Code")]
        [Required(ErrorMessage = "Program requires a join code.")]
        [StringLength(25)]
        public string ProgramJoinCode { get; set; }

        [Display(Name = "Program Supervisor")]
        [Required(ErrorMessage = "Program Supervisor required.")]
        public int ProgramSupervisorID { get; set; }

        public virtual ProgramSupervisor ProgramSupervisor { get; set; }
        public virtual ICollection<Program_Parent> Parents { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<Announcement> Announcements { get; set; }
        public virtual ICollection<Program_Child> Program_Children { get; set; }
    }
}
