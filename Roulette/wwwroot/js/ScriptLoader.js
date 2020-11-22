window.GetCurrentPos = () => {

    var a = parseInt($('#case').css("background-position").split(" ")[0].slice(0, -2));
    if (a == null) {
        a = 0;
    }
    console.log("curpos = " + a);
    
    return a;
};


window.Animate = (currentPos, numWidth,moves) => {

    console.log("curpos = " + currentPos);
    console.log("numWidth = " + numWidth);
    console.log("moves = " + moves);
    $('#case').animate({
        "background-position": currentPos - 2100 - (moves * numWidth),
    }, 3000);
};


window.Resize = () =>{
    console.log("resized")
    resize();
}
