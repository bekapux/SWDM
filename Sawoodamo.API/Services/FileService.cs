namespace Sawoodamo.API.Services;

public class FileService(IAmazonS3 s3Client, string bucketName) : IFileService
{
    public async Task<string> CreateFile(string filePath, Stream fileContent)
    {
        var request = new PutObjectRequest
        {
            BucketName = bucketName,
            Key = filePath,
            InputStream = fileContent
        };

        await s3Client.PutObjectAsync(request);
        
        return $"https://sawoodamo.s3.{s3Client.Config.RegionEndpoint}.amazonaws.com/{Uri.EscapeDataString(filePath)}";
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
