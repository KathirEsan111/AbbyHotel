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
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderHeaderRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }


        public void Update(OrderHeader OrderHeader)
        {
            _db.OrderHeader.Update(OrderHeader);
        }
        public void UpdateStatus(int id, string status)
        {
            var orderFromDb = _db.OrderHeader.FirstOrDefault(c => c.Id == id);
            if (orderFromDb != null)
            {
                orderFromDb.Status = status;
            }
        }
    }
}
