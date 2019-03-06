// custom js function
function processResponseError(errMessage, areaName) {
    if ($.trim(errMessage).toLowerCase() == "timeout") {
        jAlert("由于您长时间未进行操作，本页面已经失效，请重新登录！", "登录超时", function (r) {
            if (r) {
                window.location = getRootPath() + "/Home/Login";
            }
        });
    }
    else
        jAlert(errMessage, "提示");
}

function encodingText(entryString, strLength) {
    entryString = entryString.replace(/\</g, "&lt;");
    entryString = entryString.replace(/\>/g, "&gt;");
    if (entryString.length > strLength)
        entryString = entryString.substr(0, strLength);
    return entryString;
}

function unEncodingText(entryString) {
    entryString = entryString.replace(/&lt;/g, "<");
    entryString = entryString.replace(/&gt;/g, ">");
    return entryString;
}

function getRootPath() {
    var strFullPath = window.document.location.href;
    var strPath = window.document.location.pathname;
    var pos = strFullPath.indexOf(strPath);
    var prePath = strFullPath.substring(0, pos);
    var postPath = strPath.substring(0, strPath.substr(1).indexOf('/') + 1);

    //return (prePath + postPath);    // if has virtual directory , use this return
    return (prePath);             // if no virtual dir, use this return
}

function setTab(m, n) {
    var tli = document.getElementById("tabHeader").getElementsByTagName("li");
    var mli = document.getElementById("tabContent").getElementsByTagName("ul");
    
    for (i = 0; i < tli.length; i++) {
        for (j = 0; j < mli.length;j++)
            if (tli[i].id == mli[j].id)
            {
                tli[i].className = i == n ? "hover" : "link";
                mli[j].style.display = i == n ? "block" : "none";
        }
    }
    
}