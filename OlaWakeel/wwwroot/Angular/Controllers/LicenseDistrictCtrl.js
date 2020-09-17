App.controller("LicenseDistrictCtrl", function ($scope, $http) {
    function JsonCallParam(Controller, Action, Parameters) {
        $.ajax({
            type: "POST",
            traditional: true,
            async: false,
            cache: false,
            url: '/' + Controller + '/' + Action,
            context: document.body,
            data: Parameters,
            success: function (json) {
                list = null;
                list = json;
            }
            ,
            error: function (xhr) {
                list = null;
            }
        });
    }
    function JsonCall(Controller, Action) {
        $.ajax({
            type: "POST",
            traditional: true,
            async: false,
            cache: false,
            url: '/' + Controller + '/' + Action,
            context: document.body,
            success: function (json) {
                list = null; list = json;
            },
            error: function (xhr) {
                list = null;
                //debugger;
            }
        });
    }

    //district
    $scope.allDistrict = [];
    $scope.addDistrictObj = { DistrictName: '' };
    $scope.editDistrictObj = { licenseDistrictId: 0, districtName: '' };

    //city
    $scope.allCity = [];
    $scope.addCityObj = { cityName: '', licenseDistrictId: 0, licenseExist: true };
    $scope.editCityObj = { licenseCityId:0, cityName: '', licenseDistrictId: 0, licenseExist: true };

    $scope.getAllDistrict = function () {
        JsonCall("LicenseDistrict", "GetAllLicenseDistrict");
        if (list !== null)
        {
            $scope.allDistrict = list;
        }
    }
    
    $scope.getAllCity = function ()
    {
        JsonCall("licenseCity", "GetAllLicenseCity");
        if (list !== null) {
            $scope.allCity = list;
        }
    }
});