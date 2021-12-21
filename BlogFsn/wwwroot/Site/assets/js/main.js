(function ($) {
    'use strict';

    // Page loading
    $(window).on('load', function () {
        $('#preloader-active').delay(450).fadeOut('slow');
        $('body').delay(450).css({
            'overflow': 'visible'
        });
    });

    // Scroll progress
    var scrollProgress = function () {
        var docHeight = $(document).height(),
            windowHeight = $(window).height(),
            scrollPercent;
        $(window).on('scroll', function () {
            scrollPercent = $(window).scrollTop() / (docHeight - windowHeight) * 100;
            $('.scroll-progress').width(scrollPercent + '%');
        });
    };

    // Off canvas sidebar
    var OffCanvas = function () {
        $('#off-canvas-toggle').on('click', function () {
            $('body').toggleClass("canvas-opened");
        });

        $('.dark-mark').on('click', function () {
            $('body').removeClass("canvas-opened");
        });
        $('.off-canvas-close').on('click', function () {
            $('body').removeClass("canvas-opened");
        });
    };

    // Search form
    var openSearchForm = function () {
        $('.search-close').hide();
        $('button.search-icon').on('click', function () {
            $(this).hide();
            $('body').toggleClass("open-search-form");
            $('.search-close').show();
            $("html, body").animate({ scrollTop: 0 }, "slow");
        });
        $('.search-close').on('click', function () {
            $(this).hide();
            $('body').removeClass("open-search-form");
            $('button.search-icon').show();
        });
        
    };

    // Mobile menu
    var mobileMenu = function () {
        var menu = $('ul#navigation');
        if (menu.length) {
            menu.slicknav({
                prependTo: ".mobile_menu",
                closedSymbol: '+',
                openedSymbol: '-'
            });
        };
    };

    // Slick slider
    var customSlickSlider = function () {
        // Featured slider 1
        $('.featured-slider-1-items').slick({
            dots: false,
            infinite: true,
            speed: 500,
            arrows: true,
            slidesToShow: 1,
            autoplay: false,
            loop: true,
            adaptiveHeight: true,
            fade: true,
            cssEase: 'linear',
            prevArrow: '<button type="button" class="slick-prev"><i class="flaticon-left"></i></button>',
            nextArrow: '<button type="button" class="slick-next"><i class="flaticon-right"></i></button>',
            appendArrows: '.arrow-cover',
        });
        // post-carausel-1-items
        $('.post-carausel-1-items').slick({
            dots: false,
            infinite: true,
            speed: 1000,
            arrows: true,
            slidesToShow: 4,
            slidesToScroll: 1,
            autoplay: true,
            loop: true,
            adaptiveHeight: true,
            cssEase: 'linear',
            prevArrow: '<button type="button" class="slick-prev"><i class="flaticon-left"></i></button>',
            nextArrow: '<button type="button" class="slick-next"><i class="flaticon-right"></i></button>',
            appendArrows: '.post-carausel-1-arrow',
            centerPadding: 50,
            responsive: [{
                breakpoint: 1024,
                settings: {
                    slidesToShow: 4,
                    slidesToScroll: 4,
                    infinite: true,
                    dots: false,
                }
            },
            {
                breakpoint: 991,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 3
                }
            },
            {
                breakpoint: 480,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 2
                }
            }
            ]
        });

        // post-carausel-2
        $('.post-carausel-2').slick({
            dots: true,
            infinite: true,
            speed: 1000,
            arrows: false,
            slidesToShow: 4,
            slidesToScroll: 1,
            autoplay: false,
            loop: true,
            adaptiveHeight: true,
            cssEase: 'linear',
            centerPadding: 50,
            responsive: [{
                breakpoint: 1024,
                settings: {
                    slidesToShow: 4,
                    slidesToScroll: 4,
                    infinite: true,
                    dots: false,
                }
            },
            {
                breakpoint: 991,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 3
                }
            },
            {
                breakpoint: 480,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
            }
            ]
        });

        // post-carausel-3
        $('.post-carausel-3').slick({
            dots: true,
            infinite: true,
            speed: 1000,
            arrows: false,
            slidesToShow: 4,
            slidesToScroll: 1,
            autoplay: true,
            loop: true,
            adaptiveHeight: true,
            cssEase: 'linear',
            centerPadding: 50,
            responsive: [{
                breakpoint: 1024,
                settings: {
                    slidesToShow: 4,
                    slidesToScroll: 4,
                    infinite: true,
                    dots: false,
                }
            },
            {
                breakpoint: 991,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 1
                }
            },
            {
                breakpoint: 480,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
            }
            ]
        });

        $('.featured-slider-2-items').slick({
            fade: true,
            asNavFor: '.featured-slider-2-nav',
            arrows: true,
            prevArrow: '<button type="button" class="slick-prev"><i class="flaticon-left"></i></button>',
            nextArrow: '<button type="button" class="slick-next"><i class="flaticon-right"></i></button>',
            appendArrows: '.arrow-cover',
        });

        $('.featured-slider-2-nav').slick({
            slidesToShow: 4,
            slidesToScroll: 1,
            asNavFor: '.featured-slider-2-items',
            dots: false,
            arrows: false,
            centerMode: true,
            focusOnSelect: true,
            centerPadding: 0,
            responsive: [{
                breakpoint: 1024,
                settings: {
                    slidesToShow: 3,
                }
            },
            {
                breakpoint: 991,
                settings: {
                    slidesToShow: 2,
                }
            },
            {
                breakpoint: 480,
                settings: {
                    slidesToShow: 1,
                }
            }
            ]
        });
    };

    // Nice Select
    var niceSelectBox = function () {
        var nice_Select = $('select');
        if (nice_Select.length) {
            nice_Select.niceSelect();
        }
    };

    //Header sticky
    var headerSticky = function () {
        $(window).on('scroll', function () {
            var scroll = $(window).scrollTop();
            if (scroll < 245) {
                $(".header-sticky ").removeClass("sticky-bar");
            } else {
                $(".header-sticky").addClass("sticky-bar");
            }
        });
    };

    // Scroll up to top
    var scrollToTop = function () {
        $.scrollUp({
            scrollName: 'scrollUp', // Element ID
            topDistance: '300', // Distance from top before showing element (px)
            topSpeed: 300, // Speed back to top (ms)
            animation: 'fade', // Fade, slide, none
            animationInSpeed: 200, // Animation in speed (ms)
            animationOutSpeed: 200, // Animation out speed (ms)
            scrollText: '<i class="ti-arrow-up"></i>', // Text for element
            activeOverlay: false, // Set CSS color to display scrollUp active point, e.g '#00FFFF'
        });
    };

    //VSticker
    var VSticker = function () {
        $('#datetime').vTicker({
            speed: 500,
            pause: 2000,
            animation: 'fade',
            mousePause: false,
            showItems: 1
        });
        $('#news-flash').vTicker({
            speed: 500,
            pause: 2000,
            animation: 'fade',
            mousePause: false,
            showItems: 1
        });
    };

    //sidebar sticky
    var stickySidebar = function () {
        $('.sticky-sidebar').theiaStickySidebar();
    };

    //Custom scrollbar
    var customScrollbar = function () {
        var $ = document.querySelector.bind(document);
        var ps = new PerfectScrollbar('.custom-scrollbar');
    };

    //Mega menu
    var megaMenu = function () {
        $('.sub-mega-menu .nav-pills > a').on('mouseover', function (event) {
            $(this).tab('show');
        });
    };

    //Counter
    var numberCounter = function () {
        $('.counter-number').counterUp({
            delay: 10,
            time: 2000
        });
    };

    // Slider Range JS
    var sliderRange = function () {
        if ( $("#slider-range").length ) {
            $("#slider-range").slider({
                range: true,
                min: 0,
                max: 500,
                values: [120, 250],
                slide: function (event, ui) {
                    $("#amount").val("$" + ui.values[0] + " - $" + ui.values[1]);
                }
            });
            $("#amount").val("$" + $("#slider-range").slider("values", 0) +
                " - $" + $("#slider-range").slider("values", 1));
        }
    };

    //magnific Popup
    var magPopup = function () {
        $('.play-video').magnificPopup({
            disableOn: 700,
            type: 'iframe',
            mainClass: 'mfp-fade',
            removalDelay: 160,
            preloader: false,
            fixedContentPos: false
        });
    };

    //Shop Grid/List view
    /* ===== Grid list active  =====   */
    var gridListActive = function () {

        $('.btn-list-grid button').on('click', function () {
            if ($(this).hasClass('grid-view')) {
                $('.btn-list-grid button').addClass('active');
                $('.btn-list-grid button.list-view').removeClass('active');
            }
            else if ($(this).hasClass('list-view')) {
                $('.btn-list-grid button').addClass('active');
                $('.btn-list-grid button.grid-view').removeClass('active');
            }
        });
    };
    /* Grid list View */
    var gridListView = function () {
        // Product List
        $('#list-view').on('click', function () {
            $('.product-layout > .clearfix').remove();
            $('.product-layout').attr('class', 'product-layout product-list col-md-12');
            $('#column-left .product-layout').attr('class', 'product-layout mb_20');
            $('#column-right .product-layout').attr('class', 'product-layout mb_20');

        });
        // Product Grid
        $('#grid-view').on('click', function () {
            $('.product-layout').attr('class', 'product-layout product-grid col-md-4 col-xs-6');           
        });
    };

    /*Product Details*/
    var productDetails = function () {
        $('.product-image-slider').slick({
            slidesToShow: 1,
            slidesToScroll: 1,
            arrows: false,
            fade: false,
            asNavFor: '.slider-nav-thumbnails',
        });

        $('.slider-nav-thumbnails').slick({
            slidesToShow: 5,
            slidesToScroll: 1,
            asNavFor: '.product-image-slider',
            dots: false,
            focusOnSelect: true,
            prevArrow: '<button type="button" class="slick-prev"><i class="ti-angle-left"></i></button>',
            nextArrow: '<button type="button" class="slick-next"><i class="ti-angle-right"></i></button>'
        });

        // Remove active class from all thumbnail slides
        $('.slider-nav-thumbnails .slick-slide').removeClass('slick-active');

        // Set active class to first thumbnail slides
        $('.slider-nav-thumbnails .slick-slide').eq(0).addClass('slick-active');

        // On before slide change match active thumbnail to current slide
        $('.product-image-slider').on('beforeChange', function (event, slick, currentSlide, nextSlide) {
            var mySlideNumber = nextSlide;
            $('.slider-nav-thumbnails .slick-slide').removeClass('slick-active');
            $('.slider-nav-thumbnails .slick-slide').eq(mySlideNumber).addClass('slick-active');
        });

        $('.product-image-slider').on('beforeChange', function (event, slick, currentSlide, nextSlide) {
            var img = $(slick.$slides[nextSlide]).find("img");
            $('.zoomWindowContainer,.zoomContainer').remove();
            $(img).elevateZoom({
                zoomType: "inner",
                cursor: "crosshair",
                zoomWindowFadeIn: 500,
                zoomWindowFadeOut: 750
            });
        });
        //Elevate Zoom
        if ( $(".product-image-slider").length ) {
            $('.product-image-slider .slick-active img').elevateZoom({
                zoomType: "inner",
                cursor: "crosshair",
                zoomWindowFadeIn: 500,
                zoomWindowFadeOut: 750
            });
        }
        //Filter color/Size
        $('.list-filter').each(function () {
            $(this).find('a').on('click', function (event) {
                event.preventDefault();
                $(this).parent().siblings().removeClass('active');
                $(this).parent().toggleClass('active');
                $(this).parents('.attr-detail').find('.current-size').text($(this).text());
                $(this).parents('.attr-detail').find('.current-color').text($(this).attr('data-color'));
            });
        });
        //Qty Up-Down
        $('.detail-qty').each(function () {
            var qtyval = parseInt($(this).find('.qty-val').text(), 10);
            $('.qty-up').on('click', function (event) {
                event.preventDefault();
                qtyval = qtyval + 1;
                $(this).prev().text(qtyval);
            });
            $('.qty-down').on('click', function (event) {
                event.preventDefault();
                qtyval = qtyval - 1;
                if (qtyval > 1) {
                    $(this).next().text(qtyval);
                } else {
                    qtyval = 1;
                    $(this).next().text(qtyval);
                }
            });
        });

        $('.dropdown-menu .cart_list').click(function(e) {
            e.stopPropagation();
        });
    };
    /* WOW active */
    new WOW().init();

    //Load functions
    $(document).ready(function () {
        OffCanvas();
        customScrollbar();
        numberCounter();
        megaMenu();
        magPopup();
        scrollToTop();
        headerSticky();
        stickySidebar();
        customSlickSlider();
        mobileMenu();
        scrollProgress();
        niceSelectBox();
        openSearchForm();
        VSticker();
        sliderRange();
        gridListActive();
        gridListView();
        productDetails();
    });

})(jQuery);