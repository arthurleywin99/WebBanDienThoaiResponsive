using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Helpers;
using System.Web.Mvc;
using WebBanDienThoaiResponsive.Models;
using WebBanDienThoaiResponsive.ViewModels;

namespace WebBanDienThoaiResponsive.Controllers
{
    public class AccountController : Controller
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
                    MemberAccount memberWithEmail = context.MemberAccounts.FirstOrDefault(p => p.Email.Trim().ToUpper().Equals(username.ToUpper()) && p.Password.Trim().Equals(password));
                    MemberAccount memberWithPhone = context.MemberAccounts.FirstOrDefault(p => p.PhoneNumber.Trim().ToUpper().Equals(username.ToUpper()) && p.Password.Trim().Equals(password));
                    if (memberWithEmail == null && memberWithPhone == null)
                    {
                        ViewData["SigninError"] = "Tên tài khoản hoặc mật khẩu không chính xác";
                        return View();
                    }
                    else 
                    {
                        if (memberWithEmail != null)
                        {
                            string[] Name = memberWithEmail.FullName.Split(' ');
                            Session["Account"] = memberWithEmail;
                            return RedirectToAction("Index", "Home");
                        }
                        else if (memberWithPhone != null)
                        {
                            string[] Name = memberWithPhone.FullName.Split(' ');
                            Session["Account"] = memberWithPhone;
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return HttpNotFound();
                        }
                    }
                }
            }
        }

        [HttpGet]
        public ActionResult Signup()
        {
            using (var context = new Context())
            {
                List<AuthenticationQAndA> authListQuestions = context.AuthenticationQAndAs.ToList();
                ViewBag.AuthenticationQAList = authListQuestions;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup(SignupViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                using (var context = new Context())
                {
                    List<AuthenticationQAndA> authListQuestions = context.AuthenticationQAndAs.ToList();
                    ViewBag.AuthenticationQAList = authListQuestions;
                    return View();
                }
            }
            else
            {
                string email = viewModel.Email.ToString().Trim();
                string phoneNumber = viewModel.PhoneNumber.ToString().Trim();
                string password = viewModel.Password.ToString().Trim();
                string rePassword = viewModel.RePassword.ToString().Trim();
                string fullName = viewModel.FullName.ToString().Trim();
                Guid IdQuestion = viewModel.IDQuestion;
                string answer = viewModel.Answer.ToString().Trim();
                bool isCheck = viewModel.IsChecked;

                if (!password.Equals(rePassword))
                {
                    ViewData["RePasswordError"] = "Mật khẩu không khớp";
                    return View();
                }
                if (!isCheck)
                {
                    ViewData["IsCheck"] = "Bạn chưa đồng ý với điều khoản dịch vụ của chúng tôi";
                    return View();
                }
                if (String.IsNullOrEmpty(answer) || answer.Length == 0)
                {
                    ViewData["IsAnswer"] = "Bạn chưa điền câu trả lời";
                    return View();
                }

                using (var context = new Context())
                {
                    bool isEmailExist = context.MemberAccounts.Any(p => p.Email.Equals(email));
                    if (isEmailExist)
                    {
                        ViewData["EmailError"] = "Email đã tồn tại trong hệ thống. Vui lòng sử dụng email khác";
                        return View();
                    }
                    bool isPhoneExist = context.MemberAccounts.Any(p => p.PhoneNumber.Equals(phoneNumber));
                    if (isPhoneExist)
                    {
                        ViewData["PhoneError"] = "Số điện thoại đã tồn tại trong hệ thống. Vui lòng sử dụng số điện thoại khác";
                        return View();
                    }
                    Guid AccountTypeID = context.AccountTypes.FirstOrDefault(p => p.UserTypeName.Equals("Basic")).ID;
                    MemberAccount member = new MemberAccount()
                    {
                        ID = Guid.NewGuid(),
                        MemberTypeID = AccountTypeID,
                        Email = email,
                        Password = Utility.MD5Hash(password),
                        FullName = fullName,
                        PhoneNumber = phoneNumber,
                        IDQuestion = IdQuestion,
                        Answer = answer,
                        Status = true
                    };
                    context.MemberAccounts.Add(member);
                    context.SaveChanges();
                    TempData["AlertMessage"] = "Tạo tài khoản thành công. Vui lòng đăng nhập";
                    return RedirectToAction("Signin", "Account");
                }
            }
        }

        [HttpGet]
        public ActionResult Signout()
        {
            Session["Account"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ForgotPassword()
        {                 
            return View();
        }

        [HttpGet]
        public ActionResult AccountDetail()
        {
            return View();
        }

        [HttpGet]
        public ActionResult OrderHistory()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel viewModel)
        {
            return RedirectToAction("Signin", "Account");
        }

        [HttpGet]
        public ActionResult AccountConfig()
        {
            return View();
        }
    }
}