using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Results;
using Winnemen.Core.Extensions;

namespace Winnemen.Web.Extensions
{
    public static class ApiControllerExtensions
    {
        private static readonly string AssemblyVersion = GetVersion();
        
        private static string GetVersion()
        {
            var assembly = Assembly.GetExecutingAssembly().GetName();
            var version = assembly.Version;

            if (version.IsNotNull())
            {
                return version.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// Oks the specified controller.
        /// </summary>
        /// <typeparam name="TResult">The type of the t result.</typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="controller">The controller.</param>
        /// <param name="result">The result.</param>
        /// <param name="source"></param>
        /// <returns>IHttpActionResult.</returns>
        public static IHttpActionResult Success<TSource, TDestination>(this ApiController controller, TSource source) 
            where TSource : class
            where TDestination : class
        {
            var result = source.Map<TSource, TDestination>();

            var response = controller.Request.CreateResponse(HttpStatusCode.OK, new ResponseResult<TDestination>
            {
                Succeeded = true,
                Result = result,
                Version = AssemblyVersion
            });

            return new ResponseMessageResult(response);
        }

        /// <summary>
        /// Oks the specified controller.
        /// </summary>
        /// <typeparam name="TResult">The type of the t result.</typeparam>
        /// <param name="controller">The controller.</param>
        /// <param name="result">The result.</param>
        /// <returns>IHttpActionResult.</returns>
        public static IHttpActionResult Success<TResult>(this ApiController controller, TResult result)
        {
            var response = controller.Request.CreateResponse(HttpStatusCode.OK, new ResponseResult<TResult>
            {
                Succeeded = true,
                Result = result,
                Version = AssemblyVersion
            });

            return new ResponseMessageResult(response);
        }


        /// <summary>
        /// Oks the specified controller.
        /// </summary>
        /// <typeparam name="TResult">The type of the t result.</typeparam>
        /// <param name="controller">The controller.</param>
        /// <param name="result">The result.</param>
        /// <returns>IHttpActionResult.</returns>
        public static IHttpActionResult Success(this ApiController controller)
        {
            var response = controller.Request.CreateResponse(HttpStatusCode.OK, new ResponseResult<object>
            {
                Succeeded = true,
                Version = AssemblyVersion
            });

            return new ResponseMessageResult(response);
        }

        /// <summary>
        /// Oks the specified controller.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="errors">The errors.</param>
        /// <returns>IHttpActionResult.</returns>
        public static IHttpActionResult Error(this ApiController controller, IEnumerable<string> errors)
        {
            var error = controller.Request.CreateResponse(HttpStatusCode.BadRequest, new ResponseResult<object>
            {
                Succeeded = false,
                Errors = errors,
                Version = AssemblyVersion
            });

            return new ResponseMessageResult(error);
        }

        public static IHttpActionResult SendDocument(this ApiController controller, string filename, string mimeType, byte[] bytes)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(bytes)
            };

            result.Content.Headers.ContentType = new MediaTypeHeaderValue(mimeType);
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = filename
            };

            return new ResponseMessageResult(result);
        }

        /// <summary>
        /// Oks the specified controller.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="errors">The errors.</param>
        /// <returns>IHttpActionResult.</returns>
        public static IHttpActionResult InvalidLogin(this ApiController controller, IEnumerable<string> errors)
        {
            var response = controller.Request.CreateResponse(HttpStatusCode.Unauthorized, new ResponseResult<object>
            {
                Succeeded = false,
                Errors = errors,
                Version = AssemblyVersion
            });

            return new ResponseMessageResult(response);
        }

    }
}