var animate;

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
    if (animate !== undefined) {
        animate.finish();  
    }
    animate = $('#case').animate({
        "background-position": currentPos - 2100 - (moves * numWidth),
    }, {
        'duration': t * 1000,
        'easing': $.easing.easeOutExpo(),

    });
};


window.Resize = () => {
    console.log("resized")
    resize();
}


window.Bar = (t) => {
    console.log(t)
    $(".timer").finish().css("width", "100%");
    _is_rolling = false;
    end_timer = Math.floor(new Date() / 1000) + t;
    $(".timer").animate({
        'width': '0%'
    }, {
        'duration': t * 1000,
        'easing': 'linear',
        'progress': function (t, e, a) {
            $(".progress_timer").html(('%time%', (a / 1000).toFixed(2)));
        },

    });

}


window.AddModal = () => {
    $('.modal').modal();
    $('.sidenav').sidenav({
        menuWidth: 300, // Default is 240
        closeOnClick: true,// Closes side-nav on <a> clicks, useful for Angular/Meteor
        edge: 'right',
    });
    $('.collapsible').collapsible();
}

function scrollChatToBottom() {
    navchat = document.getElementById("navChat")
    navchat.scrollTop = navchat.scrollHeight;
}


function blazorGetTimezoneOffset() {
    return new Date().getTimezoneOffset();
}

function incrementValue(input)
{
    var value = parseInt(document.getElementById('betAmount').value, 10);
    value = isNaN(value) ? 0 : value;
    value+= input;
    document.getElementById('betAmount').value = value;
}
function multValue(input)
{
    var value = parseInt(document.getElementById('betAmount').value, 10);
    value = isNaN(value) ? 0 : value;
    value*= input;
    document.getElementById('betAmount').value = value;
}