namespace Merchain.Data.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseModel<TKey> : IAuditInfo
    {
        [Key]
        public TKey Id { get; set; }

        [Display(Name = "Създадено на")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Променено на")]
        public DateTime? ModifiedOn { get; set; }
    }
}
