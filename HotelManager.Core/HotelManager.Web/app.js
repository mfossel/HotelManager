angular.module('app', ['ngResource', 'ui.router', 'LocalStorageModule', 'ui.bootstrap.datetimepicker']);

//Internet Host
//angular.module('app').value('apiUrl', '');


//Local Host
angular.module('app').value('apiUrl', 'http://localhost:50849/api');

angular.module('app').config(function ($stateProvider, $urlRouterProvider, $httpProvider) {
    $urlRouterProvider.otherwise('login');

    $httpProvider.interceptors.push('AuthenticationInterceptor');

    $stateProvider
        .state('home', { url: '/home', templateUrl: '/templates/home/home.html', controller: "HomeController" })
        .state('login', { url: '/login', templateUrl: '/templates/accounts/login.html', controller: "LoginController" })
        .state('register', { url: '/register', templateUrl: '/templates/accounts/register.html', controller: "RegisterController" })

        .state('dashboard', { url: '/dashboard', templateUrl: '/templates/dashboard/dashboard.html', controller: "DashboardController" })

      
});

angular.module('app').run(function (AuthenticationService) {
    AuthenticationService.initialize();
});