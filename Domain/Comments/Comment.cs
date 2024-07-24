using Domain.Recipes;

namespace Domain.Comments
{
    public sealed class Comment : BaseClass {
        public string Content { get; set; } 
        public DateTime Date { get; set; }
        public AppUser Author { get; set; } 
        public Recipe Recipe { get; set; }
    }
}