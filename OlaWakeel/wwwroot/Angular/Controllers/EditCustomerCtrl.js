App.controller("CustomerCtrl", function ($scope, $http) {
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

    $scope.addCustomerAccount = { userName: '', email: '', passwordHash: '' };
    $scope.addCustomerProfile = { customerId: '0', firstName: '', lastName: '', dateOfBirth: '', gender: '', contact: '', city: '', address: '', appUserId: '0' };

   
    $scope.btnSubmit = false;
    $scope.validateCustomer = function () {
        var fileInput = document.getElementById('fileInput');
        var dob = $("#dob").val();
        if ($scope.addCustomerProfile.firstName === undefined
            || $scope.addCustomerProfile.lastName === undefined
            // || $scope.addCustomerProfile.dateOfBirth === ""
            || dob === ""
            || $scope.addCustomerAccount.userName === undefined
            || $scope.addCustomerAccount.email === undefined
           // || $scope.addCustomerAccount.passwordHash === undefined
            || $scope.addCustomerProfile.gender === undefined
            || $scope.addCustomerProfile.contact === undefined
            //  || $scope.addCustomerProfile.city === ""
            || $scope.addCustomerProfile.address === undefined
           // || fileInput.files.length === 0
        ) {
            $scope.btnSubmit = true;
        }
        else {
            //    $('#validation').hide();
            $scope.btnSubmit = false;
        }
    }

    //add Customer
    $scope.addCustomer = function () {
        $scope.addCustomerProfile.dateOfBirth = $("#dob").val();
       // if (!validateCustomer()) { return; }
        var formdata = new FormData();
        var fileInput = document.getElementById('fileInput');
       // formdata.append(fileInput.files[0].name, fileInput.files[0]);
        if (fileInput.files.length !== 0) { formdata.append(fileInput.files[0].name, fileInput.files[0]); } else { $scope.addCustomerAccount.profilePic = $("#profilepic").val() }
        formdata.append("appUser", JSON.stringify($scope.addCustomerAccount));
        formdata.append("customer", JSON.stringify($scope.addCustomerProfile));

        var xhr = new XMLHttpRequest();
        xhr.open('POST', '/Customer/AddCustomerPost');
        xhr.send(formdata);

        xhr.onreadystatechange = function () {
            if (xhr.readyState === 2 && xhr.status === 200) {
                //$("#addModal").modal("hide");
                //swal("Added", "Lawyer Added!", "success");
                //$scope.addCategoryObj = { categoryName: '', isAvailable: true };
                //$scope.getAllCategories();
                window.location.reload();
            }

        };
        return false;

        //var pram = { "appUser": JSON.stringify($scope.addCustomerAccount), "customer": JSON.stringify($scope.addCustomerProfile) };
        //JsonCallParam("Customer", "AddCustomer", pram);
        //if (list === "Success") {
        //    //swal("Added", "Customer Added!", "success");
        //    window.location.reload();
        //}
    }

    //validate CUSTOMER

    //get edit Customer
    $scope.getCustomer = function () {
        // if (!validateLawyer()) { return; }
        // var pram = { "appUser": JSON.stringify($scope.addCustomerAccount), "customer": JSON.stringify($scope.addCustomerProfile) };
        JsonCall("Customer", "GetCustomerById/" + Number($("#UserId").val()));
        if (list != null) {

            $scope.addCustomerAccount.email = list.appUser.email;
            $scope.addCustomerAccount.userName = list.appUser.userName;
            $scope.oldUser = list.appUser.userName;
            //list.appUser = null;
            $scope.addCustomerProfile.firstName = list.firstName;
            $scope.addCustomerProfile.lastName = list.lastName;
            $scope.addCustomerProfile.customerId = list.customerId;
            $scope.addCustomerProfile.appUserId = list.appUserId;
            $scope.addCustomerProfile.city = list.city;
            $scope.addCustomerProfile.address = list.address;
            $scope.addCustomerProfile.contact = list.contact;
            $scope.addCustomerProfile.gender = list.gender;
            //swal("Added", "Customer Added!", "success");
            //window.location.reload();
        }
    }
    $scope.getCustomer();


    //edit Customer
    $scope.editCustomer = function () {
        $scope.addCustomerProfile.dateOfBirth = $("#dob").val();
        $scope.addCustomerProfile.address = $("#autocomplete").val();
        $scope.addCustomerProfile.city = $("#locality").val();
        //  if (!validateLawyer()) { return; }
       // var pram = { "appUser": JSON.stringify($scope.addCustomerAccount), "customer": JSON.stringify($scope.addCustomerProfile) };
       // JsonCallParam("Customer", "EditCustomer", pram);
       // if (list === "Success") {
            //swal("Edit", "Customer Edited!", "success");
         //   window.location.href = "/Customer/Index";
            // swal("Added", "Customer Added!", "success");
            // window.location.reload();

        $scope.addCustomerProfile.dateOfBirth = $("#dob").val();
        // if (!validateCustomer()) { return; }
        var formdata = new FormData();
        var fileInput = document.getElementById('fileInput');
        if (fileInput.files.length !== 0)
        { formdata.append(fileInput.files[0].name, fileInput.files[0]); }
       
        formdata.append("appUser", JSON.stringify($scope.addCustomerAccount));
        formdata.append("customer", JSON.stringify($scope.addCustomerProfile));

        var xhr = new XMLHttpRequest();
        xhr.open('POST', '/Customer/EditCustomer');
        xhr.send(formdata);

        xhr.onreadystatechange = function () {
            if (xhr.readyState === 2 && xhr.status === 200) {
                //$("#addModal").modal("hide");
                //swal("Added", "Lawyer Added!", "success");
                //$scope.addCategoryObj = { categoryName: '', isAvailable: true };
                //$scope.getAllCategories();
                window.location.reload();
            }

        };
        return false;
        }
    
    //check username availabilty
    //$scope.checkUserAvailable = function () {
        
    //    var pram = { "appUser": JSON.stringify($scope.addCustomerAccount) };
    //    JsonCallParam("Lawyer", "CheckUserAvailabilty", pram);
    //    if (list === "Unsuccess") {
    //        $('#checkUser').show();
    //        // $scope.userValidate = "this username is already exit";
    //        // swal("Added", "Lawyer Added!", "success");
    //        // window.location.reload();
    //    }
    //    else { $('#checkUser').hide(); }
    //}
    
    //check username availabilty 
    //$scope.checkUserAvailable = function () {
    //    var old = $scope.oldUser;
    //    var pram = { "appUser": JSON.stringify($scope.addCustomerAccount), old };
    //    JsonCallParam("Auth", "CheckUserAvailabilty", pram);
    //    if (list === "Unsuccess") {
    //        $('#checkUser').show();
    //        // $scope.userValidate = "this username is already exit";
    //        // swal("Added", "Lawyer Added!", "success");
    //        // window.location.reload();
    //    }
    //    else { $('#checkUser').hide(); }
    //}
});