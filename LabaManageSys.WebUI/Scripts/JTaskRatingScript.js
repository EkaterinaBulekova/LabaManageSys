function GetStars(rating, tid) {
    var start = "<li><a style='background:url(/images/";
    var end = "></a></li>";
    var id = "";
    var stars = "<ul class='rating' style='display:inherit'>";
    for (var j = 1; j < 6; j++) {
        if (tid != "" && tid != null) {
            id = " id='" + tid + "s" + j + "' ";
        }
        if (rating >= j) {
            stars += start + "star.jpg) center;'" + id + end;
        }
        else {
            if (rating >= j - 0.2) {
                stars += start + "star1.jpg) center;'" + id + end;
            } else {
                if (rating >= j - 0.5) {
                    stars += start + "star.jpg) top;'" + id + end;
                } else {
                    if (rating >= j - 0.8) {
                        stars += start + "star1.jpg) bottom;'" + id + end;
                    } else {
                        stars += start + "star.jpg) bottom;'" + id + end;
                    }
                }
            }
        }
    }
    stars += "</ul>";
    return stars;
}

function GetPercents(percents) {
    var starPercents = "<div class='hidden-xs' style='float: right;'>";
    for (i = 5; i >= 1; i--)
    {
        starPercents += "<div style='display:inherit'><div style='display:inline-flex;'>";
        starPercents += GetStars(i);
        starPercents += "<span style='display:inherit'> - " + percents[i - 1] + "%</span></div></div>";
    }

    starPercents += "</div>";
    return starPercents;
}

function GetButtons(tags) {
    var buttons = "";
    if (tags != null) {
        for (i = 0; i < tags.length; i++) {
            buttons += "<button id='t" + tags[i].TagId + "' class='btn btn-info btn-sm hidden-xs' style='margin-right:3px;' name='" + tags[i].Name + "'>" + tags[i].Name + "</button>";
        }
    }
    else {
        buttons += "<br />"
    }

    return buttons;
}

function processTaskData(data) {
    var Taskstarget = $("#TasksList");
    Taskstarget.empty();
    var PagesCount = data.TotalPages;
    var tasks = data.Tasks;
    for (var i = 0; i < tasks.length; i++) {
        var item = tasks[i];
        var urlDelete = Taskstarget.data('request-taskdelete-url');
        var urlEdit = Taskstarget.data('request-taskedit-url');
        var avgrating = item.AvgRating;
        var percents = GetPercents(item.PercentFeature);
        var stars = GetStars(avgrating);
        var buttons = GetButtons(item.Task.Tags);
        Taskstarget.append("<div class='well'><div class='btn-group pull-right'><form action='" + urlDelete +
            "' method='post'><a class='btn btn-primary glyphicon glyphicon-edit visible-xs' style='margin-right:4px; border-radius:3px;' href='" +
            urlEdit + item.Task.TaskId + "'></a><a class='btn btn-primary hidden-xs' style='margin-right:4px; border-radius:3px;' href='" +
            urlEdit + item.Task.TaskId + "'>Изменить</a><input hidden id='TaskId' name='TaskId' value='" + item.Task.TaskId +
            "'/><button type='submit' formmethod='post' class='btn btn-danger glyphicon glyphicon-remove visible-xs'></button>" +
            "<button type='submit' class='btn btn-danger hidden-xs' style='border-radius:3px;'>Удалить</button></form></div><h4>Тема: " +
            item.Task.Topic + " уровень: " + item.Task.Level + " автор: " + item.Task.Author + "</h4><h4>" + item.Task.Name +
            "</h4><div class='well' id='textwellp" + item.Task.TaskId + "' style='display:block'>" + percents + "<div>" + stars + "</div><p>" +
            avgrating + "</p><span>" + item.Task.Text.substring(0, 10) + "</span><button id='f" + item.Task.TaskId +
            "' class='btn btn-default btn-sm hidden-xs'>...</button ><button id='f" + item.Task.TaskId +
            "' class='btn btn-default glyphicon glyphicon-arrow-down visible-xs'></button ><br /><br />" + buttons +
            "</div ><div class='well' id='textwellf" + item.Task.TaskId + "' style='display:none'><div class='btn-group pull-right'><button id='p" +
            item.Task.TaskId + "' class='btn btn-default btn-sm hidden-xs' style='float: right;'>Свернуть</button><button id='p" +
            item.Task.TaskId + "' class='btn btn-default glyphicon glyphicon-arrow-up visible-xs' style='float: right;'></button></div><br/><div>" +
            item.Task.Text + "</div><br />" + buttons + "<div id='coment" + item.Task.TaskId + "'></div></div>");
    }
    var Buttonstarget = $("#ButtonsGroup");
    Buttonstarget.empty();
    var str = '';
    var i_str = '';
    for (var i = 1; i <= PagesCount; i++) {
        i_str = i.toString();
        if (i == 1) {
            str = "<a class='btn btn-primary' href='/TaskManage/List?page=" + i_str + "'>Page " + i_str + "</a>";
            Buttonstarget.append(str);
        }
        else {
            str = "<a class='btn btn-default' href='/TaskManage/List?page=" + i_str + "'>Page " + i_str + "</a>";
            Buttonstarget.append(str);
        }
    }
}

function processTaskComent(data, taskId) {
    var fullid = "#coment" + taskId
    var Commentstarget = $(fullid);
    Commentstarget.empty();
    var coments = data.Ratings;
    var stars = "<div class='well' id='stars" + taskId + "'><div>" +
        "<fieldset><legend>Ваша оценка и коментарий:</legend>Оцените вопрос:<br>";
    stars += GetStars(data.CurrentUserRating.Evaluation, taskId);
    var form = "<input hidden id='eval" + taskId + "' type='text' name='Evaluation' value='" +
        data.CurrentUserRating.Evaluation + "'><input id='rating" + taskId + "' hidden type='text' name='RatingId' value='" +
        data.CurrentUserRating.RatingId + "'><input id='task" + taskId + "' hidden type='text' name='TaskId' value='" +
        data.CurrentUserRating.TaskId + "'><input id='user" + taskId + "' hidden type='text' name='UserId' value='" +
        data.CurrentUserRating.UserId + "'><br>Оставьте коментарий к вопросу:<br><div class='form-group'>" +
        "<textarea id='comm" + taskId + "' class='form-control' placeholder='Коментарий...' name='Comment'>" +
        data.CurrentUserRating.Comment + "</textarea></div><br><button data-taskId=" + taskId + " id='d" + data.CurrentUserRating.RatingId +
        "' class='btn btn-danger hidden-xs' style='float: right;'>Удалить</button><button  id='s" + taskId + "' class='btn btn-primary hidden-xs' " +
        "style='float: right; margin-right:4px;'>Сохранить</button><button data-taskId=" + taskId + " id='d" + data.CurrentUserRating.RatingId +
        "' class='btn btn-danger glyphicon glyphicon-remove visible-xs' style='float: right;'></button><button  id='s" + taskId +
        "' class='btn btn-primary glyphicon glyphicon-save visible-xs' style='float: right; margin-right:4px;'></button></div></div></fieldset>";
    Commentstarget.append(stars + form);
    for (var i = 0; i < coments.length; i++) {
        var item = coments[i];
        var stars = GetStars(item.Evaluation)
        str = "<div class='well'><b>" + item.UserName + "</b><div style='float: right;'>" + stars + "</div><p>" + item.Comment + "</p></div>";
        Commentstarget.append(str);
    }
}

function AjaxTask(taskLoadUrl, optionName, selectedValue, callback) {
    var fullurl = (optionName != "") ? taskLoadUrl + optionName + "=" + selectedValue : taskLoadUrl;
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: fullurl,
        success: function (result) {
            callback(result);
        },
        error: function (data) {
            alert("Извините произошла непредвиденная ошибка обратитись пожалуйста к администратору сайта.");
        }
    });
}

function AjaxComent(commentLoadUrl, taskId, callback) {
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: commentLoadUrl + taskId,
        success: function (result) {
            callback(result, taskId);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            if (jqXHR.status == 500) {
                alert('Internal error: ' + jqXHR.responseText);
            } else {
                alert('Unexpected error.' + jqXHR.responseText);
            }
        }
    });
}

function AjaxEdit(dt, curUrl, url, tid, callback){
    $.ajax({
        type: "POST",
        url: curUrl,
        data: dt,
        processData: false,
        contentType: false,
        success: function () {
            AjaxComent(url, tid, callback);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            if (jqXHR.status == 500) {
                alert('Internal error: ' + jqXHR.responseText);
            } else {
                alert('Unexpected error.' + jqXHR.responseText);
            }
        }
    });
}

$(document).ready(function () {
    let Turl = $("#container").data('request-tags-url');
    TagsAjax(Turl, undefined);


    $('select').change(function () {
        let selectedValue = $(this).val();
        let optionName = $(this).attr('name').substring(7);
        let url = $("#TasksList").data('request-tasks-url');
        AjaxTask(url, optionName, selectedValue, processTaskData)
    })

    $("#TasksList").on('mouseover', 'a', function () {
        let id = $(this).attr('id');
        if (id != null) {
            let pos = id.substring(id.length - 1);
            id = id.substring(0, id.length - 2);
            $("#eval" + id).val(pos);
            for (let i = 1; i <= 6; i++) {
                var star = $("#" + id + "s" + i);
                if (i <= pos) {
                    star.css("background", "url(/images/star.jpg) center");
                }
                if (i > pos) {
                    star.css("background", "url(/images/star.jpg) bottom");
                }
            }
        }
    });

    $("#TasksList").on('click', 'button', function () {
        let id = '';
        let start = 'textwell';
        let str = $(this).attr('id').substring(0, 1);
        let url = $("#TasksList").data('request-comments-url');
        let taskurl = $("#TasksList").data('request-tasks-url');

        if (str == 'f') {
            id = $(this).attr('id').substring(1);
            fullid = '#' + start + 'f' + id;
            $(fullid).show();
            fullid = '#' + start + 'p' + id;
            $(fullid).hide();
            AjaxComent(url, id, processTaskComent)
        }

        if (str == 'p') {
            id = $(this).attr('id').substring(1);
            fullid = '#' + start + 'f' + id;
            $(fullid).hide();
            fullid = '#' + start + 'p' + id;
            $(fullid).show();
            AjaxTask(taskurl, "", "", processTaskData)
        }

        if (str == 't') {
            id = $(this).attr('id').substring(1);
            tx = $(this).attr('name');
            $('#tokenfield').tokenfield('setTokens', [{ value: tx, label: tx }]);
            AjaxTask(taskurl, "tags", tx, processTaskData)
        }

        if (str == 'd') {
            id = $(this).attr('id').substring(1);
            let tid = $(this).attr('data-taskId');
            let dt = new FormData();
            let deleteUrl = $("#TasksList").data('request-commentdelete-url');
            dt.append('commentId', id);
            AjaxEdit(dt, deleteUrl, url, tid, processTaskComent);
        }

        if (str == 's') {
            id = $(this).attr('id').substring(1);
            let uid = $('#user' + id).val();
            let rid = $('#rating' + id).val();
            let eid = $('#eval' + id).val();
            let cid = $('#comm' + id).val();
            var dt = new FormData();
            dt.append('TaskId', id);
            dt.append('UserId', uid);
            dt.append('RatingId', rid);
            dt.append('Evaluation', eid);
            dt.append('Comment', cid);
            let saveUrl = $("#TasksList").data('request-commentsave-url');
            AjaxEdit(dt, saveUrl, url, id, processTaskComent);
        }
    })

    function TokenChange() {
        let tags = $('#tokenfield').tokenfield('getTokensList', ',', false, false);
        taskurl = $("#TasksList").data('request-tasks-url')
        AjaxTask(taskurl, "tags", tags, processTaskData);
    }

    $("#tokenfield").on("tokenfield:createtoken", function (e) {
        let selectedToken = e.attrs.value;
        let source = $('#tokenfield').data('bs.tokenfield').$input.autocomplete("option", "source");
        let isExist = false;
        let Index = 0;
        $.each(source, function (index, token) {
            if (token.value === selectedToken) {
                isExist = true;
                Index = index;
            }
        });
        if (isExist === true) {
            source.splice(Index, 1);
            let newSource = source;
            $('#tokenfield').data('bs.tokenfield').$input.autocomplete({ source: newSource });
        }
        else {
            e.preventDefault();
        }
    });

    $('#tokenfield').on('tokenfield:removetoken', function (e) {
        let selectedToken = e.attrs.value;
        let source = $('#tokenfield').data('bs.tokenfield').$input.autocomplete("option", "source");
        source.push({ value: selectedToken, label: selectedToken });
        let newSource = source;
        $('#tokenfield').data('bs.tokenfield').$input.autocomplete({ source: newSource });
    })

    $('#tokenfield').on('tokenfield:createdtoken', function (e) {
        TokenChange();
    })

    $('#tokenfield').on('tokenfield:removedtoken', function (e) {
        TokenChange();
    })
})