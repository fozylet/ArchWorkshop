using System.ComponentModel.DataAnnotations;

namespace SpartanHotels.Entities
{
    public class Customer
    {
        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "EMail ID is required. We'll never spam you.")]
        [Display(Name = "EMail ID")]
        public string EMailAddress { get; set; }
    }
}
