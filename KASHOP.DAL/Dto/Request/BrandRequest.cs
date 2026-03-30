using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Dto.Request
{
    public class BrandRequest
    {
        public IFormFile Logo { get; set; }
        //public int ProductId { get; set; }
        public List<BrandTranslationRquest> Translations { get; set; }
    }
}
