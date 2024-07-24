
using Domain.Ingredients;

namespace Application.Features.Auth.Dtos
{
    public class CreateRecipeDto
    {
        public string Title { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public string Instructions { get; set; }
        public double PreparationTime { get; set; }
    
    }
}