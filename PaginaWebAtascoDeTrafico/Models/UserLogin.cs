using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaginaWebAtascoDeTrafico.Models
{
    [Keyless]
    public class UserLogin
    {
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
    }
}
