using System;
using System.Collections.Generic;
using System.Text;
using Task.Percestance.Models;

namespace Task.percestance.Models
{
   public class Organizations
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<OrganizationsUser> OrganizationsUsers { get; set; }
        public ICollection<PermationOrganization> PermationOrganizations { get; set; }

    }
}
