function start(str) {
    $(str + "_rightSelected").click(function () {
        if ($("select option:selected").text() === '') {
            alert("You need to select option first.");
        }
        else {
            $("select option:selected").each(function () {
                if ($(this).parent(str + "_availableFrom").length) {
                    var text = $(this).text();
                    $(str + "_selectedTo").append("<option>" + text + "</option>");
                    $(this).remove();
                }
            });
        }
    });

    $(str + "_leftSelected").click(function () {
        if ($("select option:selected").text() === '') {
            alert("You need to select option first.");
        }
        else {
            $("select option:selected").each(function () {
                if ($(this).parent(str + "_selectedTo").length) {
                    var text = $(this).text();
                    $(str + "_availableFrom").append("<option>" + text + "</option>");
                    $(this).remove();
                }
            });
        }
    });

    $(str + "_rightAll").click(function () {
        $(str + "_availableFrom").find("option").each(function () {
            var text = $(this).text();
            $(str + "_selectedTo").append("<option>" + text + "</option>");
        });
        $(str + "_availableFrom").find("option").remove();
    });

    $(str + "_leftAll").click(function () {
        $(str + "_selectedTo").find("option").each(function () {
            var text = $(this).text();
            $(str + "_availableFrom").append("<option>" + text + "</option>");
        });
        $(str + "_selectedTo").find("option").remove();
    });
}