function resize() {
    if (parseInt($('#case').attr('class')) < 0) {
        for (i = 0; i <= 1; i++) {
            if (i == 0) {
                if (screenWidth > window.innerWidth) {
                    for (z = 0; z <= 1; z++) {
                        if (z == 0) {
                            if (parseFloat($('#case').css('background-position').split(' ')[0].replace("px", "")) > parseFloat($('#case').attr('class'))) {
                                console.log(parseFloat($('#case').attr('data-id')));
                                $('#case').css('background-position', parseInt($('#case').attr('class')) + "px")
                                z = 2;
                                return;
                            }
                        }
                        if (z == 1) {
                            $('#case').css('background-position', parseInt($('#case').attr('class')) + ($('#case').width() / 70) / 2 * 70 - 525 + "px")
                            $('#case').attr('data-id', ($('#case').width() / 70) / 2 * 70 - 525);
                        }
                    }
                } else {
                    for (x = 0; x <= 1; x++) {
                        if (x == 0) {
                            var a = parseFloat($('#case').css('background-position').split(' ')[0].replace("px", ""))
                            var b = parseFloat($('#case').attr('data-id'));
                            var c = ($('#case').width() / 70) / 2 * 70 - 525;
                            d = b - c;
                            e = a - d;
                            $('#case').css('background-position', e + "px")
                            // it should be a - d position perfect
                        }
                        if (x == 1) {
                            $('#case').attr('data-id', ($('#case').width() / 70) / 2 * 70 - 525);
                        }
                    }
                }
            }
            if (i == 1) {
                screenWidth = window.innerWidth;
            }
        }
    } else {
        $('#case').css('background-position', parseInt($('#case').attr('class')) + ($('#case').width() / 70) / 2 * 70 - 525 + "px")
        $('#case').attr('data-id', ($('#case').width() / 70) / 2 * 70 - 525);
    }
}

$(window).resize(function () {
    resize();
});
