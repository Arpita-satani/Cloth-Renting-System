using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace ClothRenting2
{
    public partial class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes); // Ye line routing enable karti hai
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}