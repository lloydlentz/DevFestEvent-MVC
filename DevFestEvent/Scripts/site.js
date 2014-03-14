function ShowLessonPopup(eventid) {
    $.ajax({
        url: "/Session/DetailsModal",
        type: "GET",
        data: { id: eventid },
        cache: false
    }).done(function (html) {
        $('#popupEventForm').html(html);
        $('#popupEventForm').show();
        $('#eventTitle').focus();
    });
}


function addSession(sessionid) {
    $('#popupEventForm').hide();
    $(".addedsuccess").hide();
    $("#session" + sessionid).prepend("<div class='addedsuccess'>added</div>");

    $.ajax({
        url: "/Session/Add",
        type: "GET",
        data: { id: sessionid },
        cache: false,
        success: function (response){
            $(".addedsuccess").fadeOut(2000);
        },
        fail: function (response) {
            $(".addedsuccess").hide();
            $("#session" + sessionid).prepend("<div class='addedsuccess'>failed to add :(</div>");
        },
    });
}

function delSession(sessionid) {
    $('#popupEventForm').hide();
    $(".addedsuccess").hide();
    $("#session" + sessionid).prepend("<div class='addedsuccess'>removed</div>");

    $.ajax({
        url: "/Session/Del",
        type: "GET",
        data: { id: sessionid },
        cache: false,
        success: function (response) {
        }
    });
}

$(document).keyup(function (e) {
    if (e.keyCode == 13) { $('.save').click(); }     // enter
    if (e.keyCode == 27) { $('.cancel').click(); }   // esc
});

