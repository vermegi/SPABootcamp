//controllers
eventplanner.controller('DetailEvenementCtrl', function ($scope, reservatieSvc) {

    $scope.detailEvenementOpslaan = function () {
        reservatieSvc.detailsEvenementOpslaan($scope.evenement);
    };

    $scope.init = function () {
        reservatieSvc.getReservatieData($scope.evenementId)
            .then(function (data) {
                $scope.evenement = data.evenement;
                $scope.reservatieData = data.reservatieData;
            });
    };
});

eventplanner.controller('ReservatieDetailCtrl', function ($scope) {
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
        //$scope.$broadcast('straatGeselecteerd');
    };

    $scope.straatVerwijderen = function (straat) {
        var index = $scope.toegevoegdeStraten.indexOf(straat);
        $scope.toegevoegdeStraten.splice(index, 1);
    };
});

eventplanner.controller('StratenCtrl', function ($scope, $http) {
    $scope.getLocations = function(val) {
        if (val === undefined || val.length < 2)
            return;

        return $http.get('/straten/straat?zoekstraat=' + val)
            .then(function(response) {
                return response.data;
            });
    };

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
        if (tableState === undefined) {
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
