var eventplanner = angular.module('EventPlanner', []);

eventplanner.controller('DetailEvenementController', function ($scope,ReservatieService) {
    $scope.detailEvenementOpslaan = function () { alert('test'); };

    $scope.init = function (evenementId) {
        ReservatieService.getReservatieData(evenementId)
            .then(function (data) {
                $scope.evenement = data.evenement;
                $scope.reservatieData = data.reservatieData;
            });
        
        
    };
});

eventplanner.factory('ReservatieService', function($http, $q) {

    return {
        getReservatieData: function (evenementId) {
            var dfd = $q.defer();
            $http.get('/Reservatie/GetReservatieData',
                { params: { evenementId: evenementId } })
                .success(dfd.resolve)
                .error(function() {
                    alert('oops', 'I did it again');
                });
            return dfd.promise;
        }
    };
});

