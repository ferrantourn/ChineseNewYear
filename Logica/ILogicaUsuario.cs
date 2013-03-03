using System.Collections.Generic;
using Entidades;

namespace Logica
{
    public interface ILogicaUsuario
    {
        Cliente getCliente(Cliente c);
        Cliente getCliente(int ci);
        Empleado getEmpleado(Empleado e);
        void NuevoCliente(Cliente c);
        void NuevoEmpleado(Empleado e);
        Usuario getLoginUsuario(string userName, string pass);
        List<Cliente> ListarAlumnosSinMovimientos(int NumeroDias);
        List<Empleado> ListarEmpleados();
        List<Cliente> ListarClientes();
        /// <summary>
        /// ACTUALIZA EL STATUS DE ACTIVO/NOACTIVO DE UN CLIENTE
        /// </summary>
        /// <param name="c"></param>
        /// <param name="SetActiveStatus"></param>
        void ActualizarStatusCliente(Cliente c, bool setActiveStatus);


        /// <summary>
        /// ACTUALIZA LA INFORMACION DE UN CLIENTE
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        void ModificarCliente(Cliente c);

        /// <summary>
        /// ACTUALIZA LA INFORMACION DE UN EMPLEADO
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        void ModificarEmpleado(Empleado e);
    }

}
