using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SunIQ.Models
{
    public class InverterandRange
    {
        public Calculation_Setup calc { get; set; }
        public IEnumerable<SunIQ.Models.Inverter> inv { get; set; }
    }
}