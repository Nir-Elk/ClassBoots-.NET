
 $('#test-button').click(function (e) {
    e.preventDefault();
    view('lectures');
    history.pushState('lectures', null, 'lectures');
    e.stopPropagation();
});



var view = function (req) {
    $.get('/api/' + req).always(function (data) {
        $('#body').html(JSON.stringify(data));
    });
}

$('#logout').click(function (e) {
    e.preventDefault();
    $('#logoutForm').submit();
    e.stopPropagation();
})

