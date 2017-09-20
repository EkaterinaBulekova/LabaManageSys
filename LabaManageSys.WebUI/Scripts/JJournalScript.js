function Ajax(currUrl, dt) {
    $.ajax({
        type: "POST",
        url: currUrl,
        data: dt,
        processData: false,
        contentType: false,
        success: function () {
        },
        error: function (data) {
            alert("Извините произошла непредвиденная ошибка обратитись пожалуйста к администратору сайта.");
        }
    });
}

$(document).ready(function () {
    $("td").on("click", function () {
        var target = $(this);
        var dt = new FormData();
        var lessId = $(this).attr('data-LessonId');
        var userId = $(this).attr('data-UserId');
        dt.append('lessonId', lessId);
        dt.append('userId', userId);
        var removeUrl = $("#journal").attr('data-request-Remove-url');
        var addUrl = $("#journal").attr('data-request-Add-url');
        if ($(this).is(":contains('H')")) {
            $(this).text(" ");
            Ajax(removeUrl, dt);
        }
        else {
            $(this).text("H");
            Ajax(addUrl, dt);
        }
    });
});