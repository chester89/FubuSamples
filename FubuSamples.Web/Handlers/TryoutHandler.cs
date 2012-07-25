using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FubuSamples.Web.Handlers
{
    public class LoginHandler
    {
         public FormOutputModel Execute(InputModel input)
        {
            return new FormOutputModel()
            {
                Login = "gleb",
                Password = "12345"
            };
        }
    }

    public class ShowHandler
    {
        public string Execute(FormOutputModel model)
        {
            return string.Format("the data is {0}-{1}", model.Login, model.Password);
        }
    }
}