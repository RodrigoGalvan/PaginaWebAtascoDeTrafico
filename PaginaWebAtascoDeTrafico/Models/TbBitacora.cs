using System;
using System.Collections.Generic;

#nullable disable

namespace PaginaWebAtascoDeTrafico.Models
{
    public partial class TbBitacora
    {
        public int Ids { get; set; }
        public string Nombre { get; set; }
        public DateTime? Fecha { get; set; }
        public string Tipo { get; set; }
    }
}
