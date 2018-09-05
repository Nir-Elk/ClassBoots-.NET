$('#default-button').hide();
var sideNav = $('#sidebarnav');

var current = '';

var changeToInst = function (parent) {
        $('#abovesidebar').html("");
        current = '';
        clear();
        $.get('/api/Institutions', function (data) {
            renderItems(data, changeToSchools);
        });
}

var changeToSchools = function (parentId) {
    if (current != 'schools') {

        current = 'schools';
        clear();
        $.get('/api/Institutions/' + parentId + '/Schools', function (data) {
            renderItems(data, changeToSubjects);
            $('#abovesidebar').html(BackToBtn(() => changeToInst(null)));
        });
    }
}

var changeToSubjects = function (parentId) {
    if (current != 'subjects') {
        current = 'subjects';
        clear();
        $.get('/api/Schools/' + parentId + '/Subjects', function (data) {
            renderItems(data, changeToLectures);
            $('#abovesidebar').html(BackToBtn(() => changeToSchools(parentId)));
        });
    }
}

var changeToLectures = function (parentId) {
    if (current != 'lectures') {
        current = 'lectures';
        clear();
        $.get('/api/Lectures/' + parentId + '/Videos', function (data) {
            renderItems(data, null);
            $('#abovesidebar').html(BackToBtn(() => changeToSubjects(parentId)));
        });
    }
}

var appendRow = function (title, image) {
    var $row = $('#default-button').clone();
    $row.removeAttr('id');
    $row.find('.btn-ref').find('.btn-img').attr('src', image)
    $row.find('.btn-ref').append(title);
    return $row;
}

var BackToBtn = function (callback) {
    var $btn = $('#backtobutton').clone();

    $btn.click(function () {
        callback();
    });

    $btn.show();
    return $btn;
}
var clear = function () {
    sideNav.html("");
}
var renderItems = function (data, changeTo) {
    $.each(data, function () {
        var $row = appendRow(this.name, this.image);
        var ParentId = this.id;
        var ParentRow = $row;
        $row.click(function (e) {
            e.preventDefault();
            e.stopPropagation();
            changeTo(ParentId);
        });
        $row.show();
        sideNav.append($row);
    });

}

$(function() {
    changeToInst();
});