using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MessengerClone.Validations
{
    class EmailValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(!value.ToString().Contains("@"))
            {
                return new ValidationResult(false, "Enter Valid Email");
            }
            return ValidationResult.ValidResult;
        }
    }
}
