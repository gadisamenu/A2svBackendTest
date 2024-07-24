using Application.Contracts.Persistance;
using Domain;
using Microsoft.AspNetCore.Identity;
using Application.Contracts.Services;
using AutoMapper;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IServiceProvider _services;
        private readonly UserManager<AppUser> _usermanager;
        private IUserRepository _userRepository;
        private IRecipeRepository _recipeRepository;

        private ICommentRepository _commentRepository;
        private readonly IJwtService _jwtService;


        public UnitOfWork(AppDbContext dbContext, UserManager<AppUser> userManager, IJwtService jwtService, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _jwtService = jwtService;
            _usermanager = userManager;
        }

        public IUserRepository UserRepository
        {
            get
            {
                _userRepository ??= new UserRepository(_dbContext, _usermanager, _jwtService);
                return _userRepository;
            }
        }

         public IRecipeRepository RecipeRepository
        {
            get
            {
                _recipeRepository ??= new RecipeRepository(_dbContext,_mapper);
                return _recipeRepository;
            }
        }

         public ICommentRepository CommentRepository
        {
            get
            {
                _commentRepository ??= new CommentRepository(_dbContext,_mapper);
                return _commentRepository;
            }
        }

    



        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
