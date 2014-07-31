using FluentValidation;

namespace AIMS.Modules.StatusUpdate.Models
{
    public class StatusUpdateValidator : AbstractValidator<StatusUpdate>
    {
        public StatusUpdateValidator()
        {
            RuleFor(x => x.Status).NotEmpty().WithName("Facility status");
        }

        public class StaffValidator : AbstractValidator<Staff>
        {
            public StaffValidator()
            {
                RuleFor(x => x.Status).NotEmpty().WithName("Staff status");
            }
        }

        public class SystemValidator : AbstractValidator<System>
        {
            public SystemValidator()
            {
                RuleFor(x => x.Status).NotEmpty().WithName("System status");
            }
        }

        public class FuelValidator : AbstractValidator<Fuel>
        {
            public FuelValidator()
            {
                RuleFor(x => x.Status).NotEmpty().WithName("Fuel status");
            }
        }
    }
}