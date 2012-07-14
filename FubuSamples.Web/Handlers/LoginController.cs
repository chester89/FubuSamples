using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FubuSamples.Web.Handlers
{
    public class LoginController
    {
        public FormModel Index()
        {
            return new FormModel()
                       {
                           Login = "Gleb",
                           Password = "qwerty"
                       };
        }

        public string Login(FormModel model)
        {
            return string.Format("Input was {0}, {1}", model.Login, model.Password);
        }
    }
}