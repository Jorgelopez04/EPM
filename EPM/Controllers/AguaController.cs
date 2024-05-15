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
    public class AguaController : Controller
    {
        private AlmacenEntities db = new AlmacenEntities();

        // GET: Agua
        public ActionResult Index()
        {
            var tb_Agua = db.tb_Agua.Include(t => t.tb_Cliente);
            return View(tb_Agua.ToList());
        }

        // GET: Agua/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_Agua tb_Agua = db.tb_Agua.Find(id);
            if (tb_Agua == null)
            {
                return HttpNotFound();
            }
            return View(tb_Agua);
        }

        // GET: Agua/Create
        public ActionResult Create()
        {
            ViewBag.Cedula = new SelectList(db.tb_Cliente, "Cedula", "Nombres");
            return View();
        }

        // POST: Agua/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Cedula,PromedioConsumoAgua,ConsumoActualAgua")] tb_Agua tb_Agua)
        {
            if (ModelState.IsValid)
            {
                db.tb_Agua.Add(tb_Agua);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cedula = new SelectList(db.tb_Cliente, "Cedula", "Nombres", tb_Agua.Cedula);
            return View(tb_Agua);
        }

        // GET: Agua/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_Agua tb_Agua = db.tb_Agua.Find(id);
            if (tb_Agua == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cedula = new SelectList(db.tb_Cliente, "Cedula", "Nombres", tb_Agua.Cedula);
            return View(tb_Agua);
        }

        // POST: Agua/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Cedula,PromedioConsumoAgua,ConsumoActualAgua")] tb_Agua tb_Agua)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_Agua).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cedula = new SelectList(db.tb_Cliente, "Cedula", "Nombres", tb_Agua.Cedula);
            return View(tb_Agua);
        }

        // GET: Agua/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_Agua tb_Agua = db.tb_Agua.Find(id);
            if (tb_Agua == null)
            {
                return HttpNotFound();
            }
            return View(tb_Agua);
        }

        // POST: Agua/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_Agua tb_Agua = db.tb_Agua.Find(id);
            db.tb_Agua.Remove(tb_Agua);
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
