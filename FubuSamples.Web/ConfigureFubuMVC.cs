using FubuMVC.Core;
using FubuMVC.Spark;

namespace FubuSamples.Web
{
    public class ConfigureFubuMVC : FubuRegistry
    {
        public ConfigureFubuMVC()
        {
            //Routes.HomeIs<MyHomeController>(x => x.Index());
            // This line turns on the basic diagnostics and request tracing
            IncludeDiagnostics(true);

            // All public methods from concrete classes ending in "Controller"
            // in this assembly are assumed to be action methods
            Actions.IncludeClassesSuffixedWithController();

            this.UseSpark();

            // Policies
            Routes
                .ConstrainToHttpMethod(x => x.Method.Name.Contains("Get"), "Get")
                .ConstrainToHttpMethod(x => x.Method.Name.Contains("Post"), "Post")
                .IgnoreControllerNamesEntirely()
                .IgnoreMethodSuffix("Html")
                .RootAtAssemblyNamespace();

            // Match views to action methods by matching
            // on model type, view name, and namespace
            Views.TryToAttachWithDefaultConventions();
        }
    }
}