
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

$('logout').click(function (e) {
    e.preventDefault();
    $.post('action="/Identity/Account/Logout?returnUrl=%2F"')
    e.stopPropagation();
})

document.getElementById("logout").onclick = function () {
    document.getElementById("logoutForm").submit();
}
