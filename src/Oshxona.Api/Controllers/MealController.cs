using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oshxona.Domain.Configurations;
using Oshxona.Service.DTOs;
using Oshxona.Service.Helpers;
using Oshxona.Service.Interfaces;

namespace Oshxona.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly IMealService mealService;

        public MealController(IMealService mealService)
        {
            this.mealService = mealService;
        }

        [HttpPost("Insert")]
        public async Task<MealDto> CreateAsync(MealDto dto)
        {
          return await mealService.CreateAsync(dto);
        }
        [HttpGet("GetById")]
        public async Task<MealDto> GetById(int id)
        {
            return await mealService.GetAsync(x => x.Id == id);
          
        }
        [HttpDelete("Delete")]
        public async Task DeleteAsync(int id)
        {
            await mealService.DeleteAsync(x => x.Id == id);
        }
        [HttpGet("GetAll")]
        public async Task<IEnumerable<MealDto>> GetAllAsync([FromQuery] PaginationParams @params)
        {
            if (@params.PageIndex < 1)
            {
                @params.PageIndex = 1;
            }
            if (@params.PageSize < 1)
            {
                @params.PageSize = 1;
            }

            var meals = await mealService
                    .GetAllAsync(
                        new PaginationParams()
                        {
                            PageIndex = @params.PageIndex,
                            PageSize = (short)@params.PageSize
                        });

            return meals;
        }
        [HttpPut("Update")]
        public async Task<MealDto> UpdateAsync(int id, MealDto dto)
        {
            return await mealService.UpdateAsync(id, dto);
        }
    }
}
