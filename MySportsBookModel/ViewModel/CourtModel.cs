using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySportsBookModel.ViewModel
{
    public class CourtModel : SportModel
    {
        public int CourtId { get; set; }
        public string CourtCode { get; set; }
        public string CourtName { get; set; }
    }
}
