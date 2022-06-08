using System;
using System.Collections.Generic;

#nullable disable

namespace PaginaWebAtascoDeTrafico.Models
{
    public partial class TbUsuario
    {
        public TbUsuario()
        {
            TbPuntuajes = new HashSet<TbPuntuaje>();
        }

        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
        public string Correo { get; set; }

        public virtual ICollection<TbPuntuaje> TbPuntuajes { get; set; }
    }
}
