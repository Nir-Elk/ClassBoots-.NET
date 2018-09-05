$('#default-button').hide();
var sideNav = $('#sidebarnav');
var z = -1;
var current = '';

var changeToInst = function (countryId) {
    if (current != '' || z == -1) {
        z++;
        current = '';
        clear();
        $.get('/api/Institutions', function (data) {
            $.each(data, function () {
                renderItem(this, changeToSchools);
            });
        });
    }
}

var changeToSchools = function (ParentRow, ParentId) {
    if (current != 'schools') {
        z++;
        current = 'schools';
        clear();
        $.get('/api/Institutions/' + ParentId + '/Schools', function (data) {
            sideNav.append(appendBackToBtn(changeToInst));
            sideNav.append(ParentRow);
            renderItems(data);
        });
    }
}

var appendRow = function (title, image) {
    var $row = $('#default-button').clone();
    $row.removeAttr('id');
    $row.find('.btn-ref').find('.btn-img').attr('src', image)
    $row.find('.btn-ref').append(title);
    $row.attr('style', ' padding-left: ' + z*20 +'px;');
    return $row;
}

var appendBackToBtn = function (changeTo) {
    var $btn = $('#back-to-button').clone();

    $btn.click(function () {
        z--;
        z--;
        changeTo();
    });

    $btn.show();
    return $btn;
}
var clear = function () {
    sideNav.html("");
}
var renderItems = function (data) {
    $.each(data, function () {
        renderItem(this);
    });

}
var renderItem = function(item, changeTo) {
    var $row = appendRow(item.name, item.image);
    var ParentId = item.id;
    var ParentRow = $row;
    $row.click(function (e) {
        e.preventDefault();
        e.stopPropagation();
        changeTo(ParentRow, ParentId);
    });
    $row.show();
    sideNav.append($row);
}

$(function() {
    changeToInst();
});