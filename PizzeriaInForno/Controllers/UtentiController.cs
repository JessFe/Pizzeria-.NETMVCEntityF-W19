using PizzeriaInForno.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace PizzeriaInForno.Controllers
{
    [Authorize]
    public class UtentiController : Controller
    {
        // ModelDbContext è la classe che rappresenta il database
        private ModelDbContext db = new ModelDbContext();

        // GET: Utenti
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Utenti.ToList());
        }

        // GET: Utenti/Details/5
        // Mostra i dettagli di un utente
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Include: per caricare anche gli ordini dell'utente
            Utenti utenti = db.Utenti.Include(u => u.Ordini).FirstOrDefault(u => u.IDUtente == id);
            if (utenti == null)
            {
                return HttpNotFound();
            }
            return View(utenti);
        }

        // GET: Utenti/Create
        // Mostra il form per la creazione di un nuovo utente
        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Utenti/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        // Crea un nuovo utente
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Create([Bind(Include = "Username,Password,Nome,Cognome,Email,Tel")] Utenti utente)
        {
            // Verifica la validità del modello
            if (ModelState.IsValid)
            {
                // Assicura che l'utente non sia un amministratore
                utente.IsAdmin = false;
                // Aggiunge l'utente al database
                db.Utenti.Add(utente);
                // Salva le modifiche
                db.SaveChanges();
                return RedirectToAction("Login", "Home");
            }

            return View(utente);
        }

        // GET: Utenti/Edit/5
        // Mostra il form per modificare un utente
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utenti utenti = db.Utenti.Find(id);
            if (utenti == null)
            {
                return HttpNotFound();
            }
            return View(utenti);
        }

        // POST: Utenti/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        // Modifica un utente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDUtente,Username,Password,IsAdmin,Nome,Cognome,Email,Tel")] Utenti utenti)
        {
            // Verifica la validità del modello
            if (ModelState.IsValid)
            {
                // Modifica l'utente nel database
                db.Entry(utenti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = utenti.IDUtente });
            }
            return View(utenti);
        }

        // GET: Utenti/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utenti utenti = db.Utenti.Find(id);
            if (utenti == null)
            {
                return HttpNotFound();
            }
            return View(utenti);
        }

        // POST: Utenti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Utenti utenti = db.Utenti.Find(id);
            db.Utenti.Remove(utenti);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
