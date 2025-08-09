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
    public interface IGenericService<TRequest,TResponse,TEntity>//هان مشان السيرفس ممكن امثر من نوع استلم رسبونس وريكوست ففبعثهن جنرك
    {
        int Create(TRequest request);
        IEnumerable<TResponse> GetALl(bool onlyActive = false);
        TResponse GetById(int id);
        int Update(int id, TRequest request);
        int Delete(int id);
        bool ToogleStatus(int id);
    }
}
