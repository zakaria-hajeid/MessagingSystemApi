using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Task.percestance.Models;
using Task.Percestance.Abstractions;
using Task.Percestance.Models;

namespace Task.Percestance.Data
{
     public interface IUser: IRepstory<User>
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);
      
        //Task<User> DeleteUser(User u);
        void Updates(User uP);

    }
}
