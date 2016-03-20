using SQLite.EF6.NoAppConfig.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Common;
using System.Data.SQLite;
using System.Data.SQLite.EF6;
using System.Linq;

namespace SQLite.EF6.NoAppConfig
{
    public class DataRepository : IDisposable
    {
        private readonly RepositoryContext _context;
        
        /// <summary>
        /// Provide a path to your database. 
        /// Use "database.sqlite" to use the empty database included
        /// </summary>
        /// <param name="databasePath"></param>
        public DataRepository(string databasePath = null)
        {
            _context = new RepositoryContext(databasePath);
        }
        
        public void AddPerson(string name)
        {
            _context.Persons.Add(new Person { Name = name });
            _context.SaveChanges();
        }

        public Person GetPerson(int id)
        {
            return _context.Persons.FirstOrDefault(p => p.Id == id);
        }

        public Person GetPerson(string name)
        {
            return _context.Persons.FirstOrDefault(p => p.Name == name);
        }

        #region IDisposable

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable
    }
}
