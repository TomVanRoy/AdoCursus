﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Taken
{
    public class VeldMoetIngevuldZijn : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || ((string)value).Length == 0)
            {
                return new ValidationResult(false, "Veld moet ingevuld zijn");
            }
            return ValidationResult.ValidResult;
        }
    }
}