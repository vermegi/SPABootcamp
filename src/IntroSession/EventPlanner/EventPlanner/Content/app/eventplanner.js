var eventplanner = angular.module('EventPlanner', []);

eventplanner.controller('DetailEvenementController', function ($scope,$http) {
    $scope.detailEvenementOpslaan = function () { alert('test'); };

    $scope.init = function (evenementId) {
        $http.get('/Reservatie/GetReservatieData',
                { params: { evenementId: evenementId } })
            .success(function(data) {
                $scope.evenement = data.evenement;
                $scope.reservatieData = data.reservatieData;
            })
            .error(function() {
                alert('oops', 'I did it again');
            });
        
    };
});