using Oshxona.Domain.Configurations;
using Oshxona.Domain.Entites;
using Oshxona.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Oshxona.Service.Interfaces
{
    public interface IUserService
    {
        ValueTask<UserDto> CreateAsync(UserForCreationDto dto);
        ValueTask<UserDto> UpdateAsync(long id, UserForCreationDto dto);
        ValueTask<UserDto> GetAsync(Expression<Func<User, bool>> expression);
        ValueTask<IEnumerable<UserDto>> GetAllAsync(PaginationParams @params,Expression<Func<User, bool>> expression = null);
        ValueTask<bool> DeleteAsync(Expression<Func<User, bool>> expression);
    }
}
