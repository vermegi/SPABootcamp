//app stuff
var eventplanner = angular.module("EventPlanner", ['ui.bootstrap']);

eventplanner.config(['$httpProvider', function ($httpProvider) {
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
    $httpProvider.defaults.headers.get['Pragma'] = 'no-cache';
}]);

eventplanner.run(function ($rootScope) {
    $rootScope.openCalendar = function ($event, calendar) {
        $event.preventDefault();
        $event.stopPropagation();

        $rootScope[calendar] = true;
    };
});

//controllers
eventplanner.controller('DetailEvenementCtrl', function($scope, reservatieSvc) {

    $scope.detailEvenementOpslaan = function() {
        reservatieSvc.detailsEvenementOpslaan($scope.evenement);
    };

    $scope.init = function (evenementId) {
        reservatieSvc.getReservatieData(evenementId)
            .then(function(data) {
                $scope.evenement = data.evenement;
                $scope.reservatieData = data.reservatieData;
            });
    };
});

eventplanner.controller('ReservatieDetailCtrl', function($scope) {
    $scope.nieuwePeriodeToevoegenVisibility = {
        visible: false
    };
});

eventplanner.controller('PeriodeCtrl', function ($scope) {
    $scope.toegevoegdeStraten = [];
    $scope.getOverlap = function (nieuweReservatie) {

    };

    $scope.straatToevoegen = function (geselecteerdeStraat) {
        if (geselecteerdeStraat === undefined || geselecteerdeStraat.Id === undefined)
            return;
        $scope.toegevoegdeStraten.push(geselecteerdeStraat);
        $scope.$broadcast('straatGeselecteerd');
    };

    $scope.straatVerwijderen = function (straat) {
        var index = $scope.toegevoegdeStraten.indexOf(straat);
        $scope.toegevoegdeStraten.splice(index, 1);
    };
});

eventplanner.controller('StratenCtrl', function ($scope, $http) {
    $scope.getLocations = function (val) {
        if (val === undefined || val.length < 2)
            return;

        return $http.get('/straten/straat?zoekstraat=' + val)
            .then(function (response) {
                return response.data;
            });
    }

    $scope.$on('straatGeselecteerd', function () {
        $scope.geselecteerdeStraat = '';
    });
});

//services
eventplanner.factory('reservatieSvc', function($http, $q, notifier) {
    return {
        getReservatieData: function (evenementId) {
            var dfd = $q.defer();
            $http.get('/api/Reservatie/GetReservatieData', { params: { EvenementId: evenementId } })
                .success(dfd.resolve)
                .error(function () {
                    notifier.error('something went wrong!');
                });
            return dfd.promise;
        },
        detailsEvenementOpslaan: function (evenement) {
            $http.post("/api/Reservatie/OmschrijvingAanpassen", evenement)
                .success(function() {
                    notifier.notify('changes saved succesfully');
                })
                .error(function () {
                    notifier.error("something went wrong");
                });
        }
    }
});

eventplanner.value('toastr', toastr);
eventplanner.factory('notifier', function (toastr) {
    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };
    return {
        notify: function (msg) {
            toastr["success"](msg, "");
        },
        error: function (msg) {
            toastr["error"](msg, "");
        }
    }
});
