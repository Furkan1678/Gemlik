using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace IssueTrackerPro.API.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // JWT doğrulama burada yapılabilir, ancak genelde built-in middleware kullanılır
            await _next(context);
        }
    }
}