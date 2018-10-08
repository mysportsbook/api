using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySportsBookModel.ViewModel
{
    public class EnquiryModel : VenueModel
    {
        public int EnquiryId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Game { get; set; }
        public string Slot { get; set; }
        public string Comment { get; set; }
        public List<string> Comments { get; set; }
    }
}
