namespace Trainer.BlobStorage.Services
{
    using System.Text;
    using Azure.Storage.Blobs;
    using Azure.Storage.Blobs.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;
    using Trainer.Application.Interfaces;
    using Trainer.Application.Models.Blob;
    using Trainer.Settings;

    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobStorageSettings configuration;

        public BlobStorageService(IOptions<BlobStorageSettings> configuration)
        {
            this.configuration = configuration.Value;
        }

        public async Task<IEnumerable<UploadedFileInfo>> UploadFiles(string containerName, IEnumerable<IFormFile> files,
            bool isUnique)
        {
            var blobServiceClient = this.GetBlobClient();

            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            var urls = new List<UploadedFileInfo>();
            foreach (var file in files)
            {
                var fileName = isUnique ? Guid.NewGuid() + "-" + file.FileName : file.FileName;
                var blobClient = containerClient.GetBlobClient(fileName);
                var url = await this.UploadFile(blobClient, file);
                urls.Add(new UploadedFileInfo { Url = url, ContentType = file.ContentType, FileName = file.FileName });
            }

            return urls;
        }

        public async Task<string> UploadFile(string containerName, IFormFile file, bool isUnique)
        {
            var urls = await this.UploadFiles(containerName, new[] { file }, isUnique);
            return urls.First().Url;
        }

        public async Task<Dictionary<string, byte[]>> GetFiles(string containerName, IEnumerable<string> urls)
        {
            var blobServiceClient = this.GetBlobClient();
            var blobContainer = blobServiceClient.GetBlobContainerClient(containerName);

            var files = new Dictionary<string, byte[]>();
            foreach (var url in urls)
            {
                var urlData = url.Split("/");
                var blobClient = blobContainer.GetBlobClient(urlData[urlData.Length - 1]);

                BlobDownloadInfo download = await blobClient.DownloadAsync();

                var memoryStream = new MemoryStream();
                await download.Content.CopyToAsync(memoryStream);

                files.Add(url, memoryStream.ToArray());
            }

            return files;
        }

        public async Task<byte[]> GetFile(string containerName, string url)
        {
            try
            {
                var file = await this.GetFiles(containerName, new[] { url });
                return file.First().Value;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        private BlobServiceClient GetBlobClient()
        {
            var blobServiceClient = new BlobServiceClient(this.configuration.ConnectionString);
            return blobServiceClient;
        }

        private async Task<string> UploadFile(BlobClient blobClient, IFormFile file)
        {
            var streamReader = new StreamReader(file.OpenReadStream());

            await blobClient.UploadAsync(streamReader.BaseStream, true);

            return blobClient.Uri.AbsoluteUri;
        }
    }
}
