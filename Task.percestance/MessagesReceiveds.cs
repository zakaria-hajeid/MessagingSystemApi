using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Task.Percestance.Abstractions;
using Task.Percestance.Models;

namespace Task.Percestance
{
    public class MessagesReceiveds : Repstory<MessagesReceived>, IMessagesReceived
    {
        public MessagesReceiveds(DataContext context) : base(context)
        {
                
        }
    }

}