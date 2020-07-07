using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YappiesTesting.Models
{
    public class Activity : Auditable
    {
        public int ID { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Activity requires a title.")]
        [StringLength(50)]
        public string Title { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Activity requires a description.")]
        [StringLength(250)]
        public string Description { get; set; }

        [Display(Name = "Date & Time")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Activity requires a date & time.")]
        public DateTime Date { get; set; }

        public int ProgramID { get; set; }
        public virtual Program Program { get; set; }

        /*
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Date < DateTime.Today)
            {
                yield return new ValidationResult("Date cannot be in the past.", new[] { "Date" });
            }

        }*/
    }
}
