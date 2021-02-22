using System;
using System.Collections.Generic;
using System.Text;
using Task.Percestance.Models;

namespace Task.percestance.Models
{
   public class PermationsUsers
    {
      
        public int UserHavePerId { get; set; }
        public User UserHavePer { get; set; }
        public int UserCanAccesswithHimId { get; set; }
        public User UserCanAccesswithHim { get; set; }
    }
}
