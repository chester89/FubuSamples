using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FubuSamples.Web.Handlers
{
    public class SparkController
    {
        public HelloViewModel get_spark_my_name_is_Name(NameModel input)
        {
            return new HelloViewModel() { Name = input.Name };
        }

        public FormModel GetLogin(InputModel input)
        {
            return new FormModel()
                       {
                           Login = "gleb",
                           Password = "12345"
                       };
        }

        public string PostLogin(FormModel model)
        {
            return string.Format("the data is {0}-{1}", model.Login, model.Password);
        }
    }

    public class InputModel
    {
    }

    public class FormModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class NameModel
    {
        public string Name { get; set; }
    }

    public class HelloViewModel
    {
        public string Name { get; set; } 
    }
}