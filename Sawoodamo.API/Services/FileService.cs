namespace Sawoodamo.API.Services;

public class FileService(IAmazonS3 _s3Client, string _bucketName) : IFileService
{
    public async Task CreateFile(string filePath, Stream fileContent)
    {
        var request = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = filePath,
            InputStream = fileContent
        };

        await _s3Client.PutObjectAsync(request);
    }

    public async Task DeleteFile(string filePath)
    {
        var request = new DeleteObjectRequest
        {
            BucketName = _bucketName,
            Key = filePath
        };

        await _s3Client.DeleteObjectAsync(request);
    }
}
