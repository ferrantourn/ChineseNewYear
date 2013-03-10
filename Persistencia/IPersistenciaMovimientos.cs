using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace Persistencia
{
    interface IPersistenciaMovimientos
    {
        void RealizarMovimiento(Movimiento m);
    }
}
