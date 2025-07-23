using Oshop.DAL.DTO.Requests;
using Oshop.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oshop.BLL.Services
{
    public interface ICategoryService
    {
        int CreateCategory(CategoryRequest request);
        IEnumerable<CategoryResponse> GetALlCategories();
        CategoryResponse GetCategoryById(int id);
        int UpdateCategory(int id, CategoryRequest request);
        int DeleteCategory(int id);
         bool ToogleStatus(int id);
    }
}
