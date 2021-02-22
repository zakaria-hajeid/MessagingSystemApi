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
using Task.Percestance.Abstractions;

namespace Task.Application.Servecis
{
     public class AdminServices
    {
         private readonly IAdmin _repo;
        private readonly IMapper _mapper;
        private readonly IRepstory<PermationOrganization> _permationOrganization;
        private readonly IRepstory<PermationsUsers> _permationUsers;
        private readonly DataContext _context;

        public AdminServices(IAdmin repo, DataContext context, IMapper mapper, IRepstory<PermationOrganization> permationOrganization, IRepstory<PermationsUsers> permationUsers)
        {
            _mapper = mapper;
            _repo = repo;
            _permationOrganization = permationOrganization;
            _permationUsers = permationUsers;
            _context = context;



        }
        public async Task<IEnumerable<DtoForGetOrganization>> GetOrg()
        {
            var Org = await _repo.GetOrg();

            var OrgToReturn = _mapper.Map<IEnumerable<DtoForGetOrganization>>(Org);
            return OrgToReturn;
        }
        public async Task<Organizations> AddOrg(DtoForGetOrganization dtoForGetOrganization)
        {
            var OrgToAdd = _mapper.Map<Organizations>(dtoForGetOrganization);

            var ADD = await _repo.Add(OrgToAdd);
            return ADD;
        }



        public async Task<IEnumerable<object>> GetUserinOrg(int id)
        {
            //var Org = await _repo.GetOrg(id);
           // var OrgToReturn = _mapper.Map<DtoForGetOrganization>(Org);

            var Listusers = await _context.organizations.Join(_context.OrganizationsUsers,
             o => o.Id,
             OU => OU.OrganizationsId,
             (o, OU) => new
             {
                 userId = OU.UserId,
                 OrgId= OU.OrganizationsId


             }).Join(_context.Users,
             OUa => OUa.userId,
             US=>US.Id
             ,
              (OUa,Us) => new
              {
                  id=Us.Id,
                 username=Us.UserName,
                 OrgId= OUa.OrgId

              }



             )
        .Where(ssa => ssa.OrgId == id)

        .ToListAsync();

            return Listusers;



        }
        public async Task<IEnumerable<object>> GetPermationOrganizationForUser(int id)
        {
            //var Org = await _repo.GetOrg(id);
            // var OrgToReturn = _mapper.Map<DtoForGetOrganization>(Org);

            var ListPermationOrganization = await _context.organizations.Join(_context.PermationOrganizations,
             o => o.Id,
             PO => PO.OrganizationsId,
             (o, PO) => new
             {
                 userId = PO.UserId,
                orgId=o.Id


             }).Join(_context.Users,
             POu => POu.userId,
             US => US.Id
             ,
              (POu, Us) => new
              {
                  id=Us.Id,
                  username = Us.UserName,
                  userId=POu.userId,
                  OrgId = POu.orgId

              }



             )
        .Where(ssa => ssa.userId == id)

        .ToListAsync();

            return ListPermationOrganization;



        }
        public async Task<IEnumerable<object>> GetPermationForUser(int id)
        {
            var PermationUser = await _context.Users.Join(_context.PermationsUser,
           U=> U.Id,
           PU => PU.UserCanAccesswithHimId,
           (U, PU) => new
           {
             id=U.Id,
               username = U.UserName,
               UserHavePer= PU.UserHavePerId
           })
      .Where(ssa => ssa.UserHavePer == id)

      .ToListAsync();
            return PermationUser;
        }
        public async Task<PermationOrganization> AddPermOrg(int id, PermationOrganizationAdd permationOrganizationAdd)
        {
            PermationOrganization p=null;

            for (int i = 0; i < permationOrganizationAdd.OrganizationId.Length; i++)
            {
                p = new PermationOrganization
                {
                    UserId = id,
                    OrganizationsId= permationOrganizationAdd.OrganizationId[i]

                };
           p=  await  _permationOrganization.Add(p);

            }
            return p;
         


        }
        public async Task<PermationsUsers> AddPermUsers(int id, PermationUsersAdd permationUsersAdd)
        {
            PermationsUsers p = null;

            for (int i = 0; i < permationUsersAdd.UserId.Length; i++)
            {
                p = new PermationsUsers
                {
                    UserHavePerId = id,
                    UserCanAccesswithHimId = permationUsersAdd.UserId[i]

                };
                p = await _permationUsers.Add(p);

            }
            return p;



        }

    }
    }
