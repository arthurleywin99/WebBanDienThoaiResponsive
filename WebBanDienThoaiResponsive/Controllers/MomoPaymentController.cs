using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using WebBanDienThoaiResponsive.Payment.Momo;
using WebBanDienThoaiResponsive.ViewModels;

namespace WebBanDienThoaiResponsive.Controllers
{
    public class MomoPaymentController : Controller
    {
        // GET: MomoPayment
        public ActionResult MomoPay(string orderInfo, string returnUrl, string notifyUrl, string amount, string orderId, string requestId)
        {
            string endpoint = "https://test-payment.momo.vn/v2/gateway/api/create";
            string partnerCode = Utility.partnerCode;
            string accessKey = Utility.accessKey;
            string serectkey = Utility.secretKey;

            string extraData = "";

            string rawHash = "partnerCode" +
                partnerCode + "&accessKey=" +
                accessKey + "&requestId=" +
                requestId + "&amount=" +
                amount + "&orderId=" +
                orderId + "&orderInfo=" +
                orderInfo + "&returnUrl=" +
                returnUrl + "&notifyUrl=" +
                notifyUrl + "&extraData=" + extraData;

            MoMoSecurity cryto = new MoMoSecurity();
            string signature = cryto.signSHA256(rawHash, serectkey);
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "accessKey", accessKey },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderId },
                { "orderInfo", orderInfo },
                { "returnUrl", returnUrl },
                { "notifyUrl", notifyUrl },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }
            };
            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());
            JObject jmessage = JObject.Parse(responseFromMomo);
            return Redirect(jmessage.GetValue("payUrl").ToString());
        }

        public ActionResult ReturnUrl()
        {
            string param = Request.QueryString.ToString().Substring(0, Request.QueryString.ToString().IndexOf("signature") - 1);
            param = Server.UrlDecode(param);
            MoMoSecurity cryto = new MoMoSecurity();
            string secretKey = Utility.secretKey;
            string signature = cryto.signSHA256(param, secretKey);
            if (signature != Request["signature"].ToString())
            {
                ViewBag.message = "Thông tin người dùng không hợp lệ";
                return View();
            }
            if (!Request.QueryString["errorCode"].Equals("0"))
            {
                ViewBag.message = "Thanh toán thất bại";
            }
            else
            {
                ViewBag.message = "Thanh toán thành công";
                Session["Cart"] = null;
            }
            return View();
        }

        public JsonResult NotifyUrl() 
        {
            string param = ""; //Request.QueryString.ToString().Substring(0, Request.QueryString.ToString().IndexOf("signature") - 1);
            param = "partner_code" + Request["partner_code"] +
                "&access_key=" + Request["access_key"] +
                "&amount=" + Request["amount"] +
                "&order_id=" + Request["order_id"] +
                "&order_info=" + Request["order_info"] +
                "&order_type=" + Request["order_type"] +
                "&transaction_id=" + Request["transaction_id"] +
                "&message=" + Request["message"] +
                "&response_time=" + Request["response_time"] +
                "&status_code=" + Request["status_code"];

            param = Server.UrlDecode(param);
            MoMoSecurity crypto = new MoMoSecurity();
            string secretKey = Utility.secretKey;
            string signature = crypto.signSHA256(param, secretKey);
            if (signature != Request["signature"].ToString())
            {

            }
            string status_code = Request["status_code"].ToString();
            if (status_code != "0")
            {

            }
            else
            {

            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}