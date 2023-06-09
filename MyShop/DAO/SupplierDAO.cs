﻿using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DAO
{
    public class SupplierDAO
    {
        private ThoiTrangDBContext db = new ThoiTrangDBContext();
        public List<Supplier> getList(string status = "All")
        {
            List<Supplier> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Suppliers.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Suppliers.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Suppliers.ToList();
                        break;
                    }
            }
            return list;

        }
        public Supplier getRow(int? id)
        {
            if (id == null)
                return null;
            else
            {
                return db.Suppliers.Find(id);
            }
        }
        //Thêm mẫu tin
        public int Insert(Supplier row)
        {
            db.Suppliers.Add(row);
            return db.SaveChanges();
        }
        public int Update(Supplier row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        public int Delete(Supplier row)
        {
            db.Suppliers.Remove(row);
            return db.SaveChanges();
        }
    }

}
