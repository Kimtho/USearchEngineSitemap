using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Hosting;

namespace SearchEngineSitemap
{
	internal class EmbeddedViewHandler : VirtualPathProvider
	{
		// Nested class representing the "virtual file"
		public class EmbeddedVirtualFile : VirtualFile
		{
			private Stream _stream;

			public EmbeddedVirtualFile(string virtualPath,
				Stream stream) : base(virtualPath)
			{
				if (null == stream)
					throw new ArgumentNullException("stream");

				_stream = stream;
			}

			public override Stream Open()
			{
				return _stream;
			}
		}

		public EmbeddedViewHandler()
		{
		}

		public override CacheDependency GetCacheDependency(
			string virtualPath,
			IEnumerable virtualPathDependencies,
			DateTime utcStart)
		{
			string embedded = _GetEmbeddedPath(virtualPath);

			// not embedded? fall back
			if (string.IsNullOrEmpty(embedded))
				return base.GetCacheDependency(virtualPath,
					virtualPathDependencies, utcStart);

			// there is no cache dependency for embedded resources
			return null;
		}

		public override bool FileExists(string virtualPath)
		{
			string embedded = _GetEmbeddedPath(virtualPath);

			// You can override the embed by placing a real file
			// at the virtual path...
			return base.FileExists(virtualPath)
			       || !string.IsNullOrEmpty(embedded);
		}

		public override VirtualFile GetFile(string virtualPath)
		{
			// You can override the embed by placing a real file
			// at the virtual path...
			if (base.FileExists(virtualPath))
				return base.GetFile(virtualPath);

			string embedded = _GetEmbeddedPath(virtualPath);

			// sanity...
			if (string.IsNullOrEmpty(embedded))
				return null;

			return new EmbeddedVirtualFile(virtualPath,
				GetType().Assembly
					.GetManifestResourceStream(embedded));
		}

		private string _GetEmbeddedPath(string path)
		{
			// ~/views/sample/x.cshtml
			// => /views/sample/x.cshtml
			// => FunWithMvc.views.sample.x.cshtml

			if (path.StartsWith("~/"))
				path = path.Substring(1);

			//path = path.ToLowerInvariant();
			path = "SearchEngineSitemap" + path.Replace('/', '.');

			// this makes sure the "virtual path" exists as an
			// embedded resource
			return GetType().Assembly.GetManifestResourceNames()
				.Where(o => o == path).FirstOrDefault();
		}
		public string GetEmbeddedPath2(string path)
		{
			// ~/views/sample/x.cshtml
			// => /views/sample/x.cshtml
			// => FunWithMvc.views.sample.x.cshtml

			if (path.StartsWith("~/"))
				path = path.Substring(1);

			path = path.ToLowerInvariant();
			path = "SearchEngineSitemap" + path.Replace('/', '.');

			// this makes sure the "virtual path" exists as an
			return path;
		}
	}


}
