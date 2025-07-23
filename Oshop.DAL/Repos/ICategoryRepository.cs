using Oshop.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oshop.DAL.Repos
{
    public interface ICategoryRepository
    {
        int Add(Category category);
        IEnumerable<Category> GetAll(bool withTracking=false);
        Category GetById(int id);
        int Remove(Category category);
        int Update(Category category);

    }
}
