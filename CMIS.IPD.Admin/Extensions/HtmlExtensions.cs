using Microsoft.AspNetCore.Mvc.Rendering;

namespace IPD.Admin.Extensions
{
    public static class HtmlExtensions
    {
        public static bool ShouldShowAdmission(this IHtmlHelper htmlHelper)
        {
            var routeData = htmlHelper.GetRouteData();
            var allowControllers = new List<Tuple<string, string>>
            {
                new("Clients", "ManageClient")
            };

            return allowControllers.Contains(routeData);
        }

        public static bool ShouldShowClientManagement(this IHtmlHelper htmlHelper)
        {
            var routeData = htmlHelper.GetRouteData();
            var excludeControllers = new List<Tuple<string, string>>
            {
                new("Admissions", "Index"), new("Clients", "ManageClient")
            };

            return !excludeControllers.Contains(routeData);
        }

        private static Tuple<string, string> GetRouteData(this IHtmlHelper htmlHelper)
        {
            var routeData = htmlHelper.ViewContext.RouteData;
            var routeDateValues = routeData.Values;

            var routeAction = (routeDateValues["action"] ?? string.Empty).ToString();
            var routeController = (routeDateValues["controller"] ?? string.Empty).ToString();

            return new Tuple<string, string>(routeController ?? string.Empty, routeAction ?? string.Empty);
        }
    }
}
