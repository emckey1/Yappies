using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YappiesTesting.Models
{
    public class Parent : Auditable
    {
        public Parent()
        {
            this.Programs = new HashSet<Program_Parent>();
            this.Children = new HashSet<Child>();
            this.Conversations = new HashSet<Conversation>();
        }

        public int ID { get; set; }

        [Display(Name = "Parent Name")]
        public string ParentName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        [Display(Name = "First Name")]
        [StringLength(25)]
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(25)]
        [Required(ErrorMessage = "Last name is requried.")]
        public string LastName { get; set; }

        [Display(Name = "Email Address")]
        [StringLength(255)]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email Address is required.")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:(###) ###-####}", ApplyFormatInEditMode = false)]
        public Int64 Phone { get; set; }

        public bool NotificationOptIn { get; set; }

        public bool SignOutOptIn { get; set; }

        public bool TwoFactorOptIn { get; set; }

        public bool DirectMessageOptIn { get; set; }

        public virtual ICollection<Program_Parent> Programs { get; set; }
        public virtual ICollection<Child> Children { get; set; }
        public virtual ICollection<Conversation> Conversations { get; set; }
    }
}
