var viewModel = function () {
    var self = this;
    self.newStatus = ko.observable();
    self.statuses = ko.observableArray();

    self.onAddStatus = function () {
        statusHub.server.addStatus({
            statusText: self.newStatus()
        });
    };
    self.onClearStatus = function () {
        self.newStatus("");
    };
    self.like = function(el) {
        statusHub.server.likeStatus(el.Id());
    };
    self.likeLength = function(el) {
        return el.Liked.length;
    };

    var statusHub = $.connection.statusHub;
    statusHub.client.onNewStatus = function (status) {
        self.statuses.unshift(ko.mapping.fromJS(status));
    };
    statusHub.client.onStatusLike = function (statusId, connectionId) {
        _.where(self.statuses(), function (s) {
            if (s.Id() === statusId) {
                s.Liked.push(connectionId);
            }
        });
    };

    $.connection.hub.start().done(function() {
        console.log("connected");
        statusHub.server.getStatuses().done(function (result) {
            _.each(result, function(r) {
                self.statuses.unshift(ko.mapping.fromJS(r));
            });
        });
    });
};

var viewModelInstance = new viewModel();
ko.applyBindings(viewModelInstance);