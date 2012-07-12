using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using FubuMVC.Core.View;
using HtmlTags;

namespace FubuSamples.Web
{
    public static class PageExtensions
    {
        public static HtmlTag PasswordFor<T>(this IFubuPage page, T model, Expression<Func<T, object>> expression) where T: class
        {
            var password = new PasswordTag();
            password.Attr("value", expression.Compile()(model));
            password.Attr("name", "Password");
            return password;
        }

        public static HtmlTag PasswordFor<T>(this IFubuPage<T> page, Expression<Func<T, object>> expression) where T : class
        {
            var password = new PasswordTag();
            password.Attr("value", expression.Compile()(page.Model));
            password.Attr("name", "Password");
            return password;
        }

        public static HtmlTag PasswordFor<T>(this IFubuPage page, Expression<Func<T, object>> expression) where T : class
        {
            var password = new PasswordTag();
            password.Attr("value", expression.Compile()(default(T)));
            password.Attr("name", "Password");
            return password;
        }
    }
}