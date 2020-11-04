using BooksLibraryBL;
using BooksLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibraryApi.Controllers
{
    public class UsersController : ApiController
    {

        [HttpGet]
        public List<User> GetAllUser()
        {
            Console.Write("aaa");
            return UserLogic.GetAllUser();
        }
        [HttpGet]
        public User GetUserBymail(string mailaddress)
        {

            return UserLogic.GetUserBymail(mailaddress);
        }
        [HttpPost]
        public User InsertUser(User u)
        {
            return UserLogic.InsertUser(u);
        }
        [HttpGet]
        public string deleteUser(int uId)
        {
            return UserLogic.deleteUser(uId);
        }
        [HttpPost]
        //public string updateUser(User u)
        //{
        //    return UserLogic.updateUser(u);
        //}


     
        //[HttpGet]
        //public bool sendEmail(User u)
        //{
        //    if (sendEmailAsync("y.yelin1@gmail.com", "יעל ילין ") == true)
        //        return true;
        //    return false;
        //}
        [HttpGet]
        public User GetUserById(int idUser)
        {
            return UserLogic.getUserDetailsById(idUser);
        }

        //[HttpGet]
        //public bool sendEmail(User u)
        //{
        //    string message = "<br/><br/>מחכים לכם הרבה ספרים מענינים אצלנו .... מוזמנים !!!<br/><br/>";
        //    if (sendEmailFunc.sendEmailAsync("y.yelin1@gmail.com", "יעל ילין ", message,) == true)
        //        return true;
        //    return false;
        //}
        //public void sendEmailAsync(string ToEmail, string UserName, string nPassword, string link)/*string UniqeId(int id, string code)*/
        //public bool sendEmailAsync(string ToEmail, string UserName)/*string UniqeId(int id, string code)*/

        //{
        //    try
        //    {

        //        MailMessage mail = new MailMessage();
        //        mail.From = new MailAddress("bookAbook@gmail.com", "book A book");
        //        mail.To.Add(ToEmail);
        //        StringBuilder sbEmailbody = new StringBuilder();
        //        sbEmailbody.Append("שלום " + UserName + ",");
        //        sbEmailbody.Append("<br/><br/>");
        //        sbEmailbody.Append("נרשמת בהצלחה לאתר book A book");
        //        sbEmailbody.Append("<br/><br/>");
        //        sbEmailbody.Append("מחכים לכם הרבה ספרים מענינים אצלנו .... מוזמנים !!!");
        //        sbEmailbody.Append("<br/><br/>");
        //        sbEmailbody.Append("זוהי הודעה אוטומטית מבית book A book אין להשיב להודעה זאת ");
        //        sbEmailbody.Append("<br/><br/>");
        //        sbEmailbody.Append("לכניסה לאתר :");
        //        sbEmailbody.Append("<br/><br/>");
        //        sbEmailbody.Append("www.bookAbook.co.il");
        //        //sbEmailbody.Append(" : הסיסמה החדשה היא " + nPassword);
        //        sbEmailbody.Append("<br/>");
        //        //sbEmailbody.Append(" על מנת לאתחל את הסיסמה החדשה <a href=\"" + link + "\">לחץ כאן</a>");
        //        sbEmailbody.Append("<br/><br/>");
        //        sbEmailbody.Append("<b>book A book<b>");
        //        mail.IsBodyHtml = true;
        //        mail.Body = sbEmailbody.ToString();
        //        mail.Subject = "אישור הרשמה";
        //        SmtpClient smtpClient = new SmtpClient
        //        {
        //            Host = "smtp.gmail.com",
        //            Port = 587,
        //            EnableSsl = true,
        //            DeliveryMethod = SmtpDeliveryMethod.Network,
        //            Credentials = new NetworkCredential("yael7124499@gmail.com", "yael2604")
        //        };
        //        smtpClient.Send(mail);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        [HttpPost]
        public string EditUser(User u)
        {
            return UserLogic.EditUser(u);
        }
        [HttpPost]
        public int Login(login loginModel)
        {
            return UserLogic.Login(loginModel);

        }

     
    }
}
