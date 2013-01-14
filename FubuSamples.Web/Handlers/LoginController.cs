using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FubuCore;
using FubuMVC.Core.Continuations;
using FubuMVC.Core.Security;

namespace FubuSamples.Web.Handlers
{
    public class LoginController
    {
        private readonly IAuthenticationContext authenticationContext;

        public LoginController(IAuthenticationContext authenticationContext)
        {
            this.authenticationContext = authenticationContext;
        }

        [Secure]
        public string SomeSecureStuff()
        {
            return "Hello secret dudes!";
        }

        public LoginOutputModel Login()
        {
            return new LoginOutputModel();
        }

        public FubuContinuation Login(LoginOutputModel model)
        {
            if (model.Login.Length > 2)
            {
                authenticationContext.ThisUserHasBeenAuthenticated(model.Login, false);
                if (!model.ReturnUrl.IsEmpty())
                {
                    return FubuContinuation.RedirectTo(model.ReturnUrl);
                }
            }
            return FubuContinuation.RedirectTo<LoginController>(c => c.Index());
            //return string.Format("You entered {0} and {1}", model.Login, model.Password);
        }

        public FormOutputModel Index()
        {
            return new FormOutputModel()
                       {
                           Login = "Gleb",
                           Password = "qwerty"
                       };
        }

        public string Login(FormOutputModel model)
        {
            return string.Format("Input was {0}, {1}", model.Login, model.Password);
        }
    }

    public class LoginOutputModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class SecureAttribute: Attribute
    {}
}