using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySportsBookModel.ViewModel
{
    public class EnquiryModel
    {
        public Enquiry Enquiry { get; set; }
        public List<Enquiry_Comments> Enquiry_Comments { get; set; }
    }
}
