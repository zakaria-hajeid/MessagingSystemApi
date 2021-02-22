using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Task.percestance.Models;
using Task.Percestance.Models;

namespace Task.Percestance
    
{
    public class DataContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>

    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        // public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessagesReceived> messagesReceived { get; set; }
        public DbSet<Organizations> organizations { get; set; }
           public DbSet<OrganizationsUser> OrganizationsUsers { get; set; }
           public DbSet<PermationOrganization> PermationOrganizations { get; set; }
           public DbSet<PermationsUsers> PermationsUser { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            builder.Entity<UserRole>(
                userRole =>
                {
                    userRole.HasKey(ur => new { ur.UserId, ur.RoleId });
                    userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                    userRole.HasOne(ur => ur.User)
                   .WithMany(r => r.UserRoles)
                   .HasForeignKey(ur => ur.UserId)
                   .IsRequired();

                }
            );
            builder.Entity<Message>()
          .HasOne(m => m.Sender)
          .WithMany(u => u.MessagesSent)
          .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<MessagesReceived>()
           .HasOne(m => m.Recipient)
           .WithMany(u => u.MessagesReceived)
           .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<MessagesReceived>()
         .HasOne(m => m.message)
         .WithMany(u => u.messagesReceivedcs)
         .OnDelete(DeleteBehavior.Cascade);
            //M To M ORGA AND USER 


            builder.Entity<OrganizationsUser>()
 .HasKey(Ou => new { Ou.UserId, Ou.OrganizationsId });
            builder.Entity<OrganizationsUser>()
                .HasOne(Ou => Ou.User)
                .WithMany(u => u.OrganizationsUsers)
                .HasForeignKey(Ou => Ou.UserId);
            builder.Entity<OrganizationsUser>()
                .HasOne(Ou => Ou.Organizations)
                .WithMany(o => o.OrganizationsUsers)
                .HasForeignKey(Ou => Ou.OrganizationsId);
            //PermationOrganization
            builder.Entity<PermationOrganization>()
.HasKey(PO => new { PO.UserId, PO.OrganizationsId });
            builder.Entity<PermationOrganization>()
              .HasOne(PO => PO.User)
              .WithMany(u => u.PermationOrganizations)
              .HasForeignKey(PO => PO.UserId)
             .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<PermationOrganization>()
            .HasOne(PO=> PO.Organizations)
            .WithMany(o => o.PermationOrganizations)
            .HasForeignKey(PO => PO.OrganizationsId)
            .OnDelete(DeleteBehavior.Restrict);
            //Permation Users
            builder.Entity<PermationsUsers>()
.HasKey(PU => new { PU.UserHavePerId, PU.UserCanAccesswithHimId });
            builder.Entity<PermationsUsers>()
  .HasOne(PU => PU.UserHavePer)
  .WithMany(u => u.UserHavePer)
  .HasForeignKey(PU => PU.UserHavePerId)
 .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<PermationsUsers>()
.HasOne(PU => PU.UserCanAccesswithHim)
.WithMany(u => u.UserCanAccesswithHim)
.HasForeignKey(PU => PU.UserCanAccesswithHimId)
.OnDelete(DeleteBehavior.Restrict);







        }
    }
    }
