using App.Core;
using App.DTO;
using App.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace App.Web.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Retorna la vista Index.cshtml
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            //Reemplazar por la conexion a BD SQL Server
            List<UserDTO> users = new UserCore().List();
            return View(users);
        }
        

        /// <summary>
        /// Retorna la vista AddOrEdit.cshtml
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult AddOrEdit(int id)
        {
            UserDTO user = new UserDTO();
            if (id != 0)
            {
                user = new UserCore().Get(id);
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
        public IActionResult AddOrEdit([FromForm] UserDTO user)
        {
            if (user.Id == 0)
            {
                if (ModelState.IsValid)
                {
                    UserDTO userCreated = new UserCore().Create(user);
                }
                else
                {
                    return View(user);
                }
            }
            else
            {
                bool isUpdated = new UserCore().Update(user);

            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Retorna la vista Detail.cshtml
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Detail(int id)
        {
            UserDTO user = new UserDTO();
            if (id != 0)
            {
                user = new UserCore().Get(id);
            }
            return View(user);
        }

        public IActionResult Delete(int id)
        {
            UserDTO user = new UserDTO();
            if (id != 0)
            {
                new UserCore().Delete(id);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
