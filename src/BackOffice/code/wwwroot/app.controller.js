(function () {
  'use strict';

  function appController($scope, eventsService) {

    var unsubscribe = [];

    console.log($scope.model);

    var vm = this;

    console.log(vm);

    unsubscribe.push(eventsService.on("editors.documentType.saved",
      function () {
        console.log('Doc type saved');

      }));

    vm.$onDestroy = function () {
      console.log('on destroy called');
      unsubscribe.forEach(x => x());
    }
  }

  angular.module('umbraco').controller('Umbraco.Community.Indekso.AppController',
    ['$scope', 'eventsService', appController]);
})();
