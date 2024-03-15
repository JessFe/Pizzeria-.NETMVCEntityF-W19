using PizzeriaInForno.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PizzeriaInForno.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        // modifica 1
        // modifica 2
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(Login model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Utenti.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
                if (user != null)
                {
                    // Imposta i ruoli per il ticket di autenticazione
                    string roles = user.IsAdmin.GetValueOrDefault() ? "Admin" : "User";

                    var authTicket = new FormsAuthenticationTicket(
                        1, // version
                        user.Username, // user name
                        DateTime.Now, // created
                        DateTime.Now.AddMinutes(30), // expires
                        false, // persistent?
                        roles, // can be used to store roles
                        FormsAuthentication.FormsCookiePath);

                    var ticketEncrypted = FormsAuthentication.Encrypt(authTicket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticketEncrypted);
                    Response.Cookies.Add(cookie);

                    // Imposta i valori della sessione
                    Session["UserID"] = user.IDUtente.ToString();
                    Session["Username"] = user.Username;
                    Session["IsAdmin"] = user.IsAdmin;

                    // Reindirizza l'utente in base al suo ruolo
                    if (user.IsAdmin.GetValueOrDefault())
                    {
                        return RedirectToAction("Index", "Prodotti");
                    }
                    else
                    {
                        return RedirectToAction("Catalogo", "Prodotti");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Username o Password non validi.");
                }
            }

            return View(model);
        }



        [AllowAnonymous]
        public ActionResult Logout()
        {
            // Cancella il ticket di autenticazione
            FormsAuthentication.SignOut();

            // Cancella la sessione
            Session.Clear();

            // Reindirizza alla pagina principale
            return RedirectToAction("Index", "Home");
        }




    }
}