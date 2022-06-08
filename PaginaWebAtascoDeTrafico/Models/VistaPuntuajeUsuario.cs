using System;
using System.Collections.Generic;

#nullable disable

namespace PaginaWebAtascoDeTrafico.Models
{
    public partial class VistaPuntuajeUsuario
    {
        public string Usuario { get; set; }
        public int? Puntuaje { get; set; }
        public int? IdUsuario { get; set; }
    }
}
