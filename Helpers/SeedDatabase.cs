using System;
using System.IO;
using System.Linq;
using AIMS.Models;
using BrockAllen.MembershipReboot;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;


namespace AIMS.Helpers
{
    public class SeedDatabase
    {
        private readonly ISession _session;
        private readonly UserAccountService<AimsUser> _userAccountService;
        private readonly string _basePath;
        private readonly Random _rnd = new Random(DateTime.UtcNow.Millisecond);

        public SeedDatabase(ISession session, UserAccountService<AimsUser> userAccountService, string basePath)
        {
            _session = session;
            _userAccountService = userAccountService;
            _basePath = basePath;
        }

        public void Seed(string task)
        {
            switch (task)
            {
                case "lookups":
                    PopulateLookups();
                    _session.Flush();
                    _session.Clear();
                    break;
                case "all":
                    PopulateLookups();
                    _session.Flush();
                    _session.Clear();

                    PopulateFacilities();
                    _session.Flush();
                    _session.Clear();

                    PopulateFacilityContacts();
                    _session.Flush();
                    _session.Clear();

                    PopulateUsers();
                    _session.Flush();
                    _session.Clear();
                    break;
                default:
                    throw new ApplicationException(task + " seed task not implemented.");
            }
        }

        private void PopulateUsers()
        {
            // create user
            var user = new AimsUser();
            user = _userAccountService.CreateAccount("cshitest", "password", "cshitest@mailinator.com");
            user.FirstName = "CSHI";
            user.LastName = "Test";

            //assign a facility
            var facility = _session.QueryOver<Facility>().OrderByRandom().Take(1).SingleOrDefault();
            user.Organizations.Add(facility);

            _session.Save(user);
        }

        private void PopulateLookups()
        {

            //State
            _session.Save(new State { Name = "Alabama"});
            _session.Flush();
            _session.Clear();

            //Facility Types
            var facilityTypes = GetList("facilitytypes");
            foreach (var item in facilityTypes)
            {
                _session.Save(new FacilityType { Name = item});
            }
            _session.Flush();
            _session.Clear();

            //Counties
            var state = _session.QueryOver<State>().OrderByRandom().Take(1).SingleOrDefault();
            var countyList = GetList("counties");
            foreach (var item in countyList)
            {
                _session.Save(new County { Name = item, State = state});
            }
            _session.Flush();
            _session.Clear();

            //Bed Types
            var bedTypes = GetList("bedtypes");
            foreach (var item in bedTypes)
            {
                _session.Save(new BedType { Name = item});
            } 
            _session.Flush();
            _session.Clear();

            //Staff Types
            var staffTypes = GetList("stafftypes");
            foreach (var item in staffTypes)
            {
                _session.Save(new StaffType { Name = item});
            }
            _session.Flush();
            _session.Clear();

            //Fuel Types
            var fuelTypes = GetList("fueltypes");
            foreach (var item in fuelTypes)
            {
                _session.Save(new FuelType { Name = item});
            }
            _session.Flush();
            _session.Clear();

            //Systems Types
            var systemsTypes = GetList("systemstypes");
            foreach (var item in systemsTypes)
            {
                _session.Save(new SystemsType { Name = item});
            }
            _session.Flush();
            _session.Clear();

        }

        private void PopulateFacilities()
        {
            var list = GetList("facilities");
            foreach (var item in list)
            {
                string[] fields = item.Split('|');
                var county = _session.QueryOver<County>().Where(c => c.Name == fields[10].Trim()).Take(1).SingleOrDefault();
                var facilityType = _session.QueryOver<FacilityType>().Where(c => c.Name == fields[9].Trim()).Take(1).SingleOrDefault();

                _session.Save(new Facility
                {
                    Name = fields[0],
                    Acronym = fields[1],
                    AddressLine1 = fields[2],
                    AddressLine2 = null,
                    City = fields[3],
                    ZipCode = fields[4],
                    County = county,
                    OrganizationAssociationId = fields[12],
                    Latitude = (!string.IsNullOrEmpty(fields[6])) ? double.Parse(fields[6]) : (double?) null,
                    Longitude = (!string.IsNullOrEmpty(fields[7])) ? double.Parse(fields[7]) : (double?)null,
                    Elevation = (!string.IsNullOrEmpty(fields[8])) ? double.Parse(fields[8]) : (double?)null,
                    Phone = null,
                    //OnGenerator = false,
                    //Status = "Open",
                    FacilityType = facilityType,
                });

                _session.Flush();
                _session.Clear();
            }
        }

        private void PopulateFacilityContacts()
        {
            var facilities = _session.QueryOver<Facility>().List();
            var count = 0;
            foreach (var facility in facilities)
            {
                var contact = new Contact
                {
                    FirstName = SelectRandomString(GetList("firstnames")),
                    LastName = SelectRandomString(GetList("lastnames")),
                    Title = SelectRandomString(new []{"MD","NP",""}),
                    EmailAddress = SelectRandomString(GetList("lastnames")) + count + "@al.aimslive.org",
                    PhoneNumber = String.Format("{0}-{1}-{2}", GenerateRandomNumericString(3), 
                                                                GenerateRandomNumericString(3), 
                                                                GenerateRandomNumericString(4)),
                };
                contact.Organization = facility;
                _session.Save(contact);
                count++;
            }
        }
        private string GenerateRandomAlphaString(int length)
        {
            var chars = "abcdefghijklmnopqrstuvwxyz";
            var result = new string(
                Enumerable.Repeat(chars, length)
                          .Select(s => s[_rnd.Next(s.Length)])
                          .ToArray());

            return result.Substring(0, 1).ToUpper() + result.Substring(1);
        }

        private string GenerateRandomNumericString(int length)
        {
            var chars = "0123456789";
            var result = new string(
                Enumerable.Repeat(chars, length)
                          .Select(s => s[_rnd.Next(s.Length)])
                          .ToArray());

            return result.Substring(0, 1).ToUpper() + result.Substring(1);
        }

        private string SelectRandomString(string[] input)
        {
            var index = _rnd.Next(0, input.Length);
            return input[index];
        }

        private string[] GetList(string type)
        {
            switch (type.ToLower())
            {
                case "counties":
                    return File.ReadAllLines(_basePath + "SeederFiles\\County.txt");
                case "facilitytypes":
                    return File.ReadAllLines(_basePath + "SeederFiles\\FacilityType.txt");
                case "bedtypes":
                    return File.ReadAllLines(_basePath + "SeederFiles\\BedType.txt");
                case "stafftypes":
                    return File.ReadAllLines(_basePath + "SeederFiles\\StaffType.txt");
                case "fueltypes":
                    return File.ReadAllLines(_basePath + "SeederFiles\\FuelType.txt");
                case "systemstypes":
                    return File.ReadAllLines(_basePath + "SeederFiles\\SystemsType.txt");
                case "facilities":
                    return File.ReadAllLines(_basePath + "SeederFiles\\Facility.txt");
                case "firstnames":
                    return File.ReadAllLines(_basePath + "SeederFiles\\NamesFirst.txt");
                case "lastnames":
                    return File.ReadAllLines(_basePath + "SeederFiles\\NamesLast.txt");
                default:
                    throw new ArgumentException("requested list was not found.");
            }
        }
    }

    //Helper class to generate a random order
    //Helps with the selection of a random row
    public class RandomOrder : Order
    {
        public RandomOrder() : base("", true) { }
        public override SqlString ToSqlString(
            ICriteria criteria, ICriteriaQuery criteriaQuery)
        {
            return new SqlString("now()");
        }
    }

    public static class NHibernateExtensions
    {
        public static IQueryOver<TRoot, TSubType>
            OrderByRandom<TRoot, TSubType>(
              this IQueryOver<TRoot, TSubType> query)
        {
            query.UnderlyingCriteria.AddOrder(new RandomOrder());
            return query;
        }
    }
}