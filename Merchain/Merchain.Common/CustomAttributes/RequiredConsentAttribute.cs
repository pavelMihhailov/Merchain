namespace Merchain.Common.CustomAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsageAttribute(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class RequiredConsentAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value is bool && (bool)value;
        }
    }
}
