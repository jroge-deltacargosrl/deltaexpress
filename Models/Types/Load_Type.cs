using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaXpress.Models.Types
{
    public enum Load_Type
    {
        [Description("Transporte Nacional")]
        TRANSPORTE_NACIONAL=1,

        [Description("Almacenamiento de Carga")]
        ALMACENAMIENTO_CARGA = 2


    }
}
