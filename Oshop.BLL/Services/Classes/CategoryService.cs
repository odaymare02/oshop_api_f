using Mapster;
using Oshop.BLL.Services.Interfaces;
using Oshop.DAL.DTO.Requests;
using Oshop.DAL.DTO.Responses;
using Oshop.DAL.Model;
using Oshop.DAL.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oshop.BLL.Services.Classes
{
    public class CategoryService : GenericService<CategoryRequest, CategoryResponse, Category>, ICategoryService
    {
        public CategoryService(IGenericReposetory<Category> genericReposetory) : base(genericReposetory)
        {
        }
    }
}
