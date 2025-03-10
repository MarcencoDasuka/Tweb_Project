using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CarShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Маршрут для страницы деталей
            routes.MapRoute(
                name: "ProductDetails",
                url: "Product/Details/{id}",
                defaults: new { controller = "Product", action = "Details", id = UrlParameter.Optional }
            );

            // Основной маршрут по умолчанию
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
