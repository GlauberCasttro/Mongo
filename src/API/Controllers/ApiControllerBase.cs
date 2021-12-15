using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiControllerBase : ControllerBase
    {
        /// <summary>
        /// A <see cref="StatusCodeResult"/> Personalizando retorno 406
        /// </summary>
        [DefaultStatusCode(DefaultStatusCode)]
        public class NotAcceptedResult : StatusCodeResult
        {
            private const int DefaultStatusCode = StatusCodes.Status406NotAcceptable;

            /// <summary>
            /// Initializes a new <see cref="NotAcceptedResult"/> instance.
            /// </summary>
            public NotAcceptedResult()
                : base(DefaultStatusCode)
            {
            }
        }

        [DefaultStatusCode(DefaultStatusCode)]
        public class NotAcceptedObjectResult : ObjectResult
        {
            private const int DefaultStatusCode = StatusCodes.Status406NotAcceptable;

            /// <summary>
            /// Initializes a new <see cref="NotAcceptedResult"/> instance.
            /// </summary>
            public NotAcceptedObjectResult([ActionResultObjectValue] object? error)
                : base(error)
            {
                StatusCode = DefaultStatusCode;
            }
        }

        [NonAction]
        public NotAcceptedObjectResult NotAccepted([ActionResultObjectValue] object? error) => new(error);

        [NonAction]
        public NotAcceptedResult NotAccepted() => new();
    }
}