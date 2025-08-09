using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oshop.BLL.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file);
    }
}
