using Mapster;
using Oshop.DAL.DTO.Requests;
using Oshop.DAL.DTO.Responses;
using Oshop.DAL.Model;
using Oshop.DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oshop.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        //inside service only do the logic any other operatio with DB using Repo
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public int CreateCategory(CategoryRequest request)
        {
            var category = request.Adapt<Category>();
            return categoryRepository.Add(category);
        }

        public int DeleteCategory(int id)
        {
            var category = categoryRepository.GetById(id);
            if (category is null) return 0;
            return categoryRepository.Remove(category);
        }

        public IEnumerable<CategoryResponse> GetALlCategories()
        {
            var categories= categoryRepository.GetAll();
            return categories.Adapt <IEnumerable<CategoryResponse>>();
        }

        public CategoryResponse GetCategoryById(int id)
        {
            var category = categoryRepository.GetById(id);
            return category is null?null: category.Adapt<CategoryResponse>();
        }

        public int UpdateCategory(int id, CategoryRequest request)
        {
            var categoey=categoryRepository.GetById(id);
            if (categoey is null) return 0;
            categoey.Name = request.Name;
            return categoryRepository.Update(categoey); 
        }
        public bool ToogleStatus(int id)
        {
            var category=categoryRepository.GetById(id);
            if (category is null) return false;
            category.Status = category.Status == Status.Active ? Status.Inactive : Status.Active;
            categoryRepository.Update(category);
            return true;
        }
    }
}
