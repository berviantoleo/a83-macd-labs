using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BooksCatalogueAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BooksCatalogueAPI.Helpers
{
    public class StorageHelper
    {
        public static bool IsImage(IFormFile file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }


            string[] formats = new string[] { ".jpg", ".png", ".gif", ".jpeg" };


            return formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }


        public static string UploadFileToStorage(Stream fileStream, string fileName, AzureStorageConfig _storageConfig)
        {
            // Membuat object storage credentials dengan nilai dari berkas konfigurasi (appsettings.json)
            BlobContainerClient container = new BlobContainerClient(_storageConfig.ConnectionString, _storageConfig.ImageContainer);
            container.CreateIfNotExists();
            // Mendapatkan reference block blob dari container
            BlobClient blockBlob = container.GetBlobClient(fileName);
            // Mengunggah berkas
            blockBlob.Upload(fileStream);
            return blockBlob.Uri.AbsoluteUri;
        }
    }
}
