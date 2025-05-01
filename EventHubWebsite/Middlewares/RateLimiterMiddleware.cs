

using System.Threading.RateLimiting;

namespace EventHubWebsite.Middlewares
{
    public class RateLimiterMiddleware : IMiddleware
    {
        private readonly RateLimiter rateLimiter = new TokenBucketRateLimiter(new()
        {
            AutoReplenishment = true,
            TokenLimit = 10,
            TokensPerPeriod = 10,
            ReplenishmentPeriod = TimeSpan.FromSeconds(10)
        });

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            using var lease = rateLimiter.AttemptAcquire();
            if (lease.IsAcquired)
            {
                await next(context);
            }
            else 
            {
                ReturnErrorToClient(context);
            }

        }


        private void ReturnErrorToClient(HttpContext context) 
        {
            context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            context.Response.Headers.Append("Retry After", "10");
        }
    }
}
        
