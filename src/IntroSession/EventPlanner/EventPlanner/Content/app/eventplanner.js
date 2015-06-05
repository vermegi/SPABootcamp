var eventplanner = angular.module("EventPlanner", []);

eventplanner.controller('DetailEvenementCtrl', function($scope, reservatieSvc) {

    $scope.evenement = {
        titel: 'titel van het evenement',
        id: 123,
        eigenaar: 'de eigenaar',
        omschrijving: 'een vreed lange omschrijving van je yada dada',
        muziekVergunning: true,
        reservatieData: ['21/10/1980', '30/5/2015', '27/8/2015']
    };

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
