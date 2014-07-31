var orgSetupViewModel = {
    organizationName: ko.observable(initialData.OrganizationName),
    streetAddress: ko.observable(initialData.StreetAddress),
    city: ko.observable(initialData.City),
    state: ko.observable(initialData.State),
    zipCode: ko.observable(initialData.ZipCode),

    contacts: ko.observableArray(convertContactsToObservable(initialData.Contacts)),

    beds: ko.observableArray(convertBedsToObservable(initialData.Beds)),

    staff: ko.observableArray(initialData.Staff),
    staffTypes: ko.observableArray(convertResourceTypeToObservable(initialData.StaffTypes)),
    selectedStaffTypes: ko.observableArray(initialData.SelectedStaffTypes),

    systems: ko.observableArray(initialData.Systems),
    systemsTypes: ko.observableArray(convertResourceTypeToObservable(initialData.SystemsTypes)),
    selectedSystemsTypes: ko.observableArray(initialData.SelectedSystemsTypes),

    fuels: ko.observableArray(initialData.Fuels),
    fuelTypes: ko.observableArray(convertResourceTypeToObservable(initialData.FuelTypes)),
    selectedFuelTypes: ko.observableArray(initialData.SelectedFuelTypes),

    newContactFirstName: ko.observable(""),
    newContactLastName: ko.observable(""),
    newContactTitle: ko.observable(""),
    newContactPhoneNumber: ko.observable(""),
    newContactEmailAddress: ko.observable(""),

    newBedTypeId: ko.observable(""),
    newBedType: ko.observable(""),
    newLicensedCapacity: ko.observable(""),

    addContact: function () {
        this.contacts.push(new Contact(null, ko.toJS(this.newContactFirstName), ko.toJS(this.newContactLastName),
            ko.toJS(this.newContactTitle), ko.toJS(this.newContactPhoneNumber), ko.toJS(this.newContactEmailAddress)));
        this.newContactFirstName("");
        this.newContactLastName("");
        this.newContactTitle("");
        this.newContactPhoneNumber("");
        this.newContactEmailAddress("");
        $("#add-contact-form").validate();
    },
 
    editContact: function (contact) {
        contact.showEdit(true);
        contact.showLabels(false);
    },

    updateContact: function (contact) {
        contact.showEdit(false);
        contact.showLabels(true);
    },

    removeContact: function (contact) {
        if (confirm("Do you wish to continue deleting this item?") == true) {
            window.orgSetupViewModel.contacts.remove(contact);
        }
    },

    addBed: function () {
        this.beds.push(new Bed(ko.toJS(this.newBedTypeId), ko.toJS(this.newBedType), ko.toJS(this.newLicensedCapacity)));
        this.newBedTypeId("");
        this.newBedType("");
        this.newLicensedCapacity("");
    },

    editBed: function (bed) {
        bed.showEdit(true);
        bed.showLabels(false);
    },

    updateBed: function (bed) {
        bed.showEdit(false);
        bed.showLabels(true);
    },

    removeBed: function (bed) {
        if (confirm("Do you wish to continue deleting this item?") == true) {
            window.orgSetupViewModel.beds.remove(bed);
        }
    },

    toggleStaff: function (item) {
        if (item.selected() === true) {
            window.orgSetupViewModel.staff.remove(new ResourceType(item.id(), item.name()));
            console.log("dissociate item " + item.id());
        } else {
            window.orgSetupViewModel.staff.push(new ResourceType(item.id(), item.name()));
            console.log("associate item " + item.id());
        }
        item.selected(!(item.selected()));
        return true;
    },

    toggleSystems: function (item) {
        if (item.selected() === true) {
            window.orgSetupViewModel.systems.remove(new ResourceType(item.id(), item.name()));
            console.log("dissociate item " + item.id());
        } else {
            window.orgSetupViewModel.systems.push(new ResourceType(item.id(), item.name()));
            console.log("associate item " + item.id());
        }
        item.selected(!(item.selected()));
        return true;
    },

    toggleFuel: function (item) {
        if (item.selected() === true) {
            window.orgSetupViewModel.fuels.remove(new ResourceType(item.id(), item.name()));
            console.log("dissociate item " + item.id());
        } else {
            window.orgSetupViewModel.fuels.push(new ResourceType(item.id(), item.name()));
            console.log("associate item " + item.id());
        }
        item.selected(!(item.selected()));
        return true;
    },
    save: function() {
        console.log(ko.toJSON(this));
        var request = $.ajax({
            url: updateUrl,
            type: "POST",
            data: ko.toJSON(this),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function(data) {
                if (data.isError) {
                    if (data.errors) {
                        for (var i = 0; i < data.errors.length; i++) {
                            $("#error-placeholder").append('<div class="alert alert-danger alert-dismissible">' + data.errors[i].Value[0] + '</div>');
                        }
                    }
                    if (data.error) {
                        $("#error-placeholder").append('<div class="alert alert-danger alert-dismissible">' + data.error + '</div>');
                    }
                    $("html, body").animate({ scrollTop: 0 }, "slow");
                }

                if (data.isRedirect) {
                    window.location.href = data.redirectUrl;
                }
            },
        });
        request.done(function (msg) {
            console.log(msg);
        });
        request.fail(function (jqXHR, textStatus) {
            console.log("Request failed: " + textStatus);
        });
    }
};

ko.applyBindings(orgSetupViewModel);

function Contact(id, firstName, lastName,title,phoneNumber,emailAddress) {
    this.id = id;
    this.firstName = ko.observable(firstName);
    this.lastName = ko.observable(lastName);
    this.title = ko.observable(title);
    this.phoneNumber = ko.observable(phoneNumber);
    this.emailAddress = ko.observable(emailAddress);
    this.showLabels = ko.observable(true);
    this.showEdit = ko.observable(false);
};

function convertContactsToObservable(list) {
    var newList = [];
    $.each(list, function (i, obj) {
        newList.push(new Contact(obj.Id, obj.FirstName, obj.LastName, obj.Title, obj.PhoneNumber, obj.EmailAddress));
    });
    return newList;
}

function Bed(id, type, licensedCapacity) {
    this.id = ko.observable(id);
    this.name = ko.observable(type);
    this.licensedCapacity = ko.observable(licensedCapacity);
    this.showLabels = ko.observable(true);
    this.showEdit = ko.observable(false);
};

function convertBedsToObservable(list) {
    var newList = [];
    $.each(list, function (i, obj) {
        newList.push(new Bed(obj.Id, obj.Name, obj.LicensedCapacity));
    });
    return newList;
}

function ResourceType(id, name) {
    this.id = ko.observable(id);
    this.name = ko.observable(name);
    this.selected = ko.observable(false);
}

function convertResourceTypeToObservable(list) {
    var newList = [];
    $.each(list, function (i, obj) {
        newList.push(new ResourceType(obj.Id, obj.Name, obj.Selected));
    });
    return newList;
}

