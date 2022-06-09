using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebBanDienThoaiResponsive.CustomModelStateValidation;

namespace WebBanDienThoaiResponsive.ViewModels
{
    public class PayDetailsViewModel
    {
        public Guid OrderUserID { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Họ tên không được để trống")]
        [VietnameseExpression(ErrorMessage = "Họ tên không chứa ký tự đặc biệt trừ khoảng trắng")]
        [MinLength(1, ErrorMessage = ("Định dạng họ tên từ 1 - 100 ký tự số"))]
        [MaxLength(100, ErrorMessage = ("Định dạng họ tên từ 1 - 100 ký tự số"))]
        public string OrderFullName { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [MinLength(10, ErrorMessage = ("Định dạng số điện thoại từ 10 - 13 ký tự số"))]
        [MaxLength(13, ErrorMessage = ("Định dạng số điện thoại từ 10 - 13 ký tự số"))]
        [PhoneExpression(ErrorMessage = "Số điện thoại không đúng định dạng")]
        public string OrderPhoneNumber { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        public string OrderAddress { get; set; }

        [DataType(DataType.Text)]
        public string OrderNote { get; set; }

        public bool IsPaid { get; set; }

        public string TransferID { get; set; }
    }
}