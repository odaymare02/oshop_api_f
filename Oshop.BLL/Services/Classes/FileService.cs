using Microsoft.AspNetCore.Http;
using Oshop.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oshop.BLL.Services.Classes
{
    public class FileService : IFileService
    {
        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {

                var newFileName=Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(),"images",newFileName);
                using(var stream = File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }
                return newFileName;
            }
            throw new Exception("error");
        }
    }
}
