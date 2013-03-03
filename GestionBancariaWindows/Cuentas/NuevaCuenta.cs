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

        public Cuenta CUENTA{ get; set; }

        private void NuevaCuenta_Load(object sender, EventArgs e)
        {
            try
            {

                if (CUENTA != null)
                {
                    txtApellido.Text = CLIENTE.APELLIDO;
                    txtCedula.Text = Convert.ToString(CLIENTE.CI);
                    txtDireccion.Text = CLIENTE.DIRECCION;
                    txtNombre.Text = CLIENTE.NOMBRE;
                    txtPassword.Text = CLIENTE.PASS;
                    txtReiterarPass.Text = CLIENTE.PASS;
                    txtUsuario.Text = CLIENTE.NOMBREUSUARIO;

                    string telefonos = "";
                    if (CLIENTE.TELEFONOS != null)
                    {
                        foreach (string s in CLIENTE.TELEFONOS)
                        {
                            telefonos = telefonos + s + ",";
                        }
                    }
                    txtTelefonos.Text = telefonos;

                    btnGuardar.Text = "Actualizar";

                }
                else
                {
                    btnEliminar.Visible = false;
                }
            }
            catch (Exception ex)
            {

                lblInfo.Text = ex.Message;
            }
        }
    }
}
