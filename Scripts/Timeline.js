$(function () {

    var time = $.connection.time;
    //hub login
    $.connection.hub.logging = true;
    //tell signalR to start the communication
    $.connection.hub.start();

    time.client.newMessage = function (visibility, message) {
        //use knockout to data bind
        model.addMessage(visibility, message);
    };

    time.client.AddTimelineEntry = function (visibility, message) {
        //use knockout to data bind
        model.addMessage(visibility, message);
    };


    var Message = function (visibility, message) {
        this.visibility = visibility;
        this.message = message;
        // this.date = date;
        // this.facility = facility;
    };

    var Facility = function (facilityName) {
        this.facilityName = facilityName;
    };

    var Model = function () {
        var self = this;
        self.message = ko.observable("");
        self.visibility = ko.observable();
        self.messages = ko.observableArray();
        self.facilityName = ko.observable();
        self.facilityNames = ko.observableArray(["Test Facility 1", "Test Facility 2", "Test Facility 3"]);
    };

    Model.prototype = {
        sendMessage: function () {
            var self = this;
            time.server.send(self.message(), self.visibility(), self.facilityName());
            self.message("");
        },

        addMessage: function (visibility, message, facilityName) {
            var self = this;
            self.messages.push(new Message(visibility, message));
            //self.FacilityNames.push(new Message(visibility, message, facilityName));
        },

    };

    var model = new Model();
    $(function () {
        ko.applyBindings(model);
    });
}());