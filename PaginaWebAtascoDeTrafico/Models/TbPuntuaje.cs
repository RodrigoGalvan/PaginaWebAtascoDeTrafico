using System;
using System.Collections.Generic;

#nullable disable

namespace PaginaWebAtascoDeTrafico.Models
{
    public partial class TbPuntuaje
    {
        public int IdPuntuaje { get; set; }
        public int? IdUsuario { get; set; }
        public int? Puntuaje { get; set; }
        public DateTime? Fecha { get; set; }

        public virtual TbUsuario IdUsuarioNavigation { get; set; }
    }
}
