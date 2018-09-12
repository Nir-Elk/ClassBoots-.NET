/* avivsegal@gmail.com */
$(function () {
    var area = $('#sidebarnav');
    var emptyMenuItem = $('#menuitem');
    var emptyPreviousBtn = $('#previousbtn');
    var criclebtn = $('#circlebtn');
    var options = $('#sidebarnav-options');
    criclebtn.hide();
    emptyMenuItem.hide();
    emptyPreviousBtn.hide();


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
                $.session.set('path',url + '/');
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
                                })

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
                + '<br>Please Context for any help:<br>' + '<b>avivsegal@gmail.com</b>');
        });
    }

    renderMenu();
});