using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WebBanDienThoaiResponsive.CustomModelStateValidation
{
    public class PasswordExpression : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()) || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return false;
            }
            Regex hasNumber = new Regex(@"[0-9]+");
            Regex hasUpperChar = new Regex(@"[A-Z]+");
            Regex hasLowerChar = new Regex(@"[a-z]+");
            Regex hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
            Regex hasRange = new Regex(@".{8,24}");
            return hasNumber.IsMatch(value.ToString())
                && hasUpperChar.IsMatch(value.ToString())
                && hasLowerChar.IsMatch(value.ToString())
                && hasSymbols.IsMatch(value.ToString())
                && hasRange.IsMatch(value.ToString());
        }
    }
}