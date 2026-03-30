using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Models
{
    public  class AuditEntity
    {
        public string CreateById { get; set; }
        public string? UpdateById { get; set; }
        public ApplicationUser UpdateBy { get; set; }
        // الوقت الي انضاف فيه المنتج والوقت الي تم التعديل عليه
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public ApplicationUser CreateBy { get; set; }

    }
}
