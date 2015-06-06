//directives
eventplanner.directive('evenementheader', function () {
    return {
        scope: {
            evenementInfo: '=info'
        },
        template: '{{evenementInfo.titel}} ({{evenementInfo.eigenaar}})'
    };
});