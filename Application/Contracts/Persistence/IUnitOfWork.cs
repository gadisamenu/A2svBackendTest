
namespace Application.Contracts.Persistance
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IRecipeRepository RecipeRepository { get; }
        ICommentRepository CommentRepository { get; }
        Task<int> SaveAsync();
    }
}
