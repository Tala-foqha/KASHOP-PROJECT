using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Dto.Request
{
    public class ResetPasswordRequest
    {
        public String Code { get; set; }
        public String NewPassword { get; set; }
        public string Email { get; set; }
    }
}
