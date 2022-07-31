using System.ComponentModel.DataAnnotations;

namespace eTickets.Data.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = " User Full Name is Required")]
        [Display(Name ="User Full Name")]
        public string Name { get; set; }
        [Display(Name ="Email Address")]
        [Required(ErrorMessage ="Email Address is required")]
        public  string EmailAddress { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password not matched")]
        public string ConfirmedPassword { get; set; }
    }
}
