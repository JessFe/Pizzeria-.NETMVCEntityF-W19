using PizzeriaInForno.Models;
using System.Linq;
using System.Web.Mvc;

namespace PizzeriaInForno.Controllers
{
    public class HomeController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

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

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Utenti.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
                if (user != null)
                {
                    // Imposta la sessione per tenere traccia dell'accesso dell'utente
                    Session["UserID"] = user.IDUtente.ToString();
                    Session["Username"] = user.Username;
                    Session["IsAdmin"] = user.IsAdmin;

                    // Uso GetValueOrDefault() per evitare l'errore su un nullable bool
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

        public ActionResult Logout()
        {
            // Cancella la sessione
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }



    }
}