using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task.Application.Dtos;
using Task.Application.Servecis;
using Task.Percestance;
using Task.Percestance.Abstractions;
using Task.Percestance.Data;

namespace Task.Controllers
{
    //[Authorize(Policy = "RequireAdminRole")]
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserServecis _userServecis;

        public UsersController(UserServecis userServecis)
        {
            _userServecis = userServecis;
        }
       
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetUsers()
        {
            var users =  await _userServecis.GetUsers();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Getuser(int id)
        {
            var userToReturn = await _userServecis.Getuser(id);
            return Ok(userToReturn);
        }

         [HttpPut("{id}")]
         public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto){
      var Update= await _userServecis.UpdateUser(id, userForUpdateDto);
                 return NoContent();
    
         }
       
        [HttpDelete("{id}")]
       
        public async Task<IActionResult> Delete(int id)
        {
           var dele=await _userServecis.Delete(id);
          
                return NoContent();
        
        }


    }
        
        }
