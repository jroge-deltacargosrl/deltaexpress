using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaXpress.Models
{
    public class QuotationModel
    {

        #region "Fields Database DeltaDB"
        public int id { get; set; }
        // cotizacion relacionado con datos de un cliente (nuevo/antiguo)
        //public ContactDTO contact { get; set; }
        public int idContact { get; set; }
        //public ServiceTypeDTO serviceType { get; set;}
        public int idServiceType { get; set; }

        // st : transporte (internacional/nacional)
        //public RouteDTO macroRouteOrigin { get; set; }
        public int? idMacroRouteOrigin { get; set; }

        public string routeCityOrigin { get; set; }  // no habilitadas para st: transporte nacional

        //public RouteDTO macroRouteDestination { get; set; }
        public int? idMacroRouteDestination { get; set; }

        public string routeCityDestination { get; set; } // no habilitada para st: transporte nacional

        //public TruckTypeDTO truckType { get; set; } // no habilitadas para clientes no registradas
        public int? idTruckType { get; set; }

        // para todos st (Excepto el almacenamiento de carga)
        public decimal? loadWeight { get; set; }

        //public UnitMeasurementDTO umLoadWeight { get; set; }
        public int? idUmLoadWeight { get; set; }

        public decimal? loadVolume { get; set; }
        //public UnitMeasurementDTO umLoadVolume { get; set; }
        public int? idUmLoadVolume { get; set; }
        public bool? imo { get; set; } // solo para cotizaciones de clientes antiguos

        // st: almacen de carga
        public decimal? storageCapacity { get; set; }
        //public UnitMeasurementDTO umStorageCapacity { get; set; }
        public int? idUmStorageCapacity { get; set; }

        public decimal? storageTime { get; set; }
        //public UnitMeasurementDTO umStorageTime { get; set; }
        public int? idUmStorageTime { get; set; }

        // para todos los tipos de servicio
        public string comment { get; set; }

        // valor por defecto: Afiliacion -> DeltaX
        public int id_membership { get; set; } = 2;
        #endregion
    }
}
