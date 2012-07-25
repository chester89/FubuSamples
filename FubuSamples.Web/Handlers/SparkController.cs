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
    }

    public class InputModel
    {
    }

    public class FormOutputModel
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