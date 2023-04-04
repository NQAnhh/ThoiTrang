using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyShop.DAO;
using MyShop.Models;

namespace ThoiTrang.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private ProductDAO productDAO = new ProductDAO();

        // GET: Admin/Product
        public ActionResult Index()
        {
            return View(productDAO.getList("Index"));
        }

        // GET: Admin/Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CatId,SupplierId,Name,Slug,Detail")] Product product)
        {
            if (ModelState.IsValid)
            {
                productDAO.Insert(product);
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Admin/Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CatId,SupplierId,Name,Slug,Detail")] Product product)
        {
            if (ModelState.IsValid)
            {
                productDAO.Update(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Admin/Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = productDAO.getRow(id);
            productDAO.Delete(product);
            return RedirectToAction("Index");
        }
        public ActionResult Trash()
        {
            return View(productDAO.getList("Trash"));
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phầm không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger", "Mẩu tin không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            product.Status = (product.Status == 1) ? 2 : 1;
            product.Update_By = Convert.ToInt32(Session["UserId"].ToString());
            product.UpdateAt = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Product");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phầm không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            Product Product = productDAO.getRow(id);
            if (Product == null)
            {
                TempData["message"] = new XMessage("danger", "Mẩu tin không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            Product.Status = 0; //Trang thai rac =0
            Product.Update_By = Convert.ToInt32(Session["UserId"].ToString());
            Product.UpdateAt = DateTime.Now;
            productDAO.Update(Product);
            TempData["message"] = new XMessage("success", "Xóa thành công");
            return RedirectToAction("Index", "Product");
        }
        public ActionResult Retrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phầm không tồn tại");
                return RedirectToAction("Trash", "Product");
            }
            Product Product = productDAO.getRow(id);
            if (Product == null)
            {
                TempData["message"] = new XMessage("danger", "Mẩu tin không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            Product.Status = 2;
            Product.Update_By = Convert.ToInt32(Session["UserId"].ToString());
            Product.UpdateAt = DateTime.Now;
            productDAO.Update(Product);
            TempData["message"] = new XMessage("success", "Xóa thành công");
            return RedirectToAction("Index", "Product");
        }
    }
}
