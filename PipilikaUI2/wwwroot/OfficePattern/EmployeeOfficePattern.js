
var app = angular.module('myApp', []);
app.controller('myCtrl2', function ($scope, $http) {
    $scope.product = [];
    $scope.formData = {};
    $scope.empId = '';
    $scope.btnUpdateEmpDisabled = true;
    $scope.btnEditEmpDisabled = false;
    $scope.btnRefreshEmpDisabled = false;




    $scope.pagination = {
        pageNumber: 1,
        pageSize: 12,
        TotalPages: 0,
        TotalRecords: 0
    };

    $scope.TotalPages = 0;

    $scope.getProducts = function () {
        $http.get($scope.getEmployeeByUrl, { params: { pageNumber: $scope.pagination.pageNumber, pageSize: $scope.pagination.pageSize } })
            .then(function (response) {
                $scope.product = response.data;
                $scope.pagination.TotalRecords = response.data.TotalRecords;
                $scope.TotalPages = response.data.TotalPages;
                console.log(response.data.TotalPages);
            });
    };

    $scope.nextPage = function () {
        console.log("event clicked before logic");
        if ($scope.pagination.pageNumber < $scope.TotalPages) {
            $scope.pagination.pageNumber++;
            $scope.getProducts();
            console.log("event clicked after logic");
        }
    };

    $scope.prevPage = function () {
        if ($scope.pagination.pageNumber > 1) {
            $scope.pagination.pageNumber--;
            $scope.getProducts();
        }
    };







    //$scope.getProducts = function () {

    //    $http.get($scope.getEmployeeByUrl)
    //        .then(function (response) {

    //            $scope.product = response.data;
    //        });
    //};



    //$scope.createemployee = function () {

    //    $scope.formdata.dateofbirth = $scope.formdata.dateofbirth;
    //    $scope.formdata.gender = parseint($scope.formdata.gender);
    //    $scope.formdata.bloodgroup = parseint($scope.formdata.bloodgroup);
    //    $scope.formdata.userstatus = parseint($scope.formdata.userstatus);
    //    //console.log($scope.formdata);
    //    $http.post($scope.updateEmployeeByIdUrl, $scope.formdata)
    //        .then(function (response) {
    //            $scope.clearform();
    //            $scope.getProducts();
    //        })
    //        .catch(function (error) {
    //            $scope.validationerrors = error.data;
    //        });
    //};

    $scope.createEmployee = function () {
        $scope.formData.DateOfBirth = $scope.formData.DateOfBirth;
        $scope.formData.Gender = parseInt($scope.formData.Gender);
        $scope.formData.BloodGroup = parseInt($scope.formData.BloodGroup);
        $scope.formData.UserStatus = parseInt($scope.formData.UserStatus);

        $http.post($scope.updateEmployeeByIdUrl, $scope.formData)
            .then(function (response) {

                $scope.clearForm();
                $scope.product.unshift(response.data);

            })
            .catch(function (error) {
                $scope.validationErrors = error.data;
            });
    };





    $scope.editEmployee = function (id) {
        $http.get($scope.editEmployeeByIdUrl + id)
            .then(function (response) {
                console.log(response.data);
                var result = response.data;
                if (result) {
                    $scope.empId = result.employeeId;
                    $scope.formData.EmployeeName = result.employeeName;
                    $scope.formData.FatherName = result.fatherName;
                    $scope.formData.MotherName = result.motherName;
                    $scope.formData.Username = result.username;
                    $scope.formData.Email = result.email;
                    $scope.formData.MobileNo = result.mobileNo;
                    $scope.formData.DateOfBirth = new Date(result.dateOfBirth);
                    $scope.formData.Gender = result.gender;
                    $scope.formData.BloodGroup = result.bloodGroup;
                    $scope.formData.UserStatus = result.userStatus;
                    $scope.formData.Education = result.education;


                }
                $scope.btnUpdateEmpDisabled = false;
                $scope.btnEditEmpDisabled = true;

            }, function (error) {

                console.error('Edit error', error.data);
                alert('error while fatching data :' + error.data);
            });
    };


    $scope.updateEmployee = function () {
        $http.put($scope.updateEmployeeByIdUrl + $scope.empId, $scope.formData)
            .then(function () {
                $scope.clearForm();
                $scope.getProducts();
                $scope.btnUpdateEmpDisabled = true;

            }, function (error) {
                alert("error while updating data" + error.data);
            });

    };


    $scope.deleteEmployee = function (id) {
        $http.delete($scope.deleteEmployeeByIdUrl + id)
            .then(function (response) {
                $scope.delByIndex = $scope.product.findIndex(x => x.employeeId == id);
                $scope.product.splice($scope.delByIndex, 1);
                //$scope.clearForm();
                //$scope.getProducts();
                alert("Are you sure you want to delete this employee?");
            }, function (error) {
                alert(error.data);
            });
    };



    $scope.clearForm = function () {
        $scope.formData = {};
        $scope.btnRefreshEmpDisabled = true;
        $scope.btnEditEmpDisabled = false;
        $scope.btnRefreshEmpDisabled = false;
    };



    $scope.getGenderString = function (value) {
        switch (value) {
            case 0:
                return 'Male';
            case 1:
                return 'Female';
            case 2:
                return 'Other';
            default:
                return '';
        }
    };

    $scope.getBloodGroupString = function (value) {
        switch (value) {
            case 0:
                return 'A+';
            case 1:
                return 'A-';
            case 2:
                return 'B+';
            case 3:
                return 'O+';
            case 4:
                return 'O-';
            case 5:
                return 'AB+';
            case 6:
                return 'AB-';
            default:
                return '';
        }
    };

    $scope.getUserStatusString = function (value) {
        switch (value) {
            case 0:
                return 'Active';
            case 1:
                return 'Inactive';

            default:
                return '';
        }
    };


    


    $scope.Init = function (editEmployeeByIdUrl, deleteEmployeeByIdUrl, updateEmployeeByIdUrl, getEmployeeByUrl) {
        console.log('Edit URL:', editEmployeeByIdUrl);
        console.log('Delete URL:', deleteEmployeeByIdUrl);
        console.log('Update URL:', updateEmployeeByIdUrl);
        console.log('Get URL:', getEmployeeByUrl);

        $scope.editEmployeeByIdUrl = editEmployeeByIdUrl;
        $scope.deleteEmployeeByIdUrl = deleteEmployeeByIdUrl;
        $scope.updateEmployeeByIdUrl = updateEmployeeByIdUrl;
        $scope.getEmployeeByUrl = getEmployeeByUrl;

        $scope.getProducts();
    };

    




  
});



      