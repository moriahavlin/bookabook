using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibraryBL
{
    public class sendEmailFunc
    {
        public static bool sendEmailAsync(string ToEmail, string UserName, string message, string Subject)

        {
            try
            {

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("bookAbook@gmail.com", "book A book");
                mail.To.Add(ToEmail);
                StringBuilder sbEmailbody = new StringBuilder();
                sbEmailbody.Append("שלום " + UserName + ",");
                sbEmailbody.Append("<br/><br/>");
                sbEmailbody.Append(message);
                sbEmailbody.Append("<br/><br/>");
                sbEmailbody.Append("זוהי הודעה אוטומטית מבית book A book אין להשיב להודעה זאת ");
                sbEmailbody.Append("<br/><br/>");
                sbEmailbody.Append("לכניסה לאתר :");
                sbEmailbody.Append("<br/><br/>");
                sbEmailbody.Append("www.bookAbook.co.il");
                sbEmailbody.Append("<br/>");
                sbEmailbody.Append("<b>book A book<b>");
                mail.IsBodyHtml = true;
                mail.Body = sbEmailbody.ToString();
                mail.Subject = Subject;
                SmtpClient smtpClient = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential("yael7124499@gmail.com", "yael2604")
                };
                smtpClient.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
