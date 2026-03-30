using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Dto.Request
{
    public class ProductTranslationsRequest
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public string Language { get; set; }
    }
}