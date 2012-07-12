using FubuMVC.Core;
using FubuMVC.Spark;
using FubuSamples.Web.Handlers;

namespace FubuSamples.Web
{
    public class ConfigureFubuMVC : FubuRegistry
    {
        public ConfigureFubuMVC()
        {
            //// This line turns on the basic diagnostics and request tracing
            //IncludeDiagnostics(true);

            //// All public methods from concrete classes ending in "Controller"
            //// in this assembly are assumed to be action methods
            //Actions.IncludeClassesSuffixedWithController();

            //this.UseSpark();

            //// Policies
            //Routes
            //    .ConstrainToHttpMethod(x => x.Method.Name.Contains("Get"), "Get")
            //    .ConstrainToHttpMethod(x => x.Method.Name.Contains("Post"), "Post")
            //    .IgnoreControllerNamesEntirely()
            //    .IgnoreMethodSuffix("Html")
            //    .RootAtAssemblyNamespace();

            //// Match views to action methods by matching
            //// on model type, view name, and namespace
            //Views.TryToAttachWithDefaultConventions();
            IncludeDiagnostics(true);
            this.UseSpark();
            Applies.ToThisAssembly();
            Output.ToJson.WhenCallMatches(action => action.Returns<AjaxResponse>());

            //HandlerStyle();
            ControllerStyle();
        }

        private void ControllerStyle()
        {
            Actions.IncludeTypesNamed(x => x.EndsWith("Controller"));
            Views.TryToAttach(findViews => findViews.by_ViewModel());
            Routes
                .IgnoreControllerNamespaceEntirely()
                .ConstrainToHttpMethod(x => !x.HasInput, "Get")
                .ConstrainToHttpMethod(x => x.HasInput, "Post");
        }

        private void HandlerStyle()
        {
            Actions
                .IncludeTypes(t => t.Namespace.StartsWith(typeof (HandlerUrlPolicy).Namespace) && t.Name.EndsWith("Handler"))
                .IncludeMethods(act => act.Name == "Execute");

            Routes.UrlPolicy<HandlerUrlPolicy>();
            Views.TryToAttach(findViews => findViews.by_ViewModel());
        }
    }
}