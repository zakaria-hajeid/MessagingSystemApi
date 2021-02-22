using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Application.Dtos;
using Task.percestance;
using Task.percestance.Models;
using Task.Percestance;
using Task.Percestance.Data;
using Task.Percestance.Models;

namespace Task.Application.Servecis
{
   public class UserServecis
    {
        private readonly IUser _repo;
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IPermationOrg _PermationOrg;
        private readonly IPermationUser _PermationUser;
        private readonly IMessage _Meassage;
        private readonly IMessagesReceived _MessagesReceived;




        public UserServecis(IUser repo, IMessagesReceived MessagesReceived, IMessage Meassage, IPermationUser PermationUser, IPermationOrg PermationOrg, IMapper mapper,DataContext context)
        {
            _mapper = mapper;
            _repo = repo;
            _context = context;
            _Meassage = Meassage;
            _PermationOrg = PermationOrg;
            _PermationUser = PermationUser;
            _MessagesReceived = MessagesReceived;

        }

        public async Task<IEnumerable<UserForListDto>> GetUsers()
        {
            var users = await _repo.GetUsers();

           var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);
            return usersToReturn;
        }
        public async Task<UserForDetailsDto> Getuser(int id)
        {
            var user = await _repo.GetUser(id);
            var userToReturn = _mapper.Map<UserForDetailsDto>(user);
            return userToReturn;
        }
        public async Task<User> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        {
            var userFromRepo = await  _repo.GetUser(id);
            var c = _mapper.Map(userForUpdateDto, userFromRepo);
            _repo.Updates(c);
            return c;

        }
        public async Task<User> Delete(int id)
        {
            var permOrg = await _context.PermationOrganizations.Where(x => x.UserId == id).ToListAsync();

            foreach (PermationOrganization prime in permOrg)
            {
                var deletPermOrg = await _PermationOrg.Delete(prime);
            }
                
                 var perUser= await _context.PermationsUser.Where(x => x.UserHavePerId == id || x.UserCanAccesswithHimId==id).ToListAsync();
            foreach (PermationsUsers prime in perUser)
            {
                var deletPermuS = await _PermationUser.Delete(prime);
            }

            var DELETEMessage = await _context.Messages.Where(x => x.SenderId == id).ToListAsync();

            foreach (Message prime in DELETEMessage)
            {
                var deletMessage = await _Meassage.Delete(prime);
            }
            var DELETEMessageRe = await _context.messagesReceived.Where(x => x.RecipientId == id).ToListAsync();

            foreach (MessagesReceived prime in DELETEMessageRe)
            {
                var deletMessage = await _MessagesReceived.Delete(prime);
            }
            var user = await _repo.GetUser(id);

      
             var userD=await _repo.Delete(user);
              return userD;

        }


    }

       
}
