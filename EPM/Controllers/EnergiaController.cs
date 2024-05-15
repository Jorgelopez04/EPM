using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EPM;

namespace EPM.Controllers
{
    public class EnergiaController : Controller
    {
        private AlmacenEntities db = new AlmacenEntities();

        // GET: Energia
        public ActionResult Index()
        {
            var tb_Energia = db.tb_Energia.Include(t => t.tb_Cliente);
            return View(tb_Energia.ToList());
        }

        // GET: Energia/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_Energia tb_Energia = db.tb_Energia.Find(id);
            if (tb_Energia == null)
            {
                return HttpNotFound();
            }
            return View(tb_Energia);
        }

        // GET: Energia/Create
        public ActionResult Create()
        {
            ViewBag.Cedula = new SelectList(db.tb_Cliente, "Cedula", "Nombres");
            return View();
        }

        // POST: Energia/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Cedula,MetaAhorroEnergia,ConsumoActualEnergia")] tb_Energia tb_Energia)
        {
            if (ModelState.IsValid)
            {
                db.tb_Energia.Add(tb_Energia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cedula = new SelectList(db.tb_Cliente, "Cedula", "Nombres", tb_Energia.Cedula);
            return View(tb_Energia);
        }

        // GET: Energia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_Energia tb_Energia = db.tb_Energia.Find(id);
            if (tb_Energia == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cedula = new SelectList(db.tb_Cliente, "Cedula", "Nombres", tb_Energia.Cedula);
            return View(tb_Energia);
        }

        // POST: Energia/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Cedula,MetaAhorroEnergia,ConsumoActualEnergia")] tb_Energia tb_Energia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_Energia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cedula = new SelectList(db.tb_Cliente, "Cedula", "Nombres", tb_Energia.Cedula);
            return View(tb_Energia);
        }

        // GET: Energia/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_Energia tb_Energia = db.tb_Energia.Find(id);
            if (tb_Energia == null)
            {
                return HttpNotFound();
            }
            return View(tb_Energia);
        }

        // POST: Energia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_Energia tb_Energia = db.tb_Energia.Find(id);
            db.tb_Energia.Remove(tb_Energia);
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
