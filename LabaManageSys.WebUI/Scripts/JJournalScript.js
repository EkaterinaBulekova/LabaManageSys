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
        var dt = new FormData();
        dt.append('lessonId', $(this).data('LessonId'));
        dt.append('userId', $(this).data('UserId'));
        var removeUrl = $('#journal').data('request-Remove-url');
        var addUrl = $('#journal').data('request-Add-url');
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
