angular.module('app', ['ngResource', 'ui.router', 'LocalStorageModule', 'ui.bootstrap.datetimepicker']);

//Internet Host
//angular.module('app').value('apiUrl', '');


//Local Host
angular.module('app').value('apiUrl', 'http://localhost:50849/api');

angular.module('app').config(function ($stateProvider, $urlRouterProvider, $httpProvider) {
    $urlRouterProvider.otherwise('home/dashboard');

    $httpProvider.interceptors.push('AuthenticationInterceptor');

    $stateProvider
        .state('home', { url: '/home', templateUrl: '/templates/home/home.html', controller: "HomeController" })
        .state('login', { url: '/login', templateUrl: '/templates/accounts/login.html', controller: "LoginController" })
        .state('register', { url: '/register', templateUrl: '/templates/accounts/register.html', controller: "RegisterController" })

        .state('home.dashboard', { url: '/dashboard', parent:'home', templateUrl: '/templates/dashboard/dashboard.html', controller: "DashboardController" })
        
        .state('home.rooms', { url: '/home/rooms', parent: 'home', templateUrl: '/templates/app/room.html', controller: "RoomController" })
        .state('home.reservations', { url: '/home/reservations', parent: 'home', templateUrl: '/templates/app/reservation.html', controller: "ReservationController" })
});

angular.module('app').run(function (AuthenticationService) {
    AuthenticationService.initialize();
});