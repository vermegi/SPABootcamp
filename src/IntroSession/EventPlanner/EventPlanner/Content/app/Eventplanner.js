var eventplanner = angular.module("EventPlanner", []);

eventplanner.controller('detailEvenementCtrl', function ($scope) {
    $scope.evenement = {
        titel: "titel van het evenement",
        id: 123,
        eigenaar: 'de eigenaar',
        omschrijving: 'een vreed lange omschrijving van je yada dada',
        muziekVergunning: true,
        reservatieData: ["21/10/1980", "30/5/2015", "27/8/2015"]
    };
    $scope.detailEvenementOpslaan = function () {
        
    };
});

