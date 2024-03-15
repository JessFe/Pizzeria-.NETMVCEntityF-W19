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

        // GET: Home/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        // POST: Home/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(Login model)
        {
            // Verifica la validità del modello
            if (ModelState.IsValid)
            {
                var user = db.Utenti.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
                if (user != null)
                {
                    // Imposta i ruoli per il ticket di autenticazione
                    string roles = user.IsAdmin.GetValueOrDefault() ? "Admin" : "User";

                    // Crea il ticket di autenticazione e lo aggiunge ai cookie
                    var authTicket = new FormsAuthenticationTicket(
                        1,
                        user.Username,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(30), // scadenza
                        false, // non persistente
                        roles,
                        FormsAuthentication.FormsCookiePath);

                    // Cifra il ticket di autenticazione
                    var ticketEncrypted = FormsAuthentication.Encrypt(authTicket);
                    // Crea un cookie con il ticket cifrato
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