using ApiHW.Repositories.Interfaces;

namespace ApiHW.Repositories.Implementations
{
    public class CategoryRepository:Repository<Category>,ICategoryRepository
    {
        public CategoryRepository(AppDbContext context):base(context)
        {

        }
    }
}
