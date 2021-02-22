using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using Task.Application.Dtos;
using Task.percestance.Models;
using Task.Percestance;
using Task.Percestance.Models;

namespace Task.Controllers
{
    [AllowAnonymous
        ]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly DataContext _DataContext;
        public AuthController(DataContext DataContext, IConfiguration config, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _config = config;
            _DataContext = DataContext;
        }
        //[Authorize(Policy = "RequireAdminRole")]
       
        [HttpPost("register")]
        public async Task<IActionResult> Register( UserForRegisterDto userForRegisterDto)
        {

            var userToCreate = _mapper.Map<User>(userForRegisterDto);

            var result = await _userManager.CreateAsync(userToCreate, userForRegisterDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(string.Join(", " ,result.Errors.Select(x=>x.Description).ToList()) ) ;
            }
            var Role=  await _userManager.AddToRoleAsync(userToCreate,userForRegisterDto.Role);
                        
            if (!Role.Succeeded)
            {
                return BadRequest(string.Join(", ", Role.Errors.Select(x => x.Description).ToList()));
            }
            for (int i = 0; i < userForRegisterDto.OrganizationsId.Length; i++)
            {
                var OrgUser = new OrganizationsUser
                {
                    UserId = userToCreate.Id,
                    OrganizationsId= userForRegisterDto.OrganizationsId[i]


                };

                 _DataContext.OrganizationsUsers.Add(OrgUser);
                _DataContext.SaveChanges();


            }
                return Ok();

        }
      
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var user = await _userManager.FindByNameAsync(userForLoginDto.username);
            var result = await _signInManager.CheckPasswordSignInAsync(user, userForLoginDto.password, false);
            if (result.Succeeded)
            {
               var appUser = await _userManager.Users.FirstOrDefaultAsync(
                    u => u.NormalizedUserName == userForLoginDto.username.ToUpper()
                );
                // var userToReturn = _mapper.Map<UserForRegisterDto>(appUser);
                return Ok(new
                {
                    token = GenerateJwtToken(appUser).Result,
                   //user = userToReturn
                });
            }
            return Unauthorized();
        }

        private async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}