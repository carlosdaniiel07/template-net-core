using System;
using System.ComponentModel.DataAnnotations;


namespace TemplateNetCore.Domain.Validations
{
    public class PositiveValueValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            decimal number = Convert.ToDecimal(value);
            return number > 0;
        }
    }
}
