using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace Logica
{
    interface ILogicaUsuarios
    {
        public void AltaUsuario(Usuario u);
        public void ActualizarUsuario(Usuario u);
        public void EliminarUsuario(Usuario u);
        public List<Cliente> ListarClientes();
        public List<Empleado> ListarEmpleados();
        public Usuario BuscarUsuarioPorCi(Usuario u);
        public Usuario getLoginUsuario(string NombreUsuario, string Pass);
    }
}
