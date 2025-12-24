using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace BulletinBoard.Web.Services
{
    public class JwtAuthorizationHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var jwtToken = _httpContextAccessor.HttpContext?.User.FindFirst("JwtToken")?.Value;

            if (!string.IsNullOrEmpty(jwtToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
