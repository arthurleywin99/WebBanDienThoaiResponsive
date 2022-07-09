using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanDienThoaiResponsive.CustomModelStateValidation
{
    public class IsNumber : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }
            return long.TryParse(value.ToString(), out _);
        }
    }
}