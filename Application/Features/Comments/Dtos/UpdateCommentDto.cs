using Domain;
using Domain.Ingredients;

namespace Application.Features.Comments.Dtos
{
    public class UpdateCommentDto
    {
        public long Id { get; set; }
        public string Content { get; set; }
    }
} 