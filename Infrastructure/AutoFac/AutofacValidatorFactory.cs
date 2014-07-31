using System;
using Autofac;
using FluentValidation;

namespace AIMS.Infrastructure.AutoFac
{
    public class AutofacValidatorFactory : ValidatorFactoryBase
    {
        private readonly IContainer _container;

        public AutofacValidatorFactory(IContainer container)
        {
            this._container = container;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            var validator = _container.ResolveOptionalKeyed<IValidator>(validatorType);
            return validator;
        }
    }
}