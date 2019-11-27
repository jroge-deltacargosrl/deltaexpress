using DeltaXpress.Models.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaXpress.Models
{

    // Clase futura a despreciar
    public class ContactModel
    {
        public string company { get; set; }
        public string fullname { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public Load_Type serviceType { get; set; }
        public TransporteRouteModel transportRoute { get; set; }

        public ContactModel():
            this(string.Empty,string.Empty,string.Empty,string.Empty,default,default){ }            

        public ContactModel(string company,string fullname,string phone,string email,Load_Type serviceType,TransporteRouteModel transportRoute)
        {
            this.company = company;
            this.fullname = fullname;
            this.phone = phone;
            this.email = email;
            this.serviceType = serviceType;
            this.transportRoute = transportRoute;
        }

       

        

    }
}
