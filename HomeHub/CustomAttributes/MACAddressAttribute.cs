using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HomeHub.CustomAttributes
{
    public class MACAddressAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            Regex macRegex = new Regex("^([0-9a-fA-F][0-9a-fA-F]:){5}([0-9a-fA-F][0-9a-fA-F])$");
            Match match = macRegex.Match(value.ToString());
            
            if (match.Success)
                return ValidationResult.Success;

            return new ValidationResult(GetErrorMessage()); ;
        }

        public string GetErrorMessage()
        {
            return "MAC Address is not valid";
        }
    }
}
