using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaXpress.Models.DTO
{
    public class ContactResponse
    {
        public int code { get; set; }
        public string message { get; set; }
        public DateTime dateResponse { get; set; }
    }
}
