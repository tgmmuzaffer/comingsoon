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
        [HttpPost()]
        public IActionResult Mailsender(Mail mail)
        {
            //mail.message=> gelen mesajı paneldeki maile gönder
            //mail.emailAddress=> 
            MimeMessage m = new MimeMessage();
            BodyBuilder bodyBuilder = new BodyBuilder();
            SmtpClient smtp = new SmtpClient();
            bodyBuilder.HtmlBody = "body";
            MailboxAddress from = new MailboxAddress("şirket ismi", "şirketmail adresi");
            MailboxAddress to = new MailboxAddress("", mail.emailAddress);
            m.From.Add(from);
            m.Body = bodyBuilder.ToMessageBody();
            m.Subject = "Talebinizi aldık.En kısa sürede dönüş yapılacaktır.";
            m.To.Add(to);
            smtp.Connect("smtp.gmail.com", 465, true);
            smtp.Authenticate("tgm.muzaffer.deveci@gmail.com", "Yamahar123+-*/");

            smtp.AuthenticationMechanisms.Remove("XOAUTH2");

            smtp.Send(m);
            smtp.Disconnect(true);
            smtp.Dispose();

            return Json(1);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
