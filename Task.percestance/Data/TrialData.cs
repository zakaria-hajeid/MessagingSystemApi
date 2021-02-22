
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Task.percestance.Models;
using Task.Percestance.Models;

namespace Task.Percestance.Data
{
    public class TrialData
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly DataContext _DataContext;

        public TrialData(UserManager<User> userManager, RoleManager<Role> roleManager, DataContext DataContext)
        {
           _roleManager = roleManager;
           _userManager = userManager;
            _DataContext = DataContext;
        }

        public void TrialUsers()
        {
          //  if (!_userManager.Users.Any())
            //{
                var userData = System.IO.File.ReadAllText("C:/Users/Nextwo/source/repos/Task/Task.Percestance/Data/UserTrialData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);
                var roles = new List<Role>{
                    new Role{Name="MainAdmin"},
                    new Role{Name="Admin"},
                    new Role{Name="Users"},
                };

                foreach (var role in roles)
                {
                    _roleManager.CreateAsync(role).Wait();
                }
                var Organ = new List<Organizations>{
                    new Organizations{Name="Development"},
                    new Organizations{Name="Qa"},
                    new Organizations{Name="Sw"},
                };
                foreach (var org in Organ)
                {
                    _DataContext.organizations.Add(org);
                    _DataContext.SaveChanges();
                }
             /*   foreach (var user in users)
                 {

                     _userManager.CreateAsync(user, "password").Wait();
                    var OrgUser = new OrganizationsUser
                    {
                        UserId=user.Id,
                      
                    };
                    _DataContext.OrganizationsUsers.Add(OrgUser);
                    _DataContext.SaveChanges();
                    _userManager.AddToRoleAsync(user, "Users").Wait();

                   
                 }*/
                /*var adminUser = new User
                {
                    UserName = "MainAdmin",
                    //OrganizationsId = 1
                };

                IdentityResult result = _userManager.CreateAsync(adminUser,"password").Result;
                var admin = _userManager.FindByNameAsync("MainAdmin").Result;
                _userManager.AddToRoleAsync(admin, "MainAdmin").Wait();*/
              

          //  }

        }

    }
}