using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;


using Task.Percestance.Abstractions;
//sing FiftyOne.Foundation.Mobile.Detection.Entities;
namespace Task.Percestance
{
    public class Repstory<T> : IRepstory<T> where T :class,new()
    {
        private readonly DataContext _context;
        private DbSet<T> entities;
        public Repstory(DataContext context)
        {
            _context = context;
            
            entities = _context.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            try
            {
                return entities;
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }
        
 


        public async Task<T>  Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            try
            {



                _context.Remove(entity);
             await   _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
            }
        }

        public async Task<T> Add(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            try
            {


                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
            }



        }

       public void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            try
            {


                _context.SaveChanges();
               
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
            }
        }
     
   
     
    }
}