
namespace Sawoodamo.API.Features.ProductSpecs;

public sealed record UpdateProductSpecCommand(string Id, string SpecName, string SpecValue) : IRequest;

public sealed class UpdateProductSpecCommandValidator : AbstractValidator<UpdateProductSpecCommand>
{
    public UpdateProductSpecCommandValidator()
    {
        RuleFor(x => x.SpecName)
            .NotNull()
            .NotEmpty()
                .WithMessage(ErrorMessageGenerator.Required(nameof(ProductSpec.SpecName)))
            .MaximumLength(Constants.ProductSpec.SpecNameMaxLength)
                .WithMessage(ErrorMessageGenerator.MaxLength(nameof(ProductSpec.SpecName), Constants.ProductSpec.SpecNameMaxLength));

        RuleFor(x => x.SpecValue)
            .NotNull()
            .NotEmpty()
                .WithMessage(ErrorMessageGenerator.Required(nameof(ProductSpec.SpecValue)))
            .MaximumLength(Constants.ProductSpec.SpecValueMaxLength)
                .WithMessage(ErrorMessageGenerator.MaxLength(nameof(ProductSpec.SpecValue), Constants.ProductSpec.SpecValueMaxLength));

        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull()
                .WithMessage(ErrorMessageGenerator.Invalid(nameof(ProductSpec.Id)));
    }
}

public sealed class UpdateProductSpecCommandHandler(SawoodamoDbContext context) : IRequestHandler<UpdateProductSpecCommand>
{
    public async Task Handle(UpdateProductSpecCommand request, CancellationToken cancellationToken)
    {
        var affectedRows = await context.ProductSpecs.Where(x => x.Id == request.Id)
            .ExecuteUpdateAsync(x => x
                .SetProperty(y => y.SpecValue, request.SpecValue)
                .SetProperty(y => y.SpecName, request.SpecName), cancellationToken);

        if (affectedRows is 0)
            throw new NotFoundException(nameof(affectedRows), request.Id);
    }
}
