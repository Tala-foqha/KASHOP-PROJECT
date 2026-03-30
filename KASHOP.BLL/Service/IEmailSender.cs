using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service
{
    public interface IEmailSender
    {
        public Task SendEmailAsync(String email, String subject, String message);
    }
}
