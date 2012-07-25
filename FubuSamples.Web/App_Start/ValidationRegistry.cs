using FluentValidation;
using FubuMVC.Core.Continuations;
using FubuSamples.Web.Handlers;
using FubuSamples.Web.Validation;
using StructureMap.Configuration.DSL;

namespace FubuSamples.Web.App_Start
{
    public class ValidationRegistry: Registry
    {
        public ValidationRegistry()
        {
            For<IValidator<FormOutputModel>>().Use<FormModelValidator>();
            For<IContinuationDirector>().Use<ContinuationHandler>();
            //Scan(sc =>
            //         {
            //             sc.TheCallingAssembly();
            //             sc.WithDefaultConventions();
            //             sc.ConnectImplementationsToTypesClosing(typeof (IValidator<>));
            //         });
        }
    }
}