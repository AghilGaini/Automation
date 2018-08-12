$(document).ready(function () {

    var path = document.location.pathname;

    //alert("this is path:      " + path + "/GetPrivilege")

    //var as = $('[gid]')
    //$.ajax({
    //    type: 'POST',
    //    url: 'test',
    //    data: JSON.stringify({}),
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json"
    //}).then
    //        (
    //        function (data) {
    //            if (data.d[0] == "1") {
    //                var result = JSON.parse(data.d[1]);

    //                if ($.inArray('manager', result) > -1)
    //                    return;

    //                for (i = 0 ; i < as.length ; i++) {
    //                    if ($.inArray($(as[i]).attr('gid').toLowerCase(), result) == -1)
    //                        $(as[i]).hide();
    //                    if ($(as[i]).text() == 'تغییر مشخصات')
    //                        $(as[i]).show();
    //                }

    //            }
    //            else if (data.d[1] == "0") {
    //                for (i = 0 ; i < as.length ; i++)
    //                    $(as[i]).hide();
    //            }
    //        }, function (data) {
    //            ShowError("", "عدم برقراری ارتباط");
    //        }
    //        )

})