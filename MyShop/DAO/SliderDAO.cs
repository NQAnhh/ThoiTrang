using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DAO
{
    public class SliderDAO
    {
        private ThoiTrangDBContext db = new ThoiTrangDBContext();

        public List<Slider> getList(string status = "All")
        {
            List<Slider> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Sliders.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Sliders.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Sliders.ToList();
                        break;
                    }
            }
            return list;

        }
        public Slider getRow(int? id)
        {
            if (id == null)
                return null;
            else
            {
                return db.Sliders.Find(id);
            }
        }
        public int Insert(Slider row)
        {
            db.Sliders.Add(row);
            return db.SaveChanges();
        }
        public int Update(Slider row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        public int Delete(Slider row)
        {
            db.Sliders.Remove(row);
            return db.SaveChanges();
        }
    }
}
