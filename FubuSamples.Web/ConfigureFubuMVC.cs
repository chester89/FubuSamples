using System.Linq;
using FluentValidation.Results;
using FubuMVC.Core;
using FubuMVC.Core.Runtime;
using FubuMVC.Spark;
using FubuSamples.Web.Handlers;
using FubuSamples.Web.Validation;
using HtmlTags;

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
            ApplyConvention<ValidationConfiguration>();
            this.UseSpark();
            Applies.ToThisAssembly();
            Output.ToJson.WhenCallMatches(action => action.Returns<AjaxResponse>());
            ApplyConvention<AuthenticationConvention>();

            //HandlerStyle();
            ControllerStyle();
            HtmlConventionsForValidation();
        }

        private void ControllerStyle()
        {
            Actions.IncludeTypesNamed(x => x.EndsWith("Controller"));
            Views.TryToAttach(findViews => findViews.by_ViewModel());
            Routes
                .IgnoreControllerNamespaceEntirely()
                .ConstrainToHttpMethod(x => !x.HasInput || x.InputType().Name.Contains("Input"), "Get")
                .ConstrainToHttpMethod(x => x.HasInput && x.InputType().Name.Contains("Output"), "Post");
        }

        private void HandlerStyle()
        {
            Actions
                .IncludeTypes(t => t.Namespace.StartsWith(typeof (HandlerUrlPolicy).Namespace) && t.Name.EndsWith("Handler"))
                .IncludeMethods(act => act.Name == "Execute");

            Routes.UrlPolicy<HandlerUrlPolicy>();
            Views.TryToAttach(findViews => findViews.by_ViewModel());
        }

        private void HtmlConventionsForValidation()
        {
            HtmlConvention(x => x.Editors.Always.Modify((request, tag) =>
                   {
                       var fubuRequest = request.Get<IFubuRequest>();
                       var validationResult = fubuRequest.Get<ValidationResult>();
                       if (validationResult.IsValid) return;
                       var ul = new HtmlTag("ul");
                       var liTags = validationResult.Errors.Where(error => error.PropertyName == request.Accessor.InnerProperty.Name).Select(vf => new HtmlTag("li", li => li.Text(vf.ErrorMessage)));
                       ul.Append(liTags);
                       tag.Append(ul);
                   }));
        }
    }
}