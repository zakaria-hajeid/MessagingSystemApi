
using System.Collections.Generic;
using System.Threading.Tasks;

//using Microsoft.WindowsAzure.MediaServices.Client.IMediaContextContainer
using System;
using System.Linq;

namespace Task.Percestance.Abstractions
{
    public interface IRepstory<T> where T:class,new()
    {
        Task<T> Delete(T entity);
        Task<T> Add(T entity);
        void Update(T entity);
        IQueryable<T> GetAll();
    





    }
}