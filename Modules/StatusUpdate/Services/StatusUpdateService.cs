using System;
using System.Collections.Generic;
using System.Linq;
using AIMS.Models;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace AIMS.Modules.StatusUpdate.Services
{
    public interface IStatusUpdateService
    {
        Models.StatusUpdate GetCurrentStatus(Guid organizationId);
        Models.StatusUpdate UpdateStatus(Models.StatusUpdate statusUpdate);
    }

    public class StatusUpdateService : IStatusUpdateService
    {
        private readonly ISession _session;

        public StatusUpdateService(ISession session)
        {
            _session = session;
        }

        public Models.StatusUpdate GetCurrentStatus(Guid organizationId)
        {
            var statusUpdate = new Models.StatusUpdate {OrganizationId = organizationId};

            var facilityStatus = _session.QueryOver<AIMS.Models.FacilityStatus>()
                .Where(f => f.Facility.ID == organizationId).SingleOrDefault();
            if (facilityStatus != null)
            {
                statusUpdate.OnGenerator = facilityStatus.OnGenerator;
                statusUpdate.ProjectedIba = facilityStatus.ProjectedIba;
                statusUpdate.Status = facilityStatus.Status;
                statusUpdate.StatusNote = facilityStatus.Notes;
                statusUpdate.StatusCreatedOn = facilityStatus.CreatedOn;
                statusUpdate.StatusLastUpdatedOn = facilityStatus.LastUpdateOn;
            }
            statusUpdate.Beds = GetBeds(organizationId);
            statusUpdate.Staff = GetStaff(organizationId);
            statusUpdate.Systems = GetSystems(organizationId);
            statusUpdate.Fuels = GetFuels(organizationId);
         
            return statusUpdate;
        }

        private void UpdateFacilityStatus(Models.StatusUpdate statusUpdate)
        {
            var facilityStatus = _session.QueryOver<AIMS.Models.FacilityStatus>()
                .Where(f => f.Facility.ID == statusUpdate.OrganizationId).SingleOrDefault();

            // check if it exists and update else create a new one
            if (facilityStatus != null)
            {
                facilityStatus.OnGenerator = statusUpdate.OnGenerator;
                facilityStatus.Status = statusUpdate.Status;
                facilityStatus.ProjectedIba = statusUpdate.ProjectedIba;
                facilityStatus.Notes = statusUpdate.StatusNote;
            }
            else
            {
                facilityStatus = new AIMS.Models.FacilityStatus()
                {
                    Facility = _session.Load<Facility>(statusUpdate.OrganizationId),
                    OnGenerator = statusUpdate.OnGenerator,
                    Status = statusUpdate.Status,
                    ProjectedIba = statusUpdate.ProjectedIba,
                    Notes = statusUpdate.StatusNote
                };
            }
            _session.SaveOrUpdate(facilityStatus);
        }

        public Models.StatusUpdate UpdateStatus(Models.StatusUpdate statusUpdate)
        {
            UpdateFacilityStatus(statusUpdate);
            UpdateBeds(statusUpdate);
            UpdateStaff(statusUpdate);
            UpdateSystems(statusUpdate);
            UpdateFuels(statusUpdate);
            return statusUpdate;
        }

        private void UpdateBeds(Models.StatusUpdate statusUpdate)
        {
            // get all beds for a facility
            var beds = _session.QueryOver<Bed>()
                .Where(b => b.Facility.ID == statusUpdate.OrganizationId).List();
            foreach (var item in statusUpdate.Beds)
            {
                var bed = beds.SingleOrDefault(b => b.ID == item.Id);

                // check if it exists and update else create a new one
                if (bed != null)
                {
                    bed.CurrentCapacity = item.CurrentCapacity;
                    bed.InUse = item.InUse;
                    bed.Available = item.CurrentCapacity - item.InUse;
                    bed.Notes = item.Notes;
                }
                else
                {
                    bed = new Bed
                    {
                        Facility = _session.Load<Facility>(statusUpdate.OrganizationId),
                        BedType = _session.Load<BedType>(item.TypeId),
                        CurrentCapacity = item.CurrentCapacity,
                        InUse = item.InUse,
                        Available = item.CurrentCapacity - item.InUse,
                        Notes = item.Notes
                    };
                }

                _session.SaveOrUpdate(bed);
            }
        }

        private void UpdateStaff(Models.StatusUpdate statusUpdate)
        {
            // get all staff for a facility
            var staffList = _session.QueryOver<Staff>()
                .Where(s => s.Facility.ID == statusUpdate.OrganizationId).List();
            foreach (var item in statusUpdate.Staff)
            {
                var staff = staffList.SingleOrDefault(b => b.ID == item.Id);

                // check if it exists and update else create a new one
                if (staff != null)
                {
                    staff.Status = item.Status;
                    staff.AmountShort = item.AmountShort;
                    staff.Notes = item.Notes;
                }
                else
                {
                    staff = new Staff
                    {
                        Facility = _session.Load<Facility>(statusUpdate.OrganizationId),
                        StaffType = _session.Load<StaffType>(item.TypeId),
                        Status = item.Status,
                        AmountShort = item.AmountShort,
                        Notes = item.Notes
                    };
                }

                _session.SaveOrUpdate(staff);
            }
        }

        private void UpdateSystems(Models.StatusUpdate statusUpdate)
        {
            // get all systems for a facility
            var systems = _session.QueryOver<Systems>()
                .Where(s => s.Facility.ID == statusUpdate.OrganizationId).List();
            foreach (var item in statusUpdate.Systems)
            {
                var system = systems.SingleOrDefault(b => b.ID == item.Id);

                // check if it exists and update else create a new one
                if (system != null)
                {
                    system.Status = item.Status;
                    system.Notes = item.Notes;
                }
                else
                {
                    system = new Systems
                    {
                        Facility = _session.Load<Facility>(statusUpdate.OrganizationId),
                        SystemsType = _session.Load<SystemsType>(item.TypeId),
                        Status = item.Status,
                        Notes = item.Notes
                    };
                }

                _session.SaveOrUpdate(system);
            }
        }

        private void UpdateFuels(Models.StatusUpdate statusUpdate)
        {
            // get all fuels for a facility
            var fuels = _session.QueryOver<Fuel>()
                .Where(f => f.Facility.ID == statusUpdate.OrganizationId).List();
            foreach (var item in statusUpdate.Fuels)
            {
                var fuel = fuels.SingleOrDefault(b => b.ID == item.Id);

                // check if it exists and update else create a new one
                if (fuel != null)
                {
                    fuel.Status = item.Status;
                    fuel.AmountShort = item.AmountShort;
                    fuel.Measurement = item.Measurement;
                    fuel.Notes = item.Notes;
                }
                else
                {
                    fuel = new Fuel
                    {
                        Facility = _session.Load<Facility>(statusUpdate.OrganizationId),
                        FuelType = _session.Load<FuelType>(item.TypeId),
                        Status = item.Status,
                        AmountShort = item.AmountShort,
                        Measurement = item.Measurement,
                        Notes = item.Notes
                    };
                }

                _session.SaveOrUpdate(fuel);
            }
        }
        
        // TODO: Trim list to the one selected in organization setup
        private IEnumerable<Models.Bed> GetBeds(Guid organizationId)
        {
            BedType bt = null;
            Bed b = null;
            Models.Bed staffVm = null;

            var beds = _session.QueryOver<Bed>(() => b)
                .Right.JoinAlias(() => b.BedType, () => bt)
                .Where(Restrictions.Or(
                    Restrictions.Where<Bed>(be => be.Facility.ID == organizationId),
                    Restrictions.On<Bed>(be => be.ID).IsNull))
                .OrderBy(() => bt.Name).Asc
                .SelectList(list => list
                    .Select(() => bt.ID).WithAlias(() => staffVm.TypeId)
                    .Select(() => bt.Name).WithAlias(() => staffVm.Type)
                    .Select(() => b.ID).WithAlias(() => staffVm.Id)
                    .Select(() => b.CurrentCapacity).WithAlias(() => staffVm.CurrentCapacity)
                    .Select(() => b.InUse).WithAlias(() => staffVm.InUse)
                    .Select(() => b.Available).WithAlias(() => staffVm.Available)
                    .Select(() => b.Notes).WithAlias(() => staffVm.Notes)
                    .Select(() => b.CreatedOn).WithAlias(() => staffVm.CreatedOn)
                    .Select(() => b.LastUpdateOn).WithAlias(() => staffVm.LastUpdatedOn))
                .TransformUsing(Transformers.AliasToBean<Models.Bed>()).List<Models.Bed>();
            return beds;
        }

        // TODO: Trim list to the one selected in organization setup
        private IEnumerable<Models.Staff> GetStaff(Guid organizationId)
        {
            StaffType st = null;
            Staff s = null;
            Models.Staff staffVm = null;

            var staffs = _session.QueryOver<Staff>(() => s)
                .Right.JoinAlias(() => s.StaffType, () => st)
                .Where(Restrictions.Or(
                    Restrictions.Where<Staff>(be => be.Facility.ID == organizationId),
                    Restrictions.On<Staff>(be => be.ID).IsNull))
                .OrderBy(() => st.Name).Asc
                .SelectList(list => list
                    .Select(() => st.ID).WithAlias(() => staffVm.TypeId)
                    .Select(() => st.Name).WithAlias(() => staffVm.Type)
                    .Select(() => s.ID).WithAlias(() => staffVm.Id)
                    .Select(() => s.Status).WithAlias(() => staffVm.Status)
                    .Select(() => s.AmountShort).WithAlias(() => staffVm.AmountShort)
                    .Select(() => s.Notes).WithAlias(() => staffVm.Notes)
                    .Select(() => s.CreatedOn).WithAlias(() => staffVm.CreatedOn)
                    .Select(() => s.LastUpdateOn).WithAlias(() => staffVm.LastUpdatedOn))
                .TransformUsing(Transformers.AliasToBean<Models.Staff>()).List<Models.Staff>();
            return staffs;
        }

        // TODO: Trim list to the one selected in organization setup
        private IEnumerable<Models.Fuel> GetFuels(Guid organizationId)
        {
            FuelType ft = null;
            Fuel f = null;
            Models.Fuel fuelVm = null;

            var fuels = _session.QueryOver<Fuel>(() => f)
                .Right.JoinAlias(() => f.FuelType, () => ft)
                .Where(Restrictions.Or(
                    Restrictions.Where<Fuel>(be => be.Facility.ID == organizationId),
                    Restrictions.On<Fuel>(be => be.ID).IsNull))
                .OrderBy(() => ft.Name).Asc
                .SelectList(list => list
                    .Select(() => ft.ID).WithAlias(() => fuelVm.TypeId)
                    .Select(() => ft.Name).WithAlias(() => fuelVm.Type)
                    .Select(() => f.ID).WithAlias(() => fuelVm.Id)
                    .Select(() => f.Status).WithAlias(() => fuelVm.Status)
                    .Select(() => f.AmountShort).WithAlias(() => fuelVm.AmountShort)
                    .Select(() => f.Measurement).WithAlias(() => fuelVm.Measurement)
                    .Select(() => f.Notes).WithAlias(() => fuelVm.Notes)
                    .Select(() => f.CreatedOn).WithAlias(() => fuelVm.CreatedOn)
                    .Select(() => f.LastUpdateOn).WithAlias(() => fuelVm.LastUpdatedOn))
                .TransformUsing(Transformers.AliasToBean<Models.Fuel>()).List<Models.Fuel>();
            return fuels;
        }

        // TODO: Trim list to the one selected in organization setup
        private IEnumerable<Models.System> GetSystems(Guid organizationId)
        {
            SystemsType st = null;
            Systems s = null;
            Models.System systemsVm = null;

            var systems = _session.QueryOver<Systems>(() => s)
                .Right.JoinAlias(() => s.SystemsType, () => st)
                .Where(Restrictions.Or(
                    Restrictions.Where<Systems>(be => be.Facility.ID == organizationId),
                    Restrictions.On<Systems>(be => be.ID).IsNull))
                .OrderBy(() => st.Name).Asc
                .SelectList(list => list
                    .Select(() => st.ID).WithAlias(() => systemsVm.TypeId)
                    .Select(() => st.Name).WithAlias(() => systemsVm.Type)
                    .Select(() => s.ID).WithAlias(() => systemsVm.Id)
                    .Select(() => s.Status).WithAlias(() => systemsVm.Status)
                    .Select(() => s.Notes).WithAlias(() => systemsVm.Notes)
                    .Select(() => s.CreatedOn).WithAlias(() => systemsVm.CreatedOn)
                    .Select(() => s.LastUpdateOn).WithAlias(() => systemsVm.LastUpdatedOn))
                .TransformUsing(Transformers.AliasToBean<Models.System>()).List<Models.System>();
            return systems;
        }

    }
}