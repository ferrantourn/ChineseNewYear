using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Entidades;
using ExcepcionesPersonalizadas;
using Logica;

namespace GestionBancariaWindows
{
    public partial class NuevaCuenta : Form
    {
        public NuevaCuenta()
        {
            InitializeComponent();
        }

        public Cuenta CUENTA { get; set; }

        private void NuevaCuenta_Load(object sender, EventArgs e)
        {
            try
            {
                //CARGAMOS LA INFORMACION DE CLEINTES EXISTENTES
                //---------------------------------------------
                LogicaUsuarios lc = new LogicaUsuarios();
                List<Cliente> clientes = lc.ListarClientes();

                foreach (Cliente c in clientes)
                {
                    ClientesbindingSource.Add(c);
                }

                if (CUENTA != null)
                {
                    txtNumeroCuenta.Text = Convert.ToString(CUENTA.IDCUENTA);
                    txtSaldo.Text = Convert.ToString(CUENTA.SALDO);
                    cmbClientes.SelectedValue = CUENTA.CLIENTE.CI;
                    cmbMoneda.SelectedValue = CUENTA.MONEDA;

                    btnGuardar.Text = "Actualizar";
                }
                else
                {
                    btnEliminar.Visible = false;
                    txtSaldo.Text = Convert.ToString(Decimal.Zero);//LAS CUENTAS SE ABREN CON SALDO 0
                    txtSaldo.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {

                lblInfo.Text = ex.Message;
            }
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                LogicaCuentas lc = new LogicaCuentas();
                lc.EliminarCuenta(CUENTA);
            }
            catch (Exception ex)
            {

                lblInfo.Text = ex.Message;
            }
        }



        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ValidateChildren(ValidationConstraints.Enabled))
                {
                    bool editar = false;
                    if (CUENTA == null)
                    {
                        CUENTA = new Cuenta();
                        CUENTA.CLIENTE = new Cliente();
                        CUENTA.SUCURSAL = new Sucursal { IDSUCURSAL = 1 };//** AQUI SE DEBE COLOCAR LA SUCURSAL DONDE TRABAJA EL EMPLEADO
                    }
                    else
                    {
                        editar = true;
                    }

                    //CARGAMOS INFORMACION DE CUENTA
                    //------------------------------
                    CUENTA.MONEDA = Convert.ToString(cmbMoneda.SelectedValue);
                    CUENTA.SALDO = Convert.ToDecimal(txtSaldo.Text);
                    CUENTA.CLIENTE.CI = Convert.ToInt32(cmbClientes.SelectedValue);
                  

                    //GUARDAMOS LA INFORMACION EN LA BASE DE DATOS
                    //---------------------------------------------
                    LogicaCuentas lc = new LogicaCuentas();
                    if (editar)
                    {
                        lc.ActualizarCuenta(CUENTA);
                        lblInfo.Text = "Cuenta actualizada correctamente";
                    }
                    else
                    {
                        lc.AltaCuenta(CUENTA);
                        lblInfo.Text = "Cuenta ingresada correctamente";

                        //LIMPIAMOS EL FORMULARIO
                        LimpiarFormulario();
                    }


                }
            }
            catch (ErrorUsuarioYaExiste uex)
            {
                lblInfo.Text = uex.Message;
            }
            catch (Exception ex)
            {

                lblInfo.Text = ex.Message;
            }
        }


        private void LimpiarFormulario()
        {
            try
            {
                txtNumeroCuenta.Text = "";
                txtSaldo.Text = "";
                cmbClientes.SelectedValue = "";
                cmbMoneda.SelectedValue = "";

            }
            catch (Exception ex)
            {

                lblInfo.Text = ex.Message;
            }
        }
    }
}
