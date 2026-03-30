using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Dto.Request
{
    public class RegisterRequest
    {
        public String   UserName { get; set; }
        public String Email { get; set; }
            public String Password { get; set; }
        public String PhoneNumber { get; set; }
        public String FullName { get; set; }
    }
}
