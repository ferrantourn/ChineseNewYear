﻿using System;

namespace Entidades
{
    public class Pago
    {
        private int _idRecibo;
        public int IDRECIBO { get; set; }

        private Empleado _empleado;
        public Empleado EMPLEADO { get; set; }

        private Prestamo _prestamo;
        public Prestamo Prestamo { get; set; }

        private decimal _monto;
        public decimal MONTO { get; set; }

        private DateTime _fechaPago;
        public DateTime FECHAPAGO { get; set; }

        private int _numeroCuota;
        public int NUMEROCUOTA { get; set; }


    }
}
