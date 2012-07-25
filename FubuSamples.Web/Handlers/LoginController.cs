using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FubuMVC.Core.Continuations;
using FubuMVC.Core.Security;
using FubuMVC.Core.Urls;

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

        public string Login(LoginOutputModel model)
        {
            if (model.Login.Length > 2)
            {
                authenticationContext.ThisUserHasBeenAuthenticated(model.Login, false);
            }
            return string.Format("You entered {0} and {1}", model.Login, model.Password);
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
    }

    public class SecureAttribute: Attribute
    {}
}