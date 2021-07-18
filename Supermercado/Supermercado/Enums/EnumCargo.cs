using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Supermercado
{
    [Flags]
    public enum EnumCargo
    {
        [Description("Gerente")]
        Gerente = 1,

        [Description("Repositor")]
        Repositor = 2,

        [Description("Caixa")]
        Caixa = 3,
    }
}
