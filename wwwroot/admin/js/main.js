(function ($) {

    "use strict";

    $(function () {
        //offcanvas
        $('[data-toggle="offcanvas"]').on("click", function () {
            $('.sidebar-offcanvas').toggleClass('active')
        });

        $(".nav-settings").click(function () {
            $("#right-sidebar").toggleClass("open");
        });
        $(".settings-close").click(function () {
            $("#right-sidebar,#theme-settings").removeClass("open");
        });

        $("#settings-trigger").on("click", function () {
            $("#theme-settings").toggleClass("open");
        });
        
        //Horizontal menu in mobile
        $('[data-toggle="horizontal-menu-toggle"]').on("click", function () {
            $(".horizontal-menu .bottom-navbar").toggleClass("header-toggled");
        });
        // Horizontal menu navigation in mobile menu on click
        var navItemClicked = $('.horizontal-menu .page-navigation >.nav-item');
        navItemClicked.on("click", function (event) {
            if (window.matchMedia('(max-width: 991px)').matches) {
                if (!($(this).hasClass('show-submenu'))) {
                    navItemClicked.removeClass('show-submenu');
                }
                $(this).toggleClass('show-submenu');
            }
        });

        $(window).scroll(function () {
            if (window.matchMedia('(min-width: 992px)').matches) {
                var header = $('.horizontal-menu');
                if ($(window).scrollTop() >= 71) {
                    $(header).addClass('fixed-on-scroll');
                } else {
                    $(header).removeClass('fixed-on-scroll');
                }
            }
        });

        //Upload Image
        var names = [];
        $('body').on('change', '.upload__inputfile', function (event) {
            var getAttr = $(this).attr('click-type');
            var files = event.target.files;
            if (getAttr == 'type1') {
                $('.upload__img-wrap').html('');
                $('.upload__img-wrap').html(
                    `<div class="upload__img-box">
                    <label class="plus__btn">                   
                        <div style="background-image: url('images/plus.png')" class="img-bg"></div>
                        <input type="file" click-type="type2" multiple="" class="upload__inputfile">                    
                    </label>
                </div>`,
                );
                $('#hint_brand').modal('show');

                for (var i = 0; i < files.length; i++) {
                    var file = files[i];
                    names.push($(this).get(0).files[i].name);
                    var picReader = new FileReader();
                    picReader.fileName = file.name;
                    picReader.addEventListener('load', function (event) {
                        var picFile = event.target;

                        var html = `<div class="upload__img-box">
                            <div style="background-image: url('${picFile.result}')" class="img-bg">                              
                                <a href="javascript:void(0);" data-id="${picFile.fileName}" class="upload__img-close"></a>                              
                            </div>
                        </div>`;

                        $('.upload__img-wrap').append(html);
                    });
                    picReader.readAsDataURL(file);
                }
                console.log(names);
            } else if (getAttr == 'type2') {
                for (var i = 0; i < files.length; i++) {
                    var file = files[i];
                    names.push($(this).get(0).files[i].name);
                    var picReader = new FileReader();
                    picReader.fileName = file.name;
                    picReader.addEventListener('load', function (event) {
                        var picFile = event.target;

                        var html = `<div class="upload__img-box">
                            <div style="background-image: url('${picFile.result}')" class="img-bg">                               
                                <a href="javascript:void(0);" data-id="${picFile.fileName}" class="upload__img-close"></a>                              
                            </div>
                        </div>`;

                        $('.upload__img-wrap').append(html);
                    });
                    picReader.readAsDataURL(file);
                }
                // return array of file name
                console.log(names);
            }
        });

        $('body').on('click', '.upload__img-close', function () {
            $(this).parent().parent().remove();
            var removeItem = $(this).attr('data-id');
            var yet = names.indexOf(removeItem);

            if (yet != -1) {
                names.splice(yet, 1);
            }
            // return array of file name
            console.log(names);
        });
        $('#hint_brand').on('hidden.bs.modal', function (e) {
            names = [];
            z = 0;
        });
    });

    //Compact sidebar mode
    $('[data-toggle="minimize"]').on("click", function () {
        var body = $('body');
        if ((body.hasClass('sidebar-toggle-display')) || (body.hasClass('sidebar-fixed'))) {
            body.toggleClass('sidebar-hidden');
        } else {
            body.toggleClass('sidebar-icon-only');
        }
    });

    //Open submenu on hover in compact sidebar mode and horizontal menu mode
    $(document).on('mouseenter mouseleave', '.sidebar .nav-item', function (ev) {
        var body = $('body');
        var sidebarIconOnly = body.hasClass("sidebar-icon-only");
        var sidebarFixed = body.hasClass("sidebar-fixed");
        if (!('ontouchstart' in document.documentElement)) {
            if (sidebarIconOnly) {
                if (sidebarFixed) {
                    if (ev.type === 'mouseenter') {
                        body.removeClass('sidebar-icon-only');
                    }
                } else {
                    var $menuItem = $(this);
                    if (ev.type === 'mouseenter') {
                        $menuItem.addClass('hover-open')
                    } else {
                        $menuItem.removeClass('hover-open')
                    }
                }
            }
        }
    });

})(window.jQuery);
