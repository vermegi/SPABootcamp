//describe("A suite", function () {
//    it("contains spec with an expectation", function () {
//        expect(true).toBe(true);
//    });
//});

describe("The PeriodeCtrl", function () {
    beforeEach(module("EventPlanner"));
    
    var $controller;

    beforeEach(inject(function(_$controller_) {
        $controller = _$controller_;
    }));

    describe('$scope.toegevoegdeStraten', function () {
        it('adds a straat to the toegevoegdeStraten', function() {
            var $scope = {
                $broadcast: function(){}
            };
            var controller = $controller('PeriodeCtrl', { $scope: $scope });
            $scope.straatToevoegen({naam:"een straat", Id: 123});
            expect($scope.toegevoegdeStraten.length).toBe(1);
        });
    });
});