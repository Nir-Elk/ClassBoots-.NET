$(function () {
    var path = [];
    switch (path.length) {

        // api/{institution_id}/schools
        case 1:
 
            break;

        // api/{institution_id}/{school_id}/subjects
        case 2:
            
            break;

        // api/{institution_id}/{school_id}/{subject_id}/lectures
        case 2:
            
            break;

        // api/{institution_id}/{school_id}/{subject_id}/{lecture_id}/videos
        case 3:
            
            break;
        //.....


         // api/institutions
        default:
            $.get('/api/institutions', function (institutions) {
                $.each(institutions, function () {
                    let newMenuItem = $('#menuitem').clone();
                    let path = '/' + this.id + '/schools';
                    newMenuItem.attr('id', 'institution_' + this.id);
                    newMenuItem.find('a').attr('href', path);
                    newMenuItem.find('a').find('span').html(this.name);
                    newMenuItem.find('a').click(function (event) {
                        console.log(path);
                        history.pushState(this.name , null, path);
                        return event.preventDefault();
                    });
                    $('#sidebarnav').append(newMenuItem);
                });
            });
    }

    // Revert to a previously saved state
    window.addEventListener('popstate', function (event) {
        console.log('popstate fired!');
        //updateContent(event.state);
    });
});


//$('#default-button').hide();
//var sideNav = $('#sidebarnav');

//var current = '';

//var changeToInst = function (parent) {
//        $('#abovesidebar').html("");
//        current = '';
//        clear();
//        $.get('/api/Institutions', function (data) {
//            renderItems(data, changeToSchools);
//        });
//}

//var changeToSchools = function (parentId) {
//    if (current != 'schools') {

//        current = 'schools';
//        clear();
//        $.get('/api/Institutions/' + parentId + '/Schools', function (data) {
//            renderItems(data, changeToSubjects);
//            $('#abovesidebar').html(BackToBtn(() => changeToInst(null)));
//        });
//    }
//}

//var changeToSubjects = function (parentId) {
//    if (current != 'subjects') {
//        current = 'subjects';
//        clear();
//        $.get('/api/Schools/' + parentId + '/Subjects', function (data) {
//            renderItems(data, changeToLectures);
//            $('#abovesidebar').html(BackToBtn(() => changeToSchools(parentId)));
//        });
//    }
//}

//var changeToLectures = function (parentId) {
//    if (current != 'lectures') {
//        current = 'lectures';
//        clear();
//        $.get('/api/Lectures/' + parentId + '/Videos', function (data) {
//            renderItems(data, null);
//            $('#abovesidebar').html(BackToBtn(() => changeToSubjects(parentId)));
//        });
//    }
//}

//var appendRow = function (title, image) {
//    var $row = $('#default-button').clone();
//    $row.removeAttr('id');
//    $row.find('.btn-ref').find('.btn-img').attr('src', image)
//    $row.find('.btn-ref').append(title);
//    return $row;
//}

//var BackToBtn = function (callback) {
//    var $btn = $('#backtobutton').clone();

//    $btn.click(function () {
//        callback();
//    });

//    $btn.show();
//    return $btn;
//}
//var clear = function () {
//    sideNav.html("");
//}
//var renderItems = function (data, changeTo) {
//    $.each(data, function () {
//        var $row = appendRow(this.name, this.image);
//        var ParentId = this.id;
//        var ParentRow = $row;
//        $row.click(function (e) {
//            e.preventDefault();
//            e.stopPropagation();
//            changeTo(ParentId);
//        });
//        $row.show();
//        sideNav.append($row);
//    });

//}

//$(function() {
//    changeToInst();
//});