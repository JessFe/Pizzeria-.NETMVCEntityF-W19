using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PizzeriaInForno.Models;

namespace PizzeriaInForno.Controllers
{
    public class DettagliOrdiniController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: DettagliOrdini
        public ActionResult Index()
        {
            var dettagliOrdini = db.DettagliOrdini.Include(d => d.Ordini).Include(d => d.Prodotti);
            return View(dettagliOrdini.ToList());
        }

        // GET: DettagliOrdini/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DettagliOrdini dettagliOrdini = db.DettagliOrdini.Find(id);
            if (dettagliOrdini == null)
            {
                return HttpNotFound();
            }
            return View(dettagliOrdini);
        }

        // GET: DettagliOrdini/Create
        public ActionResult Create()
        {
            ViewBag.FK_IDOrdine = new SelectList(db.Ordini, "IDOrdine", "Indirizzo");
            ViewBag.FK_IDProdotto = new SelectList(db.Prodotti, "IDProdotto", "NomeProd");
            return View();
        }

        // POST: DettagliOrdini/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDDettaglioOrd,FK_IDOrdine,FK_IDProdotto,Quantita")] DettagliOrdini dettagliOrdini)
        {
            if (ModelState.IsValid)
            {
                db.DettagliOrdini.Add(dettagliOrdini);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_IDOrdine = new SelectList(db.Ordini, "IDOrdine", "Indirizzo", dettagliOrdini.FK_IDOrdine);
            ViewBag.FK_IDProdotto = new SelectList(db.Prodotti, "IDProdotto", "NomeProd", dettagliOrdini.FK_IDProdotto);
            return View(dettagliOrdini);
        }

        // GET: DettagliOrdini/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DettagliOrdini dettagliOrdini = db.DettagliOrdini.Find(id);
            if (dettagliOrdini == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_IDOrdine = new SelectList(db.Ordini, "IDOrdine", "Indirizzo", dettagliOrdini.FK_IDOrdine);
            ViewBag.FK_IDProdotto = new SelectList(db.Prodotti, "IDProdotto", "NomeProd", dettagliOrdini.FK_IDProdotto);
            return View(dettagliOrdini);
        }

        // POST: DettagliOrdini/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDDettaglioOrd,FK_IDOrdine,FK_IDProdotto,Quantita")] DettagliOrdini dettagliOrdini)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dettagliOrdini).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_IDOrdine = new SelectList(db.Ordini, "IDOrdine", "Indirizzo", dettagliOrdini.FK_IDOrdine);
            ViewBag.FK_IDProdotto = new SelectList(db.Prodotti, "IDProdotto", "NomeProd", dettagliOrdini.FK_IDProdotto);
            return View(dettagliOrdini);
        }

        // GET: DettagliOrdini/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DettagliOrdini dettagliOrdini = db.DettagliOrdini.Find(id);
            if (dettagliOrdini == null)
            {
                return HttpNotFound();
            }
            return View(dettagliOrdini);
        }

        // POST: DettagliOrdini/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DettagliOrdini dettagliOrdini = db.DettagliOrdini.Find(id);
            db.DettagliOrdini.Remove(dettagliOrdini);
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
