using Oshxona.Data.IRepositories;
using Oshxona.Data.Repositories;
using Oshxona.Service.Interfaces;
using Oshxona.Service.Services;

namespace Oshxona.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMealService, MealService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
