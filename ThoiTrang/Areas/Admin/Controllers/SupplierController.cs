﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Generator;
using Antlr.Runtime;
using MyShop.DAO;
using MyShop.Models;
using ThoiTrang.Library;

namespace ThoiTrang.Areas.Admin.Controllers
{
    public class SupplierController : Controller
    {
        SupplierDAO supplierDAO = new SupplierDAO();
        LinkDAO linkDAO = new LinkDAO();
        // GET: Admin/supplier
        public ActionResult Index()
        {
            return View(supplierDAO.getList("Index"));
        }

        // GET: Admin/supplier/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // GET: Admin/supplier/Create
        public ActionResult Create()
        {
            ViewBag.ListOrder = new SelectList(supplierDAO.getList("Index"), "Orders", "Name", 0);
            return View();
        }

        // POST: Admin/supplier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                supplier.Slug = XString.Str_slug(supplier.Name);

                if (supplier.Orders == null)
                {
                    supplier.Orders = 1;
                }
                else
                {
                    supplier.Orders += 1;
                }
                //uploadfile
                var img = Request.Files["img"];//lấy thông tin file
                if (img.ContentLength != 0)
                {

                    string[] FileExtentions = new string[] { ".jpg", ".jepg", ".png", ".gif" };


                    if (!FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                    {
                        ModelState.AddModelError("Err", "Kiểu tập tin không đúng, kiểu tập tin hợp lệ là: " + string.Join(",", FileExtentions));
                    }
                    else
                    {
                        string slug = supplier.Slug;
                        string imgName = slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        supplier.Img = imgName;
                        string PathDir = "~/Public/Images/suppliers/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);
                    }
                }
                //end uploadFile
                supplier.Create_By = Convert.ToInt32(Session["UserId"].ToString());
                supplier.CreateAt = DateTime.Now;

                if (supplierDAO.Insert(supplier) == 1)
                {
                    Link link = new Link();
                    link.Slug = supplier.Slug;
                    link.TableId = supplier.Id;
                    link.TypeLink = "supplier";
                    linkDAO.Insert(link);
                }
                TempData["message"] = new XMessage("success", "Thêm thành hành công");

                return RedirectToAction("Index");
            }

            ViewBag.ListOrder = new SelectList(supplierDAO.getList("Index"), "Orders", "Name", 0);
            return View(supplier);
        }

        // GET: Admin/supplier/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Admin/supplier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                supplier.Slug = XString.Str_slug(supplier.Name);

                if (supplier.Orders == null)
                {
                    supplier.Orders = 1;
                }
                else
                {
                    supplier.Orders += 1;
                }
                //uploadfile
                var img = Request.Files["img"];//lấy thông tin file
                if (img.ContentLength != 0)
                {

                    string[] FileExtentions = new string[] { ".jpg", ".jepg", ".png", ".gif" };


                    if (!FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                    {
                        ModelState.AddModelError("Err", "Kiểu tập tin không đúng, kiểu tập tin hợp lệ là: " + string.Join(",", FileExtentions));
                    }
                    else
                    {
                        string slug = supplier.Slug;
                        string imgName = slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        supplier.Img = imgName;
                        string PathDir = "~/Public/Images/suppliers/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        //xóa file
                        if (supplier.Img != null)
                        {
                            string DelPath = Path.Combine(Server.MapPath(PathDir), supplier.Img);
                            System.IO.File.Delete(DelPath);//xóa hình
                        }
                        img.SaveAs(PathFile);
                    }
                }
                //end uploadFile
                supplier.Update_By = Convert.ToInt32(Session["UserId"].ToString());
                supplier.UpdateAt = DateTime.Now;
                if (supplierDAO.Update(supplier) == 1)
                {
                    Link link = linkDAO.getRow(supplier.Id, "supplier");
                    link.Slug = supplier.Slug;
                    linkDAO.Update(link);
                }
                TempData["message"] = new XMessage("success", "Cập nhật hành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListOrder = new SelectList(supplierDAO.getList("Index"), "Orders", "Name", 0);
            return View(supplier);
        }

        // GET: Admin/supplier/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Admin/supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supplier supplier = supplierDAO.getRow(id);
            Link link = linkDAO.getRow(supplier.Id, "supplier");
            string PathDir = "~/Public/Images/suppliers/";
            //xóa file
            if (supplier.Img != null)
            {
                string DelPath = Path.Combine(Server.MapPath(PathDir), supplier.Img);
                System.IO.File.Delete(DelPath);//xóa hình
            }
            
            if (supplierDAO.Delete(supplier) == 1)
            {
                linkDAO.Delete(link);
            }

            TempData["message"] = new XMessage("success", "Xóa thành công");
            return RedirectToAction("Trash", "supplier");
        }
        public ActionResult Trash()
        {
            return View(supplierDAO.getList("Trash"));
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phầm không tồn tại");
                return RedirectToAction("Index", "supplier");
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                TempData["message"] = new XMessage("danger", "Mẩu tin không tồn tại");
                return RedirectToAction("Index", "supplier");
            }
            supplier.Status = (supplier.Status == 1) ? 2 : 1;
            supplier.Update_By = Convert.ToInt32(Session["UserId"].ToString());
            supplier.UpdateAt = DateTime.Now;
            supplierDAO.Update(supplier);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "supplier");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phầm không tồn tại");
                return RedirectToAction("Index", "supplier");
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                TempData["message"] = new XMessage("danger", "Mẩu tin không tồn tại");
                return RedirectToAction("Index", "supplier");
            }
            supplier.Status = 0; //Trang thai rac =0
            supplier.Update_By = Convert.ToInt32(Session["UserId"].ToString());
            supplier.UpdateAt = DateTime.Now;
            supplierDAO.Update(supplier);
            TempData["message"] = new XMessage("success", "Xóa thành công");
            return RedirectToAction("Index", "supplier");
        }
        public ActionResult Retrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phầm không tồn tại");
                return RedirectToAction("Trash", "supplier");
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                TempData["message"] = new XMessage("danger", "Mẩu tin không tồn tại");
                return RedirectToAction("Index", "supplier");
            }
            supplier.Status = 2;
            supplier.Update_By = Convert.ToInt32(Session["UserId"].ToString());
            supplier.UpdateAt = DateTime.Now;
            supplierDAO.Update(supplier);
            TempData["message"] = new XMessage("success", "Xóa thành công");
            return RedirectToAction("Index", "supplier");
        }
    }
}
