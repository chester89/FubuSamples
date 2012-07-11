using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FubuSamples.Web.Handlers
{
    public class LoginHandler
    {
         public FormModel Execute(InputModel input)
        {
            return new FormModel()
            {
                Login = "gleb",
                Password = "12345"
            };
        }
    }

    public class ShowHandler
    {
        public string Execute(FormModel model)
        {
            return string.Format("the data is {0}-{1}", model.Login, model.Password);
        }
    }

    public class AjaxResponse
    {
        public bool Success { get; set; }
        public object Item { get; set; }
    }
}