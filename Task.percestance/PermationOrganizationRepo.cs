using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.percestance.Models;
using Task.Percestance;

namespace Task.percestance
{
   public  class PermationOrganizationRepo: Repstory<PermationOrganization>, IPermationOrg
    {
        public PermationOrganizationRepo(DataContext context) : base(context)
        {

        }
    }
}
