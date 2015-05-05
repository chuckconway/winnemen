using System;

namespace Winnemen.Core.Cryptography
{
    public class Crypto : ICrypto
    {
        private readonly IRijndaelCryptography _cryptography;

        public Crypto(IRijndaelCryptography cryptography)
        {
            _cryptography = cryptography;
        }

        public string Encrypt(string value)
        {
            return _cryptography.Encrypt(value);
        }

        public string Decrypt(string value)
        {
            string result = null;

            try
            {
                result = _cryptography.Decrypt(value);
            }
            catch (Exception)
            {
                //Swallow encrytion errors.
                //TODO: Logging here.
            }

            return result;
        }


        public string Encrypt(string key, string iv, string value)
        {
            return _cryptography.Encrypt(value, key, iv);
        }
        
        public string Decrypt(string key, string iv, string value)
        {
            string result = null;

            try
            {
                result = _cryptography.Decrypt(value, key, iv);
            }
            catch (Exception)
            {
                //Swallow encrytion errors.
                //TODO: Logging here.
            }

            return result;
        }


    }
}