using System;
using System.Collections.Generic;

namespace AIMS.Modules.FacilitySetup.Models
{
    public class FacilitySetUp
    {
        public string FacilityName { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public List<Contact> Contacts { get; set; }
        public List<Bed> Beds { get; set; }
        public List<Staff> Staff { get; set; }
        public List<System> Systems { get; set; }
        public List<Fuel> Fuels { get; set; }
    }

    public class Contact
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
    }

    public class Bed
    {
        public Guid TypeId { get; set; }
        public string Type { get; set; }
        public int LicensedCapacity { get; set; }
    }

    public class Staff
    {
        public Guid TypeId { get; set; }
        public string Type { get; set; }
    }

    public class System
    {
        public Guid TypeId { get; set; }
        public string Type { get; set; }
    }

    public class Fuel
    {
        public Guid TypeId { get; set; }
        public string Type { get; set; }
    }

}