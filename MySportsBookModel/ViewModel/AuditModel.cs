﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySportsBookModel.ViewModel
{
    public class AuditModel
    {
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get { return DateTime.Now.ToLocalTime(); } }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
