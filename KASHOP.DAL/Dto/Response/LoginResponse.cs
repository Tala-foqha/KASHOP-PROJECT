using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Dto.Response
{
    public class LoginResponse
    {
        public String Message { get; set; }
        public bool Success { get; set; }
        public String? AccessToken { get; set; }
    }
}
