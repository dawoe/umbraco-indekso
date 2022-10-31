(function () {
  'use strict';

  function appController($scope, eventsService, angularHelper) {

    var events = [];
    var watchers = [];
    var isLoading = true;

    var vm = this;

    // get the doctype editor scope so we can listen to changes being made
    var doctypEditorScope = angularHelper.traverseScopeChain($scope,
      s => s && s.vm && s.vm.constructor.name == "DocumentTypesEditController");

    vm.form = angularHelper.getRequiredCurrentForm(doctypEditorScope);

    vm.save = function() {
      console.log('Save clicked');
    }
    
    events.push(eventsService.on("editors.documentType.saved",
      function () {
        console.log('Doc type saved');

      }));

    $scope.$on('$destroy', function () {
      console.log('on destroy called');
      events.forEach(x => x());
      watchers.forEach(x => x());
    });

    function init() {
      watchers.push(
        $scope.$watch('vm.form.$dirty',
          function (newVal, oldVal) {
            if (isLoading) {
              return;
            }

            if (oldVal === undefined) {
              // still initializing, ignore
              return;
            }

            if (oldVal === newVal) {
              return;
            }

            console.log(oldVal);
            console.log(newVal);

          }, true));

      isLoading = false;
    }
    
    init();
  }

  angular.module('umbraco').controller('Umbraco.Community.Indekso.AppController',
    ['$scope', 'eventsService','angularHelper', appController]);
})();
