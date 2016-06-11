angular.module('app').controller('LoginController', function ($scope, AuthenticationService) {
    $scope.loginData = {};

    $scope.login = function () {
        AuthenticationService.login($scope.loginData).then(
            function (response) {
                location.replace('/#/home/dashboard');

            },  
            
            function (err) {
                toastr.error("Invalid login.");
            }
            
        );
    };
});
