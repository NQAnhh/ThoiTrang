using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DAO
{
    public class CategoryDAO
    {
        private ThoiTrangDBContext db = new ThoiTrangDBContext();
        public List<Category> getList(string status = "All")
        {
            List<Category> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Categorys.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Categorys.Where(m => m.Status == 0).ToList();
                        break;
                    }
                    default:
                    {
                        list = db.Categorys.ToList();
                        break;
                    }
            }
            return list;

        }
        public Category getRow(int? id)
        {
            if(id == null) 
                return null;
            else
            {
                return db.Categorys.Find(id);
            }
        }
        //Thêm mẫu tin
        public int Insert(Category row)
        {
            db.Categorys.Add(row);
            return db.SaveChanges();
        }
        public int Update(Category row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        public int Delete(Category row)
        {
            db.Categorys.Remove(row);
            return db.SaveChanges();
        }
    }
}
