var statusUpdateModel = {
    onGenerator: ko.observable(initialData.OnGenerator),
    status: ko.observable(initialData.Status),
    projectedIba: ko.observable(initialData.ProjectedIba),
    statusNote: ko.observable(initialData.StatusNote),
    beds: ko.observableArray(convertBedsToObservable(initialData.Beds)),
    staff: ko.observableArray(convertStaffToObservable(initialData.Staff)),
    fuels: ko.observableArray(convertFuelsToObservable(initialData.Fuels)),
    systems: ko.observableArray(convertSystemsToObservable(initialData.Systems)),
    toggleResourceStatus: function (status, resource) {
        resource.status = status;
        console.log(ko.toJSON(window.statusUpdateModel));
    },
    update: function () {
        console.log(ko.toJSON(this));
        var request = $.ajax({
            url: updateUrl,
            type: "POST",
            data: ko.toJSON(this),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
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
        request.fail(function (jqXHR, textStatus) {
            $("#error-placeholder").append('<div class="alert alert-danger alert-dismissible">' + "Update failed: " + textStatus + '</div>');
        });
    },
};
ko.applyBindings(statusUpdateModel);

function Bed(id, typeId, type, currentCapacity, inUse, available, notes, createdOn, lastUpdatedOn) {
    this.id = ko.observable(id);
    this.typeId = ko.observable(typeId);
    this.type = ko.observable(type);
    this.currentCapacity = ko.observable(currentCapacity);
    this.inUse = ko.observable(inUse);
    this.available = ko.observable(available);
    this.notes = ko.observable(notes);
    this.createdOn = ko.observable(createdOn);
    this.lastUpdatedOn = ko.observable(lastUpdatedOn);
    
};

function convertBedsToObservable(list) {
    var newList = [];
    $.each(list, function (i, obj) {
        newList.push(new Bed(obj.Id, obj.TypeId, obj.Type, obj.CurrentCapacity, obj.InUse,
                             obj.Available, obj.Notes, obj.CreatedOn, obj.LastUpdatedOn));
    });
    return newList;
}

function Staff(id, typeId, type, status, amountShort, notes, createdOn, lastUpdatedOn) {
    this.id = ko.observable(id);
    this.typeId = ko.observable(typeId);
    this.type = ko.observable(type);
    this.status = ko.observable(status);
    this.amountShort = ko.observable(amountShort);
    this.notes = ko.observable(notes);
    this.createdOn = ko.observable(createdOn);
    this.lastUpdatedOn = ko.observable(lastUpdatedOn);

};

function convertStaffToObservable(list) {
    var newList = [];
    $.each(list, function (i, obj) {
        newList.push(new Staff(obj.Id, obj.TypeId, obj.Type, obj.Status, obj.AmountShort,
                               obj.Notes, obj.CreatedOn, obj.LastUpdatedOn));
    });
    return newList;
}

function Fuel(id, typeId, type, status, amountShort, measurement, notes, createdOn, lastUpdatedOn) {
    this.id = ko.observable(id);
    this.typeId = ko.observable(typeId);
    this.type = ko.observable(type);
    this.status = ko.observable(status);
    this.amountShort = ko.observable(amountShort);
    this.measurement = ko.observable(measurement);
    this.notes = ko.observable(notes);
    this.createdOn = ko.observable(createdOn);
    this.lastUpdatedOn = ko.observable(lastUpdatedOn);

};

function convertFuelsToObservable(list) {
    var newList = [];
    $.each(list, function (i, obj) {
        newList.push(new Fuel(obj.Id, obj.TypeId, obj.Type, obj.Status, obj.AmountShort,
                               obj.Measurement,obj.Notes, obj.CreatedOn, obj.LastUpdatedOn));
    });
    return newList;
}

function System(id, typeId, type, status, notes, createdOn, lastUpdatedOn) {
    this.id = ko.observable(id);
    this.typeId = ko.observable(typeId);
    this.type = ko.observable(type);
    this.status = ko.observable(status);
    this.notes = ko.observable(notes);
    this.createdOn = ko.observable(createdOn);
    this.lastUpdatedOn = ko.observable(lastUpdatedOn);

};

function convertSystemsToObservable(list) {
    var newList = [];
    $.each(list, function (i, obj) {
        newList.push(new System(obj.Id, obj.TypeId, obj.Type, obj.Status,
                               obj.Notes, obj.CreatedOn, obj.LastUpdatedOn));
    });
    return newList;
}