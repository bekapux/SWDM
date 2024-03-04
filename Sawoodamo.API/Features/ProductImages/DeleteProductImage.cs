namespace Sawoodamo.API.Features.ProductImages;

public sealed record DeleteProductImageCommand(string ImageId, bool? HardDelete = false) : IRequest;

public sealed class DeleteProductImageCommandValidator : AbstractValidator<DeleteProductImageCommand>
{
    public DeleteProductImageCommandValidator()
    {
        RuleFor(x => x.ImageId)
            .NotNull()
            .NotEmpty();
    }
}

public sealed class DeleteProductImageCommandHandler(SawoodamoDbContext context, IFileService fileService) : IRequestHandler<DeleteProductImageCommand>
{
    public async Task Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
    {
        var productImage = await context.ProductImages.FirstOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException(nameof(ProductImage), request.ImageId);

        if (request.HardDelete == true)
        {
            context.ProductImages.Remove(productImage);

            await fileService.DeleteFile(productImage.Url, cancellationToken);
        }
        else
        {
            productImage.IsDeleted = true;
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}
