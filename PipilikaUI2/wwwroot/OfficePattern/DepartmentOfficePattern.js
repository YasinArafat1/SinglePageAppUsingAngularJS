

var app = angular.module('myApp', []);

app.controller('myCtrl1', function ($scope, $http, $sce) {

    $scope.product = [];
    $scope.formData = {};
    $scope.empid = '';
    $scope.btnUpdateEmpDisabled = true;
    $scope.btnRefreshEmpDisabled = false;

    $scope.btnEditEmpDisabled = false;





    $scope.getproducts = function () {
        //console. log($scope.getDepartmentByUrl);     
        $http.get($scope.getDepartmentByUrl)
            .then(function (response) {
                console.log('API Response:', response);
                $scope.product = response.data;
            })
            .catch(function (error) {
                
            });
    };





    $scope.createEmployee = function () {
        $http.post($scope.updateDepartmentByIdUrl, $scope.formData)
            .then(function (response) {
                $scope.clearform();
                $scope.getproducts();
            }, function (error) {
                alert("Error is " + error.data);
            });
    };

    $scope.editEmployee = function (id) {
        $http.get($scope.editDepartmentByIdUrl + id)
            .then(function (response) {
                var result = response.data;
                if (result) {
                    $scope.empid = result.departmentId;
                    $scope.formData.DepartmentName = result.departmentName;
                    $scope.formData.ShortName = result.shortName;
                }
                $scope.btnUpdateEmpDisabled = false;
                $scope.btnEditEmpDisabled = true;
            }, function (error) {
                console.error('Edit error', error.data);
                alert('Error while fetching data: ' + error.data);
            });
    };

    $scope.updateEmployee = function () {
        $http.put($scope.updateDepartmentByIdUrl + $scope.empid, $scope.formData)
            .then(function () {
                $scope.clearform();
                $scope.getproducts();
                $scope.btnUpdateEmpDisabled = true;
            }, function (error) {
                alert("Error while updating data" + error.data);
            });
    };

    $scope.deleteEmployee = function (id) {
        $http.delete($scope.deleteDepartmentByIdUrl + id)
            .then(function (response) {
                $scope.clearform();
                $scope.getproducts();
                alert("Are you sure you want to delete this employee?");
            }, function (error) {
                alert(error.data);
            });
    };

    $scope.clearform = function () {
        $scope.formData = {};
        $scope.btnRefreshEmpDisabled = true;
        $scope.btnEditEmpDisabled = false;
        $scope.btnRefreshEmpDisabled = false;
    };




   

    $scope.Init = function (editDepartmentByIdUrl, deleteDepartmentByIdUrl, updateDepartmentByIdUrl, getDepartmentByUrl) {
        console.log('Edit URL:', editDepartmentByIdUrl);
        console.log('Delete URL:', deleteDepartmentByIdUrl);
        console.log('Update URL:', updateDepartmentByIdUrl);
        console.log('Get URL:', getDepartmentByUrl);

        $scope.editDepartmentByIdUrl = editDepartmentByIdUrl;
        $scope.deleteDepartmentByIdUrl = deleteDepartmentByIdUrl;
        $scope.updateDepartmentByIdUrl = updateDepartmentByIdUrl;
        $scope.getDepartmentByUrl = getDepartmentByUrl;

        $scope.getproducts();
    };




});
