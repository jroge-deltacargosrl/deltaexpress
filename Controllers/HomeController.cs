using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DeltaXpress.Models;
using DeltaXpress.Models.Interfaz;
using static DeltaXpress.Utils.UtilsApp;
using DeltaXpress.Models.Infraestructure;
using DeltaXpress.Models.ViewModels;


namespace DeltaXpress.Controllers
{
    public class HomeController : Controller
    {
        // Refactorizar esto con el patron de diseño: Unit of Work
        private readonly IRepositoryApi<TruckTypeModel> repositoryTruckType;
        private readonly IRepositoryApi<ServiceTypeModel> repositoryServiceType;
        private readonly IRepositoryApi<UnitMeasurementModel> repositoryUnitMeasurement;

        public HomeController(IRepositoryApi<TruckTypeModel> repositoryTruckType, IRepositoryApi<ServiceTypeModel> repositoryServiceType, IRepositoryApi<UnitMeasurementModel> repositoryUnitMeasurement)
        {
            this.repositoryTruckType = repositoryTruckType;
            this.repositoryServiceType = repositoryServiceType;
            this.repositoryUnitMeasurement = repositoryUnitMeasurement;

            // inicializar el entorno de la aplicacion de la solicitud API al TMS Services
            initVarEnviroment(DevelopmentEnvironment.AzureServer);
        }

        public IActionResult Index()
        {
            // obtener los tipos de camiones (registro de choferes)
            repositoryTruckType.setResource("truck/");
            var registerCarrierVM = new RegisterCarrierViewModel
            {
                trucksTypes = repositoryTruckType.getAll()
            };
            ViewData["truckTypes"] = registerCarrierVM;

            // obtener los servicios para Delta X (solicitud de cotizaciones)
            repositoryServiceType.setResource("service/delta_x");
            repositoryUnitMeasurement.setResource("unitMeasurement/typeName/storage_capacity");
            var requestQuotationVM = new RequestQuotationViewModel
            {
                servicesTypes = repositoryServiceType.getAll(),
                umsStorageCapacity = repositoryUnitMeasurement.getAll(),
            };
            repositoryUnitMeasurement.setResource("unitMeasurement/typeName/storage_time");
            requestQuotationVM.umsStorageTime = repositoryUnitMeasurement.getAll();

            // guardar en los datos temporales 
            ViewData["registerCarrier"] = registerCarrierVM;
            ViewData["requestQuotation"] = requestQuotationVM;

            //ViewData["serviceTypes"] = repositoryServiceType.getAll();

            // obtener las unidades de medida para capacidad de almacenamiento (solicitud de cotizaciones)           

            //ViewData["unitMeasurementStorageCapacity"] = repositoryUnitMeasurement.getAll();

            // obtener las unidades de medida para tiempo de almacenamiento (solicitud de cotizaciones)
            
            //ViewData["unitMeasurementStorageTime"] = repositoryUnitMeasurement.getAll();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // metodo de prueba
        [HttpPost]
        public string dataTest([FromBody]testObject data)
        {
            return $"hola {data.nombre}";
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public class testObject
        {
            public int id { get; set; }
            public string nombre { get; set; }
        }

    }
}
