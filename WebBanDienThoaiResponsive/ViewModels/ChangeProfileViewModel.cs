using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebBanDienThoaiResponsive.CustomModelStateValidation;

namespace WebBanDienThoaiResponsive.ViewModels
{
    public class ChangeProfileViewModel
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Họ tên không được để trống")]
        [VietnameseExpression(ErrorMessage = "Họ tên không chứa ký tự đặc biệt trừ khoảng trắng")]
        [MinLength(1, ErrorMessage = ("Định dạng số điện thoại từ 1 - 100 ký tự số"))]
        [MaxLength(100, ErrorMessage = ("Định dạng số điện thoại từ 1 - 100 ký tự số"))]
        public string FullName { get; set; }

        public string Address { get; set; }

        [Required(ErrorMessage = "Ngày sinh không được để trống")]
        [IsValidDate(ErrorMessage = "Ngày sinh phải đúng định dạng và nhỏ hơn ngày hiện tại")]
        public string BirthDate { get; set; }

        public Guid IDQuestion { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = ("Câu trả lời không được để trống"))]
        public string Answer { get; set; }
    }
}