angular.module('app', ['ngResource', 'ui.router', 'LocalStorageModule', 'ui.bootstrap.datetimepicker']);

//Internet Host
//angular.module('app').value('apiUrl', '');


//Local Host
angular.module('app').value('apiUrl', 'http://localhost:50849/api');

angular.module('app').config(function ($stateProvider, $urlRouterProvider, $httpProvider) {
    $urlRouterProvider.otherwise('home');

    $httpProvider.interceptors.push('AuthenticationInterceptor');

    $stateProvider
        .state('home', { url: '/home', templateUrl: '/templates/home/home.html', controller: "HomeController" })
        .state('register', { url: '/register', templateUrl: '/templates/register/register.html', controller: "RegisterController" })

      
});

angular.module('app').run(function (AuthenticationService) {
    AuthenticationService.initialize();
});