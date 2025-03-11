using FluentValidation;

namespace Contact.API.UseCases.Commands.DeleteContact;

public class DeleteContactCommandValidator : AbstractValidator<DeleteContactCommand>
{
    public DeleteContactCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ContactId is required.")
            .NotEqual(Guid.Empty).WithMessage("ContactId must be a valid GUID.");
    }
}