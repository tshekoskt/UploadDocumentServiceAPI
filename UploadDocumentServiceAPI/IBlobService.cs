using Azure.Storage.Blobs;
using System;
using System.IO;
using System.Threading.Tasks;

namespace UploadDocumentServiceAPI
{
    public interface IBlobService
    {
        Task<Uri> UploadFileBlobAsync(string blobContainerName, Stream content, string contentType, string fileName);
    }
}
