﻿using Abby.DataAccess.Repository.IRepository;
using AbbyWeb.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abby.DataAccess.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Foodtype = new FoodtypeRepository(_db);
            MenuItem = new MenuItemRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);

        }
        public ICategoryRepository Category { get; private set; }
        public IFoodtypeRepository Foodtype { get; private set; }
        public IMenuItemRepository MenuItem { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
