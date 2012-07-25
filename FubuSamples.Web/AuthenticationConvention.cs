using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FubuMVC.Core.Registration;
using FubuSamples.Web.Handlers;

namespace FubuSamples.Web
{
    public class AuthenticationConvention: IConfigurationAction
    {
        public void Configure(BehaviorGraph graph)
        {
            graph
                .Actions()
                .Where(c => c.HasAttribute<SecureAttribute>())
                .Each(c => c.WrapWith<AuthenticationRequiredBehaviour>());
        }
    }
}