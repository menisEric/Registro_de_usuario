using Registro_de_Usuario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Registro_de_Usuario.Controllers
{
    public class UsersController : Controller
    {
        SessionData session = new SessionData();
        UsersDatos obj = new UsersDatos();
        // GET: Users
        public ActionResult Users()
        {

            ViewBag.User = session.getSession("userName");
            if(ViewBag.User == "")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                caches();
                return View(obj.usersDatos());
            }
         
        }

        public ActionResult Close()
        {
            session.destroySession();

            return RedirectToAction("Index", "Home");
        }
        public ActionResult Edit(int id)
        {
            ViewBag.User = session.getSession("userName");
            if(ViewBag.User == "")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                caches();
                return View(obj.editDatos(id));
            }
            
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Edit(UsersDatosModel model)
        {
            if(obj.actualizar(model)==true)
            {
                return RedirectToAction("Users");
            }
            else
            {
                return View(model);
            }
        }
        public void caches()
        {
            Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.Cache.SetNoStore();
        }
        public ActionResult Details(int id)
        {
            ViewBag.User = session.getSession("userName");
            if (ViewBag.User == "")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                caches();
                return View(obj.editDatos(id));
            }
        }
        public ActionResult Delete(int id)
        {
            ViewBag.User = session.getSession("userName");
            if (ViewBag.User == "")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                caches();
                return View(obj.editDatos(id));
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Delete(UsersDatosModel model)
        {
            if(obj.Eliminar(model) == true)
            {
                return RedirectToAction("Users");
            }
            else
            {
                return View(model);
            }
        }
        public ActionResult Create()
        {
            ViewBag.User = session.getSession("userName");
            if (ViewBag.User == "")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                caches();
                return View();
            }

        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Create(SignIn datos)
        {
            if (ModelState.IsValid)
            {
                if (datos.signIn() == false)
                {
                    ViewBag.Message = "El usuario o el email ya estan registrados";
                    return View("Create", datos);
                }
                else
                {
                    return RedirectToAction("Users");
                }
            }
            else
            {
                return View("Create");
            }
        }
    }
}