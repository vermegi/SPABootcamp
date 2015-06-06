//services
eventplanner.factory('reservatieSvc', function ($http, $q, notifier) {
    return {
        getReservatieData: function(evenementId) {
            var dfd = $q.defer();
            $http.get('/api/Reservatie/GetReservatieData', { params: { EvenementId: evenementId } })
                .success(dfd.resolve)
                .error(function() {
                    notifier.error('something went wrong!');
                });
            return dfd.promise;
        },
        detailsEvenementOpslaan: function(evenement) {
            $http.post("/api/Reservatie/OmschrijvingAanpassen", evenement)
                .success(function() {
                    notifier.notify('changes saved succesfully');
                })
                .error(function() {
                    notifier.error("something went wrong");
                });
        },
        getReservatiesData: function(datumVan, datumTot, start, number, searchTerm) {
            var dfd = $q.defer();
            $http.get('/api/Reservaties/GetForDate', { params: { DatumVan: datumVan, DatumTot: datumTot, Start: start, Number: number, SearchTerm: searchTerm } })
                .success(dfd.resolve)
                .error(function(err) {
                    notifier.error('Er is iets misgelopen');
                });
            return dfd.promise;
        }
    };
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
        notify: function(msg) {
            toastr.success(msg, "");
        },
        error: function(msg) {
            toastr.error(msg, "");
        }
    };
});
