using Domain.Comments;
using Domain.Ingredients;

namespace Application.Features.Recipes.Dtos
{
    public class RecipeDetailDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public string Instructions { get; set; }
        public double PreparationTime { get; set; }
        public List<Comment> Comments { get; set; }
    }
}