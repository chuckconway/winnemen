using System;

namespace Winnemen.Cloud
{
    public abstract class StorageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StorageBase"/> class.
        /// </summary>
        /// <param name="cloudUrl">The cloud URL.</param>
        /// <param name="cloudAccount">The cloud account.</param>
        /// <param name="cloudKey">The cloud key.</param>
        protected StorageBase(string cloudUrl, string cloudAccount, string cloudKey)
        {
            CloudUrl = cloudUrl;
            CloudAccount = cloudAccount;
            CloudKey = cloudKey;
        }

        /// <summary>
        /// Checks for configuration value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="errorMessage">The error message.</param>
        public void CheckForConfigurationValue(string value, string errorMessage)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentOutOfRangeException(errorMessage);
            }
        }

        /// <summary>
        /// Gets or sets the cloud URL.
        /// </summary>
        /// <value>The cloud URL.</value>
        public string CloudUrl { get; set; }

        /// <summary>
        /// Gets or sets the cloud account.
        /// </summary>
        /// <value>The cloud account.</value>
        public string CloudAccount { get; set; }

        /// <summary>
        /// Gets or sets the cloud key.
        /// </summary>
        /// <value>The cloud key.</value>
        public string CloudKey { get; set; }
    }
}
