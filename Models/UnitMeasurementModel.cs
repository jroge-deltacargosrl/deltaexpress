using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaXpress.Models
{
    public class UnitMeasurementModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string abbreviation { get; set; }
        public int id_unitMeasurementType { get; set; }
    }
}
