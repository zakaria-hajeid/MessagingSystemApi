using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


using Microsoft.EntityFrameworkCore;
using Task.Percestance.Models;

namespace Task.Percestance.Data
{
    public class UserS : Repstory<User>, IUser
    {
        public UserS(DataContext context) : base(context)
        {

        }
        public async Task<User> GetUser(int id)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.Id == id);

        }


        public async Task<IEnumerable<User>> GetUsers()
        {
            return await GetAll().Include(x=>x.OrganizationsUsers).ToListAsync();
        }



       /*public async Task<User> DeleteUser(User u)
        {
           var user= await Delete(u);
            return user;
        }*/
      void IUser.Updates(User uP)
        {
            Update(uP);
        }
    }
}
