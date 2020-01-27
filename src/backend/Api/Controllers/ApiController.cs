using System.Collections.Generic;
using System.Linq;
using Core.Extensions;
using Core.Notifications;
using Core.PagedList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Features;
using Tango.Types;

namespace Api.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        private readonly DomainNotificationHandler _notifications;
        private IActionResult MethodWhenSome(object obj) => IsValidOperation() ? Ok(obj) : ResponseBadRequest();
        private IActionResult MethodWhenNone() { return IsValidOperation() ? NotFound() : ResponseBadRequest(); }
        private IActionResult MethodWhenSome(string uri, object obj) => IsValidOperation() ? Created(uri, obj) : ResponseBadRequest();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notifications"></param>
        protected ApiController(INotificationHandler<DomainNotification> notifications)
        {
            _notifications = (DomainNotificationHandler)notifications;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        protected bool IsValidOperation()
        {
            return !_notifications.HasNotifications();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        protected IActionResult ResponseBadRequest()
        {
            return BadRequest(
                _notifications
                .GetNotifications()
                .GroupBy(x => x.Key.ToLower().Split(".")[0])
                .ToDictionary(k => k.Key, v => v.Select(s => s.Value))
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        protected new IActionResult Response(Option<object> result)
        {
            return result.Match(MethodWhenSome, MethodWhenNone);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="featureCollection"></param>
        /// <returns></returns>
        protected new IActionResult Response(FeatureCollection featureCollection)
        {
            return Ok(featureCollection);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        protected IActionResult ResponseCreated(string uri, Option<object> result)
        {
            return result.Match(value => MethodWhenSome(uri, value), MethodWhenNone);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagedList"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        protected new IActionResult Response<T>(IPagedList<T> pagedList)
        {
            return pagedList.Match(MethodWhenSome, MethodWhenNone);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        protected new IActionResult Response<T>(IEnumerable<T> enumerable)
        {
            return enumerable.Math(MethodWhenSome, MethodWhenNone);
        }
    }
}