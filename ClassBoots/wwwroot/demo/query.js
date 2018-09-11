var area = $('#area');
var successView = function () {
    area.append('<p style="color: green">' + JSON.stringify(this.data) + ' => ok. </p>');
}
var failView = function (XMLHttpRequest, textStatus, errorThrown) {
    area.append('<p style="color: red">' + JSON.stringify(this.data) + ' => faild: ' + textStatus +  '</p>');
}

$.getJSON('./dbDemos.json', function (data) {
    $.each(data.Institutions, function () {
        $.ajax({
            type: "POST",
            url: "/api/Institutions",
            // The key needs to match your method's input parameter (case-sensitive).
            data: JSON.stringify(this),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: successView,
            error: failView
           
        });
    });

    $.each(data.Schools, function () {
        $.ajax({
            type: "POST",
            url: "/api/Schools",
            // The key needs to match your method's input parameter (case-sensitive).
            data: JSON.stringify(this),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: successView,
            error: failView
        });
    });

    $.each(data.Subjects, function () {
        $.ajax({
            type: "POST",
            url: "/api/Subjects",
            // The key needs to match your method's input parameter (case-sensitive).
            data: JSON.stringify(this),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: successView,
            error: failView
        });
    });

    $.each(data.Lectures, function () {
        $.ajax({
            type: "POST",
            url: "/api/Lectures",
            // The key needs to match your method's input parameter (case-sensitive).
            data: JSON.stringify(this),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: successView,
            error: failView
        });
    });

    $.each(data.Videos, function () {
        $.ajax({
            type: "POST",
            url: "/api/Videos",
            // The key needs to match your method's input parameter (case-sensitive).
            data: JSON.stringify(this),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: successView,
            error: failView
        });
    });
});