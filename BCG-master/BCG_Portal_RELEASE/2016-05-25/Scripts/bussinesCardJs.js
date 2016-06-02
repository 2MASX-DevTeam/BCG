/******************** Flip card buttons *********************/
$("#FrontSide").click(function () {
    // console.log("toggleClass('flipped');");
    $('.card').removeClass('flipped');
});
$('#BackSide').click(function () {
    $('.card').addClass('flipped');
});
/******************** End Flip card buttons *********************/

/******************** Align text buttons *********************/
function function1() {
    document.all.natureBig.align = "center";
    document.all.intuneBig.align = "center";
    
}
function function2() {
    document.all.natureBig.align = "justify";
    document.all.intuneBig.align = "justify";
}
function function3() {
    document.all.natureBig.align = "left";
    document.all.intuneBig.align = "left";
}
function function4() {

    document.all.natureBig.align = "right";
    document.all.intuneBig.align = "right";

    //var offsetTobeDragged = $('.dragThis').offset();
    //var offsetContainer = $('#natureBig').offset();
    //var xPos = offsetContainer.left;
    //var yPos = offsetContainer.top;

    //console.log("natureBig offset => ");
    //console.log(offsetContainer);
    //console.log("dragThis offset => ");
    //console.log(offsetTobeDragged);

    // console.log($('.dragThis').offset.toString());
    //if (offsetContainer != offsetTobeDragged) {
    //    $('.dragThis').offset({ top: yPos, left: xPos })
    //    //offsetTobeDragged.left = xPos;
    //    //offsetTobeDragged.top = yPos;

    //    console.log("X =>" + offsetTobeDragged.left);
    //    console.log("y =>" + offsetTobeDragged.top);
    //    $('.dragThis').offset().left = xPos;
    //    $('.dragThis').offset().top = yPos;
    //    console.log($('.dragThis').offset() != offsetContainer);
    //}
    
}
/******************** End Align text buttons *********************/

/********************* Clear the contenteditable elements ****************/
$(document).mouseup(function (e) {

    /* Use this to enable futures upon many containes */
    var container = $(".dragThis");
    var flipped = $('#showCards .card');

    if (!container.is(e.target) // if the target of the click isn't the container...
        && container.has(e.target).length === 0) // ... nor a descendant of the container
    {
        container.css("border", "none");
        container.attr("contenteditable", "false");
    }
});
/********************* End Clear the contenteditable elements ****************/

/********************* Dragable and editable elements ****************/
//$('#showCards .card').click(function () {
//    $(this).removeClass('flipped');
//});


/* Enable field properties */
$(".dragThis").click(function () {
    $(this).attr("maxlength", "20");
    $(this).attr("contenteditable", "true");
    $(this).attr("type", "number");
    $(this).attr("draggable", "false");
    $(this).css("border", "1px dashed black");
    $(this).css("word-break", "break-all");
    /* Prevent new line if Enter is key pressed */
    $(this).keypress(function (e) {
        if (e.which == 13) {
            e.preventDefault();
        }
    });
});

/* Ensure the length of characters in the field */
$(".dragThis").keypress(function () {
    // console.log($(this).text().length)
    return $(this).text().length <= 46;
});

//$(".dragThis").dblclick(function () {
//    $(this).css("cursor", "e-resize");
//    $(this).attr("contenteditable", "false");


//});

//$(".dragThis").mouseleave(function () {
//    $(this).css("border", "none");
//    $(this).attr("contenteditable", "false");
//});

//$('.dragThis').draggable(
// {
//     containment: "parent",
//     drag: function (event, ui) {
//         var offset = $(this).offset();
//         var xPos = offset.left;
//         var yPos = offset.top;
//         $('#posX').text('x: ' + xPos);
//         $('#posY').text('y: ' + yPos);
//         // if(ui.position.top>21) { $('#dragThis').draggable("disable");
//         //  }
//         //if(ui.position.left>40) { ui.position.left = 0; }
//     }
// });


/********************* Dragable and editable elements ****************/