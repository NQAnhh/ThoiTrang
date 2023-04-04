using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Models;

namespace ThoiTrang.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index( string slug = null)
        {
            
            if (slug == null)
            {
                return this.Home();
            }
            else
            {

                return this.Khac();
            }
            
            return View();
        }
        private ActionResult Home ()
        { 
            return View("Home"); 
        
        }
        private ActionResult Khac()
        {
            return View("Home");

        }
    }
}