namespace Winnemen.Web.Cryptography
{
    public interface ICryptoWeb
    {
        /// <summary>
        /// Encrypts the URL safe.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        string EncryptUrlSafe(string value);

        /// <summary>
        /// Decrypts the URL safe.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        string DecryptUrlSafe(string value);
    }
}