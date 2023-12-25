namespace Sawoodamo.API.Services.Abstractions;

public interface IFileService
{
    Task<string> CreateFile(string filePath, Stream fileContent, CancellationToken cancellationToken = default);
    Task DeleteFile(string filePath, CancellationToken cancellationToken = default);
}
