using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using StructureMap;

namespace FubuSamples.Web.Validation
{
    public class ValidationConfiguration : IConfigurationAction
    {
        public void Configure(BehaviorGraph graph)
        {
            graph.Actions()
                .Where(x => x.HasInput/* && ObjectFactory.Model.HasDefaultImplementationFor(typeof(IValidator<>).MakeGenericType(x.InputType()))*/)
                .Each(x => x.AddBefore(new Wrapper(typeof(ValidationBehaviour<>).MakeGenericType(x.InputType()))));
        }
    }
}