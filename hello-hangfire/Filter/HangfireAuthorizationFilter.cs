using Hangfire.Dashboard;

namespace hello_hangfire.Filter
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return context.GetHttpContext().User.Identity.IsAuthenticated;
        }
    }
}