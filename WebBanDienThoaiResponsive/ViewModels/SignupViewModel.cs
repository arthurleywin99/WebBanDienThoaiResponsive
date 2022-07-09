using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebBanDienThoaiResponsive.CustomModelStateValidation;

namespace WebBanDienThoaiResponsive.ViewModels
{
    public class SignupViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email không được để trống")]
        [RegularExpression(@"^\w{1,}([-+.']\w{1,}){0,}@\w{1,}([-.]\w{1,}){0,}\.\w{1,}([-.]\w{1,}){0,}$", ErrorMessage = "Không đúng định dạng Email.")]
        public string Email { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [MinLength(10, ErrorMessage = ("Định dạng số điện thoại từ 10 - 13 ký tự số"))]
        [MaxLength(13, ErrorMessage = ("Định dạng số điện thoại từ 10 - 13 ký tự số"))]
        [PhoneExpression(ErrorMessage = "Số điện thoại không đúng định dạng")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [MinLength(8, ErrorMessage = ("Mật khẩu phải từ 8 - 24 ký tự"))]
        [MaxLength(24, ErrorMessage = ("Mật khẩu phải từ 8 - 24 ký tự"))]
        [PasswordExpression(ErrorMessage = ("Mật khẩu không đúng định dạng, chứa ít nhất 1 ký tự in hoa, in thường, ký tự số và ký tự đặc biệt"))]
        public string Password { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [MinLength(8, ErrorMessage = ("Mật khẩu phải từ 8 - 24 ký tự"))]
        [MaxLength(24, ErrorMessage = ("Mật khẩu phải từ 8 - 24 ký tự"))]
        [PasswordExpression(ErrorMessage = ("Nhập lại mật khẩu không đúng định dạng, chứa ít nhất 1 ký tự in hoa, in thường, ký tự số và ký tự đặc biệt"))]
        public string RePassword { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Họ tên không được để trống")]
        [VietnameseExpression(ErrorMessage = "Họ tên không chứa ký tự đặc biệt trừ khoảng trắng")]
        [MinLength(1, ErrorMessage = ("Định dạng họ tên từ 1 - 100 ký tự số"))]
        [MaxLength(100, ErrorMessage = ("Định dạng họ tên từ 1 - 100 ký tự số"))]
        public string FullName { get; set; }

        [IsChecked(ErrorMessage = ("Bạn chưa đồng ý với điều khoản dịch vụ của chúng tôi"))]
        public bool IsChecked { get; set; }

        public Guid IDQuestion { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = ("Câu trả lời không được để trống"))]
        public string Answer { get; set; }
    }
}