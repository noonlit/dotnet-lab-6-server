using FluentValidation;
using Lab6.Data;
using Lab6.ViewModels;

namespace Lab6.Validators
{
	public class CommentValidator: AbstractValidator<CommentViewModel>
	{
        private readonly ApplicationDbContext _context;

        public CommentValidator(ApplicationDbContext context)
        {
            _context = context;
            RuleFor(c => c.Text).MinimumLength(10);
            RuleFor(c => c.MovieId).NotNull();
        }
    }
}
