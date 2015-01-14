//-----------------------------------------------------------------------
// <copyright file="Registro.cs" company="Notaria Sahagun">
//     Copyright (c) Notaria Sahagun. All rights reserved.
// </copyright>
// <author>Carlos Andrés Rodriguez</author>
//-----------------------------------------------------------------------
namespace NotariaSahagun.RegistroCivil.Soporte.Entidades
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public class Registro
    {
        /// <summary>
        /// Primer Apellido de la persona registrada.
        /// </summary>
        public string PrimerApellido 
        { 
            get; 
            set; 
        }
        /// <summary>
        /// Segundo apellido de la persona registrada.
        /// </summary>
        public string SegundoApellido 
        { 
            get; 
            set; 
        }
        /// <summary>
        /// Primer Nombre de la persona registrada.
        /// </summary>
        public string PrimerNombre 
        { 
            get; 
            set; 
        }
        /// <summary>
        /// Segundo Nombre de la persona registrada.
        /// </summary>
        public string SegundoNombre 
        { 
            get; 
            set; 
        }
        /// <summary>
        /// Año en el cual se registro la persona.
        /// </summary>
        public int Ano 
        { 
            get; 
            set; 
        }
        /// <summary>
        /// Numero serial con el cual fue registrada la persona.
        /// </summary>
        public int IndicativoSerial 
        { 
            get; 
            set; 
        }
    }
}
