using System;
using System.Collections.Generic;
using System.Linq;
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
            MemberAccount account = Session["Account"] as MemberAccount;
            Guid TypeID = (Guid)account.MemberTypeID;
            using (var context = new Context())
            {
                AccountType AccountType = (from A in context.MemberAccounts
                                          join B in context.AccountTypes
                                          on A.MemberTypeID equals B.ID
                                          where A.MemberTypeID == TypeID
                                          select B).FirstOrDefault();
                ViewBag.AccountType = AccountType;
                return View();
            }
        }

        [HttpGet]
        public ActionResult OrderHistory()
        {
            using (var context = new Context())
            {
                MemberAccount account = Session["Account"] as MemberAccount;
                List<Order> orderList = context.Orders.Where(p => p.MemberID == account.ID).ToList();
                if (orderList.Count > 0)
                {
                    return View(orderList);
                }
                else
                {
                    return View();
                }
            }
        }

        [HttpGet]
        public ActionResult CancelOrder(Guid Id, string CurrentURL)
        {
            using (var context = new Context())
            {
                List<OrderDetail> removeDetails = context.OrderDetails.Where(p => p.OrderID == Id).ToList();
                foreach (var item in removeDetails)
                {
                    context.OrderDetails.Remove(item);
                }
                Order removeOrder = context.Orders.FirstOrDefault(p => p.ID == Id);
                context.Orders.Remove(removeOrder);
                context.SaveChanges();
                return Redirect(CurrentURL);
            }
        }

        [HttpGet]
        public ActionResult Details(Guid Id, string CurrentURL)
        {
            using (var context = new Context())
            {
                Order order = context.Orders.FirstOrDefault(p => p.ID == Id);
                List<OrderDetail> orderDetailList = context.OrderDetails.Where(p => p.OrderID == Id).ToList();
                List<OrderDetailsViewModel> orderDetails = new List<OrderDetailsViewModel>();
                foreach (var item in orderDetailList)
                {
                    OrderDetailsViewModel detail = new OrderDetailsViewModel();
                    detail.ImageURL = context.Products.FirstOrDefault(p => p.ID == item.ProductID).ImageURL;
                    detail.ProductName = context.Products.FirstOrDefault(p => p.ID == item.ProductID).ProductName;
                    detail.Quantity = item.Quantity;
                    detail.PriceNow = Convert.ToInt64(item.PriceNow);
                    orderDetails.Add(detail);
                }
                ViewBag.Order = order;
                ViewBag.OrderDetailList = orderDetails;
                ViewBag.ReturnURL = CurrentURL;
                return View();
            }
        }

        [HttpGet]
        public ActionResult Rating(Guid Id)
        {
            using (var context = new Context())
            {
                List<OrderDetail> orderDetailList = context.OrderDetails.Where(p => p.OrderID == Id).ToList();
                List<OrderDetailsViewModel> orderDetails = new List<OrderDetailsViewModel>();
                foreach (var item in orderDetailList)
                {
                    OrderDetailsViewModel detail = new OrderDetailsViewModel();
                    detail.OrderId = item.OrderID;
                    detail.ProductId = item.ProductID;
                    detail.ImageURL = context.Products.FirstOrDefault(p => p.ID == item.ProductID).ImageURL;
                    detail.ProductName = context.Products.FirstOrDefault(p => p.ID == item.ProductID).ProductName;
                    detail.Quantity = item.Quantity;
                    detail.PriceNow = Convert.ToInt64(item.PriceNow);
                    detail.RatingStar = Convert.ToInt32(item.RatingStar);
                    detail.Content = item.Content;
                    orderDetails.Add(detail);
                }
                return View(orderDetails);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendReviews(Guid orderId, Guid productId, FormCollection form, string CurrentURL)
        {
            using (var context = new Context())
            {
                OrderDetail detail = context.OrderDetails.FirstOrDefault(p => p.OrderID == orderId && p.ProductID == productId);
                try
                {
                    detail.RatingStar = Convert.ToInt32(form["rating"].ToString());
                }
                catch (Exception)
                {
                    detail.RatingStar = 0;
                }
                try
                {
                    detail.Content = form["content"].ToString();
                }
                catch (Exception)
                {
                    detail.Content = "";
                }
                Product product = context.Products.FirstOrDefault(p => p.ID == detail.ProductID);
                product.RatingCount++;
                context.SaveChanges();
                return Redirect(CurrentURL);
            }
        }

        [HttpGet]
        public ActionResult ChangePassword()
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
        public ActionResult ChangePassword(ChangePasswordViewModel viewModel)
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
                using (var context = new Context())
                {
                    string oldPassword = viewModel.OldPassword.ToString();
                    string newPassword = viewModel.NewPassword.ToString();
                    string reNewPassword = viewModel.ConfirmNewPassword.ToString();
                    Guid idQuestion = viewModel.IDQuestion;
                    string answer = viewModel.Answer.ToString();

                    if (!newPassword.Equals(reNewPassword))
                    {
                        ViewData["ChangePasswordError"] = "Nhập lại không khớp mật khẩu";
                        List<AuthenticationQAndA> authListQuestions = context.AuthenticationQAndAs.ToList();
                        ViewBag.AuthenticationQAList = authListQuestions;
                        return View();
                    }
                    MemberAccount accountSignedIn = Session["Account"] as MemberAccount;
                    MemberAccount tempAccount = context.MemberAccounts.FirstOrDefault(p => p.ID.Equals(accountSignedIn.ID));
                    if (tempAccount != null)
                    {
                        if (tempAccount.Password.Equals(Utility.MD5Hash(oldPassword)))
                        {
                            if (tempAccount.IDQuestion != idQuestion || !tempAccount.Answer.Equals(answer))
                            {
                                ViewData["ChangePasswordError"] = "Câu hỏi bảo mật hoặc câu trả lời không đúng";
                                List<AuthenticationQAndA> authListQuestions = context.AuthenticationQAndAs.ToList();
                                ViewBag.AuthenticationQAList = authListQuestions;
                                return View();
                            }
                            else
                            {
                                try
                                {
                                    tempAccount.Password = Utility.MD5Hash(newPassword);
                                    context.SaveChanges();
                                    return RedirectToAction("Signin", "Account");
                                }
                                catch (Exception)
                                {
                                    ViewData["ChangePasswordError"] = "Có lỗi xảy ra, vui lòng đăng nhập lại. ";
                                    List<AuthenticationQAndA> authListQuestions = context.AuthenticationQAndAs.ToList();
                                    ViewBag.AuthenticationQAList = authListQuestions;
                                    return View();
                                }
                            }
                        }
                        else
                        {
                            ViewData["ChangePasswordError"] = "Mật khẩu cũ không đúng";
                            List<AuthenticationQAndA> authListQuestions = context.AuthenticationQAndAs.ToList();
                            ViewBag.AuthenticationQAList = authListQuestions;
                            return View();
                        }
                    }
                    else
                    {
                        ViewData["ChangePasswordError"] = "Lỗi đăng nhập";
                        return RedirectToAction("Signout", "Account");
                    }
                }
            }
        }

        [HttpGet]
        public ActionResult AccountConfig()
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
        public ActionResult AccountConfig(ChangeProfileViewModel viewModel)
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
                using (var context = new Context())
                {
                    string fullName = viewModel.FullName.ToString().Trim();
                    string address = viewModel.Address.ToString().Trim();
                    string stringBirthDate = viewModel.BirthDate.ToString().Trim();
                    DateTime birthDate = Convert.ToDateTime(stringBirthDate);
                    Guid IdQuestion = viewModel.IDQuestion;
                    string answer = viewModel.Answer.ToString().Trim();

                    MemberAccount accountSignedIn = Session["Account"] as MemberAccount;
                    MemberAccount tempAccount = context.MemberAccounts.FirstOrDefault(p => p.ID.Equals(accountSignedIn.ID));
                    if (tempAccount != null)
                    {
                        if (tempAccount.IDQuestion == IdQuestion && tempAccount.Answer.Equals(answer))
                        {
                            try
                            {
                                tempAccount.FullName = fullName;
                                tempAccount.Address = address;
                                tempAccount.BirthDate = birthDate;
                                context.SaveChanges();
                                return RedirectToAction("Signin", "Account");
                            }
                            catch (Exception)
                            {
                                ViewData["ChangePasswordError"] = "Có lỗi xảy ra, vui lòng đăng nhập lại. ";
                                List<AuthenticationQAndA> authListQuestions = context.AuthenticationQAndAs.ToList();
                                ViewBag.AuthenticationQAList = authListQuestions;
                                return View();
                            }
                        }
                        else
                        {
                            ViewData["ChangeProfileError"] = "Sai câu hỏi bảo mật";
                            List<AuthenticationQAndA> authListQuestions = context.AuthenticationQAndAs.ToList();
                            ViewBag.AuthenticationQAList = authListQuestions;
                            return View();
                        }
                    }
                    else
                    {
                        ViewData["ChangeProfileError"] = "Lỗi đăng nhập";
                        return RedirectToAction("Signout", "Account");
                    }
                }
            }
        }
    }
}