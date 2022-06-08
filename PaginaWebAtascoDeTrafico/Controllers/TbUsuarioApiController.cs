using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaginaWebAtascoDeTrafico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PaginaWebAtascoDeTrafico.Controllers
{
    [Route("api/TbUsuario")]
    [ApiController]
    public class TbUsuarioApiController : ControllerBase
    {

        private readonly db_AtascoDeTraficoContext database;


        public TbUsuarioApiController(db_AtascoDeTraficoContext database)
        {
            this.database = database;
        }


        // GET: api/<TbUsuarioApiController>
        [HttpGet]
        public IEnumerable<TbUsuario> Get()
        {
            return database.TbUsuarios.ToList();
        }

        // GET api/<TbUsuarioApiController>/5
        [HttpGet("{usuario}")]
        public IActionResult Get(string usuario)
        {
            var user = database.TbUsuarios.SingleOrDefault(row => row.Usuario == usuario);
            if (user == null)
            {
                return NotFound();//regresar error
            }
            return Ok(user);
        }

        // POST api/<TbUsuarioApiController>
        [HttpPost]
        public IActionResult Post([FromBody] TbUsuario user)
        {
            if (user == null || user.Usuario == null || user.Contrasena == null || user.Correo == null)
            {
                return BadRequest(new
                {
                    error = "Empty password",
                    code = 408
                });
            }
            var sql = $"call sp_usuario_insert('{user.Usuario}'::varchar,'{user.Contrasena}'::varchar,'{user.Correo}'::varchar)";
            var insert = database.Database.ExecuteSqlRaw(sql);
            return Ok();

        }

        // PUT api/<TbUsuarioApiController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TbUsuario editedUser)
        {
            if (editedUser.IdUsuario != id)
            {
                return BadRequest(new
                {
                    error = "Empty password",
                    code = 408
                });
            }

            var user = database.TbUsuarios.SingleOrDefault(x => x.IdUsuario == id);
            if (user == null)
            {
                return NotFound();
            }

            user.Usuario = editedUser.Usuario;
            user.Correo = editedUser.Correo;
            user.Contrasena = editedUser.Contrasena;

            database.SaveChanges();

            return Ok();
        }

        // DELETE api/<TbUsuarioApiController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = database.TbUsuarios.SingleOrDefault(x => x.IdUsuario == id);
            if (user == null)
            {
                return NotFound();
            }
            var sql = $"call sp_usuario_delete({id})";
            var delete = database.Database.ExecuteSqlRaw(sql);
            return Ok();
        }
    }
}
