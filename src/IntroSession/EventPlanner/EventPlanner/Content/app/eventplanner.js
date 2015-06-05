//app stuff
var eventplanner = angular.module("EventPlanner", ['ui.bootstrap', 'smart-table', 'angularMoment']);

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

        $rootScope[calendar] = {};
        $rootScope[calendar].opened = true;
    };
});

//directives
eventplanner.directive('evenementheader', function() {
    return {
        scope: {
            evenementInfo: '=info'
        },
        template: '{{evenementInfo.titel}} ({{evenementInfo.eigenaar}})'
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

eventplanner.controller('ReservatiesCtrl', function ($scope, reservatieSvc) {
    $scope.openCalendar = function ($event, calendar) {
        $event.preventDefault();
        $event.stopPropagation();

        $scope[calendar] = true;
    };

    $scope.datumVan = moment().toJSON();
    $scope.datumTot = moment().add(1, 'month').toJSON();

    $scope.callServer = function (tableState) {
        if (tableState == undefined) {
            tableState = { pagination: { start: 0, number: 10 }, search: { predicateObject: { $: "" } } };
        }

        $scope.isLoading = true;
        var pagination = tableState.pagination;

        var start = pagination.start || 0;     // This is NOT the page number, but the index of item in the list that you want to use to display the table.
        var number = pagination.number || 10;  // Number of entries showed per page.
        var searchTerm = tableState.search.predicateObject === undefined ? '' : tableState.search.predicateObject.$;

        reservatieSvc.getReservatiesData($scope.datumVan, $scope.datumTot, start, number, searchTerm).then(function (data) {
            $scope.rowCollection = data.rows;
            tableState.pagination.numberOfPages = data.numberOfPages;  //set the number of pages so the pagination can update
            $scope.isLoading = false;
        });
    };
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
        },
        getReservatiesData: function (datumVan, datumTot, start, number, searchTerm) {
            var dfd = $q.defer();
            $http.get('/api/Reservaties/GetForDate', { params: { DatumVan: datumVan, DatumTot: datumTot, Start: start, Number: number, SearchTerm: searchTerm } })
                .success(dfd.resolve)
                .error(function (err) {
                notifier.error('Er is iets misgelopen');
            });
            return dfd.promise;
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
