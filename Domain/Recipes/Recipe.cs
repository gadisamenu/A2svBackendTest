using Domain.Ingredients;

namespace Domain.Recipes
{
    public sealed class Recipe : BaseClass {
        public string Title { get; set; }
        public List<Ingredient> Ingredients { get; set; }  
        public int PreparationTime { get; set; }
        public string Instructions { get; set; }
        public AppUser Owner { get; set; }
    }
}