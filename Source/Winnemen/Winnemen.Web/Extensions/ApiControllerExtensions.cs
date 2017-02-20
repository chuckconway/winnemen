using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Results;
using Winnemen.Core.Extensions;
using Winnemen.Notifications;
using Winnemen.ValueObjects;

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

            var payload = CreateResponsePayload(result);
            payload.Succeeded = true;

            var response = controller.Request.CreateResponse(HttpStatusCode.OK, payload);

            return new ResponseMessageResult(response);
        }

        /// <summary>
        /// Oks the specified controller.
        /// </summary>
        /// <typeparam name="TResult">The type of the t result.</typeparam>
        /// <param name="controller">The controller.</param>
        /// <param name="result">The result.</param>
        /// <returns>IHttpActionResult.</returns>
        public static IHttpActionResult Success<TResult>(this ApiController controller, TResult result) where TResult : class
        {
            var payload = CreateResponsePayload(result);
            payload.Succeeded = true;

            var response = controller.Request.CreateResponse(HttpStatusCode.OK, payload);
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
            var payload = CreateResponsePayload(new object());
            payload.Succeeded = true;

            var response = controller.Request.CreateResponse(HttpStatusCode.OK, payload);
            return new ResponseMessageResult(response);
        }

        ///// <summary>
        ///// Oks the specified controller.
        ///// </summary>
        ///// <param name="controller">The controller.</param>
        ///// <param name="errors">The errors.</param>
        ///// <returns>IHttpActionResult.</returns>
        //public static IHttpActionResult Error(this ApiController controller, IEnumerable<string> errors)
        //{
        //    var error = controller.Request.CreateResponse(HttpStatusCode.BadRequest, new ResponseResult<object>
        //    {
        //        Succeeded = false,
        //        Errors = errors,
        //        Version = AssemblyVersion
        //    });

        //    return new ResponseMessageResult(error);
        //}

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
           return Error(controller, new object(), errors, HttpStatusCode.Unauthorized);

            //var payload = CreateResponsePayload(new object());
            //payload.Succeeded = false;

            //var response = controller.Request.CreateResponse(HttpStatusCode.Unauthorized, new ResponseResult<object>
            //{
            //    Succeeded = false,
            //    Errors = errors,
            //    Version = AssemblyVersion
            //});

            //return new ResponseMessageResult(response);
        }

        public static IHttpActionResult Response<T>(this ApiController controller, T model, Action<ResponseResult<T>> interceptPayload = null) where T : class
        {
            var payload = CreateResponsePayload(model);

            //if there is an instance invoke the payload
            interceptPayload?.Invoke(payload);

            return payload.Notifications.Any(s => s.NotificationType == NotificationType.Error) ? Error(controller, payload) : Success(controller, payload);
        }

        public static IHttpActionResult Success<T>(this ApiController controller, T model, Action<ResponseResult<T>> interceptPayload = null) where T : class
        {
            var payload = CreateResponsePayload(model);
            payload.Succeeded = true;

            //if there is an instance invoke the payload
            interceptPayload?.Invoke(payload);
            var response = controller.Request.CreateResponse(HttpStatusCode.OK, payload);

            return new ResponseMessageResult(response); //new Json() new JsonResult(payload); // controller..Json(payload);
        }

        private static ResponseResult<T> CreateResponsePayload<T>(T model, IList<INotification> notifications = null) where T : class
        {
            var vm = new ResponseResult<T> { Result = model, Version = AssemblyVersion };

            //Merge the notifications on the Model and the passed in notifications
            if (model is INotifications || (notifications != null))
            {
                var defaultNotification = notifications ?? new List<INotification>();
                var extractedNotifications = model as INotifications;

                if (extractedNotifications != null)
                {
                    foreach (var item in extractedNotifications.Notifications)
                    {
                        defaultNotification.Add(item);
                    }

                    ((INotifications)model).Notifications = null;

                }

                vm.Notifications = defaultNotification;
            }

            return vm;
        }

        //public static IHttpActionResult Success(this ApiController controller)
        //{
        //    return Success<object>(controller, new { });
        //}

        public static IHttpActionResult Error<T>(this ApiController controller, T model, IEnumerable<string> errors, HttpStatusCode httpStatus = HttpStatusCode.BadRequest) where T : class
        {
            var messages = errors.Select(error => new ErrorNotification
            {
                Message = error
            }).Cast<INotification>().ToList();

            var payload = CreateResponsePayload(model, messages);
            payload.Succeeded = false;

            var response = controller.Request.CreateResponse(httpStatus, payload);

            return new ResponseMessageResult(response);
        }

        public static IHttpActionResult Error<T>(this ApiController controller, T model) where T : class
        {
            return Error(controller, model, new string[0]);
        }

        public static IHttpActionResult Error(this ApiController controller, params string[] errors)
        {
            return Error<object>(controller, null, errors);
        }

    }
}