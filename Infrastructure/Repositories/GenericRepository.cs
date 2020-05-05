using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Repositories
{
    public class GenericRepository<IEntity> : IGenericRepository<IEntity> where IEntity : class
    {
        private readonly DataContext _context;
        public DbSet<IEntity> Entities { get; set; }

        public GenericRepository(DataContext context)
        {
            this._context = context;
            this.Entities = context.Set<IEntity>();
        }

        public void Delete(object Id)
        {
            var existEntity = Entities.Find(Id);
            Entities.Remove(existEntity);
        }

        public IEnumerable<IEntity> GetAll()
        {
            return Entities.ToList();
        }

        public IEntity GetById(object Id)
        {
            return Entities.Find(Id);
        }

        public void Insert(IEntity entity)
        {
            Entities.Add(entity);
        }

        public void Update(IEntity entity)
        {
            Entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
