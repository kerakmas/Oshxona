using Microsoft.AspNetCore.Mvc;
using Oshxona.Domain.Configurations;
using Oshxona.Domain.Entites;
using Oshxona.Service.DTOs;
using Oshxona.Service.Helpers;
using Oshxona.Service.Interfaces;
using Oshxona.Service.Services;

namespace Oshxona.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userservice;

        public UserController(IUserService userservice)
        {
            this.userservice = userservice;
        }

        [HttpPost("Insert")]
        public async Task<UserDto> CreateAsync(UserForCreationDto dto)
        {
            string result = PasswordHasher.Hash(dto.Password).ToString();

            dto.Password = result;
            return await userservice.CreateAsync(dto);
        }
        [HttpGet("GetById")]
        public async Task<UserDto> GetById(int id)
        {
            var result = await userservice.GetAsync(x => x.Id == id);
            return result;
        }
        [HttpDelete("Delete")]
        public async Task DeleteAsync(int id)
        {
            await userservice.DeleteAsync(x => x.Id == id);
        }
        [HttpGet("GetAll")]
        public async Task<IEnumerable<UserDto>> GetAllAsync([FromQuery]PaginationParams @params)
        {
            if (@params.PageIndex < 1)
            {
                @params.PageIndex = 1;
            }
            if (@params.PageSize < 1)
            {
                @params.PageSize = 1;
            }

            var users = await userservice
                    .GetAllAsync(
                        new PaginationParams()
                        {
                            PageIndex = @params.PageIndex,
                            PageSize = (short)@params.PageSize
                        });

            return users;
        }
        [HttpPut("Update")] 
        public async Task<UserDto> UpdateAsync(int id, UserForCreationDto userDto)
        {
            var les = await userservice.GetAsync(x => x.Id == id);

            les.Password = PasswordHasher.Hash(userDto.Password).ToString();
             return await userservice.UpdateAsync(id,userDto);
            
        }
    }

}