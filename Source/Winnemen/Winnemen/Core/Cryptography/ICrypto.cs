namespace Winnemen.Core.Cryptography
{
    public interface ICrypto
    {
        /// <summary>
        /// Encrypts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        string Encrypt(string value);

        string Encrypt(string key, string iv, string value);

        //string Encrypt(string databaseKey, string value);
        
        string Decrypt(string key, string iv, string value);

        //string Decrypt(string databaseKey, string value);

        /// <summary>
        /// Decrypts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        string Decrypt(string value);

    }
}
