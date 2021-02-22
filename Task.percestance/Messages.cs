using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Task.Percestance.Models;

namespace Task.Percestance.Data
{
    public class Messages : Repstory<Message>, IMessage
    {
        public Messages(DataContext context) : base(context)
        {

        }

        public async Task<Message> DeleteMessage(Message M)
        {
            var message = await Delete(M);
            return message;
        }

        public async Task<IEnumerable<Message>> GetMessage(int id)
        {
            return await GetAll().Where(x => x.SenderId == id).ToListAsync();
        }

       
    }
}
