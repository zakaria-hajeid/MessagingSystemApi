using System;
using System.Collections.Generic;

namespace Task.Application.Dtos
{
    public class UserForListDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Boolean isActive { get; set; }
        public int OrgId { get; set; }
        public ICollection<OrganizaionNameToRturned> OrganizationsUsers { get; set; }



    }
}