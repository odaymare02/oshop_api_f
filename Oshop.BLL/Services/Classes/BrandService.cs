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
    public class BrandService : GenericService<BrandRequest, BrandResponse, Brand>, IBrandService
    {
        private readonly IGenericReposetory<Brand> _genericReposetory;
        private readonly IFileService _fileService;

        public BrandService(IGenericReposetory<Brand> genericReposetory,IFileService fileService) : base(genericReposetory)
        {
            _genericReposetory = genericReposetory;
            _fileService = fileService;
        }
        public async Task<int> CreatWithFile(BrandRequest request)
        {
            var entity = request.Adapt<Brand>();
            if (request.image != null)
            {
                var newName = await _fileService.UploadFileAsync(request.image);
                entity.image = newName;
            }
           return _genericReposetory.Add(entity);
        }
    }
}
