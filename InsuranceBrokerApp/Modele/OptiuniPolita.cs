using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele
{
    [Flags]
    public enum OptiuniPolita
    {
        None = 0,
        Urgenta = 1,
        Suport24_7 = 2,
        AsistentaRutiera = 4
    }
}
