using DeltaXpress.Models.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaXpress.Models
{
    public class ContactModel
    {
        public string company { get; set; } = string.Empty;
        public string fullname { get; set; }
        public string phone { get; set; }
        public string email { get; set; } = string.Empty;
        public Load_Type serviceType { get; set; }
        public TransporteRouteModel transportRoute { get; set; }

        

    }
}
