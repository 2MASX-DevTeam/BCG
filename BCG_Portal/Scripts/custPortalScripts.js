(function ($) {
    $(document).ready(function () {
        // Cache the elements we'll need
        var menu = $("#responsive-navbar");
        var menuList = menu.find("ul:first");
        var listItems = menu.find("li").not("#responsive-tab");

        // Create responsive trigger
        menuList.prepend('<li id="responsive-tab"><a href="#">Menu</a></li>');

        // Toggle menu visibility
        menu.on('click', '#responsive-tab', function () {
            listItems.slideToggle('fast');
            listItems.addClass('collapsed');
        });
    });
});

/**************Animation Banner*********************/

$(function () {
    //we want 5 steps / slides for the first banner
    //let's define what happens for each one:

    /*
    the variable steps has the following structure:
    steps:[step1,step2,...,stepN]
    On each step/transition we define the transition for each one of the areas in the banner
    For the first banner for example, we have 3 areas, each one with a specific number
    of images (the images inside of the "ca_wrap" div). One image is shown as default,
    which for our examples is always the first one (1).
    So as an example, our first step/transition can be:
    [{"to" : "2"}, {"effect": "zoomOutRotated-zoomInRotated"}],
    [{"to" : "1"}, {}],
    [{"to" : "2"}, {"effect": "slideOutRight-slideInRight"}]
    This means that for the first transition, the first and third areas are going to show
    their second image, and the second area remains untouched. When we change each image we
    apply a specific effect, which is defined in the plugin.
    */
    $('#main_banner').banner({
        steps: [
            [
                //1 step:
                [{ "to": "2" }, { "effect": "zoomOutRotated-zoomInRotated" }],
                [{ "to": "1" }, {}],
                [{ "to": "2" }, { "effect": "slideOutRight-slideInRight" }]
            ],
            [
                //2 step:
                [{ "to": "3" }, { "effect": "slideOutTop-slideInTop" }],
                [{ "to": "1" }, {}],
                [{ "to": "2" }, {}]
            ],
            [
                //3 step:
                [{ "to": "4" }, { "effect": "zoomOut-zoomIn" }],
                [{ "to": "2" }, { "effect": "slideOutRight-slideInRight" }],
                [{ "to": "2" }, {}]
            ],
            [
                //4 step
                [{ "to": "5" }, { "effect": "slideOutBottom-slideInTop" }],
                [{ "to": "2" }, {}],
                [{ "to": "3" }, { "effect": "zoomOut-zoomIn" }]
            ],
            [
                //5 step
                [{ "to": "1" }, { "effect": "slideOutLeft-slideInLeft" }],
                [{ "to": "1" }, { "effect": "zoomOut-zoomIn" }],
                [{ "to": "1" }, { "effect": "slideOutRight-slideInRight" }]
            ]
        ],
        total_steps: 5,
        speed: 6000
    });

    //we want 4 steps/slides for the second banner:
    $('#aside_banner').banner({
        steps: [
            [
                //1 step:
                [{ "to": "2" }, { "effect": "slideOutTop-slideInTop" }],
                [{ "to": "2" }, { "effect": "slideOutTop-slideInTop" }]
            ],
            [
                //2 step:
                [{ "to": "1" }, { "effect": "slideOutRight-slideInRight" }],
                [{ "to": "1" }, { "effect": "slideOutLeft-slideInLeft" }],
            ],
            [
                //3 step:
                [{ "to": "2" }, { "effect": "slideOutLeft-slideInLeft" }],
                [{ "to": "2" }, { "effect": "slideOutRight-slideInRight" }]
            ],
            [
                //4 step:
                [{ "to": "1" }, { "effect": "zoomOutRotated-zoomInRotated" }],
                [{ "to": "1" }, { "effect": "zoomOutRotated-zoomInRotated" }],
            ]
        ],
        total_steps: 4,
        speed: 6000
    });
});



/**************Animation Portfolio*********************/


    $(function() {
        //index of current item
        var current				= 0;
        //speeds / ease type for animations
        var fadeSpeed			= 400;
        var animSpeed			= 600;
        var easeType			= 'easeOutCirc';
        //caching
        var $thumbScroller		= $('#thumbScroller');
        var $scrollerContainer	= $thumbScroller.find('.container');
        var $scrollerContent	= $thumbScroller.find('.content');
        var $pg_title 			= $('#pg_title');
        var $pg_preview 		= $('#pg_preview');
        var $pg_desc1 			= $('#pg_desc1');
        var $pg_desc2 			= $('#pg_desc2');
        var $overlay			= $('#overlay');
        //number of items
        var scrollerContentCnt  = $scrollerContent.length;
        var sliderHeight		= $(window).height();
        //we will store the total height
        //of the scroller container in this variable
        var totalContent		= 0;
        //one items height
        var itemHeight			= 0;
				
        //First let's create the scrollable container,
        //after all its images are loaded
        var cnt		= 0;
        $thumbScroller.find('img').each(function(){
            var $img 	= $(this);
            $('<img/>').load(function(){
                ++cnt;
                if(cnt == scrollerContentCnt){
                    //one items height
                    itemHeight = $thumbScroller.find('.content:first').height();
                    buildScrollableItems();
                    //show the scrollable container
                    $thumbScroller.stop().animate({'left':'0px'},animSpeed);
                }
            }).attr('src',$img.attr('src'));
        });
				
        //when we click an item from the scrollable container
        //we want to display the items content
        //we use the index of the item in the scrollable container
        //to know which title / image / descriptions we will show
        $scrollerContent.bind('click',function(e){
            var $this 				= $(this);
					
            var idx 				= $this.index();
            //if we click on the one shown then return
            if(current==idx) return;
					
            //if the current image is enlarged,
            //then we will remove it but before
            //we animate it just like we would do with the thumb
            var $pg_large			= $('#pg_large');
            if($pg_large.length > 0){
                $pg_large.animate({'left':'350px','opacity':'0'},animSpeed,function(){
                    $pg_large.remove();
                });
            }
					
            //get the current and clicked items elements
            var $currentTitle 		= $pg_title.find('h1:nth-child('+(current+1)+')');
            var $nextTitle 			= $pg_title.find('h1:nth-child('+(idx+1)+')');
            var $currentThumb		= $pg_preview.find('img.pg_thumb:eq('+current+')');
            var $nextThumb			= $pg_preview.find('img.pg_thumb:eq('+idx+')');
            var $currentDesc1 		= $pg_desc1.find('div:nth-child('+(current+1)+')');
            var $nextDesc1 			= $pg_desc1.find('div:nth-child('+(idx+1)+')');
            var $currentDesc2 		= $pg_desc2.find('div:nth-child('+(current+1)+')');
            var $nextDesc2 			= $pg_desc2.find('div:nth-child('+(idx+1)+')');
					
            //the new current is now the index of the clicked scrollable item
            current		 			= idx;
					
            //animate the current title up,
            //hide it, and animate the next one down
            $currentTitle.stop().animate({'top':'-50px'},animSpeed,function(){
                $(this).hide();
                $nextTitle.show().stop().animate({'top':'25px'},animSpeed);
            });
					
            //show the next image,
            //animate the current to the left and fade it out
            //so that the next gets visible
            $nextThumb.show();
            $currentThumb.stop().animate({'left': '350px','opacity':'0'},animSpeed,function(){
                $(this).hide().css({
                    'left'		: '250px',
                    'opacity'	: 1,
                    'z-index'	: 1
                });
                $nextThumb.css({'z-index':9999});
            });
					
            //animate both current descriptions left / right and fade them out
            //fade in and animate the next ones right / left
            $currentDesc1.stop().animate({'left':'205px','opacity':'0'},animSpeed,function(){
                $(this).hide();
                $nextDesc1.show().stop().animate({'left':'250px','opacity':'1'},animSpeed);
            });
            $currentDesc2.stop().animate({'left':'295px','opacity':'0'},animSpeed,function(){
                $(this).hide();
                $nextDesc2.show().stop().animate({'left':'250px','opacity':'1'},animSpeed);
            });
            e.preventDefault();
        });
				
        //when we click a thumb, the thumb gets enlarged,
        //to the sizes of the large image (fixed values).
        //then we load the large image, and insert it after
        //the thumb. After that we hide the thumb so that
        //the large one gets displayed
        $pg_preview.find('.pg_thumb').bind('click',showLargeImage);
				
        //enlarges the thumb
        function showLargeImage(){
            //if theres a large one remove
            $('#pg_large').remove();
            var $thumb 		= $(this);
            $thumb.unbind('click');
            var large_src 	= $thumb.attr('alt');

            $overlay.fadeIn(200);
            //animate width to 600px,height to 500px
            $thumb.stop().animate({
                'width'	: '600px',
                'height': '500px'
            },500,function(){
                $('<img id="pg_large"/>').load(function(){
                    var $largeImg = $(this);
                    $largeImg.insertAfter($thumb).show();
                    $thumb.hide().css({
                        'left'		: '250px',
                        'opacity'	: 1,
                        'z-index'	: 1,
                        'width'		: '360px',
                        'height'	: '300px'
                    });
                    //when we click the large image
                    //we revert the animation
                    $largeImg.bind('click',function(){
                        $thumb.show();
                        $overlay.fadeOut(200);
                        $(this).stop().animate({
                            'width'	: '360px',
                            'height': '300px'
                        },500,function(){
                            $(this).remove();
                            $thumb.css({'z-index'	: 9999});
                            //bind again the click event
                            $thumb.bind('click',showLargeImage);
                        });
								
                    });
                }).attr('src',large_src);
            });
        }
				
        //resize window event:
        //the scroller container needs to update
        //its height based on the new windows height
        $(window).resize(function() {
            var w_h			= $(window).height();
            $thumbScroller.css('height',w_h);
            sliderHeight	= w_h;
        });
				
        //create the scrollable container
        //taken from Manos :
        //http://manos.malihu.gr/jquery-thumbnail-scroller
        function buildScrollableItems(){
            totalContent = (scrollerContentCnt-1)*itemHeight;
            $thumbScroller.css('height',sliderHeight)
            .mousemove(function(e){
                if($scrollerContainer.height()>sliderHeight){
                    var mouseCoords		= (e.pageY - this.offsetTop);
                    var mousePercentY	= mouseCoords/sliderHeight;
                    var destY			= -(((totalContent-(sliderHeight-itemHeight))-sliderHeight)*(mousePercentY));
                    var thePosA			= mouseCoords-destY;
                    var thePosB			= destY-mouseCoords;
                    if(mouseCoords==destY)
                        $scrollerContainer.stop();
                    else if(mouseCoords>destY)
                        $scrollerContainer.stop()
                    .animate({
                        top: -thePosA
                    },
                    animSpeed,
                    easeType);
                    else if(mouseCoords<destY)
                        $scrollerContainer.stop()
                    .animate({
                        top: thePosB
                    },
                    animSpeed,
                    easeType);
                }
            }).find('.thumb')
            .fadeTo(fadeSpeed, 0.6)
            .hover(
            function(){ //mouse over
                $(this).fadeTo(fadeSpeed, 1);
            },
            function(){ //mouse out
                $(this).fadeTo(fadeSpeed, 0.6);
            }
        );
        }
    });