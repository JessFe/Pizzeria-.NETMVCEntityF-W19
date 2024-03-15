using PizzeriaInForno.Models;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PizzeriaInForno.Controllers
{
    [Authorize]
    public class ProdottiController : Controller
    {
        // ModelDbContext è la classe che rappresenta il database
        private ModelDbContext db = new ModelDbContext();

        // GET: Prodotti
        // Mostra la lista dei prodotti in ordine alfabetico
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var prodOrdineAlfabetico = db.Prodotti.OrderBy(p => p.NomeProd).ToList();
            return View(prodOrdineAlfabetico);
        }

        // GET: Prodotti/Details/5
        // Mostra i dettagli di un prodotto
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotti prodotti = db.Prodotti.Find(id);
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        // GET: Prodotti/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Prodotti/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        // Crea un nuovo prodotto
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "IDProdotto,NomeProd,Prezzo,ConsMin,Ingredienti")] Prodotti prodotti, HttpPostedFileBase file)
        {
            // Verifica la validità del modello
            if (ModelState.IsValid)
            {
                // Verifica se un file è stato caricato
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/assets/img"), fileName);
                    file.SaveAs(path);

                    //salva il nome del file nel database
                    prodotti.Foto = fileName;
                }

                // Aggiunge il prodotto al database
                db.Prodotti.Add(prodotti);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(prodotti);
        }

        // GET: Prodotti/Edit/5
        // Mostra il form per modificare un prodotto
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotti prodotti = db.Prodotti.Find(id);
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        // POST: Prodotti/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        // Modifica un prodotto
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "IDProdotto,NomeProd,Prezzo,ConsMin,Ingredienti")] Prodotti prodotti, HttpPostedFileBase file)
        {
            // Verifica la validità del modello
            if (ModelState.IsValid)
            {
                // Cerca il prodotto da modificare
                var prodottoToUpdate = db.Prodotti.Find(prodotti.IDProdotto);
                if (prodottoToUpdate != null)
                {
                    // Verifica se un nuovo file è stato caricato
                    if (file != null && file.ContentLength > 0)
                    {
                        // Salva il nuovo file
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/assets/img"), fileName);
                        file.SaveAs(path);

                        // Aggiorna il percorso del file nel database solo se un nuovo file è stato caricato
                        prodottoToUpdate.Foto = fileName;
                    }
                    // Aggiorna gli altri campi
                    prodottoToUpdate.NomeProd = prodotti.NomeProd;
                    prodottoToUpdate.Prezzo = prodotti.Prezzo;
                    prodottoToUpdate.ConsMin = prodotti.ConsMin;
                    prodottoToUpdate.Ingredienti = prodotti.Ingredienti;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(prodotti);
        }


        // GET: Prodotti/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotti prodotti = db.Prodotti.Find(id);
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        // POST: Prodotti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prodotti prodotti = db.Prodotti.Find(id);
            db.Prodotti.Remove(prodotti);
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

        public ActionResult Catalogo()
        {
            var prodotti = db.Prodotti.ToList();
            return View(prodotti);
        }

    }
}
