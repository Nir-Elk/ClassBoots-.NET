
/* avivsegal@gmail.com */


$(function () {
    var area = $('#sidebarnav');
    var emptyMenuItem = $('#menuitem');
    var emptyPreviousBtn = $('#previousbtn');
    var emptyHomeBtn = $('#homebtn');

    var criclebtn = $('#circlebtn');
    var options = $('#sidebarnav-options');
    var resultLine = $('#resultLine');

    resultLine.hide();
    criclebtn.hide();
    emptyMenuItem.hide();
    emptyPreviousBtn.hide();
    emptyHomeBtn.hide();


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
            menuClickHandler(prev);
            return e.preventDefault();
        });
        newPrevBtn.show();
        return newPrevBtn;
    }

    var generateHomeBtn = function () {
        let newHomeBtn = emptyHomeBtn.clone();

        newHomeBtn.find('a').attr('href', '/');
        newHomeBtn.removeAttr('id');
        newHomeBtn.click(function (e) {
            $.session.clear();
        });
        newHomeBtn.show();
        return newHomeBtn;
    }

    var renderOption = function (param,id) {
        let newBtn = criclebtn.clone();
        newBtn.removeAttr('id');
        newBtn.click(function () {
            window.location.replace('/' +param + '/Create');
        });
        newBtn.show();
        options.append(newBtn);
    }
    var renderOptions = function (id) {
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
            case 2:
                param = 'subject';
                break;

            default:
                break;
        }
        if (param != null) {
            $.session.set('current_' + param, id);
            if (len > 2) {
                renderOption(param);
            }
        }
    }

    var renderMenu = function () {
        let url = $.session.get('path');
        area.html('');
        if (url != '/api/path') {
            area.append(generatePrevBtn());
            area.append(generateHomeBtn());
        }
        $.get(url, function (data) {
            if (data.length == 0) {
                area.append("No items here.<br>Be the first to add items.")
				$.session.set('path/', url);
            } else {
                let len = url.split('/').length - 3;
                $.each(data, function () {
                    let newMenuItem = emptyMenuItem.clone();
                    let nextPath = url + '/' + this.id;

                    if (len == 4) {
                        newMenuItem.find('a').attr('href', '/Video/View/' + this.id);

                        //newMenuItem.click(function (e) {
                        //    history.pushState('/Video/' + video.id, event.target.textContent, '/Video/View/' + video.id);
                        //    $.get('/Video/Details/' + video.id, function (data) {
                        //        view(data);
                        //    }).fail(function () {
                        //        view('<h1 class="alert alert-danger">Please Log-in.</h1>');
                        //        });
                        //    return e.preventDefault();
                        //});
                    } else {
                        newMenuItem.click(function (e) {
                            menuClickHandler(nextPath);
                            return e.preventDefault();
                        });
                        newMenuItem.find('a').attr('href', nextPath);
                    }
                    renderOptions(url.split('/').pop());

                    newMenuItem.find('a').find('img').attr('src', this.image);
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

    // Choose filters
    $.get('/api/Institutions', function (data) {
        var resultItems = $("#filterinstitution")
        $.each(data, function () {
            resultItems.append('<option value="' + this.name + '">' + this.name + '</option >');
        });
    });
    $.get('/api/Schools', function (data) {
        var resultItems = $("#filterschool")
        $.each(data, function () {
            resultItems.append('<option value="' + this.name + '">' + this.name + '</option >');
        });
    });
    $.get('/api/Subjects', function (data) {
        var resultItems = $("#filtersubject")
        $.each(data, function () {
            resultItems.append('<option value="' + this.name + '">' + this.name + '</option >');
        });
    });


    // Prevent enter key
    $('#searchIn').keypress(function (event) {
        if (event.keyCode === 10 || event.keyCode === 13) {
            event.preventDefault();
        }
    });


    // Search code
    var clearSearch = function() {
        $('#searchform').hide();
        $('#searchIn').val('')
        $('#results').html('');
    }

    //Parameters
    var institutionFilter = '';
    var schoolFilter = '';
    var subjectFilter = '';
    var renderResults = function () {
        $('#results').html('');
        $('#results').hide();
        var keyword = $('#searchIn').val();
        $.get('/api/Videos/Search/' + keyword + '/{"institution":"' + institutionFilter + '","school":"' + schoolFilter + '","subject":"' + subjectFilter +'"}', function (data) {
            $('#searchform').attr('action', 'home/search/' + keyword);
            console.log('bokertov')
            if (data.length == 0) {
                $('#results').html('');
            }
            else {
                $('#results').show();
                $.each(data, function () {
                    //let id = this.video.id;
                    let resultItem = resultLine.clone();
                    resultItem.attr('id', 'resultLine_' + this.video.id);
                    resultItem.find('a').attr('href', 'Video/View/' + this.video.id);

                    resultItem.find('a').find('.videolink').append(this.video.name);
                    resultItem.find('a').find('.lecturelink').append(this.lecture.name);
                    resultItem.find('a').find('.subjectlink').append(this.subject.name);
                    resultItem.find('a').find('.schoollink').append(this.school.name);
                    resultItem.find('a').find('.institutionlink').append(this.institution.name);
                   
                    resultItem.show();
                    //resultItem.find('a').click(function (e) {
                    //    e.preventDefault();
                    //    history.pushState('/Video/' + id, event.target.textContent, '/Video/View/' + id);

                    //    $.get('/Video/Details/' + id, function (d) {
                    //        view(d);
                    //    });
                    //    clearSearch();
                    //});
                    $('#results').append(resultItem);
                });
            }
        });
    }
    $("#filterinstitution").change(function (e) {
        institutionFilter = $("#filterinstitution").val();
        renderResults();
    });
    $("#filterschool").change(function (e) {
        schoolFilter = $("#filterschool").val();
        renderResults();
    });
    $("#filtersubject").change(function (e) {
        subjectFilter = $("#filtersubject").val();
        renderResults();
    });
    $('#searchIn').on('input', function () {
		renderResults();
	});
});