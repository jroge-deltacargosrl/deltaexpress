﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaXpress.Models.ViewModels
{
    public class RequestQuotationViewModel
    {
        public IEnumerable<ServiceTypeModel> servicesTypes { get; set; }
        public IEnumerable<UnitMeasurementModel> umsStorageCapacity { get; set; }
        public IEnumerable<UnitMeasurementModel> umsStorageTime { get; set; }


    }
}
