
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

    //arrays 
    $scope.lawyerTimings = [];
    $scope.lawyerQualifications = [];
    $scope.lawyerExperiences = [];
    $scope.lawyerSpecializations = [];
    $scope.lawyerClients = [];
    $scope.lawyerLanguages = [];
    $scope.lawyerCaseCategory = [];
    $scope.addresses = [];
    $scope.lawyerLicenses = [];
    $scope.profileAccount = [];

    //lawyer timing selectlists
    $scope.yearList = [
        { id: '1', name: '1 Year' },
        { id: '2', name: '2 Year' },
        { id: '3', name: '3 Year' },
        { id: '4', name: '4 Year' },
        { id: '5', name: '5 Year' },
        { id: '6', name: '6 Year' },
        { id: '7', name: '7 Year' },
        { id: '8', name: '8 Year' },
        { id: '9', name: '9 Year' },
        { id: '10', name: '10 Year' },
        { id: '11', name: '11 Year' },
        { id: '12', name: '12 Year' },
        { id: '13', name: '13 Year' },
        { id: '14', name: '14 Year' },
        { id: '15', name: '15 Year' },
        { id: '16', name: '16 Year' },
        { id: '17', name: '17 Year' },
        { id: '18', name: '18 Year' },
        { id: '19', name: '19 Year' },
        { id: '20', name: '20 Year' },
        { id: '21', name: '21 Year' },
        { id: '22', name: '22 Year' },
        { id: '23', name: '23 Year' },
        { id: '24', name: '24 Year' },
        { id: '25', name: '25 Year' },
        { id: '26', name: '26 Year' },
        { id: '27', name: '27 Year' },
        { id: '28', name: '28 Year' },
        { id: '29', name: '29 Year' },
        { id: '30', name: '30 Year' }];
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

    //$scope.getProfileAccount();
    // specializationList for dropdown
    //$scope.getProfileAccount = function () {
    //    JsonCall("Lawyer", "GetProfileAccount/" + Number( $("#UserId").val()));
    //    if (list != null) {
    //        $scope.addLawyerAccount.email = list.appUser.email;
    //        $scope.addLawyerAccount.userName = list.appUser.userName;
    //        $scope.addLawyerProfile = list;
    //        $scope.lawyerQualifications = list.lawyerQualifications;
    //        if (list.virtualChargesPkr !== 0) {
    //            var check = $(".charges input").prop("checked", true);
    //            $(".feeToggle input").prop("checked", true);
    //            $('#fee').show();
    //            $scope.qualification = true;}
    //    }
    //}

    // specializationList for dropdown
    $scope.getSpecialization = function () {
        JsonCall("Specialization", "SpecializationList");
        if (list != null) {
            $scope.specializationList = list;
        }
    }
    // degreeList for dropdown
    $scope.getDegree = function () {
        JsonCall("Degree", "DegreeList");
        if (list != null) {
            $scope.degreeList = list;
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
    $scope.addressesTemp = [];
    //add lawyer
    $scope.addLawyer = function () {
        //if (!validateLawyer()) { return; }

        //pushing lawyer services in array
        $scope.lawyerCaseCategoryTemp = $("#GetCaseCatIds").val();

        //console.log($scope.lawyerCaseCategory);
        for (i = 0; i < $scope.lawyerCaseCategoryTemp.length; i++) {
           // console.log($scope.lawyerCaseCategory[i]);
            $scope.lawyerCaseCategory.push({ caseCategoryId: Number($scope.lawyerCaseCategoryTemp[i]) });
        }



        $scope.lawyerLanguagesTemp = $("#GetLanguages").val();
        //console.log($scope.lawyerCaseCategory);

        for (i = 0; i < $scope.lawyerLanguagesTemp.length; i++) {
            // console.log($scope.lawyerCaseCategory[i]);
            $scope.lawyerLanguages.push({ language: $scope.lawyerLanguagesTemp[i] });
        }

       // $scope.addLawyerAccount.email = $("#email").val();
        $scope.addLawyerProfile.address = $("#autocomplete").val();
        $scope.addLawyerProfile.city = $("#locality").val();
        $scope.addLawyerProfile.country = $("#country").val();
       // $scope.addLawyerProfile.dateOfBirth = $("#dob").val();

        //for (i = 0; i < $scope.lawyerQualifications.length; i++) {
        //    $scope.row = $scope.lawyerQualifications[i];
        //    delete $scope.row["check"];
        //    delete $scope.row["degreeTypeId"];
        //}
        //for (i = 0; i < $scope.lawyerLicenses.length; i++) {
        //    $scope.row = $scope.lawyerLicenses[i];
        //    delete $scope.row["check"];
        //}
        for (i = 0; i < $scope.lawyerTimings.length; i++) {
            if ($scope.lawyerTimings[i].location === "") {
                $scope.addressesTemp.push("");
                $scope.row = $scope.lawyerTimings[i];
                delete $scope.row["location"];
            }
            else {

                $scope.addressesTemp.push($scope.lawyerTimings[i].location);
                $scope.row = $scope.lawyerTimings[i];
                delete $scope.row["location"];
            }
            // delete $scope.row["check"];
        }



        var formdata = new FormData();
        var fileInput = document.getElementById('fileInput');
        formdata.append(fileInput.files[0].name, fileInput.files[0]);
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
        formdata.append("addressesTemp", JSON.stringify($scope.addressesTemp));
        // formdata.append();

        var xhr = new XMLHttpRequest();
        xhr.open('POST', '/Lawyer/CreateLawyer2');
        xhr.send(formdata);


        // var pram = { "appUser": JSON.stringify($scope.addLawyerAccount), "lawyer": JSON.stringify($scope.addLawyerProfile), "lawyerVirtualFee": JSON.stringify($scope.addLawyerCharges), "lawyerTimings": JSON.stringify($scope.lawyerTimings), "lawyerExperiences": JSON.stringify($scope.lawyerExperiences), "lawyerQualifications": JSON.stringify($scope.lawyerQualifications), "lawyerSpecializations": JSON.stringify($scope.lawyerSpecializations) };
        //JsonCallParam("Lawyer", "CreateLawyer", pram);
        //if (list === "Success") {
        //    swal("Added", "Lawyer Added!", "success");
        //    window.location.reload();
        //}
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
    //lawyer timing push in array
    $scope.addTiming = function () {
        // alert("call me");
        var count = $scope.lawyerTimings.length;
        if (count !== 0) {
            if ($scope.lawyerTimings[count - 1].day === "") return;
        }
        // var loc= $("#txtPlaces").val();
        $scope.lawyerTimings.push({ day: "", slotType: "", location: "", appoinmentFee: "", check: true, check2: true, charges: 0, timeFrom: "", timeTo: "" });
        if (count > 0) { $scope.validatePakage(); }
        //$("#popupForm").trigger("reset");
        //$("#basicModal").modal('hide');

    }
    $scope.addTiming();
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
        $scope.lawyerTimings.splice(index, 1);
        $scope.validatePakage();
    }


    //lawyer qualification push in array
    $scope.addQualification = function () {
        var count = $scope.lawyerQualifications.length;
        if (count !== 0) {
            if ($scope.lawyerQualifications[count - 1].degreeId === "") return;
        }
        $scope.lawyerQualifications.push({ degreeTypeId: "", degreeId: "", specializationId: "", check: true, completionYear: "" });
        if (count > 0) { $scope.validateEducation(); }
    }
    $scope.addQualification();
    //lawyer Qualification remove in array
    $scope.removeQualification = function (index) {
        $scope.lawyerQualifications.splice(index, 1);
        $scope.validateEducation();
    }



    //lawyer experience push in array
    $scope.addExperience = function () {
        var count = $scope.lawyerExperiences.length;
        if (count !== 0) {
            if ($scope.lawyerExperiences[count - 1].caseCategoryId == 0) return;
        }
        $scope.lawyerExperiences.push({ caseCategoryId: 0, experienceYears: 0 });
        //$scope.btnNext = true;
        if (count > 0) { $scope.validateLaw(); }

    }
    $scope.addExperience();

    //lawyer Experience remove in array
    $scope.removeExperience = function (index) {
        $scope.lawyerExperiences.splice(index, 1);
        $scope.validateLaw();
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
        $scope.lawyerClients.push({ clientName: "" });
        if (count > 0) { $scope.validateAbout(); }
    }
    $scope.addClients();
    //lawyer Clients remove in array
    $scope.removeClient = function (index) {
        $scope.lawyerClients.splice(index, 1);
        $scope.validateAbout();
    }

    $scope.addAddress = function () {
        var count = $scope.addresses.length;
        var adr = $('#txtPlaces').val();
        if (count => 0) {
            if (adr === "") return;
        }

        $scope.addresses.push({ address: adr });
        $scope.validateOffice();
    }
    $scope.removeAddress = function (index) {
        $scope.addresses.splice(index, 1);
        $scope.validateOffice();
    }


    //lawyer addLawyerLicense push in array
    $scope.addLawyerLicense = function () {
        var count = $scope.lawyerLicenses.length;
        if (count !== 0) {
            if ($scope.lawyerLicenses[count - 1].licenseCityId == 0) return;
        }
        $scope.lawyerLicenses.push({ licenseCityId: 0, districtBar: "", cityBar: "", check: true });
        if (count > 0) { $scope.validateLaw(); }
    }
    $scope.addLawyerLicense();
    //lawyer Qualification remove in array
    $scope.removeLawyerLicense = function (index) {
        $scope.lawyerLicenses.splice(index, 1);
        $scope.validateLaw();
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

    $scope.degrees = [];
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
    $scope.btnNext = true;
    $scope.validateIntro = function () {
        var fileInput = document.getElementById('fileInput');
        var language = $('#GetLanguages').val();
        var count = language.length;
        var city = $('#locality').val();
        var country = $('#country').val();
       //  var email = $('#email').val();
        if ($scope.addLawyerProfile.firstName === undefined
            || $scope.addLawyerProfile.lastName === undefined
            || $scope.addLawyerAccount.userName === undefined
            || $scope.addLawyerProfile.dateOfBirth === undefined
            || $scope.addLawyerAccount.email === undefined
            || $scope.addLawyerAccount.passwordHash === undefined
            || $scope.addLawyerProfile.gender === undefined
            || $scope.addLawyerProfile.contact === undefined
            || $scope.addLawyerProfile.cnic === undefined
            || count === 0
            //|| $scope.addLawyerProfile.totalExperience === 0
            // || $scope.addLawyerProfile.city === ""
             || city === ""
             || country === ""
            //|| email === ""
            || $scope.addLawyerProfile.address === undefined
            || fileInput.files.length === 0)
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