using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using ExcepcionesPersonalizadas;
using Logica;
using GestionBancariaWebForms.refServiceGestionBancaria;//

namespace GestionBancariaWebForms.Masters
{
    public partial class Site : System.Web.UI.MasterPage
    {
        public Usuario USUARIO_LOGUEADO
        {
            get
            {
                if (Session["Usuario"] == null)
                    return null;
                return (Usuario)Session["Usuario"];
            }
            set
            {
                if (Session["Usuario"] == null)
                    Session.Add("Usuario", value);
                else
                    Session["Usuario"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginUser_LoggingIn(object sender, LoginCancelEventArgs e)
        {
            try
            {
                ServiceGestionBancaria sm = new ServiceGestionBancaria();
                //Usuario NuevoUsuario = LogicaUsuario.getLoginUsuario(txtUsuario.Text, txtPass.Text);
                Usuario NuevoUsuario = sm.getLoginUsuario(LoginUser.UserName, LoginUser.Password);

                USUARIO_LOGUEADO = NuevoUsuario;
                if (NuevoUsuario != null)
                {
                    if (NuevoUsuario is Cliente)
                        Response.Redirect("~/Cliente/home.aspx");
                    //else if (NuevoUsuario is Docente) Response.Redirect("~/AdminDocente/HomeDocente.aspx");
                }
                else
                {
                    lblError.Text = "El usuario o contraseña ingresados no son validos";
                }
            }

            catch (SoapException exsoap)
            {
                lblError.Text = !string.IsNullOrEmpty(exsoap.Actor) ? exsoap.Actor : exsoap.Message;
            }
            catch (Exception ex)
            {

                lblError.Text = ex.Message;
            }
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {

        }
    }
}