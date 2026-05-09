using System.Web.Optimization;
using NUglify;

namespace TestProject.App_Start
{
    public class NUglifyJsMinify : IBundleTransform
    {
        public void Process(BundleContext context, BundleResponse response)
        {
            if (string.IsNullOrWhiteSpace(response.Content))
            {
                return;
            }

            var result = Uglify.Js(response.Content);
            if (!result.HasErrors)
            {
                response.Content = result.Code;
            }
            // keep ContentType for scripts
            response.ContentType = "text/javascript";
        }
    }
}