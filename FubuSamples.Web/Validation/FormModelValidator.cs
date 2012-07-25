using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using FubuSamples.Web.Handlers;

namespace FubuSamples.Web.Validation
{
    public class FormModelValidator: AbstractValidator<FormOutputModel>
    {
        public FormModelValidator()
        {
            RuleFor(m => m.Login).Length(5, 10).WithMessage("This login you entered is not valid");
            RuleFor(m => m.Password).Length(10, 20).WithMessage("Password should be longer than 10 characters and shorter than 20");
        }
    }
}