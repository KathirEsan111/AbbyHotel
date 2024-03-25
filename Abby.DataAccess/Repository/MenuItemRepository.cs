using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using AbbyWeb.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abby.DataAccess.Repository
{
    public class MenuItemRepository:Repository<MenuItem>, IMenuItemRepository
    {
        private readonly ApplicationDbContext _db;

        public MenuItemRepository(ApplicationDbContext db):base(db)
        {
            _db = db;   
        }
        public void Update(MenuItem menuItem)
        {
            var objFromDb = _db.MenuItem.FirstOrDefault(c => c.Id == menuItem.Id);
            objFromDb.Id = menuItem.Id;
            objFromDb.Name = menuItem.Name;
            objFromDb.Description = menuItem.Description;
            objFromDb.Price = menuItem.Price;
            objFromDb.CategoryId = menuItem.CategoryId;
            objFromDb.FoodtypeId=menuItem.FoodtypeId;
            if (objFromDb.Image != null)
            {
                objFromDb.Image = menuItem.Image;
            }

        }


    }
}
