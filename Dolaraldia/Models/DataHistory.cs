using System;
using System.Collections.Generic;
using System.Text;

namespace Dolaraldia.Models
{
    public class DataHistory
    {
        public string DateGroup { get; set; }
        public List<DataPrice> PriceData { get; set; }
        public bool Status { get; set; }
    }
}
