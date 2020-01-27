using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Infrastructure.ActionResults
{
    /// <summary>
    /// 
    /// </summary>
    public class InternalServerErrorObjectResult : ObjectResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        public InternalServerErrorObjectResult(object error)
         : base(error)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}