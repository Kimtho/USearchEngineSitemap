using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using Umbraco.Core;

namespace SearchEngineSitemap
{
	public class RouteHandler :IApplicationEventHandler
	{

		

		public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
		{
		
		}

		public void OnApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
		{
			
			RouteTable.Routes.MapRoute(
			   "sitemap",                                              // Route name
			   "Sitemap.xml",                           // URL with parameters
			   new { controller = "Sitemap", action = "Index" }  // Parameter defaults
		   );
			RouteTable.Routes.MapRoute(
		   "sitemapLang",                                              // Route name
		   "{lang}/Sitemap.xml",                           // URL with parameters
		   new { controller = "Sitemap", action = "Index" }  // Parameter defaults
	   );
			HostingEnvironment.RegisterVirtualPathProvider(new EmbeddedViewHandler());

			AreaRegistration.RegisterAllAreas();
		}

		public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
		{
			
		}
	}
}
