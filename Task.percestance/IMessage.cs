using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Task.Percestance.Abstractions;
using Task.Percestance.Models;

namespace Task.Percestance.Data
{
    public interface IMessage : IRepstory<Message>
    {
        Task<IEnumerable<Message>> GetMessage(int id);
        Task<Message> DeleteMessage(Message M);
  

    }
}
