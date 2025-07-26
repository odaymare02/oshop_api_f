using Oshop.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oshop.DAL.Repos.Interfaces
{
    public interface IGenericReposetory<T> where T :BaseModel
    {
        int Add(T entity);
        IEnumerable<T> GetAll(bool withTracking = false);
        T GetById(int id);
        int Remove(T entity);
        int Update(T entity);
    }
}
