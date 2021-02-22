using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Application.Dtos;
using Task.Percestance;
using Task.Percestance.Data;

using System.Security.Claims;
using Task.Percestance.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace Task.Application.Servecis
{
    public class MessageServices
    {
        private readonly IMessage _Messagerepo;

        private readonly IUser _Userrepo;
        private readonly IMessagesReceived _MessagesReceived;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _IHttpContextAccessor;
        public MessageServices(IMessage Messagerepo, IHttpContextAccessor HttpContextAccessor, IUser Userrepo, IMessagesReceived MessagesReceived,DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _IHttpContextAccessor = HttpContextAccessor;
            _Messagerepo = Messagerepo;
            _Userrepo = Userrepo;
            _MessagesReceived = MessagesReceived;

        }
      
        public async Task<IEnumerable<object>> Get(int id)

        {
            var UserId = int.Parse(_IHttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
             var user = await _context.messagesReceived.FirstOrDefaultAsync(z =>  z.MessageId == id && z.RecipientId == UserId);

             user.IsRead = true;
           await _context.SaveChangesAsync();
            /*  MessagesReceived c = new MessagesReceived
              {
                  IsRead = true
              };*/
            var messToReturn = await _context.messagesReceived.Join(_context.Users,
           M => M.RecipientId,
           U => U.Id,
           (M, U) => new
           {
               Id = M.Id,

               Content = M.Content,
               subject = M.subject,
               IsRead= M.IsRead,
               Intrash=M.inTrash,
               MessageId = M.MessageId,
               RecipientId = M.RecipientId,
               UserName = U.Email
           })
      .Where(ssa => ssa.MessageId == id)

      .ToListAsync();
            /* for (int i = 0; i < messToReturn.Count; i++)
             {
                 Console.WriteLine(messToReturn[i].IsRead);
             }*/
        

            return messToReturn;
        }

        public async Task<Message> CreateMessage(int userId, MessageForCreationDto messageForCreationDto)
        {

            var sender = await _Userrepo.GetUser(userId);

            messageForCreationDto.SenderId = userId;






            Message message = new Message()
            {
                SenderId = messageForCreationDto.SenderId,
                //RecipientId= messageForCreationDto.RecipientId[i],
                subject = messageForCreationDto.subject,
                Content = messageForCreationDto.Content,
                MessageSent = messageForCreationDto.MessageSent
            };

           await _Messagerepo.Add(message);



            for (int i = 0; i < messageForCreationDto.RecipientId.Length; i++)
            {
                var recipient = await _Userrepo.GetUser(messageForCreationDto.RecipientId[i]);
                if (recipient == null)
                    return null;
                MessagesReceived messagesReceived = new MessagesReceived()
                {
                    subject = messageForCreationDto.subject,
                    Content = messageForCreationDto.Content,
                    RecipientId = messageForCreationDto.RecipientId[i],
                    MessageId = message.Id

                };
                 await _MessagesReceived.Add(messagesReceived);
            }
            return message;
        }
        public async Task<IEnumerable<Object>> messagesent(int userId)
        {
            /*var get = await _Messagerepo.GetMessage(userId);
            var usersToReturn = _mapper.Map<IEnumerable<MessageToReturnDto>>(get);*/
            var MessageSent = await _context.Messages.Join(_context.messagesReceived,
          M => M.Id,
          R => R.MessageId,
          (M, R) => new
          {
             
              id=M.Id,
              SenderId = M.SenderId,
              subject = M.subject,
              Content = M.Content,
              MessageSent = M.MessageSent,
              RecipientId = R.RecipientId,

          }).Join(_context.Users,
          MR => MR.RecipientId,
          U => U.Id,
           (MR, U) => new
           {

               id = MR.id,
               SenderId = MR.SenderId,
               subject = MR.subject,
               Content = MR.Content,
       
               MessageSent = MR.MessageSent,
               RecipientId = MR.RecipientId,
               Name = U.UserName,
            


           }



          )
     .Where(ssa => ssa.SenderId == userId)

     .ToListAsync();

            return MessageSent;
        }
        public async Task<MessagesReceived> DeleteMessage(int userId, TrashMessageDtos deletmessageDto)
        {
            MessagesReceived messagesReceived = null;

            for (int i = 0; i < deletmessageDto.MessageId.Length; i++)
            {
                messagesReceived =  await _context.messagesReceived.FirstOrDefaultAsync(x => x.RecipientId == userId && x.MessageId == deletmessageDto.MessageId[i]);
              var del= await  _MessagesReceived.Delete(messagesReceived);

            }
            return messagesReceived;

        }
        public async Task<IEnumerable<object>> GetMessages(int userId)
        {
   
         var ListMessage= await _context.Messages.Join(_context.messagesReceived,
          M => M.Id,
          R => R.MessageId,
          (M, R) => new 
          {
            Id = M.Id,
            
              SenderId = M.SenderId,
              subject = M.subject,
              Content = M.Content,
              inTrash=R.inTrash,
              MessageSent = M.MessageSent,
              RecipientId = R.RecipientId,

          }).Join(_context.Users,
          MR=>MR.SenderId,
          U=>U.Id,
           (MR,U) => new
           {
               
               Id = MR.Id,
               SenderId = MR.SenderId,
               subject = MR.subject,
               Content = MR.Content,
               inTrash=MR.inTrash,
               MessageSent = MR.MessageSent,
               RecipientId = MR.RecipientId,
               Name =U.UserName,
               Email=U.Email
           

           }



          )
     .Where(ssa => ssa.RecipientId== userId)

     .ToListAsync();
            return ListMessage;
         
            
        }
         public  void MessagTrash(int id, TrashMessageDtos trashMessageDtos)
        {
             //var messageRe = await _context.messagesReceived.FirstOrDefaultAsync(x => x.RecipientId == id && x.MessageId == trashMessageDtos.MessageId[0]);

            for (int i = 0; i < trashMessageDtos.MessageId.Length; i++)
            {
                var messageR =  _context.messagesReceived.FirstOrDefault(x => x.RecipientId == id && x.MessageId == trashMessageDtos.MessageId[i]);
                messageR.inTrash= true;
                 _context.SaveChanges();
            }
       
       
         


        }
        public void RestoreFromTrash(int id, TrashMessageDtos trashMessageDtos)
        {
            //var messageRe = await _context.messagesReceived.FirstOrDefaultAsync(x => x.RecipientId == id && x.MessageId == trashMessageDtos.MessageId[0]);

            for (int i = 0; i < trashMessageDtos.MessageId.Length; i++)
            {
                var messageR = _context.messagesReceived.FirstOrDefault(x => x.RecipientId == id && x.MessageId == trashMessageDtos.MessageId[i]);
                messageR.inTrash = false;
                _context.SaveChanges();
            }





        }
    }

}
