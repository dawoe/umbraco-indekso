(function() {
  'use strict';

  function appController($scope) {

    console.log($scope.model);

    var vm = this;

    console.log(vm);
  }

  angular.module('umbraco').controller('Umbraco.Community.Indekso.AppController',
    ['$scope', appController]);
})();
