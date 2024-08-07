using AutoMapper;
using Domain.Entities.Auth;
using Domain.Interface.Repository.Auth;
using Domain.Interface.Repository.Common;
using Domain.Interface.Service.Auth;
using Domain.Model.Auth;
using Domain.Model.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Repository;
using Repository.Auth;
using Repository.Common;
using Service.Auth;

namespace Mapper
{
    public class DependencyMapper
    {
        public static void MapDependenceInjection(IServiceCollection services, IConfiguration configuration, IOptions<JwtSettings> jwtSettings)
        {
            #region Repository
            services.AddScoped<IUserRepository, UserRepository>();
            
           


            services.AddScoped<IAuthRepository>(provider =>
            {
                var autenticacaoUrl = configuration["AuthenticationServiceUrl"];

                return new AuthRepository(autenticacaoUrl, configuration, jwtSettings);
            });



            #endregion

            #region Service
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUnitOfWork, UnityOfWork>();
            

            #endregion
        }


        #region Entity
        public static IMapper Configure()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                

                cfg.CreateMap<User, UserResponse>();
                cfg.CreateMap<UserRequest, User>();
                
                
            });

            IMapper mapper = mapperConfig.CreateMapper();
            return mapper;
        }
        #endregion

    }
}
