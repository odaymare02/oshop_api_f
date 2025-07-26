using Azure;
using Azure.Core;
using Oshop.DAL.DTO.Requests;
using Oshop.DAL.DTO.Responses;
using Oshop.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oshop.BLL.Services.Interfaces
{
    public interface ICategoryService:IGenericService<CategoryRequest, CategoryResponse, Category>
    {
        
    }
}
