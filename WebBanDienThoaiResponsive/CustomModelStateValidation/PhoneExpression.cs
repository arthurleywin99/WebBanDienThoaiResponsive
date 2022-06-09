using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WebBanDienThoaiResponsive.CustomModelStateValidation
{
    public class PhoneExpression : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()) || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return false;
            }
            const string pattern = @"(84|0[3|4|5|6|7|8|9)+([0-9]{8})";
            return Regex.IsMatch(value.ToString().Trim(), pattern);
        }
    }
}