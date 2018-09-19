
/* avivsegal@gmail.com */


$(function () {
    var area = $('#sidebarnav');
    var emptyMenuItem = $('#menuitem');
    var emptyPreviousBtn = $('#previousbtn');
    var criclebtn = $('#circlebtn');
    var options = $('#sidebarnav-options');
    var loginPartial = $('#_Loginform')
    var resultLine = $('#resultLine');

    resultLine.hide();
    criclebtn.hide();
    emptyMenuItem.hide();
    emptyPreviousBtn.hide();

    if (loginPartial) {
        console.log('iluz');
        $.get('Identity/Account/Loginbox', function (data) {
            loginPartial.html(data);
      });
    }

    //$.session.clear();  /* Uncommet to destroy session. */
    if (!$.session.get('path')) {
        $.session.set('path', '/api/path')
    }

    var view = function(data) {
        $('#body-content').html(data);
    }

    var menuClickHandler = function (nextPath) {
        $('#path').html(nextPath);
        $.session.set('path', nextPath);
        renderMenu();
    }

    var generatePrevBtn = function () {
        let prev = $.session.get('path');
        prev = prev.split('/');
        prev.pop();
        prev = prev.join('/');
        let newPrevBtn = emptyPreviousBtn.clone();

        newPrevBtn.find('a').attr('href', prev);
        newPrevBtn.removeAttr('id');
        newPrevBtn.click(function (e) {
            e.preventDefault();
            menuClickHandler(prev);
        });
        newPrevBtn.show();
        return newPrevBtn;
    }
    var renderOption = function (param) {
        let pathArr = $.session.get('path').split('/');
        let newBtn = criclebtn.clone();
        newBtn.removeAttr('id');
        newBtn.click(function (e) {
            $.get('/'+param+'/create', function (data) {
                $.session.set('currentID', pathArr[pathArr.length - 1]);
                view(data)
            });
        });
        newBtn.show();
        options.append(newBtn);
    }
    var renderOptions = function () {
        options.html('');
        let pathArr = $.session.get('path').split('/');
        let len = pathArr.length - 3;
        let param = null;
        switch (len) {
            case 4:
                param = 'video';
                break;
            case 3:
                param = 'lecture';
                break;

            default:
                break;
        }
        if (param != null) {
            renderOption(param);
        }
    }

    var renderMenu = function () {
        renderOptions();
        let url = $.session.get('path');
        area.html('');
        if (url != '/api/path') {
            area.append(generatePrevBtn());
        }
        $.get(url, function (data) {
            if (data.length == 0) {
                area.append("No items here.<br>Be the first to add items.")
                $.session.set('path',url);
            } else {
                let len = url.split('/').length - 3;
                $.each(data, function () {
                    let newMenuItem = emptyMenuItem.clone();
                    let nextPath = url + '/' + this.id;

                    if (len == 4) {
                        let video = this;
                        newMenuItem.click(function (e) {
                            e.preventDefault();
                            $.get('Video/Details/' + video.id, function (data) {
                                view(data);
                            }).fail(function () {
                                view('<h1 class="alert alert-danger">Login first madafaka!</h1>');
                            });

                        });
                    } else {
                        newMenuItem.click(function (e) {
                            e.preventDefault();
                            menuClickHandler(nextPath);
                        });
                    }

                    newMenuItem.find('a').attr('href', nextPath);
                    newMenuItem.find('a').append(this.name);

                    newMenuItem.show();
                    area.append(newMenuItem);
                });         
            }
            
        }).fail(function (e) {
            area.append('There is an error ): <br>'
                + JSON.stringify(JSON.parse(JSON.stringify(e)), null, 2)
                + '<br>Please content for any help:<br>' + '<b>avivsegal@gmail.com</b>');
        });
    }

    renderMenu();


    /* Search */

    // Prevent enter key
    $('#searchIn').keypress(function (event) {
        if (event.keyCode === 10 || event.keyCode === 13) {
            event.preventDefault();
        }
    });

    var clearSearch = function() {
        $('#searchform').hide();
        $('#searchIn').val('')
        $('#results').html('');
    }
    $('#searchIn').on('input', function () {
        $('#results').clone();
            $('#results').html('');
            $('#results').hide();
            var keyword = $('#searchIn').val();
            $.get('/api/Videos/Search/' + keyword + '/{"institution":"colman","school":"Design","subject":"Mavo to ART"}', function (data) {
                $('#searchform').attr('action', 'home/search/' + keyword);
                console.log('bokertov')
                if (data.length == 0) {
                    $('#results').html('');
                }
                else {
                    $('#results').show();
                    $.each(data, function () {
                        let resultItem = resultLine.clone();
                        resultItem.attr('id', 'resultLine_' + this.id);
                        resultItem.find('.videolink').append(this.video.name);
                        resultItem.find('.lecturelink').append(this.lecture.name);
                        resultItem.find('.subjectlink').append(this.subject.name);
                        resultItem.find('.schoollink').append(this.school.name);
                        resultItem.find('.institutionlink').append(this.institution.name);
                        resultItem.show();
                        resultItem.click(function (e) {
                            e.preventDefault();
                            $.get('Video/Details/' + id, function (data) {
                                view(data);
                            });
                            clearSearch();
                        });
                        $('#results').append(resultItem);
                    });
                }
            });
    });
});