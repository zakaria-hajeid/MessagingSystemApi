using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Task.percestance.Models;
using Task.Percestance;

namespace Task.percestance
{
    public class Admin : Repstory<Organizations>, IAdmin
    {
        public Admin(DataContext context) : base(context)
        {

        }

     

        public async Task<IEnumerable<Organizations>> GetOrg()
        {
       
            return await GetAll().ToListAsync();
        }

        public async Task<Organizations> GetOrg(int id)
        {
           return await GetAll().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
