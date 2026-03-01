using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Models
{
    public class Category
    {
        public int Id { get; set; }
        // نسميها ترانزليشن نفس اسم المودل تبعها عشان يضل فاهم علينا
        public List<CategoryTranslation> translations { get; set; }
    }
}
