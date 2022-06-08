using Microsoft.AspNetCore.Mvc;
using PaginaWebAtascoDeTrafico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PaginaWebAtascoDeTrafico.Controllers
{
    [Route("api/TbPuntuaje")]
    [ApiController]
    public class TbPuntuajeApiController : ControllerBase
    {
        private readonly db_AtascoDeTraficoContext database;


        public TbPuntuajeApiController(db_AtascoDeTraficoContext database)
        {
            this.database = database;
        }


        // GET: api/<TbUsuarioApiController>
        //Get de highscores de todos los que juegan el juego
        [HttpGet]
        public IEnumerable<VistaPuntuajeUsuario> Get()
        {
            var sql = "SELECT * FROM vista_puntuaje_usuario";
            var result = database.VistaPuntuajeUsuarios.FromSqlRaw(sql).ToList();
            return result;
        }

        // GET api/<TbUsuarioApiController>/5
        //Get de un jugador en especifico
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var sql = $"select * from tb_puntuaje where id_usuario = {id}";

            var puntos = database.TbPuntuajes.FromSqlRaw<TbPuntuaje>(sql);
            
            if (puntos == null)
            {
                return NotFound();//regresar error
            }
            return Ok(puntos);
        }


        ///????????????????????????????????????????????????????????????????????????
        // POST api/<TbUsuarioApiController>
        //Post de puntuaje de un jugador en especifico
        [HttpPost]
        public IActionResult Post([FromBody] UsuarioPuntos puntos)
        {
            var sql = $"call sp_puntuaje_insert({puntos.IdUsuario},{puntos.Puntuaje})";
            var insert = database.Database.ExecuteSqlRaw(sql);
            return Ok();

        }



    }
}
