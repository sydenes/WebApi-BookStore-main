
using FluentValidation;
using WebApi.Application.GenreOperations.Commands.CreateGenre;

namespace WebApi.Application.GenreOperations.Commands.CreateCommand
{
    public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator()
        {
            RuleFor(command=>command.Model.Name).NotEmpty().MinimumLength(4).MaximumLength(50);
        }
    }
}