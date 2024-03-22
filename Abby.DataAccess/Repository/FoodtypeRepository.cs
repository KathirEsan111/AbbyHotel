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
    public class FoodtypeRepository : Repository<Foodtype>, IFoodtypeRepository
    {
        private readonly ApplicationDbContext _db;

        public FoodtypeRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public void Update(Foodtype foodtype)
        {
            var objFromDb = _db.Foodtype.FirstOrDefault(c => c.Id == foodtype.Id);
            objFromDb.Id = foodtype.Id;
            objFromDb.Name = foodtype.Name;
        }
    }
}
