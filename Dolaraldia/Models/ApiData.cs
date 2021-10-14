using System;
using System.Collections.Generic;
using System.Text;

namespace Dolaraldia.Models
{
    public class ApiData
    {
        public string PriceNow { get; set; }
        public string DateNow { get; set; }
        public string HourNow { get; set; }
        public string ValuePorcentNow { get; set; }
        public string ColorTextNow { get; set; }
        public string IconViewNow { get; set; }
        public List<DataHistory> PriceDataHistory { get; set; }
    }
}
