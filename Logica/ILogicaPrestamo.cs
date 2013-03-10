using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace Logica
{
    interface ILogicaPrestamo
    {
        public decimal CalcularMontoCuotaPrestamo(Prestamo p);
        public List<Prestamo> ListarPrestamosAtrasados(Sucursal s);
        public List<Prestamo> ListarPrestamo();
        public void AltaPrestamo(Prestamo s);
        public void CancelarPrestamo(Prestamo s);
        public Prestamo BuscarPrestamo(Prestamo s);
        public void ActualizarPrestamo(Prestamo c);
        public List<Pago> IsPrestamoCancelado(ref Prestamo p);
    }
}
