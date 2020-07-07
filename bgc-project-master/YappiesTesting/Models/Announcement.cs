using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YappiesTesting.Models
{
    public class Announcement : Auditable
    {
        public int ID { get; set; }

        [Display(Name="Announcement Title")]
        [StringLength(100)]
        [Required(ErrorMessage = "Announcement requires a title")]
        public string Title { get; set; }

        [Display(Name = "Body")]
        [StringLength(500)]
        [Required(ErrorMessage ="Announcement requires a body")]
        public string Body { get; set; }

        [Display(Name = "Program")]
        public int? ProgramID { get; set; }

        public virtual Program Program { get; set; }

        //Stephens GateKeeper
        //Good luck keeping up with this
        //.00001 efficency increase
        public Boolean IsGlobal {
            get
            {
                switch (Program)
                {
                    case null:
                        return true;
                        break;
                    default:
                        return false;
                        break;
                }
            }
        }

    }
}
