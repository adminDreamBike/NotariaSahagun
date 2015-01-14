//-----------------------------------------------------------------------
// <copyright file="FrmInscripcion.cs" company="Notaria Sahagun">
//     Copyright (c) Notaria Sahagun. All rights reserved.
// </copyright>
// <author>Carlos Andrés Rodriguez</author>
//-----------------------------------------------------------------------
namespace NotariaSahagun.RegistroCivil.Formulario
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using NotariaSahagun.RegistroCivil.Soporte.Entidades;
    using NotariaSahagun.RegistroCivil.Negocio.Fachada;
    

    public partial class FrmInscripcion : Form
    {
        public FrmInscripcion()
        {
            InitializeComponent();
        }
        #region Events
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string primerAp;
            string segundoAp;
            string primerNombre;
            string segundoNombre;
            int ano;
            int? idSerial;

            if (String.IsNullOrEmpty(txtPrimerAp.Text))
            {
                MessageBox.Show("Debe ingresar el primer apellido", "Primer Apellido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                primerAp = txtPrimerAp.Text.Trim();
            }

            if (String.IsNullOrEmpty(txtSegundoAp.Text))
            {
                segundoAp = string.Empty;
            }
            else
            {
                segundoAp = txtSegundoAp.Text.Trim();
            }

            if (String.IsNullOrEmpty(txtPrimerNombre.Text))
            {
                MessageBox.Show("Debe ingresar los nombres", "Nombres", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                primerNombre = txtPrimerNombre.Text.Trim();
            }

            if (String.IsNullOrEmpty(txtSegundoNombre.Text))
            {
                segundoNombre = string.Empty;
            }
            else
            {
                segundoNombre = txtSegundoNombre.Text.Trim();
            }

            if (String.IsNullOrEmpty(cbAno.SelectedItem.ToString()))
            {
                MessageBox.Show("Debe ingresar un año", "Año Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                ano = Int32.Parse(cbAno.SelectedItem.ToString());
            }

            if (String.IsNullOrEmpty(txtIndSerial.Text.ToString()))
            {
                MessageBox.Show("Debe ingresar un Numero de Serial", "No Serial", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                idSerial = Int32.Parse(txtIndSerial.Text.Trim());
            }

            try
            {
                if (RegistroFachada.IngresarRegistro(primerAp, segundoAp, primerNombre, segundoNombre, ano, idSerial.Value))
                {
                    MessageBox.Show("El registro fue ingresado exitosamente.", "Nuevo Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("El registro que intenta ingresar, ya existe.", "Nuevo Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Nuevo Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBuscar s = new FrmBuscar();

            s.Show();
        }
        /// <summary>
        /// 
        /// </summary>
        public void LimpiarCampos()
        {
            txtPrimerAp.Text = string.Empty;
            txtSegundoAp.Text = string.Empty;
            txtPrimerNombre.Text = string.Empty;
            txtSegundoNombre.Text = string.Empty;
            txtIndSerial.Text = string.Empty;
            cbAno.SelectedItem = null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        } 
        #endregion
    }
}
