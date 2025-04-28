using Microsoft.AspNetCore.Http;
using Minio;
using Minio.DataModel.Args;
using MRent.Application.Interfaces;

namespace MRent.Application.Services
{
    public class MinioService : IMinioService
    {
        private readonly IMinioClient _minioClient;

        public MinioService(IMinioClient minioClient)
        {
            _minioClient = minioClient;
        }

        public async Task UploadFileAsync(IFormFile formFile, string objectName)
        {
            using (var stream = formFile.OpenReadStream())
            {
                var putObjectArgs = new PutObjectArgs()
                    .WithBucket(objectName)
                    .WithObject(objectName)
                    .WithStreamData(stream)
                    .WithObjectSize(formFile.Length)
                    .WithContentType(formFile.ContentType);

                await _minioClient.PutObjectAsync(putObjectArgs);
            }
        }

        public async Task UploadFileAsync(string bucketName, string objectName, string filePath, string contentType)
        {
            var putObjectArgs = new PutObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(objectName)
                    .WithFileName(filePath)
                    .WithContentType(contentType);
            await _minioClient.PutObjectAsync(putObjectArgs).ConfigureAwait(false);
        }

        public async Task EnsureBucketExistsAsync(string bucketName)
        {
            // Make a bucket on the server, if not already present.
            var beArgs = new BucketExistsArgs()
                .WithBucket(bucketName);
            bool found = await _minioClient.BucketExistsAsync(beArgs);
            if (!found)
            {
                var mbArgs = new MakeBucketArgs()
                    .WithBucket(bucketName);
                await _minioClient.MakeBucketAsync(mbArgs);
            }
        }

        public async Task SetBucketPublicAsync(string bucketName)
        {
            var policyJson =
                $@"{{""Version"": ""2012-10-17"", ""Statement"": [{{""Effect"": ""Allow"",""Principal"": ""*"",""Action"": ""s3:GetObject"",""Resource"": ""arn:aws:s3:::{bucketName}/*""}}]}}";
            // Change policy type parameter
            var args = new SetPolicyArgs()
                .WithBucket(bucketName)
                .WithPolicy(policyJson);
            await _minioClient.SetPolicyAsync(args);
        }
    }
}
