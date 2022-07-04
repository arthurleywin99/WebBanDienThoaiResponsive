using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDienThoaiResponsive.ViewModels;
using WebBanDienThoaiResponsive.Models;

namespace WebBanDienThoaiResponsive.Areas.Admin.Controllers
{
    public class AdminAccountController : Controller
    {
        [HttpGet]
        public ActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signin(SigninViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                string username = viewModel.Username.ToString().Trim();
                string password = Utility.MD5Hash(viewModel.Password.ToString().Trim());
                using (var context = new Context())
                {
                    bool isExist = context.MemberAccounts.Any(p => p.Email.Trim().ToUpper().Equals(username.ToUpper()) && p.Password.Trim().Equals(password))
                        || context.MemberAccounts.Any(p => p.PhoneNumber.Trim().ToUpper().Equals(username.ToUpper()) && p.Password.Trim().Equals(password));
                    if (isExist == false)
                    {
                        ViewData["SigninError"] = "Tên tài khoản hoặc mật khẩu không chính xác";
                        return View();
                    }
                    else
                    {
                        MemberAccount memberWithEmail = context.MemberAccounts.FirstOrDefault(p => p.Email.Trim().ToUpper().Equals(username.ToUpper()) && p.Password.Trim().Equals(password));
                        MemberAccount memberWithPhone = context.MemberAccounts.FirstOrDefault(p => p.PhoneNumber.Trim().ToUpper().Equals(username.ToUpper()) && p.Password.Trim().Equals(password));

                        if (memberWithEmail != null)
                        {
                            if (context.AccountTypes.FirstOrDefault(p => p.ID == memberWithEmail.MemberTypeID).UserTypeName == "Admin")
                            {
                                string[] Name = memberWithEmail.FullName.Split(' ');
                                Session["Account"] = memberWithEmail;
                                return RedirectToAction("Index", "AdminStatistic");
                            }
                            else
                            {
                                ViewData["SigninError"] = "Bạn không có quyền vào trang quản trị";
                            }
                        }
                        else if (memberWithPhone != null)
                        {
                            if (context.AccountTypes.FirstOrDefault(p => p.ID == memberWithPhone.MemberTypeID).UserTypeName == "Admin")
                            {
                                string[] Name = memberWithPhone.FullName.Split(' ');
                                Session["Account"] = memberWithPhone;
                                return RedirectToAction("Index", "AdminStatistic");
                            }
                            else
                            {
                                ViewData["SigninError"] = "Bạn không có quyền vào trang quản trị";
                            }
                        }
                        else
                        {
                            return HttpNotFound();
                        }
                    }
                }
            }
            return View();
        }
    }
}