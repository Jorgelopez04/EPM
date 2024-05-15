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
    public class FacturaController : Controller
    {
        private AlmacenEntities db = new AlmacenEntities();

        // GET: Factura
        public ActionResult Index()
        {
            var tb_Factura = db.tb_Factura.Include(t => t.tb_Cliente);
            return View(tb_Factura.ToList());
        }

        // GET: Factura/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_Factura tb_Factura = db.tb_Factura.Find(id);
            if (tb_Factura == null)
            {
                return HttpNotFound();
            }
            return View(tb_Factura);
        }

        // GET: Factura/Create
        public ActionResult Create()
        {
            ViewBag.Cedula = new SelectList(db.tb_Cliente, "Cedula", "Nombres");
            return View();
        }

        // POST: Factura/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Cedula,ValoraPagarEnergia,ValoraPagarAgua,TotalFactura")] tb_Factura tb_Factura)
        {
            if (ModelState.IsValid)
            {
                db.tb_Factura.Add(tb_Factura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cedula = new SelectList(db.tb_Cliente, "Cedula", "Nombres", tb_Factura.Cedula);
            return View(tb_Factura);
        }

        // GET: Factura/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_Factura tb_Factura = db.tb_Factura.Find(id);
            if (tb_Factura == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cedula = new SelectList(db.tb_Cliente, "Cedula", "Nombres", tb_Factura.Cedula);
            return View(tb_Factura);
        }

        // POST: Factura/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Cedula,ValoraPagarEnergia,ValoraPagarAgua,TotalFactura")] tb_Factura tb_Factura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_Factura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cedula = new SelectList(db.tb_Cliente, "Cedula", "Nombres", tb_Factura.Cedula);
            return View(tb_Factura);
        }

        // GET: Factura/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_Factura tb_Factura = db.tb_Factura.Find(id);
            if (tb_Factura == null)
            {
                return HttpNotFound();
            }
            return View(tb_Factura);
        }

        // POST: Factura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_Factura tb_Factura = db.tb_Factura.Find(id);
            db.tb_Factura.Remove(tb_Factura);
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
