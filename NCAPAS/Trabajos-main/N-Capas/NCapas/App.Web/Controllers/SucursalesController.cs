using App.Core;
using App.DTO;
using App.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace App.Web.Controllers
{
    public class SucursalesController : Controller
    {
       
        public IActionResult Sucursal()
        {
            //Reemplazar por la conexion a BD SQL Server
            List<BODTO> users = new BOCore().List();
            return View(users);
        }

        /// <summary>
        /// Retorna la vista AddOrEdit.cshtml
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult AddOrEditBO(int id)
        {
            BODTO user = new BODTO();
            if (id != 0)
            {
                user = new BOCore().Get(id);
            }
            return View(user);
        }

        /// <summary>
        /// No retorna una vista, solo ejecuta una acción
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult AddOrEditBO([FromForm] BODTO user)
        {
            if (user.Id == 0)
            {
                if (ModelState.IsValid)
                {
                    BODTO userCreated = new BOCore().Create(user);
                }
                else
                {
                    return View(user);
                }
            }
            else
            {
                bool isUpdated = new BOCore().Update(user);

            }

            return RedirectToAction("Sucursal");
        }

        /// <summary>
        /// Retorna la vista Detail.cshtml
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult DetailBO(int id)
        {
            BODTO user = new BODTO();
            if (id != 0)
            {
                user = new BOCore().Get(id);
            }
            return View(user);
        }

        public IActionResult Delete(int id)
        {
            BODTO user = new BODTO();
            if (id != 0)
            {
                new BOCore().Delete(id);
            }

            return RedirectToAction("Sucursal");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
