function IsFirstNameEmpty() {
    if (document.getElementById('FirstName').value == "") {
        return "First Name should not be empty";
    }
    else {
        return "";
    }
}

function IsFirstNameInValid() {
    if (document.getElementById('FirstName').value.indexOf("@") != -1) {
        return "First Name should not contain @";
    }
    else {
        return "";
    }
}

function IsLastNameEmpty() {
    if (document.getElementById('LastName').value == "") {
        return "Last Name should not be empty";
    }
    else {
        return "";
    }
}

function IsLastNameInValid() {
    if (document.getElementById('LastName').value.indexOf("@") != -1) {
        return "Last Name should not contain @";
    }
    else {
        return "";
    }
}

function IsMobileNumber() {
    var txtMobile = document.getElementById('PhoneNumber').value;
    var mob = /^[1-9]{1}[0-9]{9}$/;
    if (txtMobile == null) {
        return "Please enter mobile number.";
    }
    else if (mob.test(txtMobile) == false) {
        return "Please enter valid mobile number.";
    }
    return "";
}

function validateEmail(emailField) {
    var email = document.getElementById('Email');
    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (email == null) {
        return "Please enter email address."
    }
    if (!filter.test(email.value)) {
        return "Please provide a valid email address.";
    }
    return "";
}

function validateStatus() {
    if (document.getElementById("Status").value == 0) {
        return "Please select status";
    }
    return "";
}


function IsValid() {
    var FirstNameEmptyMessage = IsFirstNameEmpty();
    var FirstNameInValidMessage = IsFirstNameInValid();
    var LastNameEmptyMessage = IsLastNameEmpty();
    var LastNameInValidMessage = IsLastNameInValid();
    var MobileNumberValidMessage = IsMobileNumber();
    var validateEmailMessage = validateEmail();
    var validateStatusMessage = validateStatus();

    var FinalErrorMessage = "Errors:";
    if (FirstNameEmptyMessage != "")
        FinalErrorMessage += "\n" + FirstNameEmptyMessage;
    else if (FirstNameInValidMessage != "")
        FinalErrorMessage += "\n" + FirstNameInValidMessage;
    if (LastNameEmptyMessage != "")
        FinalErrorMessage += "\n" + LastNameEmptyMessage;
    else if (LastNameInValidMessage != "")
        FinalErrorMessage += "\n" + LastNameInValidMessage;
    if (MobileNumberValidMessage != "")
        FinalErrorMessage += "\n" + MobileNumberValidMessage;
    if (validateEmailMessage != "")
        FinalErrorMessage += "\n" + validateEmailMessage;
    if (validateStatusMessage != "")
        FinalErrorMessage += "\n" + validateStatusMessage;

    if (FinalErrorMessage != "Errors:") {
        alert(FinalErrorMessage);
        return false;
    }
    else {
        return true;
    }
}