
    drop table if exists Groups cascade

    drop table if exists GroupChildren cascade

    drop table if exists LinkedAccounts cascade

    drop table if exists LinkedAccountClaims cascade

    drop table if exists PasswordResetSecrets cascade

    drop table if exists TwoFactorAuthTokens cascade

    drop table if exists UserAccounts cascade

    drop table if exists UserCertificates cascade

    drop table if exists UserClaims cascade

    drop table if exists Bed cascade

    drop table if exists BedType cascade

    drop table if exists Capacity cascade

    drop table if exists Contact cascade

    drop table if exists County cascade

    drop table if exists FacilityStatus cascade

    drop table if exists FacilityType cascade

    drop table if exists Fuel cascade

    drop table if exists FuelType cascade

    drop table if exists Organization cascade

    drop table if exists OrganizationUsers cascade

    drop table if exists OrganizationType cascade

    drop table if exists Staff cascade

    drop table if exists StaffType cascade

    drop table if exists State cascade

    drop table if exists Systems cascade

    drop table if exists SystemsType cascade

    drop table if exists AimsUser cascade

    drop table if exists Facility cascade

    drop table if exists security_EntitiesGroups cascade

    drop table if exists security_EntityReferencesToEntitiesGroups cascade

    drop table if exists security_EntityGroupsHierarchy cascade

    drop table if exists security_EntityReferences cascade

    drop table if exists security_EntityTypes cascade

    drop table if exists security_Operations cascade

    drop table if exists security_Permissions cascade

    drop table if exists security_UsersGroups cascade

    drop table if exists security_UsersToUsersGroups cascade

    drop table if exists security_UsersGroupsHierarchy cascade

    create table Groups (
        ID uuid not null,
       Version int8 not null,
       Tenant varchar(50) not null,
       Name varchar(100) not null,
       Created timestamp,
       LastUpdated timestamp,
       primary key (ID)
    )

    create table GroupChildren (
        GroupID uuid not null,
       ChildGroupID uuid not null,
       Version int8 not null,
       primary key (GroupID, ChildGroupID)
    )

    create table LinkedAccounts (
        UserAccountID uuid not null,
       ProviderName varchar(30) not null,
       ProviderAccountID varchar(100) not null,
       Version int8 not null,
       LastLogin timestamp,
       primary key (UserAccountID, ProviderName, ProviderAccountID)
    )

    create table LinkedAccountClaims (
        UserAccountID uuid not null,
       ProviderName varchar(30) not null,
       ProviderAccountID varchar(100) not null,
       Type varchar(150) not null,
       Value varchar(150) not null,
       Version int8 not null,
       primary key (UserAccountID, ProviderName, ProviderAccountID, Type, Value)
    )

    create table PasswordResetSecrets (
        UserAccountID uuid not null,
       PasswordResetSecretID uuid not null,
       Version int8 not null,
       Question varchar(150) not null,
       Answer varchar(150) not null,
       primary key (UserAccountID, PasswordResetSecretID)
    )

    create table TwoFactorAuthTokens (
        UserAccountID uuid not null,
       Token varchar(100) not null,
       Version int8 not null,
       Issued timestamp,
       primary key (UserAccountID, Token)
    )

    create table UserAccounts (
        ID uuid not null,
       Version int8 not null,
       Tenant varchar(50) not null,
       Username varchar(100) not null,
       Created timestamp,
       LastUpdated timestamp,
       IsAccountClosed boolean,
       AccountClosed timestamp,
       IsLoginAllowed boolean,
       LastLogin timestamp,
       LastFailedLogin timestamp,
       FailedLoginCount int4,
       PasswordChanged timestamp,
       RequiresPasswordReset boolean,
       Email varchar(100),
       IsAccountVerified boolean,
       LastFailedPasswordReset timestamp,
       FailedPasswordResetCount int4,
       MobileCode varchar(100),
       MobileCodeSent timestamp,
       MobilePhoneNumber varchar(20),
       MobilePhoneNumberChanged timestamp,
       AccountTwoFactorAuthMode int4,
       CurrentTwoFactorAuthStatus int4,
       VerificationKey varchar(100),
       VerificationPurpose int4,
       VerificationKeySent timestamp,
       VerificationStorage varchar(100),
       HashedPassword varchar(100),
       primary key (ID)
    )

    create table UserCertificates (
        UserAccountID uuid not null,
       Thumbprint varchar(150) not null,
       Version int8 not null,
       Subject varchar(250),
       primary key (UserAccountID, Thumbprint)
    )

    create table UserClaims (
        UserAccountID uuid not null,
       Type varchar(150) not null,
       Value varchar(150) not null,
       Version int8 not null,
       primary key (UserAccountID, Type, Value)
    )

    create table Bed (
        ID uuid not null,
       Version int8 not null,
       CreatedOn timestamptz default (now() at time zone 'utc') ,
       LastUpdateOn timestamptz default (now() at time zone 'utc') ,
       StandardCapacity int4 not null,
       CurrentCapacity int4 not null,
       InUse int4 not null,
       Available int4 not null,
       Notes varchar(255),
       FacilityId uuid,
       BedTypeId uuid,
       primary key (ID)
    )

    create table BedType (
        ID uuid not null,
       Version int8 not null,
       CreatedOn timestamptz default (now() at time zone 'utc') ,
       LastUpdateOn timestamptz default (now() at time zone 'utc') ,
       Name varchar(255) not null,
       primary key (ID)
    )

    create table Capacity (
        ID uuid not null,
       Version int8 not null,
       CreatedOn timestamptz default (now() at time zone 'utc') ,
       LastUpdateOn timestamptz default (now() at time zone 'utc') ,
       TotalCapacity int4 not null,
       FacilityId uuid,
       primary key (ID)
    )

    create table Contact (
        ID uuid not null,
       Version int8 not null,
       CreatedOn timestamptz default (now() at time zone 'utc') ,
       LastUpdateOn timestamptz default (now() at time zone 'utc') ,
       FirstName varchar(255) not null,
       LastName varchar(255) not null,
       Title varchar(255),
       PhoneNumber varchar(255) not null,
       EmailAddress varchar(255) not null,
       OrganizationId uuid,
       primary key (ID)
    )

    create table County (
        ID uuid not null,
       Version int8 not null,
       CreatedOn timestamptz default (now() at time zone 'utc') ,
       LastUpdateOn timestamptz default (now() at time zone 'utc') ,
       Name varchar(255) not null,
       StateId uuid,
       primary key (ID)
    )

    create table FacilityStatus (
        ID uuid not null,
       Version int8 not null,
       CreatedOn timestamptz default (now() at time zone 'utc') ,
       LastUpdateOn timestamptz default (now() at time zone 'utc') ,
       OnGenerator boolean not null,
       Status varchar(255) not null,
       ProjectedIba int4,
       Notes varchar(255),
       FacilityId uuid,
       primary key (ID)
    )

    create table FacilityType (
        ID uuid not null,
       Version int8 not null,
       CreatedOn timestamptz default (now() at time zone 'utc') ,
       LastUpdateOn timestamptz default (now() at time zone 'utc') ,
       Name varchar(255) not null,
       primary key (ID)
    )

    create table Fuel (
        ID uuid not null,
       Version int8 not null,
       CreatedOn timestamptz default (now() at time zone 'utc') ,
       LastUpdateOn timestamptz default (now() at time zone 'utc') ,
       TotalCapacity int4 not null,
       Status varchar(255) not null,
       AmountShort int4 not null,
       Measurement varchar(255),
       Notes varchar(255),
       FacilityId uuid,
       FuelTypeId uuid,
       primary key (ID)
    )

    create table FuelType (
        ID uuid not null,
       Version int8 not null,
       CreatedOn timestamptz default (now() at time zone 'utc') ,
       LastUpdateOn timestamptz default (now() at time zone 'utc') ,
       Name varchar(255) not null,
       primary key (ID)
    )

    create table Organization (
        ID uuid not null,
       Version int8 not null,
       CreatedOn timestamptz default (now() at time zone 'utc') ,
       LastUpdateOn timestamptz default (now() at time zone 'utc') ,
       Name varchar(255) not null,
       AddressLine1 varchar(255) not null,
       AddressLine2 varchar(255),
       Latitude float8,
       Longitude float8,
       Elevation float8,
       Phone varchar(255),
       Acronym varchar(255) not null,
       OrganizationPreferences varchar(255),
       OrganizationAssociationId varchar(255),
       City varchar(255) not null,
       ZipCode varchar(255) not null,
       OrganizationTypeId uuid,
       CountyId uuid,
       ParentOrganizationId uuid,
       Capabilities text,
       primary key (ID)
    )

    create table OrganizationUsers (
        OrganizationId uuid not null,
       UserId uuid not null,
       primary key (UserId, OrganizationId)
    )

    create table OrganizationType (
        ID uuid not null,
       Version int8 not null,
       CreatedOn timestamptz default (now() at time zone 'utc') ,
       LastUpdateOn timestamptz default (now() at time zone 'utc') ,
       Name varchar(255) not null,
       primary key (ID)
    )

    create table Staff (
        ID uuid not null,
       Version int8 not null,
       CreatedOn timestamptz default (now() at time zone 'utc') ,
       LastUpdateOn timestamptz default (now() at time zone 'utc') ,
       Status varchar(255) not null,
       AmountShort int4 not null,
       Notes varchar(255),
       FacilityId uuid,
       StaffTypeId uuid,
       primary key (ID)
    )

    create table StaffType (
        ID uuid not null,
       Version int8 not null,
       CreatedOn timestamptz default (now() at time zone 'utc') ,
       LastUpdateOn timestamptz default (now() at time zone 'utc') ,
       Name varchar(255) not null,
       primary key (ID)
    )

    create table State (
        ID uuid not null,
       Version int8 not null,
       CreatedOn timestamptz default (now() at time zone 'utc') ,
       LastUpdateOn timestamptz default (now() at time zone 'utc') ,
       Name varchar(255) not null,
       primary key (ID)
    )

    create table Systems (
        ID uuid not null,
       Version int8 not null,
       CreatedOn timestamptz default (now() at time zone 'utc') ,
       LastUpdateOn timestamptz default (now() at time zone 'utc') ,
       Status varchar(255) not null,
       Notes varchar(255),
       FacilityId uuid,
       SystemsTypeId uuid,
       primary key (ID)
    )

    create table SystemsType (
        ID uuid not null,
       Version int8 not null,
       CreatedOn timestamptz default (now() at time zone 'utc') ,
       LastUpdateOn timestamptz default (now() at time zone 'utc') ,
       Name varchar(255) not null,
       primary key (ID)
    )

    create table AimsUser (
        UserId uuid not null unique,
       FirstName varchar(100),
       LastName varchar(100),
       primary key (UserId)
    )

    create table Facility (
        OrganizationId uuid not null unique,
       FacilityTypeId uuid,
       primary key (OrganizationId)
    )

    create table security_EntitiesGroups (
        Id uuid not null,
       Name varchar(255) not null unique,
       Parent uuid,
       primary key (Id)
    )

    create table security_EntityReferencesToEntitiesGroups (
        GroupId uuid not null,
       EntityReferenceId uuid not null,
       primary key (GroupId, EntityReferenceId)
    )

    create table security_EntityGroupsHierarchy (
        ParentGroup uuid not null,
       ChildGroup uuid not null,
       primary key (ChildGroup, ParentGroup)
    )

    create table security_EntityReferences (
        Id uuid not null,
       EntitySecurityKey uuid not null unique,
       Type uuid not null,
       primary key (Id)
    )

    create table security_EntityTypes (
        Id uuid not null,
       Name varchar(255) not null unique,
       primary key (Id)
    )

    create table security_Operations (
        Id uuid not null,
       Name varchar(255) not null unique,
       Comment varchar(255),
       Parent uuid,
       primary key (Id)
    )

    create table security_Permissions (
        Id uuid not null,
       EntitySecurityKey uuid,
       Allow boolean not null,
       Level int4 not null,
       EntityTypeName varchar(255),
       Operation uuid not null,
       "User" uuid,
       UsersGroup uuid,
       EntitiesGroup uuid,
       primary key (Id)
    )

    create table security_UsersGroups (
        Id uuid not null,
       Name varchar(255) not null unique,
       Parent uuid,
       primary key (Id)
    )

    create table security_UsersToUsersGroups (
        GroupId uuid not null,
       UserId uuid not null,
       primary key (GroupId, UserId)
    )

    create table security_UsersGroupsHierarchy (
        ParentGroup uuid not null,
       ChildGroup uuid not null,
       primary key (ChildGroup, ParentGroup)
    )

    alter table GroupChildren 
        add constraint FK_Group_GroupChildren 
        foreign key (GroupID) 
        references Groups

    alter table LinkedAccounts 
        add constraint FK_UserAccount_LinkedAccounts 
        foreign key (UserAccountID) 
        references UserAccounts

    alter table LinkedAccountClaims 
        add constraint FK_UserAccount_LinkedAccountClaims 
        foreign key (UserAccountID) 
        references UserAccounts

    alter table PasswordResetSecrets 
        add constraint FK_UserAccount_PasswordResetSecrets 
        foreign key (UserAccountID) 
        references UserAccounts

    alter table TwoFactorAuthTokens 
        add constraint FK_UserAccount_TwoFactorAuthTokens 
        foreign key (UserAccountID) 
        references UserAccounts

    alter table UserCertificates 
        add constraint FK_UserAccount_UserCertificates 
        foreign key (UserAccountID) 
        references UserAccounts

    alter table UserClaims 
        add constraint FK_UserAccount_UserClaims 
        foreign key (UserAccountID) 
        references UserAccounts

    alter table Bed 
        add constraint facility_bed_fk 
        foreign key (FacilityId) 
        references Facility

    alter table Bed 
        add constraint bedtype_bed_fk 
        foreign key (BedTypeId) 
        references BedType

    alter table Capacity 
        add constraint facility_capacity_fk 
        foreign key (FacilityId) 
        references Facility

    alter table Contact 
        add constraint organization_contact_fk 
        foreign key (OrganizationId) 
        references Organization

    alter table County 
        add constraint state_county_fk 
        foreign key (StateId) 
        references State

    alter table FacilityStatus 
        add constraint facility_status_fk 
        foreign key (FacilityId) 
        references Facility

    alter table Fuel 
        add constraint facility_fuel_fk 
        foreign key (FacilityId) 
        references Facility

    alter table Fuel 
        add constraint fueltype_fuel_fk 
        foreign key (FuelTypeId) 
        references FuelType

    alter table Organization 
        add constraint organizationtype_organization_fk 
        foreign key (OrganizationTypeId) 
        references OrganizationType

    alter table Organization 
        add constraint county_organization_fk 
        foreign key (CountyId) 
        references County

    alter table Organization 
        add constraint organization_childorganization_fk 
        foreign key (ParentOrganizationId) 
        references Organization

    alter table OrganizationUsers 
        add constraint user_organization_fk 
        foreign key (UserId) 
        references AimsUser

    alter table OrganizationUsers 
        add constraint organization_user_fk 
        foreign key (OrganizationId) 
        references Organization

    alter table Staff 
        add constraint facility_staff_fk 
        foreign key (FacilityId) 
        references Facility

    alter table Staff 
        add constraint stafftype_staff_fk 
        foreign key (StaffTypeId) 
        references StaffType

    alter table Systems 
        add constraint facility_systems_fk 
        foreign key (FacilityId) 
        references Facility

    alter table Systems 
        add constraint systemstype_systems_fk 
        foreign key (SystemsTypeId) 
        references SystemsType

    create index firstname_idx on AimsUser (FirstName)

    create index lastname_idx on AimsUser (LastName)

    alter table AimsUser 
        add constraint User_fk 
        foreign key (UserId) 
        references UserAccounts 
        on delete cascade

    alter table Facility 
        add constraint organization_facility_fk 
        foreign key (OrganizationId) 
        references Organization 
        on delete cascade

    alter table Facility 
        add constraint facilitytype_facility_fk 
        foreign key (FacilityTypeId) 
        references FacilityType

    alter table security_EntitiesGroups 
        add constraint FKD0398EC71CF0F16E 
        foreign key (Parent) 
        references security_EntitiesGroups

    alter table security_EntityReferencesToEntitiesGroups 
        add constraint FK17A323D6DDB11ADF 
        foreign key (EntityReferenceId) 
        references security_EntityReferences

    alter table security_EntityReferencesToEntitiesGroups 
        add constraint FK17A323D6DE03A26A 
        foreign key (GroupId) 
        references security_EntitiesGroups

    alter table security_EntityGroupsHierarchy 
        add constraint FK76531C74645BDDCE 
        foreign key (ChildGroup) 
        references security_EntitiesGroups

    alter table security_EntityGroupsHierarchy 
        add constraint FK76531C746440D8EE 
        foreign key (ParentGroup) 
        references security_EntitiesGroups

    alter table security_EntityReferences 
        add constraint FKBBE4029387CC6C80 
        foreign key (Type) 
        references security_EntityTypes

    alter table security_Operations 
        add constraint FKE58BBFF82B7CDCD3 
        foreign key (Parent) 
        references security_Operations

    alter table security_Permissions 
        add constraint FKEA223C4C71C937C7 
        foreign key (Operation) 
        references security_Operations

    alter table security_Permissions 
        add constraint FKEA223C4CFC8C2B95 
        foreign key ("User") 
        references AimsUser

    alter table security_Permissions 
        add constraint FKEA223C4C2EE8F612 
        foreign key (UsersGroup) 
        references security_UsersGroups

    alter table security_Permissions 
        add constraint FKEA223C4C6C8EC3A5 
        foreign key (EntitiesGroup) 
        references security_EntitiesGroups

    alter table security_UsersGroups 
        add constraint FKEC3AF233D0CB87D0 
        foreign key (Parent) 
        references security_UsersGroups

    alter table security_UsersToUsersGroups 
        add constraint FK7817F27AA6C99102 
        foreign key (UserId) 
        references AimsUser

    alter table security_UsersToUsersGroups 
        add constraint FK7817F27A1238D4D4 
        foreign key (GroupId) 
        references security_UsersGroups

    alter table security_UsersGroupsHierarchy 
        add constraint FK69A3B61FA860AB70 
        foreign key (ChildGroup) 
        references security_UsersGroups

    alter table security_UsersGroupsHierarchy 
        add constraint FK69A3B61FA87BAE50 
        foreign key (ParentGroup) 
        references security_UsersGroups
