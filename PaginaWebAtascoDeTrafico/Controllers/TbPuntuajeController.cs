using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaginaWebAtascoDeTrafico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaginaWebAtascoDeTrafico.Controllers
{
    public class TbPuntuajeController : Controller
    {
        private readonly db_AtascoDeTraficoContext database;
        
        public TbPuntuajeController(db_AtascoDeTraficoContext database) {
            
            this.database = database;
        }
            // GET: TbPuntuaje
            //Despliege de los highscores de todos los que juegan
        public ActionResult Index()
        {
            var sql = "SELECT * FROM vista_puntuaje_usuario";
            var result = database.VistaPuntuajeUsuarios.FromSqlRaw(sql).ToList();
            if (result.Count() == 0)
            {
                TempData["Error"] = "Todavia no se tiene un puntuaje";
                return View("Index", "Home");
            }
            return View(result);
        }

        
    }
}
