angular.module('app').controller('RoomGridController', function ($scope, RoomResource) {

    function activate() {
        $scope.rooms = RoomResource.query();
    }

    activate();

    $scope.deleteRoom = function (room) {
        room.$remove(function (data) {
            toastr.error('Room Deleted');
            activate();
        })

    };




});