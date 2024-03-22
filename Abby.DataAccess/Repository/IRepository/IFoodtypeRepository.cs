using Abby.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abby.DataAccess.Repository.IRepository
{
    public interface IFoodtypeRepository:IRepository<Foodtype>
    {
        void Update(Foodtype foodtype);
    }
}
