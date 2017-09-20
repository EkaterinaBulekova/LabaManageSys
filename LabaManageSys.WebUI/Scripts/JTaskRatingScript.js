function processTaskData(data) {
    var Taskstarget = $("#TasksList");
    Taskstarget.empty();
    var PagesCount = data.TotalPages;
    var tasks = data.Tasks;
    for (var i = 0; i < tasks.length; i++) {
        var item = tasks[i];
        var stars = "<ul class=\"rating\">"
        for (var j = 1; j < 6; j++) {
            if (item.AvgRating >= j) {
                stars += "<li><a style=\"background:url(/images/star.jpg) center;\"></a></li>"
            }
            else {
                if (item.AvgRating >= j - 0.2) {
                    stars += "<li><a style=\"background:url(/images/star1.jpg) center;\"></a></li>"
                } else {
                    if (item.AvgRating >= j - 0.5) {
                        stars += "<li><a style=\"background:url(/images/star.jpg) top;\"></a></li>"
                    } else {
                        if (item.AvgRating >= j - 0.8) {
                            stars += "<li><a style=\"background:url(/images/star1.jpg) bottom;\"></a></li>"
                        } else {
                            stars += "<li><a style=\"background:url(/images/star.jpg) bottom;\"></a></li>"
                        }
                    }
                }
            }
        }
        stars += "</ul>";
        var urlDelete = Taskstarget.data('request-taskdelete-url');
        var urlEdit = Taskstarget.data('request-taskedit-url');
        var avgrating = item.AvgRating.toString().substring(0, 3);
        Taskstarget.append("<div class=\"well\"><div class=\"btn-group pull-right\"><a class=\"btn btn-primary\" style=\"margin-right:4px; border-radius:3px;\" href='" +
            urlEdit + item.Task.TaskId + "'>Изменить</a><a class=\"btn btn-danger\" style=\"border-radius:3px;\" href='" + urlDelete + item.Task.TaskId +
            "'>Удалить</a></div><h4>Тема: " + item.Task.Topic + " уровень: " + item.Task.Level + " автор: " + item.Task.Author + "</h4><h4>" + item.Task.Name +
            "</h4><div class=\"well\" id=\"textwellp" + item.Task.TaskId + "\" style=\"display:block\"><div>" + stars + "</div><p>" + avgrating + "</p><p>" +
            item.Task.Text.substring(0, 10) + "</p><button id=\"f" + item.Task.TaskId + "\" class=\"btn btn-default\">Подробнее</button ></div ><div class=\"well\" id=\"textwellf" +
            item.Task.TaskId + "\" style=\"display:none\"><div class=\"btn-group pull-right\"><button id=\"p" + item.Task.TaskId + "\" class=\"btn btn-default\" " +
            "style=\"float: right;\">Закрыть</button></div><br><div>" + item.Task.Text + "</div >" +
            "<div id=\"coment" + item.Task.TaskId + "\"></div></div>");
    }
    var Buttonstarget = $("#ButtonsGroup");
    Buttonstarget.empty();
    var str = '';
    var i_str = '';
    for (var i = 1; i <= PagesCount; i++) {
        i_str = i.toString();
        if (i == 1) {
            str = "<a class=\"btn btn-primary\" href=\"/TaskManage/List?page=" + i_str + "\">Page " + i_str + "</a>";
            Buttonstarget.append(str);
        }
        else {
            str = "<a class=\"btn btn-default\" href=\"/TaskManage/List?page=" + i_str + "\">Page " + i_str + "</a>";
            Buttonstarget.append(str);
        }
    }
}

function processTaskComent(data, taskId) {
    var fullid = "#coment" + taskId
    var Commentstarget = $(fullid);
    Commentstarget.empty();
    var coments = data.Ratings;
    var stars = "<div class=\"well\" id=\"stars" + taskId + "\"><div>" +
        "<fieldset><legend>Ваша оценка и коментарий:</legend>Оцените вопрос:<br><ul class=\"rating\">";
    for (var j = 1; j < 6; j++) {
        if (data.CurrentUserRating.Evaluation >= j) {
            stars += "<li><a id=" + taskId + "s" + j + " style=\"background:url(/images/star.jpg) center;\"></a></li>";
        }
        else {
            stars += "<li><a id=" + taskId + "s" + j + " style=\"background:url(/images/star.jpg) bottom;\"></a></li>";
        }
    }
    stars += "</ul>"
    var form = "<input hidden id=\"eval" + taskId + "\" type=\"text\" name=\"Evaluation\" value=\"" +
        data.CurrentUserRating.Evaluation + "\"><input id=\"rating" + taskId + "\" hidden type=\"text\" name=\"RatingId\" value=\"" +
        data.CurrentUserRating.RatingId + "\"><input id=\"task" + taskId + "\" hidden type=\"text\" name=\"TaskId\" value=\"" +
        data.CurrentUserRating.TaskId + "\"><input id=\"user" + taskId + "\" hidden type=\"text\" name=\"UserId\" value=\"" +
        data.CurrentUserRating.UserId + "\"><br>Оставьте коментарий к вопросу:<br><div class=\"form-group\">" +
        "<textarea id=\"comm" + taskId + "\" class=\"form-control\" placeholder=\"Коментарий...\" name=\"Comment\">" +
        data.CurrentUserRating.Comment + "</textarea></div><br><button data-taskId=" + taskId + " id=\"d" + data.CurrentUserRating.RatingId +
        "\" class=\"btn btn-danger\" style=\"float: right;\">Удалить</button><button  id=\"s" + taskId + "\" class=\"btn btn-primary\" " +
        "style=\"float: right; margin-right:4px;\">Сохранить</button></div></div></fieldset>";
    Commentstarget.append(stars + form);
    for (var i = 0; i < coments.length; i++) {
        var item = coments[i];
        var stars = "<ul class=\"rating\">";
        for (var j = 1; j < 6; j++) {
            if (item.Evaluation >= j) {
                stars += "<li><a style=\"background:url(/images/star.jpg) center;\"></a></li>";
            }
            else {
                stars += "<li><a style=\"background:url(/images/star.jpg) bottom;\"></a></li>";
            }
        }
        stars += "</ul>";
        str = "<div class=\"well\"><b>" + item.UserName + "</b><div style=\"float: right;\">" + stars + "</div><p>" + item.Comment + "</p></div>";
        Commentstarget.append(str);
    }
}

function AjaxTask(taskLoadUrl, optionName, selectedValue, callback) {
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: taskLoadUrl + optionName + "=" + selectedValue,
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

$(document).ready(function () {
    $('select').change(function () {
        var selectedValue = $(this).val();
        var optionName = $(this).attr('name').substring(7);
        var url = $("#TasksList").data('request-tasks-url');
        AjaxTask(url, optionName, selectedValue, processTaskData)
    })

    $("#TasksList").on('mouseover', 'a', function () {
        var id = $(this).attr('id');
        if (id != null) {
            var pos = id.substring(id.length - 1);
            id = id.substring(0, id.length - 2);
            $("#eval" + id).val(pos);
            for (var i = 1; i <= 6; i++) {
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
        var id = '';
        var start = 'textwell';
        var str = $(this).attr('id').substring(0, 1);
        var url = $("#TasksList").data('request-comments-url');

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
        }

        if (str == 'd') {
            id = $(this).attr('id').substring(1);
            var tid = $(this).attr('data-taskId');
            var dt = new FormData();
            var deleteUrl = $("#TasksList").data('request-commentdelete-url');
            dt.append('commentId', id);
            $.ajax({
                type: "POST",
                url: deleteUrl,
                data: dt,
                processData: false,
                contentType: false,
                success: function () {
                    AjaxComent(url, tid, processTaskComent);
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

        if (str == 's') {
            id = $(this).attr('id').substring(1);
            var uid = $('#user' + id).val();
            var rid = $('#rating' + id).val();
            var eid = $('#eval' + id).val();
            var cid = $('#comm' + id).val();
            var dt = new FormData();
            dt.append('TaskId', id);
            dt.append('UserId', uid);
            dt.append('RatingId', rid);
            dt.append('Evaluation', eid);
            dt.append('Comment', cid);
            var saveUrl = $("#TasksList").data('request-commentsave-url');
            $.ajax({
                type: "POST",
                url: saveUrl,
                data: dt,
                processData: false,
                contentType: false,
                success: function () {
                    AjaxComent(url, id, processTaskComent);
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
    })
})