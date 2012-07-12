using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlTags;

namespace FubuSamples.Web
{
    public class PasswordTag: TextboxTag
    {
        public PasswordTag()
        {
            this.Attr("type", "password");
        }
    }
}