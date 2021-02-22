using System;
using System.Collections.Generic;
using System.Text;
using Task.Percestance.Models;

namespace Task.percestance.Models
{
   public class OrganizationsUser
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int OrganizationsId { get; set; }
        public Organizations Organizations { get; set; }
      


    }
}
