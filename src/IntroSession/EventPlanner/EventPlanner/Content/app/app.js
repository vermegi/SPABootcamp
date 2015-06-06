//app stuff
var eventplanner = angular.module("EventPlanner", ['ui.bootstrap', 'smart-table', 'ngRoute']);

eventplanner.config(['$httpProvider', '$routeProvider', function ($httpProvider, $routeProvider) {
    //initialize get if not there
    if (!$httpProvider.defaults.headers.get) {
        $httpProvider.defaults.headers.get = {
            'foobar': new Date().getTime()
        };
    }

    // Answer edited to include suggestions from comments
    // because previous version of code introduced browser-related errors

    //disable IE ajax request caching
    $httpProvider.defaults.headers.get['If-Modified-Since'] = 'Mon, 26 Jul 1997 05:00:00 GMT';
    // extra
    $httpProvider.defaults.headers.get['Cache-Control'] = 'no-cache';
    $httpProvider.defaults.headers.get.Pragma = 'no-cache';

    $routeProvider
        .when('/evenementen', {
            templateUrl: '/Content/app/partials/evenementlist.html',
            controller: 'ReservatiesCtrl'
        })
        .when('/evenement/:evenementId', {
            templateUrl: '/Content/app/partials/evenement.html',
            controller: 'ReservatieDetailCtrl'
        })
        .otherwise({
            redirectTo: '/evenementen'
        });
}]);

eventplanner.run(function ($rootScope) {
    $rootScope.openCalendar = function ($event, calendar) {
        $event.preventDefault();
        $event.stopPropagation();

        $rootScope[calendar] = {};
        $rootScope[calendar].opened = true;
    };
});