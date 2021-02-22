using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task.Application.Dtos;
using Task.Application.Servecis;

namespace Task.Controllers
{
    //[Authorize(Policy = "RequireAdminRole")]
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
       {
        private readonly AdminServices _AdminServices;
        public AdminController(AdminServices AdminServices)
        {
            _AdminServices = AdminServices;
        }
        [HttpGet]
        
        public async Task<IActionResult> GetOrganizations()
        {
            var Org = await _AdminServices.GetOrg();
            return Ok(Org);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrg(DtoForGetOrganization dtoForGetOrganization)
        {

            var Add = await _AdminServices.AddOrg(dtoForGetOrganization);
            if (Add == null)
                return NotFound();

            return Ok();



        }
        [HttpGet("GetUserinOrg/{id}")]
        public async Task<IActionResult> GetuserinOrg(int id)
        {
            var userToReturn= await _AdminServices.GetUserinOrg(id);
            return Ok(userToReturn);
        }
        [HttpGet("GetPermationOrganizationForUser/{id}")]
        public async Task<IActionResult> GetPermationOrganizationForUser(int id)
        {
            var PermationOrganizationForUser = await _AdminServices.GetPermationOrganizationForUser(id);
            return Ok(PermationOrganizationForUser);
        }
        [HttpGet("GetPermationForUser/{id}")]
        public async Task<IActionResult> GetPermationForUser(int id)
        {
            var PermationUser = await _AdminServices.GetPermationForUser(id);
            return Ok(PermationUser);
        }

        [HttpPost("PermationOrganization/{id}")]
        public async Task<IActionResult> AddPermOrg(int id, PermationOrganizationAdd permationOrganizationAdd)
        {

           var P=  await _AdminServices.AddPermOrg(id,permationOrganizationAdd);
            if(P==null)
                return NotFound();

            return Ok();



        }
        [HttpPost("PermationUsers/{id}")]
        public async Task<IActionResult> AddPermUsers(int id, PermationUsersAdd permationUsersAdd)
        {

            var P = await _AdminServices.AddPermUsers(id, permationUsersAdd);
            if (P == null)
                return NotFound();

            return Ok();



        }

    }
}
