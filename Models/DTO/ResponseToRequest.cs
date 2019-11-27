using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaXpress.Models.DTO
{
    public class ResponseToRequest<T> where T: class
    {
        public int code { get; set; }
        public T modelResponse { get; set; }
        public string message { get; set; }
        public DateTime dateResponse { get; set; }

    }
}
