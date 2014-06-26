using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using umbraco;
using Umbraco.Web;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

[assembly: WebResource("SearchEngineSitemap.Sitemap.cshtml", "cshtml")]
namespace SearchEngineSitemap
{
	
	public class SitemapController :UmbracoController
	{
	
		public ActionResult Index()
		{
			var help = new UmbracoHelper(UmbracoContext.Current);
			var url =Request.Url.AbsolutePath.Replace("sitemap.xml", "");
			
			
			var model = uQuery.GetNodeByUrl(url);

			return View("~/Views/Sitemap.cshtml", help.TypedContent(model.Id));
		}
	}
}
