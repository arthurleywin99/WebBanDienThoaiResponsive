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
                    bool isExist = context.AdminConfigs.Any(p => p.AdEmail.Trim().ToUpper().Equals(username.ToUpper()) && p.AdPassword.Trim().Equals(password))
                        || context.AdminConfigs.Any(p => p.AdPhoneNumber.Trim().ToUpper().Equals(username.ToUpper()) && p.AdPassword.Trim().Equals(password));
                    if (isExist == false)
                    {
                        ViewData["SigninError"] = "Tên tài khoản hoặc mật khẩu không chính xác";
                        return View();
                    }
                    else
                    {
                        AdminConfig memberWithEmail = context.AdminConfigs.FirstOrDefault(p => p.AdEmail.Trim().ToUpper().Equals(username.ToUpper()) && p.AdPassword.Trim().Equals(password));
                        AdminConfig memberWithPhone = context.AdminConfigs.FirstOrDefault(p => p.AdPhoneNumber.Trim().ToUpper().Equals(username.ToUpper()) && p.AdPassword.Trim().Equals(password));

                        if (memberWithEmail != null)
                        {
                            string[] Name = memberWithEmail.Name.Split(' ');
                            Session["AdminAccount"] = memberWithEmail;
                            return RedirectToAction("Index", "AdminStatistic");
                        }
                        else if (memberWithPhone != null)
                        {
                            string[] Name = memberWithPhone.Name.Split(' ');
                            Session["AdminAccount"] = memberWithPhone;
                            return RedirectToAction("Index", "AdminStatistic");
                        }
                    }
                    return HttpNotFound();
                }
            }
        }

        [HttpGet]
        public ActionResult UpdateAccount()
        {
            AdminConfig adminAccount = Session["AdminAccount"] as AdminConfig;
            return View(adminAccount);
        }
    }
}