﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Models
{
    public class ThoiTrangDBContext:DbContext
    {
        public ThoiTrangDBContext():base("name=MyShop.Properties.Settings.StrConnect") { }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Config> Configs { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Link> Links { get; set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Slider> Sliders { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
