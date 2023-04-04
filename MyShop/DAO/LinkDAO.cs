using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DAO
{
    public class LinkDAO
    {
        private ThoiTrangDBContext db = new ThoiTrangDBContext();
        public Link getRow(int tableid, string typelink)
        {
            return db.Links.Where(m=>m.TableId==tableid && m.TypeLink == typelink).FirstOrDefault();
        }
        public int Insert(Link row)
        {
            db.Links.Add(row);
            return db.SaveChanges();
        }
        public int Update(Link row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        public int Delete(Link row)
        {
            db.Links.Remove(row);
            return db.SaveChanges();
        }
    }
}
