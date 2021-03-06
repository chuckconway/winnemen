﻿using System.Collections.Generic;
using Winnemen.Notifications;

namespace Winnemen.Web
{
    public class ResponseResult<T>
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ResponseResult{T}"/> is succeeded.
        /// </summary>
        /// <value><c>true</c> if succeeded; otherwise, <c>false</c>.</value>
        public bool Succeeded { get; set; }

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        /// <value>The errors.</value>
       // public IEnumerable<string> Errors { get; set; }

        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>The result.</value>
        public T Result { get; set; }

        public dynamic DataBag { get; set; }

        public IList<INotification> Notifications { get; set; }
    }
}
