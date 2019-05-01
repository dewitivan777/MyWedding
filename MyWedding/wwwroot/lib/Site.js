//Iniciate WOW.js
new WOW().init();

//Smooth Scrolling
$('nav a').bind('click', function () {
    $('html, body').stop().animate({
        scrollTop: $($(this).attr('href')).offset().top
    }, 2000, 'easeInOutExpo');
    event.preventDefault();
})

// Activate Carousel
$("#myCarousel").carousel();

// Enable Carousel Indicators
$(".item").click(function () {
    $("#myCarousel").carousel(1);
});

// Enable Carousel Controls
$(".left").click(function () {
    $("#myCarousel").carousel("prev");
});


$('.collapse').collapse();

