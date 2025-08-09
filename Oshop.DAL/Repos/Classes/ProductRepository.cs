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
    public class ProductRepository : GenericReposetory<Product>,IProductRepository
    {

        public ProductRepository(ApplicationDbContext context):base(context)
        {
        }
    }
}
