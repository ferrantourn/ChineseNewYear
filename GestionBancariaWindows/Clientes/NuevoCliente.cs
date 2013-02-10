using System;
using System.Linq;
using System.Windows.Forms;
using Entidades;
using ExcepcionesPersonalizadas;
using Logica;

namespace GestionBancariaWindows
{
    public partial class NuevoCliente : Form
    {
        private int? ClienteId;
        public int? CLIENTEID { get; set; }


        public NuevoCliente()
        {
            InitializeComponent();

        }

        private void NuevoCliente_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(txtTelefonos, "Ingrese los telefonos separados por una coma ','");
            if (CLIENTEID != null)
            {
                //CARGAMOS LA INFORMACION DEL CLIENTE CON ESE ID
                //-----------------------------------------------
                LogicaUsuarios lu = new LogicaUsuarios();

                Cliente c = new Cliente();
                c.CI = Convert.ToInt32(CLIENTEID);

                c = (Cliente)lu.BuscarUsuarioPorCi(c);
                if (c != null)
                {
                    txtApellido.Text = c.APELLIDO;
                    txtCedula.Text = Convert.ToString(c.CI);
                    txtDireccion.Text = c.DIRECCION;
                    txtNombre.Text = c.NOMBRE;
                    txtPassword.Text = c.PASS;
                    txtReiterarPass.Text = c.PASS;
                    string telefonos = "";
                    foreach (string s in c.TELEFONOS)
                    {
                        telefonos = telefonos + s + ",";
                    }

                    txtUsuario.Text = c.NOMBREUSUARIO;

                }
            }
        }



        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ValidateChildren(ValidationConstraints.Enabled))
                {
                    if (CLIENTEID == null)
                    {
                        //CARGAMOS INFORMACION DEL CLIENTE
                        //--------------------------------
                        Cliente c = new Cliente
                                        {
                                            CI = Convert.ToInt32(txtCedula.Text),
                                            DIRECCION = txtDireccion.Text,
                                            APELLIDO = txtApellido.Text,
                                            NOMBRE = txtNombre.Text,
                                            NOMBREUSUARIO = txtUsuario.Text,
                                            PASS = txtPassword.Text
                                        };

                        //TELEFONOS
                        //---------
                        if (!String.IsNullOrEmpty(txtTelefonos.Text))
                        {
                            c.TELEFONOS = txtTelefonos.Text.Split(',').ToList();
                        }

                        //GUARDAMOS LA INFORMACION EN LA BASE DE DATOS
                        LogicaUsuarios lu = new LogicaUsuarios();
                        lu.AltaUsuario(c);

                        lblInfo.Text = "Cliente ingresado correctamente";

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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void LimpiarFormulario()
        {
            try
            {
                txtApellido.Text = "";
                txtCedula.Text = "";
                txtDireccion.Text = "";
                txtNombre.Text = "";
                txtPassword.Text = "";
                txtReiterarPass.Text = "";
                txtTelefonos.Text = "";
                txtUsuario.Text = "";

            }
            catch (Exception ex)
            {

                lblInfo.Text = ex.Message;
            }
        }

        #region VALIDACIONES
        private void txtCedula_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtCedula.Text))
            {
                errorProvider.SetError(txtCedula, "Debe ingresar un numero de cedula.");
                e.Cancel = true;
            }
        }

        private void txtCedula_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(txtCedula, string.Empty);
        }



        private void txtNombre_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtNombre.Text))
            {
                errorProvider.SetError(txtNombre, "Debe ingresar un nombre.");
                e.Cancel = true;
            }

        }

        private void txtApellido_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtApellido.Text))
            {
                errorProvider.SetError(txtApellido, "Debe ingresar un apellido.");
                e.Cancel = true;
            }

        }

        private void txtUsuario_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtUsuario.Text))
            {
                errorProvider.SetError(txtUsuario, "Debe ingresar un nombre de usuario.");
                e.Cancel = true;
            }

        }

        private void txtReiterarPass_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtPassword.Text))
            {
                errorProvider.SetError(txtPassword, "Debe ingresar un password.");
                e.Cancel = true;
            }
            else if (String.IsNullOrEmpty(txtReiterarPass.Text))
            {
                errorProvider.SetError(txtReiterarPass, "Debe reiterar el password previamente ingresado.");
                e.Cancel = true;

            }
            else if (txtReiterarPass.Text != txtPassword.Text)
            {
                errorProvider.SetError(txtReiterarPass, "Los passwords ingresados no coinciden. Por favor verifique");
                e.Cancel = true;
            }

        }

        private void txtNombre_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(txtNombre, string.Empty);

        }

        private void txtApellido_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(txtApellido, string.Empty);

        }

        private void txtUsuario_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(txtUsuario, string.Empty);

        }

        private void txtReiterarPass_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(txtReiterarPass, string.Empty);

        }
        #endregion










    }
}
