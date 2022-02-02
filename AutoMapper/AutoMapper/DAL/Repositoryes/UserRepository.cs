using AutoMapperExample.DAL.Context;
using AutoMapperExample.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoMapperExample.DAL.Repositoryes
{
    public class UserRepository : IRepository<User>
    {
        private bool disposed = false;
        DataContext _context;
        public UserRepository()
        {
            _context = new DataContext();
        }

        public void Create(User item)
        {
            _context.Users.Add(item);
        }

        public void Delete(int id)
        {
            User user = _context.Users.Find(id);
            if(user != null)
                _context.Users.Remove(user);
        }

        public User Get(int id)
        {
            return _context.Users.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(User item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    _context.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
