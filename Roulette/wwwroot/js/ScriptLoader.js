window.GetCurrentPos = () => {

    var a = parseInt($('#case').css("background-position").split(" ")[0].slice(0, -2));
    if (a == null) {
        a = 0;
    }
    console.log("curpos = " + a);
    
    return a;
};


window.Animate = (currentPos, numWidth, moves, t) => {

    console.log("curpos = " + currentPos);
    console.log("numWidth = " + numWidth);
    console.log("moves = " + moves);
    $('#case').animate({
        "background-position": currentPos - 2100 - (moves * numWidth),
    }, {
        'duration': t * 1000,
        'easing': $.easing.easeOutExpo(),

    });
};


window.Resize = () =>{
    console.log("resized")
    resize();
}


window.Bar = (t) => {
    $(".timer").finish().css("width", "100%");
    _is_rolling = false;
    end_timer = Math.floor(new Date() / 1000) + t;
    $(".timer").animate({
        'width': '0%'
    }, {
        'duration': t * 1000,
        'easing': 'linear',
        // 'progress': function (t, e, a) {
        //     $BANNER.html(LNG.ROLL_TIME.replace('%time%', (a / 1000).toFixed(2)));
        // },

    });
}