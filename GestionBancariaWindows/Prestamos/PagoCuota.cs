﻿using System.Windows.Forms;
using System;
using Logica;
using Entidades;
using ExcepcionesPersonalizadas;
using System.Collections.Generic;

namespace GestionBancariaWindows
{
    public partial class PagoCuota : Form
    {
        public PagoCuota()
        {
            InitializeComponent();
        }

        public Prestamo PRESTAMO { get; set; }


        private void PagoCuota_Load(object sender, System.EventArgs e)
        {
            try
            {
                lblInfo.Text = "";

                
                LogicaSucursal lu = new LogicaSucursal();
                List<Sucursal> sucursales = lu.ListarSucursales();

                foreach (Sucursal s in sucursales)
                {
                    SucursalbindingSource.Add(s);
                }
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }

        private void btnBuscar_Click(object sender, System.EventArgs e)
        {
            try
            {
                int numPrestamo;
                if (Int32.TryParse(txtNumPrestamo.Text, out numPrestamo))
                {
                    LogicaPrestamo lu = new LogicaPrestamo();
                    Prestamo p = new Prestamo { IDPRESTAMO = numPrestamo };
                    if (p != null)
                    {
                        p = (Prestamo)lu.BuscarPrestamo(p);

                        txtCliente.Text = p.CLIENTE.NOMBRE + " " + p.CLIENTE.APELLIDO;
                        txtMontoaPagar.Text = Convert.ToString(p.MONTO);

                        PRESTAMO = p;
                    }
                    else
                    {
                        lblInfo.Text = "No se encontro ningun prestamo.";
                    }

                }
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }


        private void LimpiarFormulario()
        {
            txtMontoaPagar.Text = "";
            txtNumPrestamo.Text = "";
            txtCliente.Text = "";
            cmbSucursal.SelectedValue = "";

        }


        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ValidateChildren(ValidationConstraints.Enabled))
                {
                    LogicaPagos lp = new LogicaPagos();

                    lp.PagarCuota(PRESTAMO);

                    lblInfo.Text = "Cuota pagada correctamente";
                    LimpiarFormulario();

                }
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }


    }
}
