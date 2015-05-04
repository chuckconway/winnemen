using System;
using System.Text;

namespace Winnemen.Core.Cryptography
{
    public class Crypto : ICrypto
    {

        public string Encrypt(string value)
        {
            return RijndaelCryptography.Encrypt(value);
        }

        public string Decrypt(string value)
        {
            string result = null;

            try
            {
                result = RijndaelCryptography.Decrypt(value);
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
            return RijndaelCryptography.Encrypt(value, key, iv);
        }
        
        public string Decrypt(string key, string iv, string value)
        {
            string result = null;

            try
            {
              result = RijndaelCryptography.Decrypt(value, key, iv);
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