using System.Linq;
using System.Net;
using FluentValidation;
using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Continuations;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Runtime;

namespace FubuSamples.Web.Validation
{
    public class ValidationBehaviour<T> : BasicBehavior where T : class
    {
        private readonly IContinuationDirector continuationDirector;
        private readonly BehaviorGraph behaviorGraph;
        private readonly IFubuRequest fubuRequest;
        private readonly IValidator<T> validator;

        public override string ToString()
        {
            return "Validation behaviour for FluentValidation";
        }

        public ValidationBehaviour(IContinuationDirector continuationDirector, BehaviorGraph behaviorGraph, IFubuRequest fubuRequest, IValidator<T> validator)
            : base(PartialBehavior.Ignored)
        {
            this.continuationDirector = continuationDirector;
            this.behaviorGraph = behaviorGraph;
            this.fubuRequest = fubuRequest;
            this.validator = validator;
        }

        protected override DoNext performInvoke()
        {
            var inputModel = fubuRequest.Get<T>();
            var validationResult = validator.Validate(inputModel);
            if (validationResult.IsValid)
            {
                return DoNext.Continue;
            }
            fubuRequest.Set(validationResult);
            var actionCall = GetActionCallFromBehaviorGraph();
            continuationDirector.TransferToCall(actionCall);
            return DoNext.Stop;
        }

        private ActionCall GetActionCallFromBehaviorGraph()
        {
            return behaviorGraph
                .Behaviors
                .Where(chain => chain.ActionOutputType() == typeof(T)/* && chain.Route.AllowedHttpMethods.Contains(WebRequestMethods.Http.Get)*/)
                .Select(chain => chain.FirstCall())
                .FirstOrDefault();
        }
    }
}