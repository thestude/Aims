using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AIMS.Modules.OrganizationSetup.Models
{
    public class OrganizationSetUp
    {
        public string OrganizationId { get; set; }
        [Display(Name = "Organization Name")]
        public string OrganizationName { get; set; }
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        public List<Contact> Contacts { get; set; }
        public List<Bed> Beds { get; set; }
        public List<Staff> Staff { get; set; }
        public string[] SelectedStaffTypes {
            get { return (this.Staff.Any()) ? this.Staff.Select(s => s.Name).ToArray() : null; }
        }
        public List<System> Systems { get; set; }
        public string[] SelectedSystemsTypes
        {
            get { return (this.Systems.Any()) ? this.Systems.Select(s => s.Name).ToArray() : null; }
        }
        public List<Fuel> Fuels { get; set; }
        public string[] SelectedFuelTypes
        {
            get { return (this.Fuels.Any()) ? this.Fuels.Select(s => s.Name).ToArray() : null; }
        }
        public IList<ResourceType> BedTypes { get; set; }
        public IList<ResourceType> StaffTypes { get; set; }
        public IList<ResourceType> SystemsTypes { get; set; }
        public IList<ResourceType> FuelTypes { get; set; }
    }

    public class Capabilities
    {
        public List<Bed> Beds { get; set; }
        public List<Staff> Staff { get; set; }
        public List<System> Systems { get; set; }
        public List<Fuel> Fuels { get; set; }
    }

    public class Contact
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
    }

    public class Bed
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int? LicensedCapacity { get; set; }
    }

    public class Staff
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class System
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class Fuel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class ResourceType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
    }

}