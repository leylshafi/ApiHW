using ApiHW.DTOs.Category;
using ApiHW.Repositories.Interfaces;
using ApiHW.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ApiHW.Services.Implementations
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(CreateCategoryDto categoryDto)
        {
            Category category = new Category
            {
                Name = categoryDto.Name
            };
            await _repository.AddAsync(category);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Category category = await _repository.GetByIdAsync(id);
            if (category is null) throw new Exception("Not found");
            _repository.Delete(category);
            await _repository.SaveChangesAsync();
        }

        public async Task<ICollection<GetCategoryDto>> GetAllAsync(int page, int take)
        {
            var categories = await _repository.GetAllAsync(skip:(page-1)*take,take:take, isTracking:false).ToListAsync();
            ICollection<GetCategoryDto> result=new List<GetCategoryDto>();
            foreach (Category item in categories)
            {
                result.Add(new()
                {
                    Id = item.Id,
                    Name = item.Name,
                });
            }
            return result;
        }


        public async Task<GetCategoryDto> GetAsync(int id)
        {
          Category category =   await _repository.GetByIdAsync(id);
            if (category is null) throw new Exception("Not found");
            return new GetCategoryDto()
            {
                Id = category.Id,
                Name = category.Name,
            };
        }

        public async Task UpdateAsync(int id, UpdateCategoryDto categoryDto)
        {
            Category category = await _repository.GetByIdAsync(id);
            if (category is null) throw new Exception("Not found");
            category.Name = categoryDto.Name;
            _repository.Update(category);
            await _repository.SaveChangesAsync();
        }
    }
}
