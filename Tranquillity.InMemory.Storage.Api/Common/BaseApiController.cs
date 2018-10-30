using System.Net;
using System.Web.Http;

namespace Tranquillity.InMemory.Storage.Api.Common
{
    public class Response
    {
        /// <summary>
        /// Indicates if the response contains a result
        /// </summary>
        public virtual bool HasResult
        {
            get
            {
                return (this.Result != null);
            }
        }

        /// <summary>
        /// The result of the response
        /// </summary>
        public object Result { get; set; }

        /// <summary>
        /// Indicates if the response is successful or not. Warning or success result type indicate success
        /// </summary>
        public bool Successful
        {
            get
            {
                return (string.IsNullOrEmpty(ErrorMessage));
            }
        }

        /// <summary>
        /// The message returned with the response
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Creates a successful response with a given result object
        /// </summary>
        /// <returns>The response object</returns>
        public static Response Success(object result)
        {
            var response = new Response { Result = result };

            return response;
        }

        /// <summary>
        /// Creates a failed result. It takes no result object
        /// </summary>
        /// <param name="errorMessage">The error message returned with the response</param>
        /// <returns>The created response object</returns>
        public static Response Failed(string errorMessage)
        {
            var response = new Response { ErrorMessage = errorMessage };

            return response;
        }

    }

        public class BaseApiController : ApiController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseApiController"/> class. 
        /// </summary>
        public BaseApiController() { }

        /// <summary>
        /// Method that creates a <see cref="IHttpActionResult"/> based on the content of <see cref="Response"/>
        /// </summary>
        /// <param name="response">The api reponse wrapper</param>
        /// <returns>The IHttpActionResult created</returns>
        protected virtual IHttpActionResult CreateHttpResponse(Response response)
        {
            if (response.Successful)
            {
                if (response.HasResult)
                {
                    return this.Ok(response.Result);
                }

                return this.StatusCode(HttpStatusCode.NoContent);
            }

            return this.BadRequest(response.ErrorMessage);
        }
    }
}