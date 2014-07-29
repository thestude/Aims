using System;
using AIMS.Helpers;
using AIMS.Modules.FacilitySetup.Models;
using NHibernate;

namespace AIMS.Modules.FacilitySetup.Services
{
    public interface IFacilitySetupService
    {
        FacilitySetUp GetFacilitySetUp(Guid userName);
    }
    public class FacilitySetupService : IFacilitySetupService
    {
        private readonly ISession _session;

        public FacilitySetupService(ISession session)
        {
            _session = session;
        }

        public FacilitySetUp GetFacilitySetUp(Guid facilityId)
        {
            return new FacilitySetUp
            {
                
            };
        }
    }
}