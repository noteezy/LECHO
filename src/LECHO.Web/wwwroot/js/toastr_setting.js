toastr.options = {
    "closeButton": false,
    "debug": false,
    "newestOnTop": true,
    "progressBar": false,
    "positionClass": "toast-bottom-right",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}
var connection = new signalR.HubConnectionBuilder().withUrl("/NewsHub").build();
connection.on("ReceiveMessage", function (message) {
    var msg = message;
    toastr.success(msg);
});
connection.start().then(() => { console.log("connection started") });