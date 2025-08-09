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
    public class ProductService : GenericService<ProductRequest, ProductResponse, Product>, IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IFileService _fileService;

        public ProductService(IProductRepository repository ,IFileService fileService):base(repository)
        {
            _repository = repository;
            _fileService = fileService;
        }

        public async Task<int> CreatWithFile(ProductRequest request)
        {
            var entity = request.Adapt<Product>();
            entity.CreatedAt = DateTime.UtcNow;
            if (request.MainImage != null)
            {
                var nameInDB=await _fileService.UploadFileAsync(request.MainImage);
                entity.MainImage = nameInDB;
            }
            return _repository.Add(entity);
        }
    }
}
