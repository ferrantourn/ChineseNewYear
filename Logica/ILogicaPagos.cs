using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace Logica
{
    interface ILogicaPagos
    {
        public void PagarCuota(Prestamo p);
        public List<Pago> ListarPagos(Prestamo p);
        public void EliminarPago(Pago p);
        public Pago BuscarPago(Pago p);


    }
}
