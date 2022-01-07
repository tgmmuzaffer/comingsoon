using comingsoon.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace comingsoon.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Mailsender(Mail mail)

        {
            try
            {
                MimeMessage m = new MimeMessage();
                BodyBuilder bodyBuilder = new BodyBuilder();
                SmtpClient smtp = new SmtpClient();
                bodyBuilder.HtmlBody = "Talebinizi aldık.En kısa sürede dönüş yapılacaktır.";
                MailboxAddress from = new MailboxAddress("Camaşırhane Ekipmanları", "info@camasirhaneekipmanlari.com");
                MailboxAddress to = new MailboxAddress(mail.emailAddress, mail.emailAddress);
                m.From.Add(from);
                m.Body = bodyBuilder.ToMessageBody();
                m.Subject = "Bilgilendirme";
                m.To.Add(to);
                smtp.Connect("win5.wlsrv.com", 465, true);
                smtp.Authenticate("ugur.yalcin@camasirhaneekipmanlari.com", "$q1q9T3u");
                smtp.Send(m);
                smtp.Disconnect(true);
                smtp.Dispose();


                MimeMessage mG = new MimeMessage();
                BodyBuilder bodyBuilderG = new BodyBuilder();
                SmtpClient smtpG = new SmtpClient();
                bodyBuilderG.HtmlBody = string.Format(@"<p>Kimden: <a href='mailto: {0}'>{1}</a></p><br><p>Mesaj İçeriği: {2}</p>", mail.emailAddress,mail.emailAddress, mail.message);
                MailboxAddress fromG = new MailboxAddress("ComingSoon Site", "info@camasirhaneekipmanlari.com");
                MailboxAddress toG = new MailboxAddress(mail.emailAddress, "ugur.yalcin@camasirhaneekipmanlari.com");
                mG.From.Add(fromG);
                mG.Body = bodyBuilderG.ToMessageBody();
                mG.Subject = "Bilgilendirme";
                mG.To.Add(toG);
                smtpG.Connect("win5.wlsrv.com", 465, true);
                smtpG.Authenticate("info@camasirhaneekipmanlari.com", "Yamahar123+-*/");
                smtpG.Send(mG);
                smtpG.Disconnect(true);
                smtpG.Dispose();

                TempData["success"] = "Mesajınız gönderilmiştir.";

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["fail"] = "Mesajınız gönderilemedi.";
                return RedirectToAction("Index");
            }

        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
