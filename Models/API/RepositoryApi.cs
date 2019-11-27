using DeltaXpress.Models.Infraestructure;
using DeltaXpress.Models.Interfaz;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static DeltaXpress.Utils.UtilsApp;

namespace DeltaXpress.Models.API
{
    public class RepositoryApi<T> : IRepositoryApi<T>
    {
        private readonly RequestAPI requestApi;
        private string resource;

        public RepositoryApi(RequestAPI requestApi)
        {
            this.requestApi = requestApi;
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

        public IEnumerable<T> getAll(Expression<Func<T, bool>> filter = null)
        {
            
            var result = JsonConvert.DeserializeObject<List<T>>(
                requestApi.addClient(URL_BASE)
               .addRequest(new RestRequest(resource, Method.GET, DataFormat.Json))
               .addHeader(new KeyValuePair<string, object>("Accept", "application/json"))
               .buildRequest()
               ).AsQueryable();

            if (!isNull(filter))
            {
                result = result.Where(filter);
            }
            return result;
            
        }

        public void setResource(string resource)
        {
            this.resource = resource;
        }

        public T update(int id, T value)
        {
            throw new NotImplementedException();
        }

        T IRepositoryApi<T>.delete(int id)
        {
            throw new NotImplementedException();
        }

        T IRepositoryApi<T>.get(int? id)
        {
            throw new NotImplementedException();
        }

        /*IEnumerable<T> IRepositoryApi<T>.getAll()
        {
            return JsonConvert.DeserializeObject<List<T>>(
                requestApi.addClient(URL_BASE)
                .addRequest(new RestRequest(resource, Method.GET, DataFormat.Json))
                .addHeader(new KeyValuePair<string, object>("Accept", "application/json"))
                .buildRequest()
             );
        }*/
    }
}
