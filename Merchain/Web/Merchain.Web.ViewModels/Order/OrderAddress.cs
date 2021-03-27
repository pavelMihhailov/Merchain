namespace Merchain.Web.ViewModels.Order
{
    using System.ComponentModel.DataAnnotations;

    public class OrderAddress
    {
        public bool ShipToOffice { get; set; }

        public string Country { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string OfficeIdSelected { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
