var eventplanner = angular.module("EventPlanner", []);

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

eventplanner.controller('detailEvenementCtrl', function ($scope, reservatieSvc) {
    $scope.evenement = { };

    $scope.detailEvenementOpslaan = function (evenement) {
        reservatieSvc.detailsEvenementOpslaan(evenement);
    };

    $scope.init = function (evenementId) {
        reservatieSvc.getReservatieData(evenementId).then(function(data) {
            $scope.evenement = data.evenement;
            $scope.evenement.reservatieData = data.reservatieData;
        });
    };
});

eventplanner.factory('reservatieSvc', function($http, $q, notifier) {
    return {
        getReservatieData: function (evenementId) {
            var dfd = $q.defer();
            $http.get('/Reservatie/GetReservatieData', { params: { EvenementId: evenementId } })
                .success(dfd.resolve)
                .error(function () {
                    notifier.error("something went wrong");
                });
            return dfd.promise;
        },
        detailsEvenementOpslaan: function (evenement) {
            $http.post("/Reservatie/OmschrijvingAanpassen", evenement)
                .success(function() {
                    notifier.notify("wijzigingen succesvol opgeslagen");
                })
                .error(function() {
                    notifier.error("someting went wrong");
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
