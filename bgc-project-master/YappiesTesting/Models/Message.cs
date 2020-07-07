using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YappiesTesting.Models
{
    public class Message : Auditable
    {
        public int ID { get; set; }

        [Display(Name = "Message Text")]
        [Required(ErrorMessage = "You must enter a message to send.")]
        [StringLength(500)]
        public string MessageText { get; set; }

        public bool SentByParent { get; set; }

        public int ParentID { get; set; }
        public int ProgramSupervisorID { get; set; }
    }
}
