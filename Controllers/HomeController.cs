using Registro_de_Usuario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Registro_de_Usuario.Controllers
{
    public class HomeController : Controller
    {
        SessionData session = new SessionData();
        // GET: Home
        public ActionResult Index()
        {

            //session.destroySession();
            return View();
        }
        public ActionResult Usuarios()
        {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Usuarios(UserLogin datos)
        {
            if (ModelState.IsValid)
            {
                if (datos.login() == true)
                {
                    session.setSession("userName", datos.userName);
                    ViewBag.User = session.getSession("userName");
                    return RedirectToAction("Users", "Users");
                }
                else
                {
                    ViewBag.Message = "Error";
                    return View("Index");
                }
            }
            else
            {
                return View("Index");
            }
        }
        [AllowAnonymous]
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> SignIn(SignIn datos)
        {
            if (ModelState.IsValid)
            {
                if(datos.signIn() == false)
                {
                    ViewBag.Message = "El usuario o el email ya estan registrados";
                    return View("SignIn", datos);    
                }
                else 
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return View("SignIn");
            }
        }
    }
}