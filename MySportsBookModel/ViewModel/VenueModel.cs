using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySportsBookModel.ViewModel
{
    public class VenueModel : AuditModel
    {
        public int VenueId { get; set; }
        public string VenueCode { get; set; }
        public string VenueName { get; set; }
    }
}
