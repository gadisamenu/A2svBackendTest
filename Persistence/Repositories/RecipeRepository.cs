
using Application.Contracts.Persistance;
using AutoMapper;
using Domain.Recipes;

namespace Persistence.Repositories
{
    public class RecipeRepository : Repository<Recipe>, IRecipeRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public RecipeRepository(
            AppDbContext dbContext,
            IMapper mapper
           ): base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
