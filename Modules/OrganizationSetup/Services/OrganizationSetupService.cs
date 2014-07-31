using System;
using System.Collections.Generic;
using System.Linq;
using AIMS.Models;
using AIMS.Modules.OrganizationSetup.Models;
using Newtonsoft.Json;
using NHibernate;
using NHibernate.Transform;
using Bed = AIMS.Modules.OrganizationSetup.Models.Bed;
using Contact = AIMS.Modules.OrganizationSetup.Models.Contact;

namespace AIMS.Modules.OrganizationSetup.Services
{
    public interface IOrganizationSetupService
    {
        OrganizationSetUp GetOrganizationSetUp(Guid organizationId);
        OrganizationSetUp UpdateOrganizationSetUp(OrganizationSetUp organizationSetUp);
        void FillTypes(OrganizationSetUp orgSetup);
    }
    public class OrganizationSetupService : IOrganizationSetupService
    {
        private readonly ISession _session;

        public OrganizationSetupService(ISession session)
        {
            _session = session;
        }

        public void FillTypes(OrganizationSetUp orgSetup)
        {
            ResourceType bedTypes = null;
            orgSetup.BedTypes = _session.QueryOver<BedType>()
                .SelectList(list => list
                    .Select(r => r.ID).WithAlias(() => bedTypes.Id)
                    .Select(r => r.Name).WithAlias(() => bedTypes.Name))
                .TransformUsing(Transformers.AliasToBean<ResourceType>())
                .List<ResourceType>();
            ResourceType staffTypes = null;
            orgSetup.StaffTypes = _session.QueryOver<StaffType>()
                .SelectList(list => list
                    .Select(r => r.ID).WithAlias(() => staffTypes.Id)
                    .Select(r => r.Name).WithAlias(() => staffTypes.Name))
                .TransformUsing(Transformers.AliasToBean<ResourceType>())
                .List<ResourceType>();
            ResourceType systemsTypes = null;
            orgSetup.SystemsTypes = _session.QueryOver<SystemsType>()
                .SelectList(list => list
                    .Select(r => r.ID).WithAlias(() => systemsTypes.Id)
                    .Select(r => r.Name).WithAlias(() => systemsTypes.Name))
                .TransformUsing(Transformers.AliasToBean<ResourceType>())
                .List<ResourceType>();
            ResourceType fuelTypes = null;
            orgSetup.FuelTypes = _session.QueryOver<FuelType>()
                .SelectList(list => list
                    .Select(r => r.ID).WithAlias(() => fuelTypes.Id)
                    .Select(r => r.Name).WithAlias(() => fuelTypes.Name))
                .TransformUsing(Transformers.AliasToBean<ResourceType>())
                .List<ResourceType>();
        }

        public OrganizationSetUp GetOrganizationSetUp(Guid organizationId)
        {
            var organizationSetUp = new OrganizationSetUp();
            var organization = _session.QueryOver<Organization>().Where(f => f.ID == organizationId).SingleOrDefault();
            
            if (organization != null)
            {
                organizationSetUp.OrganizationId = organization.ID.ToString();
                organizationSetUp.OrganizationName = organization.Name;
                organizationSetUp.StreetAddress = organization.AddressLine1;
                organizationSetUp.City = organization.City;
                organizationSetUp.State = organization.County.State.Name;
                organizationSetUp.ZipCode = organization.ZipCode;
                organizationSetUp.Contacts = new List<Contact>();
                foreach (var contact in organization.Contacts)
                {
                    organizationSetUp.Contacts.Add(new Contact
                    {
                        Id = contact.ID.ToString(),
                        FirstName = contact.FirstName,
                        LastName = contact.LastName,
                        Title = contact.Title,
                        PhoneNumber = contact.PhoneNumber,
                        EmailAddress = contact.EmailAddress
                    });
                }
                var capabilities = new Capabilities();
                organizationSetUp.Beds = new List<Bed>();
                organizationSetUp.Staff = new List<Models.Staff>();
                organizationSetUp.Systems = new List<Models.System>();
                organizationSetUp.Fuels = new List<Models.Fuel>(); 

                if (!string.IsNullOrEmpty(organization.Capabilities))
                {
                    capabilities = JsonConvert.DeserializeObject<Capabilities>(organization.Capabilities);
                    if (capabilities.Beds != null)
                        organizationSetUp.Beds = capabilities.Beds;
                    if (capabilities.Staff != null)
                        organizationSetUp.Staff = capabilities.Staff;
                    if (capabilities.Systems != null)
                        organizationSetUp.Systems = capabilities.Systems;
                    if (capabilities.Fuels != null)
                        organizationSetUp.Fuels = capabilities.Fuels; 
                }
            }

            return organizationSetUp;
        }


        public OrganizationSetUp UpdateOrganizationSetUp(OrganizationSetUp organizationSetUp)
        {
            var organization = _session.QueryOver<Organization>().Where(f => f.ID == new Guid(organizationSetUp.OrganizationId)).SingleOrDefault();
            if (organization != null)
            {
                organization.AddressLine1 = organizationSetUp.StreetAddress;
                organization.City = organizationSetUp.City;
                organization.ZipCode = organizationSetUp.ZipCode;
                organization.Capabilities = JsonConvert.SerializeObject(new Capabilities
                {
                    Beds = organizationSetUp.Beds,
                    Staff = organizationSetUp.Staff,
                    Systems = organizationSetUp.Systems,
                    Fuels = organizationSetUp.Fuels,
                });
                _session.SaveOrUpdate(organization);

                foreach (var item in organizationSetUp.Contacts)
                {
                    var contact = new AIMS.Models.Contact();
                    //is new
                    if (!string.IsNullOrEmpty(item.Id))
                    {
                        contact = _session.QueryOver<AIMS.Models.Contact>().Where(c => c.ID == new Guid(item.Id)).SingleOrDefault();
                    }
                    contact.FirstName = item.FirstName;
                    contact.LastName = item.LastName;
                    contact.Title = item.Title;
                    contact.PhoneNumber = item.PhoneNumber;
                    contact.EmailAddress = item.EmailAddress;
                    contact.Organization = organization;
                    _session.SaveOrUpdate(contact);
                }

                return organizationSetUp;
            }
            else
            {
               throw new ApplicationException("Organization record was not found.");
            }
        }
    }
}