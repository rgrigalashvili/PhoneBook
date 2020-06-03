using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Phonebook.API.Middlewares;
using Phonebook.BLL.Managers;
using Phonebook.BLL.Managers.Interfaces;
using Phonebook.BLL.Managers.Settings;
using Phonebook.BLL.Profiles;
using Phonebook.DAL.Database;
using Phonebook.DAL.Repositories;
using Phonebook.DAL.Repositories.Interfaces;
using Phonebook.DAL.UnitOfWork;
using Phonebook.DAL.UnitOfWork.Interface;

namespace Phonebook.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Add(new ServiceDescriptor(typeof(ApiSettings),
                Configuration.GetSection(nameof(ApiSettings))
                .Get<ApiSettings>()));

            var appSettingsSection = Configuration.GetSection("ApiSettings");
            services.Configure<ApiSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<ApiSettings>();

            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            AddDbContext(services);
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();

            // Add Mapper
            services.AddSingleton(mapper);

            // Add UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Add Managers
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IPersonManager, PersonManager>();
            services.AddScoped<IExceptionLogManager, ExceptionLogManager>();

            // Add Repositories
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPhoneNumberRepository, PhoneNumberRepository>();

            services.AddScoped<IPasswordHasher<string>, PasswordHasher<string>>();
            services.AddControllers();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            loggerFactory.AddFile(@"../Phonebook.API/Logs/Phonebook.API-{Date}.txt");

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private void AddJsonOptions(IServiceCollection services)
        {
            services.Add(new ServiceDescriptor(typeof(ApiSettings),
                Configuration.GetSection(nameof(ApiSettings))
                .Get<ApiSettings>()));
        }
        public void AddDbContext(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("PhoneBookDataContext");
            int commandTimeOut = int.Parse(Configuration.GetConnectionString("CommandTimeout"));

            services.AddDbContext<PhoneBookDataContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlServerOptions =>
                {
                    sqlServerOptions.CommandTimeout(commandTimeOut);
                });
            });
        }
    }
}
