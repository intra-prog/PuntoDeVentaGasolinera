using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaGasolinera
{
    public partial class Form1 : Form
    {
        string[] productos = { "Gasolina Magna", "Gasolina Premium", "Diesel" };
        float[] precios = { 24.50f, 26.20f, 25.80f };

        public Form1()
        {
            InitializeComponent();
        }

        public void LimpiarCampos()
        {
            txtFolio.Clear();
            txtFecha.Clear();
            txtUsuario.Clear();
            txtCliente.Clear();
            txtLitros.Clear();

            rbMagna.Checked = true;
            rbPremium.Checked = false;
            rbDiesel.Checked = false;

            lblSubtotal.Text = "0.0";
            lblIva.Text = "0.0";
            lblTotal.Text = "0.0";
        }

        public void GuardarVenta()
        {
            try
            {
                if (txtFolio.Text != "" && txtCliente.Text != "" && txtLitros.Text != "")
                {
                    int folio = int.Parse(txtFolio.Text);
                    float litros = float.Parse(txtLitros.Text);

                    string fecha = txtFecha.Text;
                    string usuario = txtUsuario.Text;
                    string cliente = txtCliente.Text;

                    string productoSeleccionado = productos[0];
                    float precioLitro = precios[0];

                    if (rbPremium.Checked)
                    {
                        productoSeleccionado = productos[1];
                        precioLitro = precios[1];
                    }
                    else if (rbDiesel.Checked)
                    {
                        productoSeleccionado = productos[2];
                        precioLitro = precios[2];
                    }

                    float subtotal = litros * precioLitro;
                    float iva = subtotal * 0.16f;
                    float total = subtotal + iva;

                    lblSubtotal.Text = subtotal.ToString();
                    lblIva.Text = iva.ToString();
                    lblTotal.Text = total.ToString();

                    string ticket = $"Folio: {folio.ToString()} \n\r Fecha: {fecha} \n\r Usuario: {usuario} \n\r Cliente: {cliente} \n\r Producto: {productoSeleccionado} \n\r Litros: {litros.ToString()} \n\r Subtotal: {subtotal.ToString()} \n\r IVA (16%): {iva.ToString()} \n\r Total: {total.ToString()}";

                    string rutaArchivo = "Venta_" + folio.ToString() + ".txt";

                    using (StreamWriter escritor = new StreamWriter(rutaArchivo, false))
                    {
                        escritor.WriteLine(ticket);
                    }

                    MessageBox.Show(ticket, "Venta Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Complete los datos requeridos para la venta", "Mensaje de Error: ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ingrese un numero valido en el campo habilitado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarVenta();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }
    }
}