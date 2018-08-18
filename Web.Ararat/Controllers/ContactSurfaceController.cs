using System;
using System.Configuration;
using System.Web.Mvc;
using MailKit.Net.Smtp;
using MimeKit;
using Umbraco.Web.Mvc;
using Web.Ararat.Model;

namespace Web.Ararat.Controllers
{
    public class ContactSurfaceController : SurfaceController
    {
        public ActionResult Send(ContactModel contact)
        {
            var url = System.Web.HttpContext.Current.Request.UrlReferrer;

            try
            {
                if (ModelState.IsValid)
                {
                    TempData["ContactResult"] = true;

                    var userName = ConfigurationManager.AppSettings["MailUserName"];
                    var password = ConfigurationManager.AppSettings["MailPassword"];
                    var host = ConfigurationManager.AppSettings["MailHost"];

                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("ararat-restaurant.cz", userName));
                    message.To.Add(new MailboxAddress(contact.SendTo, contact.SendTo));
                    message.Subject = contact.Title;

                    message.Body = new TextPart("plain")
                    {
                        Text = string.Format("{0}\n{1}", contact.EmailFrom, contact.Message)
                    };

                    using (var client = new SmtpClient())
                    {
                        client.Connect(host, 465, true);

                        // Note: since we don't have an OAuth2 token, disable
                        // the XOAUTH2 authentication mechanism.
                        client.AuthenticationMechanisms.Remove("XOAUTH2");

                        // Note: only needed if the SMTP server requires authentication

                        //T1ck3tB0tS@zk@
                        //ticketBot@wp.pl
                        client.Authenticate(userName, password);

                        client.Send(message);
                        client.Disconnect(true);
                    }

                    TempData["ContactResult"] = true;
                }
                else
                {
                    TempData["ContactResult"] = false;
                }
            }
            catch
            {
                TempData["ContactResult"] = false;
            }

            return new RedirectResult(url.AbsoluteUri);
        }
    }
}