using FluentValidation;

namespace AIMS.Modules.OrganizationSetup.Models
{
    public class OrganizationSetUpValidator : AbstractValidator<OrganizationSetUp>
    {
        public OrganizationSetUpValidator()
        {
            RuleFor(x => x.StreetAddress).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.ZipCode).NotEmpty();
        }
    }

    public class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty()
                .Matches(@"^[0-9]{3}-[0-9]{3}-[0-9]{4}$")
                .WithMessage("Phone Number is not valid. Expected format is xxx-xxx-xxxx.");
            RuleFor(x => x.EmailAddress).NotEmpty().EmailAddress();
        }
    }

    public class BedValidator : AbstractValidator<Bed>
    {
        public BedValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithName("Bed type");
            RuleFor(x => x.LicensedCapacity).NotEmpty().GreaterThan(0);
        }
    }

    public class StaffValidator : AbstractValidator<Staff>
    {
        public StaffValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithName("Staff type");
        }
    }

    public class FuelValidator : AbstractValidator<Fuel>
    {
        public FuelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithName("Fuel type");
        }
    }

    public class SystemsValidator : AbstractValidator<System>
    {
        public SystemsValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithName("System type");
        }
    }

}