
using Application.Contracts.Persistance;
using AutoMapper;
using Domain.Comments;

namespace Persistence.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public CommentRepository(
            AppDbContext dbContext,
            IMapper mapper
           ): base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
