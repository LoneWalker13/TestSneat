var PermissionAuthorize = (function () {
    var AllowEdit = function (allow) {
        console.log(allow)
        if (allow == "False") {
            $(".form-control").attr("disabled", "disabled");
            $(".form-control").attr("readonly", "readonly");
        }
    }
    
    return {
        AllowEdit: AllowEdit        
    }
})();