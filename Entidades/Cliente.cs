using System.Collections.Generic;

namespace Entidades
{
    public class Cliente :Usuario
    {
        private int _idCliente;
        public int IDCLIENTE { get; set; }

        private string _direccion;
        public string DIRECCION { get; set; }

        private List<int> _telefonos;
        public List<int> TELEFONOS { get; set; }

    }
}
