$('#Dates').datepicker({
    inline: true,
    timepicker: true,
    startHour: 9,
    minHours: 9,
    maxHours: 18,
    minDate: new Date()
})
var disabledDays = [0, 6];

$('#Dates').datepicker({
    onRenderCell: function (date, cellType) {
        if (cellType == 'day') {
            var day = date.getDay(),
                isDisabled = disabledDays.indexOf(day) != -1;

            return {
                disabled: isDisabled
            }
        }
    }
})

$(document).ready(function () {
    var myDatepicker = $('#Dates').datepicker().data('datepicker');
    var str = $('#Dates').attr('data-dates');
    if (str != "" && str != null) {
        var dates = str.split(',');
        var date = [];
        for (var i = 0; i < dates.length; i++) {
            date[i] = (new Date(dates[i]));
        }
        myDatepicker.selectDate(date);
    }
});