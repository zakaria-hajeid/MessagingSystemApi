using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.Filters;
using Task.Application;

using Task.Percestance.Abstractions;
using Task.Percestance;


using Task.Percestance.Data;
using Task.Percestance.Models;
using Task.Application.Servecis;
using Task.Application.Helpers;
using Task.percestance;

namespace Task
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<TrialData>();

            /* services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
        .AddJsonOptions(option =>
        {
            option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

        });*/
            services.AddControllersWithViews()
     .AddNewtonsoftJson(options =>
     options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
 );
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
          .AddJwtBearer(Options =>
          {
              Options.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuerSigningKey = true,
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                  ValidateIssuer = false,
                  ValidateAudience = false
                  //ValidateLifetime = true

              };
          });
            services.AddDbContext<DataContext>(x =>
 x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Task.percestance")));
            IdentityBuilder builder = services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 4;
                opt.Password.RequireNonAlphanumeric = false;//@ or space etc...
                opt.Password.RequireUppercase = false;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<DataContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<User>>();
            services.AddControllers();
            services.AddCors();
            services.AddScoped(typeof(IRepstory<>), typeof(Repstory<>));
            services.AddScoped<IUser, UserS>();
            services.AddScoped<IMessage, Messages>();
            services.AddScoped<IPermationOrg, PermationOrganizationRepo>();
            services.AddScoped<IPermationUser, PermationsUsersRepo>();
            services.AddScoped<IMessagesReceived, MessagesReceiveds>();
            services.AddScoped<IAdmin, Admin>();
            services.AddScoped<UserServecis>();
            services.AddScoped<MessageServices>();
            services.AddScoped<AdminServices>();
            services.AddAutoMapper(typeof(MapperProfiles));
            services.AddAuthorization(
              options =>
              {
                  options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                  options.AddPolicy("RequireUsers", policy => policy.RequireRole("Users"));

              }
          );
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "My Api", Version = "v1" });
                /* var security = new Dictionary<string, IEnumerable<string>>
                 {
                     {"Bearer", new string[] { }},
                 };*/
                // c.AddSecurityRequirement(security);
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.OperationFilter<SecurityRequirementsOperationFilter>();



            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env/*,TrialData trialData*/)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
          //  trialData.TrialUsers();

            app.UseAuthentication();

            app.UseAuthorization();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "MY API V1"));
            app.UseHttpsRedirection();

            app.UseRouting();
  
        

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
