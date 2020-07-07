using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YappiesTesting.Models
{
    public class ProgramSupervisor : Auditable
    {

        public ProgramSupervisor()
        {
            this.Programs = new HashSet<Program>();
            this.Conversations = new HashSet<Conversation>();
        }

        public int ID { get; set; }

        [Display(Name = "Supervisor")]
        public string Supervisor
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }


        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You cannot leave the supervisor first name field blank.")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You cannot leave the supervisor last name field blank.")]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters")]
        public string LastName { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "You cannot leave the supervisor e-mail field blank")]
        [DataType(DataType.EmailAddress)]
        [StringLength(255)]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "You cannot leave the supervisor phone number field blank")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:(###) ###-####}", ApplyFormatInEditMode = false)]
        public Int64 Phone { get; set; }

        public virtual ICollection<Program> Programs { get; set; }
        public virtual ICollection<Conversation> Conversations { get; set; }
    }
}
