using System.Text;
using System.Web;
using Winnemen.Core.Cryptography;

namespace Winnemen.Web.Cryptography
{
    public class CryptoWeb : ICryptoWeb
    {
        private readonly ICrypto _crypto;

        public CryptoWeb(ICrypto crypto)
        {
            _crypto = crypto;
        }

        /// <summary>
        /// Encrypts the URL safe.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public string EncryptUrlSafe(string value)
        {
            var encrypt = _crypto.Encrypt(value);
            return HttpServerUtility.UrlTokenEncode(Encoding.Default.GetBytes(encrypt));
        }

        /// <summary>
        /// Decrypts the URL safe.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public string DecryptUrlSafe(string value)
        {
            var bytes = HttpServerUtility.UrlTokenDecode(value);
            var encryptedString = Encoding.Default.GetString(bytes);

            var decrypt = _crypto.Decrypt(encryptedString);

            return decrypt;
        }
    }
}
