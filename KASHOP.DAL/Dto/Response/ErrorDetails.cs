using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Dto.Response
{
    public class ErrorDetails
    {
        public int StatusCode {  get; set; }
        public string Message { get; set; }
    }
}
