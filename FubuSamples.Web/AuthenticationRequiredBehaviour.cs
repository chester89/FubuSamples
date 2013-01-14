using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Http;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.Security;
using FubuMVC.Core.Urls;
using FubuSamples.Web.Handlers;

namespace FubuSamples.Web
{
    public class AuthenticationRequiredBehaviour: BasicBehavior
    {
        private readonly ISecurityContext securityContext;
        private readonly ICurrentHttpRequest httpRequest;
        private readonly IUrlRegistry urlRegistry;
        private readonly IOutputWriter outputWriter;

        public AuthenticationRequiredBehaviour(ISecurityContext securityContext, ICurrentHttpRequest httpRequest, IUrlRegistry urlRegistry, IOutputWriter outputWriter): 
            base(PartialBehavior.Ignored)
        {
            this.securityContext = securityContext;
            this.httpRequest = httpRequest;
            this.urlRegistry = urlRegistry;
            this.outputWriter = outputWriter;
        }

        protected override DoNext performInvoke()
        {
            if (securityContext.IsAuthenticated())
            {
                return DoNext.Continue;
            }

            var loginModel = new LoginOutputModel();
            //if user didn't specifically type login url in a browser - it's a redirect
            if (httpRequest.RawUrl() != urlRegistry.UrlFor<LoginOutputModel>())
            {
                loginModel.ReturnUrl = httpRequest.RawUrl();
            }
            outputWriter.RedirectToUrl(urlRegistry.UrlFor(loginModel));
            return DoNext.Stop;
        }
    }
}