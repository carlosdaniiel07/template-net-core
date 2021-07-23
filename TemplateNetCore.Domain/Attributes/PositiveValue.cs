using System;
using System.ComponentModel.DataAnnotations;


namespace TemplateNetCore.Domain.Attributes
{
    public class PositiveValue : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            decimal number = Convert.ToDecimal(value);
            return number > 0;
        }
    }
}
