var area = $('#area');
$.getJSON('./dbDemos.json', function (data) {
    $.each(data.Institutions, function () {
        area.append(JSON.stringify(data.Institutions));
        $.ajax({
            type: "POST",
            url: "/api/Institutions",
            // The key needs to match your method's input parameter (case-sensitive).
            data: JSON.stringify(this),
            contentType: "application/json; charset=utf-8",
            dataType: "json"
           
        });
    });

    $.each(data.Schools, function () {
        area.append(JSON.stringify(data.Schools));

        $.ajax({
            type: "POST",
            url: "/api/Schools",
            // The key needs to match your method's input parameter (case-sensitive).
            data: JSON.stringify(this),
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    });

    $.each(data.Subjects, function () {
        area.append(JSON.stringify(data.Subjects));

        $.ajax({
            type: "POST",
            url: "/api/Subjects",
            // The key needs to match your method's input parameter (case-sensitive).
            data: JSON.stringify(this),
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    });
});