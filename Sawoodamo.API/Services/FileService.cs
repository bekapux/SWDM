namespace Sawoodamo.API.Services;

public class FileService(IAmazonS3 s3Client, IConfiguration configuration) : IFileService
{
    private readonly string bucketName = configuration.GetSection("S3:Bucketname").Value!;

    public async Task<string> CreateFile(string filePath, Stream fileContent)
    {
        var request = new PutObjectRequest
        {
            BucketName = bucketName,
            Key = filePath,
            InputStream = fileContent
        };

        await s3Client.PutObjectAsync(request);
        
        return $"https://{bucketName}.s3.{s3Client.Config.RegionEndpoint.SystemName}.amazonaws.com/{Uri.EscapeDataString(filePath)}";
    }

    public async Task DeleteFile(string filePath)
    {
        var request = new DeleteObjectRequest
        {
            BucketName = bucketName,
            Key = filePath
        };

        await s3Client.DeleteObjectAsync(request);
    }
}
