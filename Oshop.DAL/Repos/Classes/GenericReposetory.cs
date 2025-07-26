using Microsoft.EntityFrameworkCore;
using Oshop.DAL.Data;
using Oshop.DAL.Model;
using Oshop.DAL.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oshop.DAL.Repos.Classes
{
    public class GenericReposetory<T> : IGenericReposetory<T> where T : BaseModel
    {
        private readonly ApplicationDbContext _context;

        public GenericReposetory(ApplicationDbContext context)
        {
            _context = context;
        }
        public int Add(T entity)
        {
            
            _context.Set<T>().Add(entity);
             _context.SaveChanges();
            return entity.Id;
           
        }

        public IEnumerable<T> GetAll(bool withTracking = false)
        {
            if (withTracking)
                return _context.Set<T>().ToList();
            return _context.Set<T>().AsNoTracking().ToList();
        }

        public T GetById(int id) => _context.Set<T>().Find(id);

        public int Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
            return _context.SaveChanges();
        }

        public int Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return _context.SaveChanges();
        }
    }
}
