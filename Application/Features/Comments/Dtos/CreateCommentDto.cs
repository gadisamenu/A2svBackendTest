
using Domain;
using Domain.Ingredients;

namespace Application.Features.Auth.Dtos
{
    public class CreateCommentDto
    {
        public string Content {get; set;}
        public DateTime Date { get; set; }
    
    }
}