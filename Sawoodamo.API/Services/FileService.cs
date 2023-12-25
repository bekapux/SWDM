namespace Sawoodamo.API.Services;

public class FileService(IAmazonS3 s3Client, IConfiguration configuration) : IFileService
{
    private readonly string bucketName = configuration.GetSection("S3:Bucketname").Value!;

    public async Task<string> CreateFile(string filePath, Stream fileContent, CancellationToken cancellationToken = default)
    {
        var request = new PutObjectRequest
        {
            BucketName = bucketName,
            Key = filePath,
            InputStream = fileContent
        };

        await s3Client.PutObjectAsync(request, cancellationToken);
        
        return $"https://{bucketName}.s3.{s3Client.Config.RegionEndpoint.SystemName}.amazonaws.com/{Uri.EscapeDataString(filePath)}";
    }

    public async Task DeleteFile(string filePath, CancellationToken cancellationToken = default)
    {
        var fileKey = filePath.Split('/').Last();

        var request = new DeleteObjectRequest
        {
            BucketName = bucketName,
            Key = fileKey
        };

        await s3Client.DeleteObjectAsync(request, cancellationToken);
    }
}
