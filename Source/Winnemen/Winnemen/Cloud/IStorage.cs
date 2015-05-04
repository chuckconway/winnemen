namespace Winnemen.Cloud
{
    public interface IStorage
    {
        /// <summary>
        /// Adds the file.
        /// </summary>
        /// <param name="bucketName">Name of the bucket.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="file">The file.</param>
        void AddFile(string bucketName, string keyName, byte[] file);


        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="bucketName">Name of the bucket.</param>
        /// <param name="keyName">Name of the key.</param>
        void DeleteFile(string bucketName, string keyName);

        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <returns></returns>
        byte[] GetFile(string bucketName, string keyName);
    }
}
