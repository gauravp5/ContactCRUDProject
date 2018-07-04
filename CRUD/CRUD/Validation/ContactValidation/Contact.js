function IsFirstNameEmpty() {
    if (document.getElementById('FirstName').value == "") {
        return "First Name should not be empty";
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


function IsValid() {
    var FirstNameEmptyMessage = IsFirstNameEmpty();

    var LastNameEmptyMessage = IsLastNameEmpty();

    var MobileNumberValidMessage = IsMobileNumber();
    var validateEmailMessage = validateEmail();

    var FinalErrorMessage = "Errors:";
    if (FirstNameEmptyMessage != "")
        FinalErrorMessage += "\n" + FirstNameEmptyMessage;
   
    if (LastNameEmptyMessage != "")
        FinalErrorMessage += "\n" + LastNameEmptyMessage;
  
    if (MobileNumberValidMessage != "")
        FinalErrorMessage += "\n" + MobileNumberValidMessage;
    if (validateEmailMessage != "")
        FinalErrorMessage += "\n" + validateEmailMessage;
   
    if (FinalErrorMessage != "Errors:") {
        alert(FinalErrorMessage);
        return false;
    }
    else {
        return true;
    }
}