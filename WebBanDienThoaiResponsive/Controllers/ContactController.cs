using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDienThoaiResponsive.Helper;
using WebBanDienThoaiResponsive.Models;
using WebBanDienThoaiResponsive.ViewModels;

namespace WebBanDienThoaiResponsive.Controllers
{
    public class ContactController : Controller
    {
        [HttpGet]
        public ActionResult SendContact()
        {
            using (var context = new Context())
            {
                List<ProblemContact> problemContactList = context.ProblemContacts.ToList();
                ViewBag.ProblemContacts = problemContactList;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendContact(ContactViewModel viewModel)
        {
            using (var context = new Context())
            {
                if (!ModelState.IsValid)
                {
                    List<ProblemContact> problemContactList = context.ProblemContacts.ToList();
                    ViewBag.ProblemContacts = problemContactList;
                    return View();
                }
                else
                {
                    string email = viewModel.Email.ToString().Trim();
                    string fullname = viewModel.Fullname.ToString().Trim();
                    string phoneNumber = viewModel.PhoneNumber.ToString().Trim();
                    string title = viewModel.Title.ToString().Trim();
                    string content = viewModel.Content.ToString().Trim();
                    Guid IdProblem = viewModel.IDProblem;

                    QAndA qAndA = new QAndA
                    {
                        ID = Guid.NewGuid(),
                        Email = email,
                        Fullname = fullname,
                        PhoneNumber = phoneNumber,
                        Title = title,
                        Content = content,
                        Status = false,
                        IDProblem = IdProblem
                    };
                    context.QAndAs.Add(qAndA);
                    context.SaveChanges();

                    string data = "Cám ơn bạn đã gửi thông tin thắc mắc cho chúng tôi. Sẽ gửi phản hồi đến với bạn trong thời gian sớm nhất!";

                    MailHelper.SendMail(email, "Thông tin liên hệ từ Website", data);
                    return RedirectToAction("Index", "Home");
                }
            }
        }
    }
}