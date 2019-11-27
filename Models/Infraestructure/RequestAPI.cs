using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DeltaXpress.Utils.UtilsApp;

namespace DeltaXpress.Models.Infraestructure
{
    public class RequestAPI
    {

        public RestClient client { get; set; }
        public RestRequest request { get; set; }
        public IRestResponse response { get; set; }

        public RequestAPI() : this(null, null) { }

        public RequestAPI(RestClient client, RestRequest request)
        {
            this.client = client;
            this.request = request;
        }

        // Metodos de construccion por partes
        public RequestAPI addClient(RestClient client)
        {
            this.client = client;
            return this;
        }

        public RequestAPI addClient(string url)
        {
            this.client = new RestClient(url);
            return this;
        }


        public RequestAPI addRequest(RestRequest request)
        {
            this.request = request;
            return this;
        }

        public bool requestValid() => !isNull(client) && !isNull(request);

        // rediseñar este metodo de una forma optima para futuras iteraciones
        public RequestAPI addParameters(Dictionary<string, object> paramsRequest, ParameterType paramType)
        {
            paramsRequest
              .ToList()
              .ForEach((k) => addParameter(k, paramType));
            return this;
        }

        public RequestAPI setRequestFormat(DataFormat format)
        {
            request.RequestFormat = format;
            return this;
        }

        public RequestAPI addBodyData(object param)
        {
            request.AddJsonBody(param);
            return this;
        }

        public RequestAPI addHeader(KeyValuePair<string, object> paramHeader)
        {
            request.AddHeader(paramHeader.Key, paramHeader.Value.ToString());
            return this;
        }

        public RequestAPI addParameter(KeyValuePair<string, object> param, ParameterType paramType)
        {
            request.AddParameter(param.Key, param.Value, paramType);
            return this;
        }

        public RequestAPI addUrlSegmentParam(KeyValuePair<string, object> param)
        {
            request.AddUrlSegment(param.Key, param.Value);
            return this;
        }

        /// <summary>
        /// Metodo que ejecuta la solicitud HTTP/ HTTPS desde el lado del cliente hacia el servidor, procesa la informacion y lanza una respuesta de parte del servidor
        /// </summary>
        /// <returns></returns>
        public string buildRequest()
        {
            try
            {
                string contentOut = string.Empty;
                if (requestValid())
                {
                    response = client.Execute(request);
                    contentOut = response.Content;
                }
                return contentOut;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static T deserilizeProject<T>(string content) => JsonConvert.DeserializeObject<T>(content);

        public static string serializeObject<T>(T value) => JsonConvert.SerializeObject(value);


    }
}
