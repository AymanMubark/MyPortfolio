using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IGenericRepository<IEntity> where IEntity : class
    {
        public IEnumerable<IEntity> GetAll();
        public IEntity GetById(object Id);
        void Insert(IEntity entity);
        void Update(IEntity entity);
        void Delete(object Id);
    }
}
