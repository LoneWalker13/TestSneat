var SignalR = function (url, clientId) {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl(url)
        .configureLogging(signalR.LogLevel.Information)
        .build();

    var start = async function () {
        try {
            await connection.start();
            console.log("SignalR Connected.");
            addToGroup(clientId);
        } catch (err) {
            console.log(err);
            setTimeout(start, 5000);
        }
    }
    var joinGroup = async function (ticketId) {
        $("#TicketId").val(ticketId);
        await connection.invoke("JoinGroup", connection.connectionId, ticketId)
            .then(() => {
                console.log('Joined Group: ' + ticketId);
            })
            .catch(function (err) {
                console.error(err.toString());
                setTimeout(function () { joinGroup(ticketId) }, 3000);
            });
    }

    var bindFunctions = function () {
        connection.onclose(start);

        var depositPermission = false;
        var withdrawalPermission = false;
        $.ajax({
            url: "/Home/GetUserDepositWithdrawalPermission",
            async: false,
            success: function (data) {
                console.log(data);
                depositPermission = data.deposit;
                withdrawalPermission = data.withdrawal;
            }, error: function (data) {
                //ErrorModal();
            }
        });


        connection.on("ReceiveWithdrawalNotification", (ticketId, loginId, clientName, offRecord) => {
            var cookieValue = getCookie("DataDisplay");
            if (cookieValue.toLowerCase() == "real" && offRecord == true) {
                return;
            }

            var currentpath = window.location.pathname;
            if (currentpath == "/Payment/TransactionDashboard") {
                var type = "Deposit,Withdrawal";
                renderDashboardPartial(type);
            }
            var userId = "";
            $.ajax({
                url: "/User/UserId",
                async: false,
                success: function (data) {
                    console.log(data);
                    userId = data;
                }, error: function (data) {
                    //ErrorModal();
                }
            });
           

            let msg = "[" + clientName + "]" + " A Withdrawal Ticket has been created. Login Id : " + loginId + ", TicketId : " + ticketId;
            let title = "New Withdrawal";
            Site.RefreshNotification(true);
            var onclick = function () {
                Site.ShowLoading();
                var date = new Date();
                var mSec = date.getTime();
                Site.WithdrawalNotifications.pop(this);
                Ticket.CheckStatus('', ticketId, "Withdrawal", userId, "New", true); // refresh notification after taketicket
                //window.open(location.origin + "/Payment/WithdrawalTicket?ticketId=" + ticketId, mSec, 'width=800,height=800');
                //Site.RefreshNotification(true);

                var totalNotifications = Site.WithdrawalNotifications.length + Site.DepositNotifications.length + Site.MemberRebateGenerateNotifications.length + Site.BirthdayNotifications.length;
                let notificationCount = totalNotifications == 0 ? "" : totalNotifications;
                let withdrawalNotificationCount = Site.WithdrawalNotifications.length == 0 ? "" : Site.WithdrawalNotifications.length;
                let depositNotificationCount = Site.DepositNotifications.length == 0 ? "" : Site.DepositNotifications.length;
                //$(".notificationcount").html(notificationCount);
                //$(".notificationCountTransaction").html(withdrawalNotificationCount + depositNotificationCount);
            } 

            var option = {
                "onclick": onclick
            }

            var toast = {
                onclick: onclick,
                msg: msg,
                title: title
            }

            //Site.WithdrawalNotifications.push(toast);
            var totalNotifications = Site.WithdrawalNotifications.length + Site.DepositNotifications.length + Site.MemberRebateGenerateNotifications.length + Site.BirthdayNotifications.length;
            let notificationCount = totalNotifications == 0 ? "" : totalNotifications;
            let withdrawalNotificationCount = Site.WithdrawalNotifications.length == 0 ? "" : Site.WithdrawalNotifications.length;
            let depositNotificationCount = Site.DepositNotifications.length == 0 ? "" : Site.DepositNotifications.length;
            //$(".notificationcount").html(notificationCount);
            //$(".notificationCountTransaction").html(withdrawalNotificationCount + depositNotificationCount);
            if (withdrawalPermission == true) {
                Site.ChangeTitle(notificationCount)
                Site.PlaySound1();
                toastr["success"](toast.msg, "New Withdrawal", option);

                if (Notification.permission === "granted") {
                    showWindowsNotification("New Withdrawal", toast.msg, onclick);
                }
                else if (Notification.permission !== "denied") {
                    Notification.requestPermission().then(permission => {
                        if (permission === "granted") {
                            showWindowsNotification("New Withdrawal", toast.msg, onclick);
                        }
                    });
                }
            }
           
        });
        connection.on("WithdrawalGoToTacPage", (ticketId, tacNumberRequired, endTime) => {
         
            console.log("In receive withdrawal notification");
            if (withdrawalPermission == true) {
                Site.ChangeTitle(notificationCount)
                Site.PlaySound1();
                toastr["success"]("receive", "New Withdrawal");
            }

        });
        connection.on("ReceiveManualDepositNotification", (ticketId, loginId, clientName, offRecord) => {
            var cookieValue = getCookie("DataDisplay");
            if (cookieValue.toLowerCase() == "real" && offRecord == true) {
                return;
            }
                

            var currentpath = window.location.pathname;
            if (currentpath == "/Payment/TransactionDashboard") {
                var type = "Deposit,Withdrawal";
                renderDashboardPartial(type);
            }

            let msg = "[" + clientName + "]" + " A Manual Deposit Ticket has been created. Login Id : " + loginId + ", TicketId : " + ticketId;
            let title = "New Manual Deposit"
            var userId = "";
            $.ajax({
                url: "/User/UserId",
                async: false,
                success: function (data) {
                    console.log(data);
                    userId = data;
                }, error: function (data) {
                    //ErrorModal();
                }
            });
            Site.RefreshNotification(true);
            var onclick = function () {
                Site.ShowLoading();
                var date = new Date();
                var mSec = date.getTime();
                Site.DepositNotifications.pop(this);
                Ticket.CheckStatus('', ticketId, "Deposit", userId, "New", true);  // refresh notification after taketicket
                //window.open(location.origin + "/Payment/DepositTicket?ticketId=" + ticketId, mSec, 'width=800,height=800');
                
                var totalNotifications = Site.WithdrawalNotifications.length + Site.DepositNotifications.length + Site.MemberRebateGenerateNotifications.length + Site.BirthdayNotifications.length;
                let notificationCount = totalNotifications == 0 ? "" : totalNotifications
                let depositNotificationCount = Site.DepositNotifications.length == 0 ? "" : Site.DepositNotifications.length
                let withdrawalNotificationCount = Site.WithdrawalNotifications.length == 0 ? "" : Site.WithdrawalNotifications.length
                //$(".notificationcount").html(notificationCount);
                //$(".notificationCountTransaction").html(depositNotificationCount + withdrawalNotificationCount);
            }

            var option = {
                "onclick": onclick
            }

            var toast = {
                onclick: onclick,
                msg: msg,
                title: title
            }

            //Site.DepositNotifications.push(toast);
            var totalNotifications = Site.WithdrawalNotifications.length + Site.DepositNotifications.length + Site.MemberRebateGenerateNotifications.length + Site.BirthdayNotifications.length;
            let notificationCount = totalNotifications == 0 ? "" : totalNotifications
            let depositNotificationCount = Site.DepositNotifications.length == 0 ? "" : Site.DepositNotifications.length;
            let withdrawalNotificationCount = Site.WithdrawalNotifications.length == 0 ? "" : Site.WithdrawalNotifications.length
            //$(".notificationcount").html(notificationCount);
            //$(".notificationCountTransaction").html(depositNotificationCount + withdrawalNotificationCount);


            if (depositPermission == true) {
                Site.ChangeTitle(notificationCount)
                Site.PlaySound1();
                toastr["success"](toast.msg, "New Manual Deposit", option);

                if (Notification.permission === "granted") {
                    showWindowsNotification("New Manual Deposit", toast.msg, onclick);
                }
                else if (Notification.permission !== "denied") {
                    Notification.requestPermission().then(permission => {
                        if (permission === "granted") {
                            showWindowsNotification("New Manual Deposit", toast.msg, onclick);
                        }
                    });
                }
            }

           
        });

        connection.on("ReceiveSmsLowBalanceAlert", (smsProvider, username, balance) => {

            var onclick = function () {
                let redirectUrl = "";

                if (smsProvider.toLowerCase() == "bulksms") {
                    redirectUrl = "https://www.bulksms.com/account/#!/login/";
                }
                else if (smsProvider.toLowerCase() == "isms") {
                    redirectUrl = "http://www.isms.com.my/";
                }
                window.open(currentUrl, mSec);
            }

            let option = {
                "onclick": onclick,
                "timeOut": "0",
                "extendedTimeOut": "0",
                "closeButton": true,
            }

            toastr["error"]("SMS Provider " + smsProvider + " with username " + username + " credit is running low. Current balance is " + balance + ".", "SMS Balance Low!!", option);
        });

        connection.on("ReceiveMemRebateAllGeneratedNotification", (rebateSettingId, name) => {

            let msg = "A Rebate List has been generated. Rebate Setting Id : " + rebateSettingId + ", Rebate Setting Name : " + name;
            let title = "Rebate List Generated"

            var onclick = function () {
                var date = new Date();
                var mSec = date.getTime();
                Site.MemberRebateGenerateNotifications.pop(this);
                window.open(location.origin + "/Rebate/RebateSetting?Id=" + rebateSettingId, '_blank');


                var totalNotifications = Site.WithdrawalNotifications.length + Site.DepositNotifications.length + Site.MemberRebateGenerateNotifications.length + Site.BirthdayNotifications.length;
                let notificationCount = totalNotifications == 0 ? "" : totalNotifications
                let rebateNotificationCount = Site.MemberRebateGenerateNotifications.length == 0 ? "" : Site.MemberRebateGenerateNotifications.length

                $(".notificationcount").html(notificationCount);
                $(".notificationCountRebate").html(rebateNotificationCount);
            }

            var option = {
                "onclick": onclick
            }

            var toast = {
                onclick: onclick,
                msg: msg,
                title: title
            }

            Site.MemberRebateGenerateNotifications.push(toast);
            var totalNotifications = Site.WithdrawalNotifications.length + Site.DepositNotifications.length + Site.MemberRebateGenerateNotifications.length + Site.BirthdayNotifications.length;
            let notificationCount = totalNotifications == 0 ? "" : totalNotifications
            let rebateNotificationCount = Site.MemberRebateGenerateNotifications.length == 0 ? "" : Site.MemberRebateGenerateNotifications.length;

            $(".notificationcount").html(notificationCount);
            $(".notificationCountRebate").html(rebateNotificationCount);
            Site.ChangeTitle(notificationCount)
            Site.PlaySound1();
            toastr["success"](toast.msg, "Rebate List Generated", option);

            if (Notification.permission === "granted") {
                showWindowsNotification("Rebate List Generated", toast.msg, onclick);
            }
            else if (Notification.permission !== "denied") {
                Notification.requestPermission().then(permission => {
                    if (permission === "granted") {
                        showWindowsNotification("Rebate List Generated", toast.msg, onclick);
                    }
                });
            }
        });

        connection.on("ReceiveBirthdayBonusNotification", (ticketId, loginId) => {

            let msg = "A Birthday Bonus Ticket has been created. Login Id : " + loginId + ", TicketId : " + ticketId;
            let title = "New Birthday Bonus Claim"

            var onclick = function () {
                var date = new Date();
                var mSec = date.getTime();
                Site.BirthdayNotifications.pop(this);
                window.open(location.origin + "/Payment/TransferNewTransactions?Id=" + ticketId, '_blank');


                var totalNotifications = Site.WithdrawalNotifications.length + Site.DepositNotifications.length + Site.MemberRebateGenerateNotifications.length + Site.BirthdayNotifications.length;
                let notificationCount = totalNotifications == 0 ? "" : totalNotifications
                let birthdayNotificationCount = Site.BirthdayNotifications.length == 0 ? "" : Site.BirthdayNotifications.length;

                $(".notificationcount").html(notificationCount);
                $(".notificationCountBirthday").html(birthdayNotificationCount);
            }

            var option = {
                "onclick": onclick
            }

            var toast = {
                onclick: onclick,
                msg: msg,
                title: title
            }

            Site.BirthdayNotifications.push(toast);
            var totalNotifications = Site.WithdrawalNotifications.length + Site.DepositNotifications.length + Site.MemberRebateGenerateNotifications.length + Site.BirthdayNotifications.length;
            let notificationCount = totalNotifications == 0 ? "" : totalNotifications
            let birthdayNotificationCount = Site.BirthdayNotifications.length == 0 ? "" : Site.BirthdayNotifications.length;

            $(".notificationcount").html(notificationCount);
            $(".notificationCountBirthday").html(birthdayNotificationCount);
            Site.ChangeTitle(notificationCount)
            Site.PlaySound1();
            toastr["success"](toast.msg, "New Birthday Bonus", option);

            if (Notification.permission === "granted") {
                showWindowsNotification("New Birthday Bonus", toast.msg, onclick);
            }
            else if (Notification.permission !== "denied") {
                Notification.requestPermission().then(permission => {
                    if (permission === "granted") {
                        showWindowsNotification("New Birthday Bonus", toast.msg, onclick);
                    }
                });
            }
        });
        connection.on("ReceiveKioskBalanceDepositNotification", (ticketId, clientId) => {
            Site.PlayAlertSound();
            let msg = "[Kiosk] New Kiosk Deposit Request \nTicketId : #" + ticketId;
            let title = "Kiosk Deposit Request"
            var onclick = function () {
                window.open("/Kiosk/NewClientTransactionHistory", '_blank');
            }
            var option = {
                "onclick": onclick
            }

            var toast = {
                onclick: onclick,
                msg: msg,
                title: title
            }
            toastr["warning"](toast.msg, toast.title, option);
        });
        connection.on("ReceiveKioskBalanceDepositApproval", (ticketId, clientId) => {
            Site.PlayAlertSound();
            let msg = "[Kiosk] Deposit Id : #" + ticketId +"\n has been approved";
            let title = "Notification"
            var onclick = function () {
                window.open("/Kiosk/TransactionHistory", '_blank');
            }
            var option = {
                "onclick": onclick
            }

            var toast = {
                onclick: onclick,
                msg: msg,
                title: title
            }
            toastr["success"](toast.msg, toast.title, option);
        });
        connection.on("ReceiveKioskBalanceDepositReject", (ticketId, clientId) => {
            Site.PlayAlertSound();
            let msg = "[Kiosk] Deposit Id : #" + ticketId + "\n has been rejected";
            let title = "Notification"
            var onclick = function () {
                window.open("/Kiosk/TransactionHistory", '_blank');
            }
            var option = {
                "onclick": onclick
            }

            var toast = {
                onclick: onclick,
                msg: msg,
                title: title
            }
            toastr["error"](toast.msg, toast.title, option);
        });
        connection.on("ReceiveKioskBalanceThresholdNotification", (clientId) => {
            Site.PlayAlertSound();
            let msg = "[Kiosk Warning] Kiosk Credit is running low. Please reload your credit";
            let title = "Warning"
            var onclick = function () {
                window.open("/Kiosk/TransactionHistory", '_blank');
            }
            var option = {
                "onclick": onclick
            }

            var toast = {
                onclick: onclick,
                msg: msg,
                title: title
            }
            toastr["warning"](toast.msg, toast.title, option);
        }); 
        connection.on("ReceiveAgentReportGenerateNotification", (userId, clientId, result) => {
            Site.PlayAlertSound();

            let msg = "[Agent Commission Report]";
            let title = "Warning"
            if (result == 1) {
                msg = "[Agent Commission Report] Agent Commission Report Generation Complete. You may check on the Report now.";
                title = "Success"
            } else if (result == 2) {
                msg = "[Agent Commission Report] Agent Commission Report Generation Failed. Please retry.";
                title = "Warning"
            }

            var onclick = function () {
                window.open("/Agent/AgentCommissionReportList", '_blank');
            }
            var option = {
                "onclick": onclick
            }

            var toast = {
                onclick: onclick,
                msg: msg,
                title: title
            }
            if (result == 1) {
                toastr["success"](toast.msg, toast.title, option);

            } else if (result == 2) {
                toastr["warning"](toast.msg, toast.title, option);

            }
        });
        connection.on("ReceiveBOUserKickedOutNotification", (sid) => {
            console.log("ReceiveBOUserKickedOutNotification")
            toastr.options = {
                "positionClass": "toast-center-center",
            }

            toastr["warning"]("Alert: Account accessed from another location.", "Warning");
            setTimeout(function () {
                window.location.href = "/Account/AccessDenied";
            }, 3000); 
        });
        connection.on("ReceiveAgentReportGenerateNotification", (userId, clientId, result) => {
            Site.PlayAlertSound();
            
            let msg = "[Agent Commission Report]";
            let title = "Warning"
            if (result == 1) {
                msg = "[Agent Commission Report] Agent Commission Report Generation Complete. You may check on the Report now.";
                title = "Success"
            } else if (result == 2) {
                msg = "[Agent Commission Report] Agent Commission Report Generation Failed. Please retry.";
                title = "Warning"
            }

            var onclick = function () {
                window.open("/Agent/AgentCommissionReportList", '_blank');
            }
            var option = {
                "onclick": onclick
            }

            var toast = {
                onclick: onclick,
                msg: msg,
                title: title
            }
            if (result == 1) {
                toastr["success"](toast.msg, toast.title, option);

            } else if (result == 2) {
                toastr["warning"](toast.msg, toast.title, option);

            }
        });
        connection.on("ReceiveBOSetVendorMaintenanceNotification", (vendor) => {
            console.log("In receive bo set vendor maitnenance notification");           
            //Site.ChangeTitle(notificationCount)
            Site.PlayAlertSound();
            toastr["error"](vendor + " has been set to maintenance mode.", vendor + " Under Maintenance.");
           
        });
    }

    var showWindowsNotification = (title, body, callback) => {
        const notification = new Notification(title, {
            body: body,
            icon: "../img/vava88logo.png"
        });
        notification.onclick = callback;
    }
    var addToGroup = async function (clientId) {
        await connection.invoke("AddToGroup", connection.connectionId, clientId)
            .then(function () {
                console.log('added to group ' + clientId);
            })
            .catch(function (err) {
                console.log(err)
                console.log(connection.connectionId)
                return console.error(err.toString());
            });
    };

    function getCookie(cookieName) {
        const cookies = document.cookie.split('; ');
        for (let i = 0; i < cookies.length; i++) {
            const cookie = cookies[i].split('=');
            if (cookie[0] === cookieName) {
                return decodeURIComponent(cookie[1]);
            }
        }
        return null; // Return null if cookie not found
    }

    function getUserId() {
        var userId = "";
        $.ajax({
            url: "/User/UserId",
            async: false,
            success: function (data) {
                userId = data;
            }, error: function (data) {
                //ErrorModal();
            }
        });
        return userId;
    }

    var renderDashboardPartial = function (type) {
        $.ajax({
            url: "DashboardPartial?type=" + type,
            success: function (data) {
                $('#TransactionPartial').html(data);
                $(".reloadFontsize").removeClass("spinmotion");
                if (!type.includes("Deposit")) {
                    $(".depamtfooter").hide();
                }
                if (!type.includes("Withdrawal")) {
                    $(".witamtfooter").hide();
                }
            }, error: function (data) {
                toastr["error"](data);
                //Site.HideLoading();
            }
        });
    }


    var renderDashboardPartial = function (type) {
        $.ajax({
            url: "DashboardPartial?type=" + type,
            success: function (data) {
                $('#TransactionPartial').html(data);
                $(".reloadFontsize").removeClass("spinmotion");
                if (!type.includes("Deposit")) {
                    $(".depamtfooter").hide();
                }
                if (!type.includes("Withdrawal")) {
                    $(".witamtfooter").hide();
                }
            }, error: function (data) {
                toastr["error"](data);
                //Site.HideLoading();
            }
        });
    }

    var init = function () {
        bindFunctions();
        start();
    };

    return {
        init: init,
        Join: joinGroup,
    };
};
