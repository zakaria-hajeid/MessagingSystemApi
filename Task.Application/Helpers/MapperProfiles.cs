using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task.Application.Dtos;
using Task.percestance.Models;
using Task.Percestance.Models;

namespace Task.Application.Helpers
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<User, UserForListDto>();

            //CreateMap<User, Organizations>();
            // .ForMember(dest => dest.organizationName, opt => { opt.MapFrom(src => src.OrganizationsUsers});
            CreateMap<OrganizationsUser, OrganizaionNameToRturned>();
              // .ForMember(dest => dest.name, opt => { opt.MapFrom(src => src.Organizations.Name); });
            CreateMap<User, UserForLoginDto>();
            CreateMap<UserForRegisterDto, User>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<User, UserForDetailsDto>();
            CreateMap<Organizations, DtoForGetOrganization>().ReverseMap();
            CreateMap<Message, MessageToReturnDto>();



            //  
            //to update
            //
            /* CreateMap<MessageForCreationDto, Message>().
                  ForMember(dest => dest.RecipientId, opt => { opt.MapFrom(src => src.RecipientId[0]); });*/
            //CreateMap<Message, MessageToReturnDto>();

        }
    }
}