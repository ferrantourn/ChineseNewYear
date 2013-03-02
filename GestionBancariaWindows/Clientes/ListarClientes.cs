using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Logica;
using Entidades;
namespace GestionBancariaWindows
{
    public partial class ListarClientes : Form
    {
        public ListarClientes()
        {
            InitializeComponent();
        }

        private void ListarClientes_Load(object sender, EventArgs e)
        {
            try
            {
                LogicaUsuarios lu = new LogicaUsuarios();
                List<Cliente> clientes = lu.ListarClientes();

                foreach (Cliente c in clientes)
                {
                    object[] row0 = {"Editar", c.CI, c.NOMBRE + " " +  c.APELLIDO, c.NOMBREUSUARIO, Convert.ToString(c.ACTIVO).Trim() == "True" ? "Activo" : "No Activo"
                                                };
                    lvClientes.Rows.Add(row0);
                }

            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }

        private void lvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0 && e.RowIndex >= 0)
                {
                    int ci;
                    if (Int32.TryParse(Convert.ToString(lvClientes.Rows[e.RowIndex].Cells[1].Value), out ci))
                    {
                        // Cliente a = new Cliente{ CI = ci };
                        //LogicaUsuarios lu = new LogicaUsuarios();
                        //lu.BuscarUsuarioPorCi(a);
                        //CARGAMOS LA INFORMACION DEL CLIENTE CON ESE ID
                        //-----------------------------------------------
                        LogicaUsuarios lu = new LogicaUsuarios();

                        Cliente c = new Cliente {CI = ci};

                        c = (Cliente)lu.BuscarUsuarioPorCi(c);
                        NuevoCliente nu = new NuevoCliente { CLIENTE = c };

                        nu.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }



        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }


    }
}
