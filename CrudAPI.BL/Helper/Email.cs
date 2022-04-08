using Crud.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace CrudAPI.BL.Helper
{
    public static class Email
    {
        public static string SendEmail(string Email, string Subject, string Body)
        {
            try
            {
                var MyGmail = "abderhmansaid58@gmail.com";
                var Smtp = new SmtpClient("smtp.gmail.com", 587);
                Smtp.EnableSsl = true;
                Smtp.Credentials = new NetworkCredential(MyGmail, "22255577");
                Smtp.Send(MyGmail, Email, Subject, Body);
                return "Succeed";
            }
            catch (Exception ex)
            {
                return (ex.Message); ;
            }

        }
    }
}
