using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Supermercado
{
    [Flags]
    public enum EnumProductType
    {
        [Description("Congelados")]
        Congelados = 1,

        [Description("Prateleira")]
        Prateleira = 2,

        [Description("Enlatados")]
        Enlatados = 3,
    }
}
