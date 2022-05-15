
using FluentValidation;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;

namespace WebApi.Application.GenreOperations.Commands.UpdateCommand
{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(command=>command.Model.Name).MinimumLength(4).MaximumLength(50).When(x=>x.Model.Name.Trim() != string.Empty);
            //'When' ile koyduğumuz kuralın sorgulanması için bir durum belirttik. Yani 'Name' boş geldiğinde bu MinMax şartlarına bakılmayacak.
        }
    }
}