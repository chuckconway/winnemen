using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Winnemen.Cloud.Azure
{
    public class AzureStorage : IStorage
    {
        private readonly string _cloudAccount;
        private readonly string _cloudKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageBase" /> class.
        /// </summary>
        /// <param name="cloudUrl">The cloud URL.</param>
        /// <param name="cloudAccount">The cloud account.</param>
        /// <param name="cloudKey">The cloud key.</param>
        public AzureStorage(string cloudAccount, string cloudKey)
        {
            _cloudAccount = cloudAccount;
            _cloudKey = cloudKey;
        }

        /// <summary>
        /// Adds the file.
        /// </summary>
        /// <param name="bucketName">Name of the bucket.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="file">The file.</param>
        public void AddFile(string bucketName, string keyName, byte[] file)
        {
            var bucket = GetCloudBlobContainer(bucketName);
            bucket.CreateIfNotExists(BlobContainerPublicAccessType.Off);

            var cloudBlob = bucket.GetBlockBlobReference(keyName);

            cloudBlob.UploadFromByteArray(file, 0, file.Length);
        }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="bucketName">Name of the bucket.</param>
        /// <param name="keyName">Name of the key.</param>
        public void DeleteFile(string bucketName, string keyName)
        {
            ICloudBlob blob = GetCloudBlob(bucketName, keyName);
            blob.DeleteIfExists();
        }

        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public byte[] GetFile(string bucketName, string keyName)
        {
            var cloudBlob = GetCloudBlob(bucketName, keyName);

            var stream = new MemoryStream();
            cloudBlob.DownloadToStream(stream);
            byte[] bytes = stream.ToArray();
            stream.Dispose();

            return bytes;
        }

        private ICloudBlob GetCloudBlob(string bucketName, string keyName)
        {
            var bucket = GetCloudBlobContainer(bucketName);

            var cloudBlob = bucket.GetBlobReferenceFromServer(keyName);
            return cloudBlob;
        }

        private CloudBlobContainer GetCloudBlobContainer(string bucketName)
        {
            var account = CloudStorageAccount.Parse(string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", _cloudAccount, _cloudKey));
            CloudBlobClient client = account.CreateCloudBlobClient();

            var bucket = client.GetContainerReference(bucketName);
            bucket.CreateIfNotExists(BlobContainerPublicAccessType.Off);
            return bucket;
        }
    }
}
