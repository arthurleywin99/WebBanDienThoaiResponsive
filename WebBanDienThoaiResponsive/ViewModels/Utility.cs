using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebBanDienThoaiResponsive.ViewModels
{
    public class Utility
    {
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }

        public static string secretKey = "n9bGuX6uBoIm341UyQbDlpO0013ZySvY";
        public static string partnerCode = "MOMOSJUT20220606";
        public static string accessKey = "V5WdjHkPzwPLv0Ir";

        public static string PENDING = "Đang Xử Lý";
        public static int DELIVERY_DAYS = 3;

        public static bool SendMail(string _to, string subject, string content)
        {
            try
            {
                var host = ConfigurationManager.AppSettings["SMTPHost"];
                var port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
                var fromEmail = ConfigurationManager.AppSettings["FromEmailAddress"];
                var password = ConfigurationManager.AppSettings["FromEmailPassword"];
                var fromName = ConfigurationManager.AppSettings["FromName"];

                SmtpClient smtpClient = new SmtpClient(host, port)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromName, password),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true,
                    Timeout = 100000
                };

                MailMessage mail = new MailMessage
                {
                    Body = content,
                    Subject = subject,
                    From = new MailAddress(fromEmail, fromName)
                };
                mail.To.Add(new MailAddress(_to));
                mail.BodyEncoding = Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                smtpClient.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                var ans = ex.ToString();
                return false;
            }
        }
    }
}