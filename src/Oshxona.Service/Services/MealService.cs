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
    public class MealService : IMealService
    {
        private readonly IRepository<Meal> mealRepository;
        private readonly IMapper mapper;
        public MealService(IRepository<Meal> repository, IMapper mapper)
        {
            this.mealRepository = repository;
            this.mapper = mapper;
        }
        public async ValueTask<MealDto> CreateAsync(MealDto dto)
        {
            Meal meal = await this.mealRepository.GetAsync(m => m.Name.ToLower() == dto.Name.ToLower());

            if (meal is not null)
            {
                meal.Count += dto.Count;
                await this.mealRepository.UpdateAsync(meal);
                await this.mealRepository.SaveAsync();

                return this.mapper.Map<MealDto>(meal);
            }

            Meal mappedMeal = mapper.Map<Meal>(dto);

            try
            {
                var result = await this.mealRepository.InsertAsync(mappedMeal);
                await this.mealRepository.SaveAsync();

                return this.mapper.Map<MealDto>(result);
            }

            catch (Exception)
            {
                throw new OshxonaExeption(500, "Something went wrong");
            }
        }
        public async ValueTask<bool> DeleteAsync(Expression<Func<Meal, bool>> expression)
        {
            var meal = await this.mealRepository.GetAsync(expression);

            if (meal is null)
            {
                throw new OshxonaExeption(404, "Meal doesnt exists in our database");
            }

            await mealRepository.DeleteAsync(meal);

            await this.mealRepository.SaveAsync();

            return true;
        }
        public async ValueTask<MealDto> GetAsync(Expression<Func<Meal, bool>> expression)
        {
            var user = await mealRepository.GetAsync(expression);

            if (user is null)
                throw new OshxonaExeption(404, "Meal not found");

            try
            {
                var result = mapper.Map<MealDto>(user);
                return result;
            }

            catch
            {
                throw new OshxonaExeption(500, "Something went wrong");
            }
        }
        public async ValueTask<MealDto> UpdateAsync(long id, MealDto dto)
        {
            var updatinMeal = await mealRepository.GetAsync(u => u.Id == id);

            if (updatinMeal is null)
            {
                throw new OshxonaExeption(404, "User not found");
            }

            var meal = mapper.Map<Meal>(dto);

            meal.UpdatedAt = DateTime.UtcNow;

            await mealRepository.UpdateAsync(meal);

            await mealRepository.SaveAsync();

            return mapper.Map<MealDto>(meal);
        }
        public async ValueTask<IEnumerable<MealDto>> GetAllAsync(PaginationParams @params, Expression<Func<Meal, bool>> expression = null)
        {
            if (expression is null)
            {
                expression = (x => true);
            }

            var entities = mealRepository.GetAll();

            entities = entities.Where(expression).ToPagedList<Meal>(@params);

            var filteredUsers = entities.ToList();

            var result = mapper.Map<List<MealDto>>(entities);

            return await Task.FromResult(result);
        }


    }
}
