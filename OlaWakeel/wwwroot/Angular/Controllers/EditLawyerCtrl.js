
App.controller("LawyerCtrl", function ($scope, $http) {
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
    //Add Lawyer Objects
    $scope.addLawyerAccount = { userName: '', email: '', passwordHash: '' };

    $scope.addLawyerProfile = {
        firstName: '', lastName: '', dateOfBirth: '', gender: '', contact: '', cnic: '', city: '', country: '',
        address: '', biography: '', virtualChargesPkr: 0, virtualChargesUs: 0, totalExperience: 0, rating: 0, userId: '0'
    };
    $scope.addLawyerCharges = { pkrCharges: 0, usCharges: 0 };

    //bool type for toggles
    $scope.timing = true;
    $scope.qualification = Boolean;
    $scope.experience = Boolean;
    $scope.license = Boolean;
    $scope.about = Boolean;
    $scope.office = Boolean;
    $scope.pakage = Boolean;
    $scope.specialization = false;
    $scope.charges = true;
    $scope.feesToggle = false;

    //lists for dropdown
    $scope.degreeTypeList = [];
    $scope.degreeList = [];
    $scope.specializationList = [];
    $scope.caseCategoryList = [];
    $scope.cityList = [];
    $scope.degrees = [];

    //arrays 
    $scope.lawyerTimings = [];
    $scope.lawyerQualifications = [];
    $scope.lawyerExperiences = [];
    $scope.lawyerSpecializations = [];
    $scope.lawyerClients = [];
    $scope.lawyerLanguages = [];
    $scope.addresses = [];
    $scope.lawyerLicenses = [];
    $scope.lawyerCaseCategory = [];
    $scope.profileAccount = [];

    //lawyer timing selectlists
    $scope.yearList = [
        { id: 1, name: '1 Year' },
        { id: 2, name: '2 Year' },
        { id: 3, name: '3 Year' },
        { id: 4, name: '4 Year' },
        { id: 5, name: '5 Year' },
        { id: 6, name: '6 Year' },
        { id: 7, name: '7 Year' },
        { id: 8, name: '8 Year' },
        { id: 9, name: '9 Year' },
        { id: 10, name: '10 Year' },
        { id: 11, name: '11 Year' },
        { id: 12, name: '12 Year' },
        { id: 13, name: '13 Year' },
        { id: 14, name: '14 Year' },
        { id: 15, name: '15 Year' },
        { id: 16, name: '16 Year' },
        { id: 17, name: '17 Year' },
        { id: 18, name: '18 Year' },
        { id: 19, name: '19 Year' },
        { id: 20, name: '20 Year' },
        { id: 21, name: '21 Year' },
        { id: 22, name: '22 Year' },
        { id: 23, name: '23 Year' },
        { id: 24, name: '24 Year' },
        { id: 25, name: '25 Year' },
        { id: 26, name: '26 Year' },
        { id: 27, name: '27 Year' },
        { id: 28, name: '28 Year' },
        { id: 29, name: '29 Year' },
        { id: 30, name: '30 Year' }];
    $scope.days = [{ id: "Monday", name: 'Monday' }, { id: "Tuesday", name: 'Tuesday' }, { id: 'Wednesday', name: 'Wednesday' }, { id: 'Thursday', name: 'Thursday' }, { id: 'Friday', name: 'Friday' }, { id: 'Saturday', name: 'Saturday' }, { id: 'Sunday', name: 'Sunday' }];
    $scope.timeList = [
        { id: "12:00 AM", time: "12:00 AM" },
        { id: "01:00 AM", time: "01:00 AM" },
        { id: "02:00 AM", time: "02:00 AM" },
        { id: "03:00 AM", time: "03:00 AM" },
        { id: "04:00 AM", time: "04:00 AM" },
        { id: "05:00 AM", time: "05:00 AM" },
        { id: "06:00 AM", time: "06:00 AM" },
        { id: "07:00 AM", time: "07:00 AM" },
        { id: "08:00 AM", time: "08:00 AM" },
        { id: "09:00 AM", time: "09:00 AM" },
        { id: "10:00 AM", time: "10:00 AM" },
        { id: "11:00 AM", time: "11:00 AM" },
        { id: "12:00 PM", time: "12:00 PM" },
        { id: "01:00 PM", time: "01:00 PM" },
        { id: "02:00 PM", time: "02:00 PM" },
        { id: "03:00 PM", time: "03:00 PM" },
        { id: "04:00 PM", time: "04:00 PM" },
        { id: "05:00 PM", time: "05:00 PM" },
        { id: "06:00 PM", time: "06:00 PM" },
        { id: "07:00 PM", time: "07:00 PM" },
        { id: "08:00 PM", time: "08:00 PM" },
        { id: "09:00 PM", time: "09:00 PM" },
        { id: "10:00 PM", time: "10:00 PM" },
        { id: "11:00 PM", time: "11:00 PM" }];

    // degreeList for dropdown
    $scope.getDegree = function () {
        JsonCall("Degree", "DegreeList");
        if (list != null) {
            $scope.degreeList = list;
        }
    }
    $scope.getDegree();


    //$scope.casCadingEdit = function (i, degreeTypeId) {
    //    $scope.degrees[i] = $scope.degreeList.filter(function (c) {
            
            
            

    //        return c.degreeTypeId == degreeTypeId;

    //    });
    //}
    // specializationList for dropdown
    $scope.getProfileAccount = function () {
        JsonCall("Lawyer", "UpdateLawyer/" + Number( $("#UserId").val()));
        if (list != null) {
            $scope.getLang = [];
            $scope.getServices = [];
            $scope.addLawyerProfile.lawyerId = list.lawyerId;
            $scope.addLawyerAccount.email = list.appUser.email;
            $scope.addLawyerAccount.userName = list.appUser.userName;
            $scope.addLawyerProfile.firstName = list.firstName;
            $scope.addLawyerProfile.lastName = list.lastName;
           // $scope.addLawyerProfile.dateOfBirth = list.dateOfBirth;  
            $scope.addLawyerProfile.gender = list.gender;
            $scope.addLawyerProfile.contact = list.contact;  
            $scope.addLawyerProfile.address = list.address;
            $scope.addLawyerProfile.city = list.city;
            $scope.addLawyerProfile.cnic = list.cnic;
            $scope.addLawyerProfile.country = list.country;
            $scope.addLawyerProfile.biography = list.biography;
            $scope.addLawyerProfile.totalExperience = list.totalExperience;
            $scope.addLawyerProfile.appUserId = list.appUserId;




            //$scope.addLawyerProfile.dateOfBirth = $filter('date')(list.dateOfBirth, "yyyy-MM-dd");
            $scope.lawyerQualifications = list.lawyerQualifications;
            $scope.lawyerExperiences = list.lawyerExperiences;
            $scope.lawyerLicenses = list.lawyerLicenses;
            $scope.lawyerClients = list.lawyerClients;
            $scope.lawyerTimings = list.lawerTimings;
            $scope.addresses = list.lawyerAddresses

            //lawyer languages as selected in select2 multiple
            for (i = 0; i < list.lawyerCaseCategories.length; i++) {
                $scope.getServices.push(list.lawyerCaseCategories[i].caseCategoryId.toString());
            }
            //$("#GetCaseCatIds").val($scope.getServices).change();

            //lawyer languages as selected in select2 multiple
            for (i = 0; i < list.lawyerLanguages.length; i++) {
                $scope.getLang.push(list.lawyerLanguages[i].language);
            }

            for (i = 0; i < $scope.lawyerQualifications.length; i++) {

                $scope.degrees[i] = $scope.degreeList.filter(function (c) {
                    return c.degreeTypeId == $scope.lawyerQualifications[i].degreeTypeId;
                });
            }
            //$("#GetLanguages").val("Urdu","English").change();
            //if (list.virtualChargesPkr !== 0) {
            //    var check = $(".charges input").prop("checked", true);
            //    $(".feeToggle input").prop("checked", true);
            //    $('#fee').show();
            //    $scope.qualification = true;}
        }
    }

    $scope.getProfileAccount();
    
    // specializationList for dropdown
    $scope.getSpecialization = function () {
        JsonCall("Specialization", "SpecializationList");
        if (list != null) {
            $scope.specializationList = list;
        }
    }
   

    // getDegreeType for dropdown
    $scope.getDegreeType = function () {
        JsonCall("DegreeType", "GetAllDegreeTypes");
        if (list != null) {
            $scope.degreeTypeList = list;
        }
    }

    // GetCityList for dropdown
    $scope.getCityList = function () {
        JsonCall("LicenseCity", "GetAllLicenseCity");
        if (list != null) {
            $scope.cityList = list;
        }
    }

    // caseCategoryList for dropdown
    $scope.getCaseCategory = function () {
        JsonCall("CaseCategory", "GetAllCaseCategoryList");
        if (list != null) {
            $scope.caseCategoryList = list;
           // console.log($scope.caseCategoryList);
        }
    }
    //check username availabilty
    $scope.checkUserAvailable = function () {

        var pram = { "appUser": JSON.stringify($scope.addLawyerAccount) };
        JsonCallParam("Auth", "CheckUserAvailabilty", pram);
        if (list === "Unsuccess") {
            $('#checkUser').show();
            // $scope.userValidate = "this username is already exit";
            // swal("Added", "Lawyer Added!", "success");
            // window.location.reload();
        }
        else { $('#checkUser').hide(); }
    }



    //validate Lawyer
    //function validateLawyer() {
    //    var fileInput = document.getElementById('fileInput');
    //    if ($scope.addLawyerProfile.firstName === ""
    //        || $scope.addLawyerProfile.lastName === ""
    //        || $scope.addLawyerAccount.userName === ""
    //        || $scope.addLawyerAccount.email === ""
    //        || $scope.addLawyerAccount.passwordHash === ""
    //        || $scope.addLawyerProfile.gender === ""
    //        || $scope.addLawyerProfile.contact === ""
    //        || $scope.addLawyerProfile.cnic === ""
    //        || $scope.addLawyerProfile.totalExperience === 0
    //        || $scope.addLawyerProfile.city === ""
    //        || $scope.addLawyerProfile.address === ""
    //        || fileInput.files.length === 0) {
    //        $('#validation').show();
    //        return false;
    //    }
    //    else {
    //        $('#validation').hide();
    //        return true;
    //    }
    //}
    $scope.lawyerLanguagesTemp = [];
    $scope.lawyerCaseCategoryTemp = [];
    //add lawyer
    $scope.addLawyer = function () {
        //if (!validateLawyer()) { return; }

        ////pushing lawyer services in array
        $scope.lawyerLanguagesTemp = $("#GetLanguages").val();
        $scope.lawyerCaseCategoryTemp = $("#GetCaseCatIds").val();
      //  console.log($scope.lawyerCaseCategory);
        //console.log($("#caseCategoryId").val());

        for (i = 0; i < $scope.lawyerLanguagesTemp.length; i++) {
                // console.log($scope.lawyerCaseCategory[i]);
            $scope.lawyerLanguages.push({ language: $scope.lawyerLanguagesTemp[i], lawyerId: Number($("#UserId").val()) });
        }
        for (i = 0; i < $scope.lawyerCaseCategoryTemp.length; i++) {
            // console.log($scope.lawyerCaseCategory[i]);
            $scope.lawyerCaseCategory.push({ caseCategoryId: $scope.lawyerCaseCategoryTemp[i], lawyerId: Number($("#UserId").val()) });
        }

       // $scope.addLawyerAccount.email = $("#email").val();
        $scope.addLawyerProfile.address = $("#autocomplete").val();
        $scope.addLawyerProfile.city = $("#locality").val();
        $scope.addLawyerProfile.country = $("#country").val();
        $scope.addLawyerProfile.dateOfBirth = $("#dob").val();

        //for (i = 0; i < $scope.lawyerQualifications.length; i++) {
        //    $scope.row = $scope.lawyerQualifications[i];
        //    delete $scope.row["check"];
        //    delete $scope.row["degreeTypeId"];
        //}
        //for (i = 0; i < $scope.lawyerLicenses.length; i++) {
        //    $scope.row = $scope.lawyerLicenses[i];
        //    delete $scope.row["check"];
        //}
        //for (i = 0; i < $scope.lawyerTimings.length; i++) {
        //    $scope.row = $scope.lawyerTimings[i];
        //    delete $scope.row["appoinmentFee"];
        //    // delete $scope.row["check"];
        //}



        var formdata = new FormData();
        var fileInput = document.getElementById('fileInput');
        if (fileInput.files.length !== 0) { formdata.append(fileInput.files[0].name, fileInput.files[0]); } else {$scope.addLawyerProfile.profilePic = $("#profilepic").val() }
        
        formdata.append("appUser", JSON.stringify($scope.addLawyerAccount));
        formdata.append("lawyer", JSON.stringify($scope.addLawyerProfile));
        formdata.append("lawyerTimings", JSON.stringify($scope.lawyerTimings));
        formdata.append("lawyerExperiences", JSON.stringify($scope.lawyerExperiences));
        formdata.append("lawyerQualifications", JSON.stringify($scope.lawyerQualifications));
        formdata.append("lawyerClient", JSON.stringify($scope.lawyerClients));
        // formdata.append("lawyerSpecializations", JSON.stringify($scope.lawyerSpecializations));
        formdata.append("lawyerLanguages", JSON.stringify($scope.lawyerLanguages));
        formdata.append("lawyerLicenses", JSON.stringify($scope.lawyerLicenses));
        formdata.append("lawyerCaseCategory", JSON.stringify($scope.lawyerCaseCategory));
        formdata.append("lawyerAddress", JSON.stringify($scope.addresses));
        // formdata.append();

        var xhr = new XMLHttpRequest();
        xhr.open('POST', '/Lawyer/EditLawyer');
        xhr.send(formdata);


        // var pram = { "appUser": JSON.stringify($scope.addLawyerAccount), "lawyer": JSON.stringify($scope.addLawyerProfile), "lawyerVirtualFee": JSON.stringify($scope.addLawyerCharges), "lawyerTimings": JSON.stringify($scope.lawyerTimings), "lawyerExperiences": JSON.stringify($scope.lawyerExperiences), "lawyerQualifications": JSON.stringify($scope.lawyerQualifications), "lawyerSpecializations": JSON.stringify($scope.lawyerSpecializations) };
        //JsonCallParam("Lawyer", "CreateLawyer", pram);
        //if (list === "Success") {
        //    swal("Added", "Lawyer Added!", "success");
        //    window.location.reload();
        //}
        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4 && xhr.status === 200) {
                //$("#addModal").modal("hide");
                //swal("Added", "Lawyer Added!", "success");
                //$scope.addCategoryObj = { categoryName: '', isAvailable: true };
                //$scope.getAllCategories();
                window.location.reload();
               // $scope.getProfileAccount();
            }

        };
        return false;
    }
    //lawyer timing push in array
    $scope.addTiming = function () {
        // alert("call me");
        var count = $scope.lawyerTimings.length;
        if (count !== 0) {
            if ($scope.lawyerTimings[count - 1].day === "") return;
        }
        // var loc= $("#txtPlaces").val();
        $scope.lawyerTimings.push({ day: "", slotType: "", location: "", appoinmentFee: "", check: true, check2: true, charges: 0, timeFrom: "", timeTo: "", lawyerId: Number($("#UserId").val()) });
        if (count > 0) { $scope.validatePakage(); }
        //$("#popupForm").trigger("reset");
        //$("#basicModal").modal('hide');

    }
    //$scope.addTiming();
    $scope.validateFee = function (index, fee) {
        // var test = $scope.lawyerQualifications[index].degreeId
        $scope.validatePakage();
        if (fee === "Fee") { $scope.lawyerTimings[index].check = false }
        else {
            $scope.lawyerTimings[index].check = true;
            $scope.lawyerTimings[index].physicalCharges = 0;
        }
    }

    $scope.validateSlotType = function (index, type) {
        // var test = $scope.lawyerQualifications[index].degreeId
        $scope.validatePakage();
        if (type === "Virtual") { $scope.lawyerTimings[index].check2 = true; $scope.lawyerTimings[index].location = ""; }
        else {
            $scope.lawyerTimings[index].check2 = false;
            
        }
    }
    //lawyer timing remove in array
    $scope.removeTiming = function (index) {
        if ($scope.lawyerTimings[index].lawyerTimingId != undefined) {
            JsonCall("Lawyer", "DeleteLawyerTiming/" + $scope.lawyerTimings[index].lawyerTimingId);
            if (list != null) {
                $scope.getProfileAccount();
            }
        }
        else {
            $scope.lawyerTimings.splice(index, 1);
            $scope.validatePakage();
        }  
        
    }


    //lawyer qualification push in array
    $scope.addQualification = function () {
        var count = $scope.lawyerQualifications.length;
        if (count !== 0) {
            if ($scope.lawyerQualifications[count - 1].degreeId === "") return;
        }
        $scope.lawyerQualifications.push({ degreeTypeId: "", degreeId: "", specializationId: "", check: true, completionYear: "",lawyerId: Number($("#UserId").val()) });
        if (count > 0) { $scope.validateEducation(); }
    }
    //$scope.addQualification();
    //lawyer Qualification remove in array
    $scope.removeQualification = function (index) {
        if ($scope.lawyerQualifications[index].lawyerQualificationId != undefined)
        {
            JsonCall("Lawyer", "DeleteLawyerQualification/" + $scope.lawyerQualifications[index].lawyerQualificationId);
            if (list != null)
            {
                $scope.getProfileAccount();
            }
        }
        else {
            $scope.lawyerQualifications.splice(index, 1);
            $scope.validateEducation(); }
        
    }



    //lawyer experience push in array
    $scope.addExperience = function () {
        var count = $scope.lawyerExperiences.length;
        if (count !== 0) {
            if ($scope.lawyerExperiences[count - 1].caseCategoryId == 0) return;
        }
        $scope.lawyerExperiences.push({ caseCategoryId: 0, experienceYears: 0, lawyerId: Number($("#UserId").val()) });
        //$scope.btnNext = true;
        if (count > 0) { $scope.validateLaw(); }

    }
   // $scope.addExperience();

    //lawyer Experience remove in array
    $scope.removeExperience = function (index) {
        if ($scope.lawyerExperiences[index].lawyerExperienceId != undefined) {
            JsonCall("Lawyer", "DeleteLawyerExperience/" + $scope.lawyerExperiences[index].lawyerExperienceId);
            if (list != null) {
                $scope.getProfileAccount();
            }
        }
        else {
            $scope.lawyerExperiences.splice(index, 1);
            $scope.validateLaw();
        }   
    }

    //lawyer specialization push in array
    $scope.addSpecialization = function () {
        var count = $scope.lawyerSpecializations.length;
        if (count !== 0) {
            if ($scope.lawyerSpecializations[count - 1].specializationId == 0) return;
        }
        $scope.lawyerSpecializations.push({ specializationId: 0, endYear: 0 });
    }
    //lawyer Specialization remove in array
    $scope.removeSpecialization = function (index) {
        $scope.lawyerSpecializations.splice(index, 1);
    }

    //lawyer Clients push in array
    $scope.addClients = function () {
        var count = $scope.lawyerClients.length;
        if (count !== 0) {
            if ($scope.lawyerClients[count - 1].clientName === "") return;
        }
        $scope.lawyerClients.push({ clientName: "", lawyerId: Number($("#UserId").val()) });
        if (count > 0) { $scope.validateAbout(); }
    }
  //  $scope.addClients();
    //lawyer Clients remove in array
    $scope.removeClient = function (index) {
        if ($scope.lawyerClients[index].lawyerClientId != undefined) {
            JsonCall("Lawyer", "DeleteLawyerClient/" + $scope.lawyerClients[index].lawyerClientId);
            if (list != null) {
                $scope.getProfileAccount();
            }
        }
        else {
            $scope.lawyerClients.splice(index, 1);
            $scope.validateAbout();
        }   
       
    }

    $scope.addAddress = function () {
        var count = $scope.addresses.length;
        var adr = $('#txtPlaces').val();
        if (count => 0) {
            if (adr === "") return;
        }

        $scope.addresses.push({ address: adr, lawyerId: Number($("#UserId").val()) });
        var pram = { "lawyerAddresses": JSON.stringify($scope.addresses) };
        JsonCallParam("Lawyer", "AddLawyerAddress", pram);
        if (list === "Success")
        {
            $scope.getProfileAccount();
        }
        $scope.validateOffice();
    }
    $scope.removeAddress = function (index) {
        if ($scope.addresses[index].lawyerAddressId != undefined) {
            JsonCall("Lawyer", "DeleteLawyerAddress/" + $scope.addresses[index].lawyerAddressId);
            if (list != null) {
                $scope.getProfileAccount();
            }
        }
        else {
            $scope.addresses.splice(index, 1);
            $scope.validateOffice();
        }  
       
    }


    //lawyer addLawyerLicense push in array
    $scope.addLawyerLicense = function () {
        var count = $scope.lawyerLicenses.length;
        if (count !== 0) {
            if ($scope.lawyerLicenses[count - 1].licenseCityId == 0) return;
        }
        $scope.lawyerLicenses.push({ licenseCityId: 0, districtBar: "", cityBar: "", check: true, lawyerId: Number($("#UserId").val()) });
        if (count > 0) { $scope.validateLaw(); }
    }
   // $scope.addLawyerLicense();
    //lawyer Qualification remove in array
    $scope.removeLawyerLicense = function (index) {
        if ($scope.lawyerLicenses[index].lawyerLicenseId != undefined) {
            JsonCall("Lawyer", "DeleteLawyerLicense/" + $scope.lawyerLicenses[index].lawyerLicenseId);
            if (list != null) {
                $scope.getProfileAccount();
            }
        }
        else {
            $scope.lawyerLicenses.splice(index, 1);
            $scope.validateLaw();
        }  
        
    }
    $scope.validateCity = function (index, licenseCityId) {
        angular.forEach($scope.cityList, function (value, key) {
            if (licenseCityId == value.licenseCityId) {

                if (value.licenseExist == true) {
                    $scope.lawyerLicenses[index].check = false
                    $scope.validateLaw();
                } else {
                    $scope.lawyerLicenses[index].check = true;
                    $scope.lawyerLicenses[index].districtBar = "";
                    $scope.lawyerLicenses[index].cityBar = "";
                    $scope.validateLaw();
                }
            }
        })
    }

    $scope.validateDegree = function (index, degreeId) {
        // var test = $scope.lawyerQualifications[index].degreeId
        $scope.validateEducation();
        angular.forEach($scope.degreeList, function (value, key) {

            if (degreeId == value.degreeId) {
                var name = angular.lowercase(value.name);
                if (name == "llm") {
                    $scope.lawyerQualifications[index].check = false
                    $scope.validateEducation();
                } else {
                    $scope.lawyerQualifications[index].check = true;
                    $scope.lawyerQualifications[index].specializationId = "";
                    $scope.validateEducation();
                }
            }
        })
        //if (degreeName == 3) { $scope.lawyerQualifications[index].check = false }
        //else
        //{
        //    $scope.lawyerQualifications[index].check = true;
        //    $scope.lawyerQualifications[index].caseCategoryId = "";
        //}
    }

    
    $scope.casCading = function (index, degreeTypeId) {
        $scope.degrees[index] = $scope.degreeList.filter(function (c) {
            $scope.lawyerQualifications[index].specializationId = "";
            $scope.lawyerQualifications[index].degreeId = "";
            if ($scope.lawyerQualifications.length > 0) { $scope.validateEducation(); }

            return c.degreeTypeId == degreeTypeId;

        });
    }
    $scope.btnNextFunc = function ()
    { $scope.btnNext = true; $scope.intro = true; }
    $scope.btnPrevFunc = function () {
        if ($scope.intro = true) {
            $scope.btnNext = false;
        }

    }
    $scope.btnNext = false;
    $scope.validateIntro = function () {
        var fileInput = document.getElementById('fileInput');
        var language = $('#GetLanguages').val();
        var count = language.length;
       // var city = $('#locality').val();
       // var country = $('#country').val();
       //  var email = $('#email').val();
        if ($scope.addLawyerProfile.firstName === ""
            || $scope.addLawyerProfile.lastName === ""
            || $scope.addLawyerAccount.userName === undefined
            || $scope.addLawyerProfile.dateOfBirth === undefined
            || $scope.addLawyerAccount.email === undefined
           // || $scope.addLawyerAccount.passwordHash === undefined
            || $scope.addLawyerProfile.gender === ""
            || $scope.addLawyerProfile.contact === ""
            || $scope.addLawyerProfile.cnic === ""
            || count === 0
            //|| $scope.addLawyerProfile.totalExperience === 0
            // || $scope.addLawyerProfile.city === ""
            // || city === ""
            // || country === ""
            //|| email === ""
            || $scope.addLawyerProfile.address === ""
          //  || fileInput.files.length === 0
        )
        {
            // $('#validation').show();
            //  return false;
            $scope.btnNext = true;
        }
        else {
            // $('#validation').hide();
            // return true;
            $scope.btnNext = false;
        }
    }
    $scope.validateEducation = function () {
        if ($scope.lawyerQualifications.length != 0) {
            for (i = 0; i < $scope.lawyerQualifications.length; i++) {
                if (
                    $scope.lawyerQualifications[i].degreeTypeId === ""
                    || $scope.lawyerQualifications[i].degreeId === ""
                    || $scope.lawyerQualifications[i].degreeId == null
                    // || $scope.lawyerQualifications[index].specializationId === ""
                    || $scope.lawyerQualifications[i].completionYear == null
                    || $scope.lawyerQualifications[i].completionYear === "") { $scope.qualification = false; }
                else { $scope.qualification = true; }
            }
        } else { $scope.qualification = false; }
        if ($scope.qualification) {
            $scope.btnNext = false;
        }
        else { $scope.btnNext = true; }
    }
    $scope.validateLaw = function () {
        if ($scope.lawyerExperiences.length !== 0 && $scope.lawyerLicenses.length !== 0) {
            for (i = 0; i < $scope.lawyerExperiences.length; i++) {
                if ($scope.lawyerExperiences[i].caseCategoryId == 0 || $scope.lawyerExperiences[i].experienceYears == 0) { $scope.experience = false; }
                else { $scope.experience = true; }
            }

            for (i = 0; i < $scope.lawyerLicenses.length; i++) {
                if ($scope.lawyerLicenses[i].licenseCityId == 0 || $scope.lawyerLicenses[i].cityBar === "" || $scope.lawyerLicenses[i].cityBar == null || $scope.lawyerLicenses[i].districtBar === "" || $scope.lawyerLicenses[i].districtBar == null) { $scope.license = false; }
                else { $scope.license = true; }
            }
        } else { $scope.experience = false; $scope.license = false; }
        if ($scope.experience && $scope.license && $scope.addLawyerProfile.totalExperience != 0) {
            $scope.btnNext = false;
        }
        else { $scope.btnNext = true; }

    }
    $scope.validateAbout = function () {
        if ($scope.lawyerClients.length !== 0) {
            for (i = 0; i < $scope.lawyerClients.length; i++) {
                if ($scope.lawyerClients[i].clientName === "") {
                    $scope.about = false;
                }
                else { $scope.about = true; }
            }
        } else { $scope.about = false }
        if ($scope.about && $scope.addLawyerProfile.biography != "") { $scope.btnNext = false; }
        else { $scope.btnNext = true; }
    }
    $scope.validateOffice = function () {
        if ($scope.addresses.length == 0) { $scope.office = false; }
        for (i = 0; i < $scope.addresses.length; i++) {
            if ($scope.addresses[i].address === "") {
                $scope.office = false;
            }
            else { $scope.office = true; }
        }
        if ($scope.office) { $scope.btnNext = false; }
        else { $scope.btnNext = true; }
    }
    $scope.validatePakage = function () {
        if ($scope.lawyerTimings.length !== 0) {
            for (i = 0; i < $scope.lawyerTimings.length; i++) {
                if ($scope.lawyerTimings[i].day === ""
                    || $scope.lawyerTimings[i].slotType === ""
                    || $scope.lawyerTimings[i].charges === ""
                    || $scope.lawyerTimings[i].appoinmentFee === ""
                    || $scope.lawyerTimings[i].timeFrom === ""
                    || $scope.lawyerTimings[i].timeTo === "") {
                    $scope.pakage = false;
                }
                else { $scope.pakage = true; }
            }
        } else { $scope.pakage = false }
        if ($scope.pakage) { $scope.btnNext = false; }
        else { $scope.btnNext = true; }

    }
});