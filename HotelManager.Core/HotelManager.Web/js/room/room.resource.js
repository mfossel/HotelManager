angular.module('app').factory('RoomResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/rooms/:roomId', { roomId: '@RoomId' },
        {
            'update': {
                method: 'PUT'
            }

        });
});