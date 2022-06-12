using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebBanDienThoaiResponsive.CustomModelStateValidation;

namespace WebBanDienThoaiResponsive.ViewModels
{
    public class ChangePasswordViewModel
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [MinLength(8, ErrorMessage = ("Mật khẩu phải từ 8 - 24 ký tự"))]
        [MaxLength(24, ErrorMessage = ("Mật khẩu phải từ 8 - 24 ký tự"))]
        [PasswordExpression(ErrorMessage = ("Nhập lại mật khẩu không đúng định dạng, chứa ít nhất 1 ký tự in hoa, in thường, ký tự số và ký tự đặc biệt"))]
        public string OldPassword { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [MinLength(8, ErrorMessage = ("Mật khẩu phải từ 8 - 24 ký tự"))]
        [MaxLength(24, ErrorMessage = ("Mật khẩu phải từ 8 - 24 ký tự"))]
        [PasswordExpression(ErrorMessage = ("Nhập lại mật khẩu không đúng định dạng, chứa ít nhất 1 ký tự in hoa, in thường, ký tự số và ký tự đặc biệt"))]
        public string NewPassword { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [MinLength(8, ErrorMessage = ("Mật khẩu phải từ 8 - 24 ký tự"))]
        [MaxLength(24, ErrorMessage = ("Mật khẩu phải từ 8 - 24 ký tự"))]
        [PasswordExpression(ErrorMessage = ("Nhập lại mật khẩu không đúng định dạng, chứa ít nhất 1 ký tự in hoa, in thường, ký tự số và ký tự đặc biệt"))]
        public string ConfirmNewPassword { get; set; }

        [Required(ErrorMessage = "Bạn phải chọn câu hỏi")]
        public Guid IDQuestion { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Câu trả lời không được để trống")]
        public string Answer { get; set; }
    }
}