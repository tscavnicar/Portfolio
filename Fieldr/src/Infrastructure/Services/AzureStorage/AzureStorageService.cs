using Fieldr.Application.Common.Interfaces;
using Fieldr.Application.Common.Models;
using Fieldr.Infrastructure.Services.AzureStorage;
using Microsoft.Azure;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fieldr.Infrastructure.Services
{
    public class AzureStorageService : IAzureStorageService
    {
        private readonly IOptionsMonitor<StorageAccountSettings> _storageConfig;

        public AzureStorageService(IOptionsMonitor<StorageAccountSettings> storageConfig)
        {
            _storageConfig = storageConfig;
        }


        public async Task<string> UploadFileToStorage(string base64, string photoName)
        {
            //https://docs.microsoft.com/en-us/azure/storage/blobs/storage-upload-process-images?tabs=dotnet#upload-an-image
            // Create storagecredentials object by reading the values from the configuration (appsettings.json)
            StorageCredentials storageCredentials = new StorageCredentials(_storageConfig.CurrentValue.AccountNameOption, _storageConfig.CurrentValue.AccountKeyOption);

            // Create cloudstorage account by passing the storagecredentials
            CloudStorageAccount storageAccount = new CloudStorageAccount(storageCredentials, true);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Get reference to the blob container by passing the name by reading the value from the configuration (appsettings.json)
            CloudBlobContainer container = blobClient.GetContainerReference(_storageConfig.CurrentValue.FullImagesContainerNameOption);

            // Get the reference to the block blob from the container
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(photoName);

            var imageString = base64.Substring(base64.LastIndexOf(',') + 1);

            byte[] imageBytes = Convert.FromBase64String(imageString);

            // Upload the file
            await blockBlob.UploadFromByteArrayAsync(imageBytes, 0, imageBytes.Length);
            

            return blockBlob.StorageUri.PrimaryUri.AbsoluteUri;
        }
    }
}
