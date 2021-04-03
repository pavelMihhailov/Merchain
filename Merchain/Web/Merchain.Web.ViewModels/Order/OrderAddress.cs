namespace Merchain.Web.ViewModels.Order
{
    using System.ComponentModel.DataAnnotations;

    public class OrderAddress
    {
        public bool ShipToOffice { get; set; }

        [Display(Name = "* Населено Място")]
        public string Country { get; set; }

        [Display(Name = "* Адрес")]
        public string Address { get; set; }

        [Display(Name = "Допълнителен Адрес")]
        public string Address2 { get; set; }

        public string OfficeIdSelected { get; set; }

        [Display(Name = "* Име")]
        [Required(ErrorMessage = "Името е задължително.")]
        public string FirstName { get; set; }

        [Display(Name = "* Фамилия")]
        [Required(ErrorMessage = "Фамилията е задължителна.")]
        public string LastName { get; set; }

        [Display(Name = "* Телефон")]
        [Required(ErrorMessage = "Телефонът е задължителен.")]
        public string Phone { get; set; }

        [Display(Name = "* Имейл Адрес")]
        [EmailAddress(ErrorMessage = "Имейл адресът е задължителен.")]
        public string Email { get; set; }
    }
}
