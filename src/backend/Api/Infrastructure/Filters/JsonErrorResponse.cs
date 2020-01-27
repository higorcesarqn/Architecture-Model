using System.Collections.Generic;

namespace Api.Infrastructure.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonErrorResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public JsonErrorResponse()
        {
            Messages = new Dictionary<string, IEnumerable<string>>();
        }

        /// <summary>
        /// 
        /// </summary>
        public IDictionary<string, IEnumerable<string>> Messages { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public object DeveloperMeesage { get; set; }
    }
}