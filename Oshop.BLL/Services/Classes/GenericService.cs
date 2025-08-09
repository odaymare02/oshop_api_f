using Azure;
using Azure.Core;
using Mapster;
using Oshop.BLL.Services.Interfaces;
using Oshop.DAL.DTO.Requests;
using Oshop.DAL.DTO.Responses;
using Oshop.DAL.Model;
using Oshop.DAL.Repos.Classes;
using Oshop.DAL.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oshop.BLL.Services.Classes
{
    public class GenericService<TRequest, TResponse, TEntity> : IGenericService<TRequest, TResponse, TEntity> where TEntity : BaseModel
    {
        private readonly IGenericReposetory<TEntity> _genericReposetory;

        public GenericService(IGenericReposetory<TEntity> genericReposetory)
        {
            _genericReposetory = genericReposetory;
        }
        public int Create(TRequest request)
        {
            var entity = request.Adapt<TEntity>();
            return _genericReposetory.Add(entity);
        }

        public int Delete(int id)
        {
            var entity = _genericReposetory.GetById(id);
            if (entity is null) return 0;
            return _genericReposetory.Remove(entity);
        }

        public IEnumerable<TResponse> GetALl(bool onlyActive=false)
        {

            var entities = _genericReposetory.GetAll();
            if (onlyActive)
            {
                entities = entities.Where(e => e.Status == Status.Active);
            }
            return entities.Adapt<IEnumerable<TResponse>>();
        }

        public TResponse GetById(int id)
        {
            var entity = _genericReposetory.GetById(id);
            return entity is null ? default : entity.Adapt<TResponse>();
        }

        public int Update(int id, TRequest request)
        {
            var entity = _genericReposetory.GetById(id);
            if (entity is null) return 0;
            var updatedEntity = request.Adapt(entity);
            return _genericReposetory.Update(updatedEntity);
        }
        public bool ToogleStatus(int id)
        {
            var entity = _genericReposetory.GetById(id);
            if (entity is null) return false;
            entity.Status = entity.Status == Status.Active ? Status.Inactive : Status.Active;
            _genericReposetory.Update(entity);
            return true;
        }

       
    }
}
