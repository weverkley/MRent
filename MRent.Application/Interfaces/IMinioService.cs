using Microsoft.AspNetCore.Http;

namespace MRent.Application.Interfaces
{
    public interface IMinioService
    {
        Task UploadFileAsync(IFormFile formFile, string objectName);
        Task UploadFileAsync(string bucketName, string objectName, string filePath, string contentType);
        Task EnsureBucketExistsAsync(string bucketName);
        Task SetBucketPublicAsync(string bucketName);
    }
}
