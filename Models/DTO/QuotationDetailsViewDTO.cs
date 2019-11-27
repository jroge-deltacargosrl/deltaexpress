using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaXpress.Models.DTO
{
    public class QuotationDetailsViewDTO
    {
        
        public string name { get; set; }
        public string company { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string serviceType { get; set; }
        public string countryOrigin { get; set; }
        public string cityOrigin { get; set; }
        public string countryDestination { get; set; }
        public string cityDestination { get; set; }
        public string storageCapacity { get; set; }
        public string storageTime { get; set; }
        public string loadWeight { get; set; }
        public string loadVolume { get; set; }
        public string comment { get; set; }
        public string street { get; set; }
    }
}
