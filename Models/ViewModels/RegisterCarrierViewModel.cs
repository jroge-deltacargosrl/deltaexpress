using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaXpress.Models.ViewModels
{
    public class RegisterCarrierViewModel
    {
        public IEnumerable<TruckTypeModel> trucksTypes { get; set; }
    }
}
