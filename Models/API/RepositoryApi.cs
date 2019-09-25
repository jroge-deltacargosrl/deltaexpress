using DeltaXpress.Models.Infraestructure;
using DeltaXpress.Models.Interfaz;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DeltaXpress.Utils.UtilsApp;

namespace DeltaXpress.Models.API
{
    public class RepositoryApi<T>:IRepositoryCrud<T>
    {
        private readonly RequestAPI requestApi;

        private string resource;

        public RepositoryApi()
        {
            requestApi = new RequestAPI();
        }


        public T create(T value)
        {
            return JsonConvert.DeserializeObject<T>(
                requestApi.addClient(URL_BASE)
                   .addRequest(new RestRequest(resource, Method.POST, DataFormat.Json))
                   .addHeader(new KeyValuePair<string, object>("Accept", "application/json"))
                   .addBodyData(value)
                   .buildRequest()
                );
        }

        public void setResource(string resource)
        {
            this.resource = resource;
        }

        /*public TruckTypeModel create(TruckTypeModel value)
        {
            throw new NotImplementedException();
        }

        public TruckTypeModel delete(int id)
        {
            throw new NotImplementedException();
        }

        public TruckTypeModel get(int? id)
        {
            throw new NotImplementedException();
        }

        public List<TruckTypeModel> getAll()
        {
            return JsonConvert.DeserializeObject<List<TruckTypeModel>>(requestApi.addClient(URL_BASE)
                .addRequest(new RestRequest("truck/", Method.GET, DataFormat.Json))
                .addHeader(new KeyValuePair<string, object>("Accept", "application/json"))
                .buildRequest());
        }

        public TruckTypeModel update(int id, TruckTypeModel value)
        {
            throw new NotImplementedException();
        }*/

        public T update(int id, T value)
        {
            throw new NotImplementedException();
        }

        T IRepositoryCrud<T>.delete(int id)
        {
            throw new NotImplementedException();
        }

        T IRepositoryCrud<T>.get(int? id)
        {
            throw new NotImplementedException();
        }

        List<T> IRepositoryCrud<T>.getAll()
        {
            return JsonConvert.DeserializeObject<List<T>>(
                requestApi.addClient(URL_BASE)
                .addRequest(new RestRequest(resource, Method.GET, DataFormat.Json))
                .addHeader(new KeyValuePair<string, object>("Accept", "application/json"))
                .buildRequest()
             );
        }
    }
}
