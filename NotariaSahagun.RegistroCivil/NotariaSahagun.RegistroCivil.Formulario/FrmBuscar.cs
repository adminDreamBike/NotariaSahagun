//-----------------------------------------------------------------------
// <copyright file="FrmBuscar.cs" company="Notaria Sahagun">
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
    using NotariaSahagun.RegistroCivil.Negocio.Fachada;
    using NotariaSahagun.RegistroCivil.Soporte.Entidades;

    public partial class FrmBuscar : Form
    {
        public FrmBuscar()
        {
            InitializeComponent();
        }

        #region Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string primerAp;
            string primerNombre;
            int idSerial = !string.IsNullOrEmpty(txtSerial.Text.ToString()) ? Int32.Parse(txtSerial.Text.ToString()) : 0;

            if (String.IsNullOrEmpty(txtPrimerApellido.Text))
            {
                primerAp = null;
            }
            else
            {
                primerAp = txtPrimerApellido.Text.Trim();
            }

            if (String.IsNullOrEmpty(txtPrimerNombre.Text))
            {
                primerNombre = null;
            }
            else
            {
                primerNombre = txtPrimerNombre.Text.Trim();
            }
            
            List<Registro> reg = new List<Registro>();

            dgvRegistros.DataSource = null;

            if (idSerial > 0 && !string.IsNullOrEmpty(primerAp) && !string.IsNullOrEmpty(primerNombre))
            {
                reg = RegistroFachada.ConsultarRegistro(primerAp, primerNombre, idSerial);
            }
            else if (idSerial > 0)
            {
                reg = RegistroFachada.ConsultarRegistroConSerial(idSerial);
            }
            else
            {
                reg = RegistroFachada.ConsultarRegistroConNombres(primerAp, primerNombre);
            }

            dgvRegistros.DataSource = reg;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtSerial.Text = string.Empty;
            txtPrimerApellido.Text = string.Empty;
            txtPrimerNombre.Text = string.Empty;
        } 
        #endregion
    }
}
