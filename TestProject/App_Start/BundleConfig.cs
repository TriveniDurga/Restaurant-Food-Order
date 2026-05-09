using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;
using System.Web.Optimization;
using TestProject.App_Start; // NUglifyJsMinify

namespace TestProject
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            Func<string, bool> FileExists = virtualPath =>
            {
                try
                {
                    var physical = HostingEnvironment.MapPath(virtualPath);
                    return !string.IsNullOrEmpty(physical) && File.Exists(physical);
                }
                catch
                {
                    return false;
                }
            };

            Func<string, bool> DirectoryExists = virtualPath =>
            {
                try
                {
                    var lastSlash = virtualPath.LastIndexOf('/');
                    var dirVirtual = lastSlash >= 0 ? virtualPath.Substring(0, lastSlash) : virtualPath;
                    var physicalDir = HostingEnvironment.MapPath(dirVirtual);
                    return !string.IsNullOrEmpty(physicalDir) && Directory.Exists(physicalDir);
                }
                catch
                {
                    return false;
                }
            };

            Action<string, string[]> AddScriptBundleIfAny = (bundlePath, includes) =>
            {
                var available = new List<string>();
                foreach (var inc in includes)
                {
                    if (inc.Contains("*") || inc.Contains("{"))
                    {
                        if (DirectoryExists(inc)) available.Add(inc);
                    }
                    else
                    {
                        if (FileExists(inc)) available.Add(inc);
                    }
                }

                if (available.Count > 0)
                {
                    var b = new ScriptBundle(bundlePath).Include(available.ToArray());
                    // use NUglify (robust modern JS minifier)
                    b.Transforms.Clear();
                    b.Transforms.Add(new NUglifyJsMinify());
                    bundles.Add(b);
                }
            };

            Action<string, string[]> AddStyleBundleIfAny = (bundlePath, includes) =>
            {
                var available = new List<string>();
                foreach (var inc in includes)
                {
                    if (inc.Contains("*") || inc.Contains("{"))
                    {
                        if (DirectoryExists(inc)) available.Add(inc);
                    }
                    else
                    {
                        if (FileExists(inc)) available.Add(inc);
                    }
                }

                if (available.Count > 0)
                {
                    var b = new StyleBundle(bundlePath).Include(available.ToArray());
                    // keep default CSS minifier (safe); you can replace if needed
                    bundles.Add(b);
                }
            };

            // Register bundles (prefer lib/* from LibMan, fallback to Scripts/Content patterns only if folders exist)
            AddScriptBundleIfAny("~/bundles/jquery", new[] {
                "~/lib/jquery/jquery.min.js",
                "~/Scripts/jquery-{version}.js"
            });

            AddScriptBundleIfAny("~/bundles/jqueryval", new[] {
                "~/lib/jquery-validate/jquery.validate.min.js",
                "~/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js",
                "~/Scripts/jquery.validate*"
            });

            AddScriptBundleIfAny("~/bundles/modernizr", new[] {
                "~/lib/modernizr/modernizr.js",
                "~/Scripts/modernizr-*"
            });

            AddScriptBundleIfAny("~/bundles/bootstrap", new[] {
                "~/lib/bootstrap/js/bootstrap.js",
                "~/Scripts/bootstrap.js"
            });

            AddStyleBundleIfAny("~/Content/css", new[] {
                "~/lib/bootstrap/css/bootstrap.css",
                "~/Content/bootstrap.css",
                "~/Content/site.css"
            });
        }
    }
}
