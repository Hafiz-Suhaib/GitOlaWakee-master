App.controller("CategoryCtrl", function ($scope, $http) {
    $scope.displayed = false;
    $scope.toggleClick = function () {
        if (!$scope.displayed) { 
            $scope.displayed = true;
        }
        else {
            $scope.displayed = false;
        }
    }
});