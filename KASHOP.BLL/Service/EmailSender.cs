using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service
{
    public class EmailSender : IEmailSender
    {
        //public Task SendEmailAsync(string email, string subject, string message)
        //{
        //    //بدي احط البروفايدر تبع الايميل الي بدي ابعت من خلاله من السيرفر الي عنا بس حاليا ما عنا 
        //    var client = new SmtpClient(" smtp.gmail.com", 587)
        //    {
        //        EnableSsl = true,
        //        UseDefaultCredentials = false,
        //        // ما بصير نحط الباسورد تبع الايميل مش  اشي منيح فجوجل عاملة انه نعمل باسور للتكبيق نستخدمه
        //        //اذا غيرت الباسورد تبع اميلي الكرييت باسورد الي عملتهم لاي تطبيق بنحذفو
        //        Credentials = new NetworkCredential("foqhat835@gmail.com", "xias mpgo nnuy fjny\r\n")
        //    };

        //    return client.SendMailAsync(
        //        new MailMessage(from:email,
        //                        to: "foqhat835@gmail.com",
        //                        subject,
        //                        message
        //                        //المسج بالعادة HTML

        //)
        //        { IsBodyHtml=true}
        //        //عشان يقبل html
        //        );
        //}
        //}
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("foqhat835@gmail.com", "xias mpgo nnuy fjny")
            };

            var mailMessage = new MailMessage(
                from: "foqhat835@gmail.com",
                to: email,
                subject,
                message
            )
            {
                IsBodyHtml = true
            };

            return client.SendMailAsync(mailMessage);
        }
    }
}
