using System;
using System.Collections.Generic;

namespace AIMS.Modules.StatusUpdate.Models
{
    public class StatusUpdate
    {
        public Guid OrganizationId { get; set; }
        public bool OnGenerator { get; set; }
        public string Status { get; set; }
        public int? ProjectedIba { get; set; }
        public string StatusNote { get; set; }
        public DateTime? StatusCreatedOn { get; set; }
        public DateTime? StatusLastUpdatedOn { get; set; }
        public IEnumerable<Bed> Beds { get; set; }
        public IEnumerable<Staff> Staff { get; set; }
        public IEnumerable<System> Systems { get; set; }
        public IEnumerable<Fuel> Fuels { get; set; }
    }

    public class Bed
    {
        public Guid Id { get; set; }
        public Guid TypeId { get; set; }
        public string Type { get; set; }
        public int CurrentCapacity { get; set; }
        public int InUse { get; set; }
        public int Available { get; set; }
        public string Notes { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
    }

    public class Staff
    {
        public Guid Id { get; set; }
        public Guid TypeId { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public int AmountShort { get; set; }
        public string Notes { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
    }

    public class System
    {
        public Guid Id { get; set; }
        public Guid TypeId { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
    }

    public class Fuel
    {
        public Guid Id { get; set; }
        public Guid TypeId { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public int AmountShort { get; set; }
        public string Measurement { get; set; }
        public string Notes { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
    }
}