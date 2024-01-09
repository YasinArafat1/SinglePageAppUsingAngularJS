var app = angular.module('myApp', []);
app.controller('myCtrl', function ($scope,$http) {
    $scope.product = [];
    $scope.formData = {};
    $scope.empId = '';
    $scope.btnUpdateEmpDisabled = true;
    $scope.btnEditEmpDisabled = false;

    $scope.getProducts = function () {

        $http.get("https://localhost:7133/api/Departments")
            .then(function (response) {

                $scope.product = response.data;
            });
    };


    $scope.createEmployee = function () {
        $http.post('https://localhost:7133/api/Departments', $scope.formData)
            .then(function (response) {
                $scope.clearForm();
                $scope.getProducts();
            }, function (error) {
                alert(error.data);

            });


    };


    $scope.editEmployee = function (id) {
        $http.get('https://localhost:7133/api/Departments/' + id)
            .then(function (response) {
                var result = response.data;
                if (result) {
                    $scope.empId = result.departmentId;
                    $scope.formData.DepartmentName = result.departmentName;
                    $scope.formData.ShortName = result.shortName;

                }
                $scope.btnUpdateEmpDisabled = false;
                $scope.btnEditEmpDisabled = true;
                
            }, function (error) {

                console.error('Edit error', error.data);
                alert('error while fatching data :'+ error.data);
            });
    };


    $scope.updateEmployee = function () {
        $http.put('https://localhost:7133/api/Departments/' + $scope.empId, $scope.formData)
            .then(function () {
                $scope.clearForm();
                $scope.getProducts();
                $scope.btnUpdateEmpDisabled = true;
               
            }, function (error) {
                alert("error while updating data" + error.data);
            });

    };


    $scope.deleteEmployee = function (id) {
        $http.delete('https://localhost:7133/api/Departments/' + id)
            .then(function (response) {
                $scope.clearForm();
                $scope.getProducts();
                alert("Are you sure you want to delete this employee?");
            }, function (error) {
                alert(error.data);
            });
    };



    $scope.clearForm= function(){
        $scope.formData = {};
    };

    $scope.getProducts();
});
