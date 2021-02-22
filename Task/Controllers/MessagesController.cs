using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Task.Application.Dtos;
using Task.Percestance;
using Task.Percestance.Data;
using Task.Percestance;
using Task.Percestance.Abstractions;
using Task.Percestance.Data;
using Task.Percestance.Models;
using Task.Application.Servecis;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Task.Controllers
{
   
    [Route("api/Users/{userId}/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly MessageServices _MessageServices;
        private readonly DataContext _context;
        public MessagesController(MessageServices MessageServices, DataContext context)
        {
            _context = context;
            _MessageServices = MessageServices;

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessage(int id)
        {

            var messageFromRepo = await _MessageServices.Get(id);
            if (messageFromRepo == null)
                return NotFound();
            return Ok(messageFromRepo);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int userId, MessageForCreationDto messageForCreationDto)
        {

            var Message = await _MessageServices.CreateMessage(userId, messageForCreationDto);
            if (Message == null)
                return NotFound();

            return Ok();



        }
        [HttpPut("DeleteMessag")]
        public async Task<IActionResult> DeleteMessage(int userId,TrashMessageDtos deletmessageDto)
        {
            var del= await _MessageServices.DeleteMessage(userId,deletmessageDto);
            if (del == null)
                return NotFound();
           
            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> GetMessageForSpeacificUser(int userId)
        {

            var list = await _MessageServices.GetMessages(userId);

            return Ok(list);
        }
        [HttpGet("messageSent")]
        public async Task<IActionResult> messagesent(int userId)
        {

            var list = await _MessageServices.messagesent(userId);

            return Ok(list);
        }
        [HttpPut]
        public async Task<IActionResult> MessagTrash(int userId, TrashMessageDtos trashMessageDtos)
        {

          _MessageServices.MessagTrash(userId,trashMessageDtos);

            return NoContent();
        }
        [HttpPut("RestoreFromTrash")]
        public async Task<IActionResult> RestoreFromTrash(int userId, TrashMessageDtos trashMessageDtos)
        {

            _MessageServices.RestoreFromTrash(userId, trashMessageDtos);

            return NoContent();
        }


    }
}