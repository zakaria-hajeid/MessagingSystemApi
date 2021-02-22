using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Task.percestance.Models;

namespace Task.Percestance.Models
{
    public class User : IdentityUser<int>
    {
        public string name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Boolean isActive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<MessagesReceived> MessagesReceived { get; set; }
        public ICollection<OrganizationsUser> OrganizationsUsers { get; set; }
        public ICollection<PermationOrganization> PermationOrganizations { get; set; }
        public ICollection<PermationsUsers> UserCanAccesswithHim { get; set; }
        public ICollection<PermationsUsers> UserHavePer { get; set; }


    }
}
