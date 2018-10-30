using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Tranquillity.InMemory.Storage.Api.App_Start
{
    public class AllowOptionsHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Method == HttpMethod.Options)
            {
                var response = await base.SendAsync(request, cancellationToken);

                if (response.StatusCode == HttpStatusCode.MethodNotAllowed)
                {
                    response.StatusCode = HttpStatusCode.OK;
                }

                return response;
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}