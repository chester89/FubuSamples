using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.Security;
using FubuMVC.Core.Urls;
using FubuSamples.Web.Handlers;

namespace FubuSamples.Web
{
    public class AuthenticationRequiredBehaviour: BasicBehavior
    {
        private readonly ISecurityContext securityContext;
        private readonly IUrlRegistry urlRegistry;
        private readonly IOutputWriter outputWriter;

        public AuthenticationRequiredBehaviour(ISecurityContext securityContext, IUrlRegistry urlRegistry, IOutputWriter outputWriter): 
            base(PartialBehavior.Ignored)
        {
            this.securityContext = securityContext;
            this.urlRegistry = urlRegistry;
            this.outputWriter = outputWriter;
        }

        protected override DoNext performInvoke()
        {
            if (securityContext.IsAuthenticated())
            {
                return DoNext.Continue;
            }
            var url = urlRegistry.UrlFor<LoginOutputModel>();
            outputWriter.RedirectToUrl(url);
            return DoNext.Stop;
        }
    }
}