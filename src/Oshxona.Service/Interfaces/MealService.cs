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
    public interface IMealService
    {
        ValueTask<MealDto> CreateAsync(MealDto dto);
        ValueTask<MealDto> UpdateAsync(long id, MealDto dto);
        ValueTask<MealDto> GetAsync(Expression<Func<Meal, bool>> expression);
        ValueTask<IEnumerable<MealDto>> GetAllAsync(PaginationParams @params, Expression<Func<Meal, bool>> expression = null);
        ValueTask<bool> DeleteAsync(Expression<Func<Meal, bool>> expression);
    }
}
