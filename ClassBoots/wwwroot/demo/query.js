var area = $('#area');
$.getJSON('./dbDemos.json', function (data) {
    $.each(data.Institutions, function () {
        $.ajax({
            type: "POST",
            url: "/api/Institutions",
            // The key needs to match your method's input parameter (case-sensitive).
            data: JSON.stringify(this),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
        });

    });
});