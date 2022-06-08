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
    public class TBUsuarioController : Controller
    {
        private readonly db_AtascoDeTraficoContext database;

        public TBUsuarioController(db_AtascoDeTraficoContext database) {
            this.database = database;
        }

        // GET: UsuarioController
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController/Create
        //Agregar un usuario que se registra
        [HttpPost]
        public IActionResult Create(TbUsuario user)
        {

            if (user.Usuario == null || user.Contrasena == null || user.Correo == null) {
                TempData["Error"] = "Datos insufficientes para crear usuario";
                return View();
            }

            var data = database.TbUsuarios.SingleOrDefault(row => row.Usuario == user.Usuario);

            if (data != null) {
                TempData["Error"] = "El nombre de usuario que ha elegido ya existe";
                return View();
            }

            var sql = $"call sp_usuario_insert('{user.Usuario}'::varchar,'{user.Contrasena}'::varchar,'{user.Correo}'::varchar)";
            var insert = database.Database.ExecuteSqlRaw(sql);
            return RedirectToAction("Index");

        }

        // GET: UsuarioController/Edit/5
        public IActionResult Edit(int id)
        {
            var user = database.TbUsuarios.SingleOrDefault(x => x.IdUsuario == id);

            if (user == null)
            {
                TempData["Error"] = "No se encontro el registro para editar";
                return RedirectToAction("Index");
            }


            return View(user);
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        public IActionResult Edit(TbUsuario newUserData)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "No se encontro registro";
                return View(newUserData);
            }

          
            var user = database.TbUsuarios.SingleOrDefault(x => x.IdUsuario == newUserData.IdUsuario);

            if (newUserData.Contrasena == null || newUserData.Usuario == null)
            {
                TempData["Error"] = "Usuario o contrasena vacia";
                return View(user);
            }


            if (newUserData == null)
            {
                return RedirectToAction("Index");
            }

            var data = database.TbUsuarios.SingleOrDefault(row => row.Usuario == user.Usuario);

            if (data != null && data.Usuario == newUserData.Usuario && user.Usuario != newUserData.Usuario)
            {
                TempData["Error"] = "El nombre de usuario que ha elegido ya existe";
                return View(newUserData);
            }

            var sql = $"call sp_usuario_update({newUserData.IdUsuario}, '{newUserData.Usuario}', '{newUserData.Contrasena}', '{newUserData.Correo}')";
            var delete = database.Database.ExecuteSqlRaw(sql);

            return View("StatScreen", newUserData);

        }


        [HttpGet]
        public IActionResult Login() {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserLogin user)
        {

            TbUsuario data = database.TbUsuarios.SingleOrDefault(row => row.Usuario == user.Usuario);

            if (user == null || data == null) {
                TempData["Error"] = "Usuario no existente";
                return View();
            }

            if (user.Contrasena == null || user.Usuario == null) {
                TempData["Error"] = "Usuario o contrasena vacia";
                return View();
            }

            if (user.Contrasena != data.Contrasena) {
                TempData["Error"] = "Contraseña o usuario erroneo";
                return View();
            }

            var sql = "SELECT * FROM vista_puntuaje_usuario";
            var puntuaje = database.VistaPuntuajeUsuarios.FromSqlRaw(sql).ToList();

            return View("StatScreen", data);
        }

        [HttpGet]
        public IActionResult StatScreen(int id) {
            TbUsuario user = database.TbUsuarios.SingleOrDefault(row => row.IdUsuario == id);
            return View(user);
        }

        public IActionResult PuntuajeUsuario(int id)
        {
            TbUsuario data = database.TbUsuarios.SingleOrDefault(row => row.IdUsuario == id);
            var sql = $"SELECT * FROM vista_puntuaje_usuario where usuario = '{data.Usuario}'";
            var result = database.VistaPuntuajeUsuarios.FromSqlRaw(sql).ToList();
            if (result.Count() == 0) {
                TempData["Error"] = "Todavia no se tiene un puntuaje";
                return View("StatScreen", data);
            }
            return View(result);
        }

        public IActionResult Delete(int id)
        {
            var sql = $"call sp_usuario_delete({id})";
            var delete = database.Database.ExecuteSqlRaw(sql);
            return RedirectToAction("Index", "Home");
        }
    }
}
