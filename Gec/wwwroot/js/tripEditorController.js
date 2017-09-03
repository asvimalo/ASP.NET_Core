﻿//tripEditorController.js

(function () {

    "use strict";
    console.log("editor");
    angular.module("app-trips")
        .controller("tripEditorController", tripEditorController);

    function tripEditorController($routeParams, $http) {
        var vm = this;

        console.log($routeParams.tripName);

        vm.tripName = $routeParams.tripName;
        vm.stops = [];
        vm.errorMessage = "";
        vm.isBusy = true;
        vm.newStop = {};

        var url = "/api/trips/" + vm.tripName + "/stops";

        $http.get(url)
            .then(function (response) {
                angular.copy(response.data, vm.stops);
                _showMap(vm.stops);
            }, function (error) {
                vm.errorMessage = "Failed to load stops";
            })
        .finally(function () {
            vm.isBusy = false;
        });

        vm.addStop = function () {
            vm.isBusy = true;

            $http.post(url, vm.newStop)
                .then(function (response) {
                    //success
                    vm.stops.push(response.data);
                    _showMap(vm.stops);
                    vm.newStop = {};
                }, function () {
                    //failure
                    vm.errorMessage = "Failed to add new stop";
                })
            .finally(function () {
                vm.isBusy = false;
            });
        };
    }
    function _showMap(stops) {
        console.log("stops" + stops);
        if (stops && stops.length > 0) {
            var mapStops = _.map(stops, function (item) {

                return {
                    lat: item.latitude,
                    long: item.longitude,
                    info: item.name
                };

            });
            console.log("Mapstops" + mapStops);
            //show map
            travelMap.createMap({
                stops: mapStops,
                selector: "#map",
                currentStop: 1,
                InitialZoom: 3
            });
            console.log("travelMap" + travelMap);
        }
    }

})();