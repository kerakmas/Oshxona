using AutoMapper;
using Oshxona.Data.IRepositories;
using Oshxona.Domain.Configurations;
using Oshxona.Domain.Entites;
using Oshxona.Service.DTOs;
using Oshxona.Service.Exeptions;
using Oshxona.Service.Extensions;
using Oshxona.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Oshxona.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;
        private readonly IMapper mapper;
        public UserService(IRepository<User> repository, IMapper mapper)
        {
            this.userRepository = repository;
            this.mapper = mapper;
        }

        public async ValueTask<UserDto> CreateAsync(UserForCreationDto dto)
        {
            User user = await this.userRepository.GetAsync(u => u.Email.ToLower() == dto.Email.ToLower());

            if (user is not null)
            {
                throw new OshxonaExeption(403, "User already exist");
            }

            User mappedUser = mapper.Map<User>(dto);

            try
            {
                var result = await this.userRepository.InsertAsync(mappedUser);
                await this.userRepository.SaveAsync();

                return this.mapper.Map<UserDto>(result);
            }

            catch (Exception)
            {
                throw new OshxonaExeption(500, "Something went wrong");
            }
        }


        public async ValueTask<bool> DeleteAsync(Expression<Func<User, bool>> expression)
        {
            var user = await this.userRepository.GetAsync(expression);

            if (user is null)
            {
                throw new OshxonaExeption(404, "User not found");
            }

            await userRepository.DeleteAsync(user);

            await this.userRepository.SaveAsync();

            return true;
        }


        public async ValueTask<IEnumerable<UserDto>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression = null)
        {
            if (expression is null)
            {
                expression = (x => true);
            }

            var entities = userRepository.GetAll();

            entities = entities.Where(expression).ToPagedList<User>(@params);

            var filteredUsers = entities.ToList();

            var result = mapper.Map<List<UserDto>>(entities);

            return await Task.FromResult(result);
        }

        public async ValueTask<UserDto> GetAsync(Expression<Func<User, bool>> expression)
        {
            var user = await userRepository.GetAsync(expression);

            if (user is null)
                throw new OshxonaExeption(404, "User not found");

            try
            {
                var result = mapper.Map<UserDto>(user);
                return result;
            }

            catch
            {
                throw new OshxonaExeption(500, "Something went wrong");
            }
        }

        public async ValueTask<UserDto> UpdateAsync(long id, UserForCreationDto dto)
        {
            var updatingUser = await userRepository.GetAsync(u => u.Id == id);

            if (updatingUser is null)
            {
                throw new OshxonaExeption(404, "User not found");
            }

            var user = mapper.Map<User>(dto);

            user.UpdatedAt = DateTime.UtcNow;

            await userRepository.UpdateAsync(user);

            await userRepository.SaveAsync();

            return mapper.Map<UserDto>(user);
        }
    }
}
