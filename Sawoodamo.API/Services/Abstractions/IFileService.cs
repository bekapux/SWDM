namespace Sawoodamo.API.Services.Abstractions;

public interface IFileService
{
    Task CreateFile(string filePath, Stream fileContent);
    Task DeleteFile(string filePath);
    //Task<IEnumerable<DirectoryItemModelDto>> ListFilesInDirectory(string? directoryPath);
}
