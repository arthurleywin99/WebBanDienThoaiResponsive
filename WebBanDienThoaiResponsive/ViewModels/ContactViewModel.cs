using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebBanDienThoaiResponsive.CustomModelStateValidation;

namespace WebBanDienThoaiResponsive.ViewModels
{
    public class ContactViewModel
    {
        public Guid IDProblem { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email không được để trống")]
        [RegularExpression(@"^\w{1,}([-+.']\w{1,}){0,}@\w{1,}([-.]\w{1,}){0,}\.\w{1,}([-.]\w{1,}){0,}$", ErrorMessage = "Không đúng định dạng Email.")]
        [MinLength(5, ErrorMessage = ("Định dạng số điện thoại từ 5 - 25 ký tự số"))]
        [MaxLength(25, ErrorMessage = ("Định dạng số điện thoại từ 5 - 25 ký tự số"))]
        public string Email { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Họ tên không được để trống")]
        [VietnameseExpression(ErrorMessage = "Họ tên không chứa ký tự đặc biệt trừ khoảng trắng")]
        [MinLength(1, ErrorMessage = ("Định dạng số điện thoại từ 1 - 100 ký tự số"))]
        [MaxLength(100, ErrorMessage = ("Định dạng số điện thoại từ 1 - 100 ký tự số"))]
        public string Fullname { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [MinLength(10, ErrorMessage = ("Định dạng số điện thoại từ 10 - 13 ký tự số"))]
        [MaxLength(13, ErrorMessage = ("Định dạng số điện thoại từ 10 - 13 ký tự số"))]
        [PhoneExpression(ErrorMessage = "Số điện thoại không đúng định dạng")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [MaxLength(255, ErrorMessage = ("Nội dung dưới 255 ký tự"))]
        public string Title { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Nội dung không được để trống")]
        public string Content { get; set; }
    }
}