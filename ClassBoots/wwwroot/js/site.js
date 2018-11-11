
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
    $.session.clear();
    e.preventDefault();
    $('#logoutForm').submit();
    e.stopPropagation();
});

$.get('https://api.openweathermap.org/data/2.5/weather?q=rishon lezion,il&APPID=e9ee973ac45743df1c6a06c49a6b1b7c', function (data) {
    var weather = data.weather[0].main;
    var img;
    switch (weather) {
        case "Clear":
            img = "https://uploads-ssl.webflow.com/54723f1c79be4eb47403f478/584c2de9c4d93d8e0d0ab151_weather-sunrise_yellow_on_white.gif";
            break;
        case "Clouds":
            img = "https://media.giphy.com/media/XCG51GD8W6XEQ/giphy.gif";
            break;
        case "Rain":
            img = "https://66.media.tumblr.com/c08ba1d8a62c75b85b9513eb9fb964fc/tumblr_p6k33vvoxd1vpf6ddo1_500.gif";
            break;
        default:
            img = "https://uploads-ssl.webflow.com/54723f1c79be4eb47403f478/584c2de9c4d93d8e0d0ab151_weather-sunrise_yellow_on_white.gif";

    }
    $('#weather').html("<img class='weather' src='"+ img +"' \>");
});