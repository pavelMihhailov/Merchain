// ReSharper disable VirtualMemberCallInConstructor
namespace Merchain.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    using Merchain.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Orders = new HashSet<Order>();
            this.PromoCodes = new HashSet<PromoCode>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        [DefaultValue("")]
        public string Address { get; set; }

        [DefaultValue("")]
        public string Address2 { get; set; }

        [DefaultValue("")]
        public string Country { get; set; }

        [DefaultValue("")]
        public string ZipCode { get; set; }

        [DefaultValue("")]
        public override string PhoneNumber { get; set; }

        [DefaultValue(false)]
        public bool IsSubscribed { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<PromoCode> PromoCodes { get; set; }
    }
}
