
function ClearRequired() {
    $("label.required").html("");
}

function onkeyupOnlyNum(element) {
    element.value = element.value.replace(/[^\d]/g, '');
}

function getRadioChecked(name) {
    return $.trim($("input[name='" + name + "']:checked").val());
}

function setRadioChecked(name, value) {
    var radios = document.getElementsByName(name);
    for (i = 0; i < radios.length; i++) {
        if (value == radios[i].value) {
            radios[i].checked = true;
            break;
        }
        else
            radios[i].checked = false;
    }
}

function getFileSizeKB(fileElementId) {
    if ($("#" + fileElementId).val() == "")
        return 0;

    var fileSize = 0;
    //for IE
    if (/msie/.test(navigator.userAgent.toLowerCase())) {
        //before making an object of ActiveXObject,
        //please make sure ActiveX is enabled in your IE browser
        var objFSO = new ActiveXObject("Scripting.FileSystemObject");
        var filePath = $("#" + fileElementId)[0].value;
        var objFile = objFSO.getFile(filePath);
        var fileSize = objFile.size; //size in kb
        fileSize = fileSize / 1048576; //size in mb
    }
        //for FF, Safari, Opeara and Others
    else {
        fileSize = $("#" + fileElementId)[0].files[0].size //size in kb
        fileSize = fileSize / 1048; //size in kb
    }
    return fileSize;
}

//Get File Resource
function LoadUploadFile(fileElementId, UploadPathId) {
    $("#req" + UploadPathId).html("");
    if ($("#" + fileElementId)[0].files.length == 0) return;

    var uploadFile = document.getElementById(UploadPathId);
    var reader = new FileReader();
    reader.onload = function (evt) { uploadFile.value = evt.target.result; }
    reader.readAsDataURL($("#" + fileElementId)[0].files[0]);
}

function SetPageWaiting(isWaiting) {
    if (isWaiting) {
        $("button").prop("disabled", true);
        $("body").css("cursor", "progress");
    } else {
        $("button").prop("disabled", false);
        $("body").css("cursor", "default");
    }
}

function SetTotalPage4Pager(totalPages) {
    $("#totalPages").val(totalPages);
    if (totalPages > 0) {
        if (parseInt($("#currentPage").val()) > totalPages)
            $("#currentPage").val(totalPages);
    }
    else {
        $("#currentPage").val(1);
    }

    $("#pager").pager({
        pagenumber: $("#currentPage").val(),
        pagecount: totalPages,
        buttonClickCallback: PageClick
    });
}

