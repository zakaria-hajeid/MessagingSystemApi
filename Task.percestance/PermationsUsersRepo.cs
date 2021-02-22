using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.percestance.Models;
using Task.Percestance;

namespace Task.percestance
{
   public class PermationsUsersRepo: Repstory<PermationsUsers>, IPermationUser
    {
        public PermationsUsersRepo(DataContext context) : base(context)
        {

        }
    }
}
