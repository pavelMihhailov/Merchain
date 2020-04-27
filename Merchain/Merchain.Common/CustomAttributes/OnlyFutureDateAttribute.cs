namespace Merchain.Common.CustomAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class OnlyFutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            value = (DateTime)value;

            if (DateTime.UtcNow.CompareTo(value) <= 0)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Date must be only future date!");
            }
        }
    }
}
