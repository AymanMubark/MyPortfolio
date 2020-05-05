using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class UnitOfWork<IEntity> : IUnitOfWork<IEntity> where IEntity : class
    {
        
        private readonly DataContext _context;
        private IGenericRepository<IEntity> _entity;


        public UnitOfWork(DataContext context)
        {
            this._context = context;
        }

        public IGenericRepository<IEntity> Entity
        {
            get
            {
                return _entity ?? (_entity = new GenericRepository<IEntity>(_context));
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
