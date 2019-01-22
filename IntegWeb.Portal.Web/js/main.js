//script incluido para atualização cadastral

var transparent = true;

var fixedTop = false;

var navbar_initialized = false;

var scroll;

scroll = (2500 - $(window).width()) / $(window).width();

var window_height;
var window_width;

var content_opacity = 0;
var content_transition = 0;
var no_touch_screen = false;

var scroll_distance = 500;

$(document).ready(function () {

    $('.ddd').mask('00');
    $('.phone').mask('0000-0000');
    $('.celphone').mask('00000-0000');
    $('.cpf').mask('000.000.000-00');
    $('.date').mask('00/00/0000');


    $('#btnValidarLogin').click(step2)
    $('.step_1 .error a').click(hideStep1Error)
    $('#btnAtualizarCadastro').click(finish)

    function step2() {

        console.log('step2()')

        var cpf = $('#txtCpf').val()
        // console.log(cpf)
        var cpfIsValid = validateCPF(cpf)
        // console.log(cpfIsValid)
        if (!cpfIsValid) {
            console.log('if(!cpfIsValid){')
            showStep1Error('CPF ou Data de nascimento incorretos')
            return false;
        }

        var cpfIsUpdated = updatedCPF(cpf)
        if (cpfIsUpdated) {
            console.log('if(cpfIsUpdated){')
            showStep1Error('Já foi realizada a atualização para este CPF.')
            return false;
        }

        var birthday = $('#txtDtNascimento').val()
        // console.log('birthday: ', birthday)
        var birthdayIsValid = validateBirthday(birthday)
        // console.log(birthdayIsValid)
        if (!birthdayIsValid) {
            console.log('if(!birthdayIsValid){')
            showStep1Error('CPF ou Data de nascimento incorretos')
            return false;
        }

        cpf = cpf.split('.').join('').split('-').join('');

        //Para verificar o formato válido para data
        if (new Date('31/01/2018') == 'Invalid Date') //Se for true, então o formato correto é mm/dd/yyyy
        {
            var dataBirthday = birthday.split('/');
            if (birthday.length > 0) birthday = dataBirthday[1] + '/' + dataBirthday[0] + '/' + dataBirthday[2];
        }

        var obj = {};
        obj.cpf = cpf;
        obj.nascimento = birthday;
                
        startLoading();

        $('#btnValidarLogin').prop('disabled', true);
        $('#btnValidarLogin').val('Verificando...');

        $.ajax({
            type: "POST",
            url: "AtualizacaoCadastralLogin.aspx/ValidaLogin",
            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                $('.step_1').hide();
                $('.img_header_step_1').show();
                $('.step_2').show();
            },
            error: function (erro) {
                showStep1Error(erro.responseJSON.Message)
            },
            complete: function (msg) {
                $.mobile.loading("hide");
                $('#btnValidarLogin').prop('disabled', false);
                $('#btnValidarLogin').val('Próximo');
            }
        });
    }

    function startLoading() {
        var $this = $(this),
        theme = $this.jqmData("theme") || $.mobile.loader.prototype.options.theme,
        msgText = $this.jqmData("msgtext") || $.mobile.loader.prototype.options.text,
        textVisible = $this.jqmData("textvisible") || $.mobile.loader.prototype.options.textVisible,
        textonly = !!$this.jqmData("textonly");
        html = $this.jqmData("html") || "";
        $.mobile.loading('show', {
            text: msgText,
            textVisible: textVisible,
            theme: theme,
            textonly: textonly,
            html: html
        });
    }

    function finish() {
        console.log('finish()')

        hideStep2Error()

        var ddd_cel = $('#txtDDDCelular').val();
        var cel = $('#txtCelular').val();
        // console.log(cpf)
        var celIsValid = validateCel(ddd_cel, cel)
        // console.log(celIsValid)
        if (!celIsValid) {
            console.log('if(!celIsValid){')
            showStep2Error('DDD ou Número do celular estão incorretos')
            return false;
        }

        var ddd_tel = $('#txtDDDTelefone').val();
        var tel = $('#txtTelefone').val();
        // console.log(cpf)
        var telIsValid = validateTel(ddd_tel, tel)
        // console.log(telIsValid)
        if (!telIsValid) {
            console.log('if(!telIsValid){')
            showStep2Error('DDD ou Númedo do telefone estão incorretos')
            return false;
        }

        var email = $('#txtEmail').val()
        var emailIsValid = validateEmail(email)
        $('.form-group-email').removeClass('w_error')
        if (!emailIsValid) {
            console.log('if(!emailIsValid){')
            showStep2Error('Email incorreto')
            $('.form-group-email').addClass('w_error')
            return false;
        }

        var confirmemail = $('#txtEmailConfirmacao').val()
        var confirmEmailIsValid = validateEmail(confirmemail)
        $('.form-group-confirm-email').removeClass('w_error')
        if (!confirmEmailIsValid) {
            console.log('if(!confirmEmailIsValid){')
            showStep2Error('Email de confirmação incorreto')
            $('.form-group-confirm-email').addClass('w_error')
            return false;
        }
        if (confirmemail !== email) {
            console.log('if(!confirmEmailIsValid){')
            showStep2Error('O campo de confirmação deve ser igual ao campo email pessoal')
            $('.form-group-confirm-email').addClass('w_error')
            return false;
        }

        var cpf = $('#txtCpf').val();
        var birthday = $('#txtDtNascimento').val();

        cpf = cpf.split('.').join('').split('-').join('');

        //Para verificar o formato válido para data
        if (new Date('31/01/2018') == 'Invalid Date') //Se for true, então o formato correto é mm/dd/yyyy
        {
            var dataBirthday = birthday.split('/');
            if (birthday.length > 0) birthday = dataBirthday[1] + '/' + dataBirthday[0] + '/' + dataBirthday[2];
        }

        var obj = {};
        obj.cpf = cpf;
        obj.nascimento = birthday;
        obj.dddTelefone = ddd_tel;
        obj.telefone = tel.replace('-', '');
        obj.dddCelular = ddd_cel;
        obj.celular = cel.replace('-', '');
        obj.email = email;

        startLoading();

        $('#btnAtualizarCadastro').prop('disabled', true);
        $('#btnAtualizarCadastro').val('Atualizando as informações...');

        $.ajax({
            type: "POST",
            url: "AtualizacaoCadastralLogin.aspx/AtualizarCadastro",
            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                $('.step_2').hide();
                $('.finish').show();
                $('.img_header_step_2').show();
            },
            error: function (erro) {
                showStep2Error(erro.responseJSON.Message)
            },
            complete: function (msg) {
                $.mobile.loading("hide");
                $('#btnAtualizarCadastro').prop('disabled', false);
                $('#btnAtualizarCadastro').val('Próximo');
            }
        });
    }

    function showStep2Error(msg) {
        console.log(msg)
        $('.step_2 .error-block span').html(msg)
        $('.step_2 .error-block').show()
    }

    function hideStep2Error() {
        $('.step_2 .error-block span').html('')
        $('.step_2 .error-block').hide()
    }

    function showStep1Error(msg) {
        console.log(msg)
        $('.step_1 .error p').html(msg)
        $('.step_1 .form').hide();
        $('.step_1 .error').show();
    }

    function hideStep1Error() {
        $('.step_1 .error p').html('')
        $('.step_1 .form').show();
        $('.step_1 .error').hide();
    }

    function validateCel(ddd, number) {
        $('.form-group-cel').removeClass('w_error')
        if (
            ddd === ''
            ||
            number === ''
            ||
            ddd === undefined
            ||
            number === undefined
            ||
            ddd.length !== 2
            ||
            number.length !== 10
          ) {
            $('.form-group-cel').addClass('w_error')
            return false
        }
        return true
    }

    function validateTel(ddd, number) {
        $('.form-group-tel').removeClass('w_error')
        if (ddd === '' && number === '') {
            return true
        }
        if (ddd === undefined && number === undefined) {
            return true
        }
        if (
            ddd !== number
            &&
            (
              ddd.length !== 2
              ||
              number.length !== 9
            )
          ) {
            $('.form-group-tel').addClass('w_error')
            return false
        }
        return true
    }

    function validateBirthday(birthday) {
        if (birthday === '' || birthday === undefined || birthday.length != 10) {
            return false;
        }

        var dataBirthday = birthday.split('/');
        if (new Date(birthday = dataBirthday[0] + '/' + dataBirthday[1] + '/' + dataBirthday[2]) == 'Invalid Date'
            && new Date(birthday = dataBirthday[1] + '/' + dataBirthday[0] + '/' + dataBirthday[2]) == 'Invalid Date')
            return false;

        return true;
    }

    function updatedCPF(strCPF) {
        return false;
        if (Math.random() >= .5) {
            return true;
        } else {
            return false;
        }
    }

    function validateCPF(strCPF) {
        // console.log(strCPF)
        strCPF = strCPF.split('.').join('').split('-').join('')
        // console.log(strCPF)
        var Soma;
        var Resto;
        Soma = 0;
        if (strCPF == "00000000000") return false;
        if (strCPF == "11111111111") return false;
        if (strCPF == "22222222222") return false;
        if (strCPF == "33333333333") return false;
        if (strCPF == "44444444444") return false;
        if (strCPF == "55555555555") return false;
        if (strCPF == "66666666666") return false;
        if (strCPF == "77777777777") return false;
        if (strCPF == "88888888888") return false;
        if (strCPF == "99999999999") return false;

        for (i = 1; i <= 9; i++) Soma = Soma + parseInt(strCPF.substring(i - 1, i)) * (11 - i);
        Resto = (Soma * 10) % 11;

        if ((Resto == 10) || (Resto == 11)) Resto = 0;
        if (Resto != parseInt(strCPF.substring(9, 10))) return false;

        Soma = 0;
        for (i = 1; i <= 10; i++) Soma = Soma + parseInt(strCPF.substring(i - 1, i)) * (12 - i);
        Resto = (Soma * 10) % 11;

        if ((Resto == 10) || (Resto == 11)) Resto = 0;
        if (Resto != parseInt(strCPF.substring(10, 11))) return false;
        return true;
    }

    function validateEmail(email) {
        var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(String(email).toLowerCase());
    }

    if ($(window).width() >= 990) {
        $('.div_form').height($(document).height() - 100)
    }
    
    BrowserDetect.init();

    if (BrowserDetect.browser == 'Explorer' && BrowserDetect.version <= 9) {
        $('body').html(better_browser);
    }

    window_width = $(window).width();
    window_height = $(window).height();

    burger_menu = $('.navbar').hasClass('navbar-burger') ? true : false;

    if (!Modernizr.touch) {
        $('body').addClass('no-touch');
        no_touch_screen = true;
    }

    // Init navigation toggle for small screens
    if (window_width < 992 || burger_menu) {
        gaia.initRightMenu();
    }

    if ($('.content-with-opacity').length != 0) {
        content_opacity = 1;
    }

    $navbar = $('.navbar[color-on-scroll]');
    scroll_distance = $navbar.attr('color-on-scroll') || 500;

    $('.google-map').each(function () {
        var lng = $(this).data('lng');
        var lat = $(this).data('lat');

        gaia.initGoogleMaps(this, lat, lng);
    });
});

//activate collapse right menu when the windows is resized
$(window).resize(function () {
    if ($(window).width() < 992) {
        gaia.initRightMenu();
        //gaia.checkResponsiveImage();
    }
    if ($(window).width() > 992 && !burger_menu) {
        $('nav[role="navigation"]').removeClass('navbar-burger');
        gaia.misc.navbar_menu_visible = 1;
        navbar_initialized = false;
    }
});

$(window).on('scroll', function () {

    gaia.checkScrollForTransparentNavbar();


    if (window_width > 992) {
        gaia.checkScrollForParallax();
    }

    if (content_opacity == 1) {
        gaia.checkScrollForContentTransitions();
    }

});

$('a[data-scroll="true"]').click(function (e) {
    var scroll_target = $(this).data('id');
    var scroll_trigger = $(this).data('scroll');

    if (scroll_trigger == true && scroll_target !== undefined) {
        e.preventDefault();

        $('html, body').animate({
            scrollTop: $(scroll_target).offset().top - 50
        }, 1000);
    }

});

gaia = {
    misc: {
        navbar_menu_visible: 0
    },
    initRightMenu: function () {

        if (!navbar_initialized) {
            $toggle = $('.navbar-toggle');
            $toggle.click(function () {

                if (gaia.misc.navbar_menu_visible == 1) {
                    $('html').removeClass('nav-open');
                    gaia.misc.navbar_menu_visible = 0;
                    $('#bodyClick').remove();
                    setTimeout(function () {
                        $toggle.removeClass('toggled');
                    }, 550);

                } else {
                    setTimeout(function () {
                        $toggle.addClass('toggled');
                    }, 580);

                    div = '<div id="bodyClick"></div>';
                    $(div).appendTo("body").click(function () {
                        $('html').removeClass('nav-open');
                        gaia.misc.navbar_menu_visible = 0;
                        $('#bodyClick').remove();
                        setTimeout(function () {
                            $toggle.removeClass('toggled');
                        }, 550);
                    });

                    $('html').addClass('nav-open');
                    gaia.misc.navbar_menu_visible = 1;

                }
            });
            navbar_initialized = true;
        }

    },

    checkScrollForTransparentNavbar: debounce(function () {
        if ($(document).scrollTop() > scroll_distance) {
            if (transparent) {
                transparent = false;
                $navbar.removeClass('navbar-transparent');
            }
        } else {
            if (!transparent) {
                transparent = true;
                $navbar.addClass('navbar-transparent');
            }
        }
    }, 17),

    checkScrollForParallax: debounce(function () {
        $('.parallax').each(function () {
            var $elem = $(this);

            if (isElementInViewport($elem)) {
                var parent_top = $elem.offset().top;
                var window_bottom = $(window).scrollTop();
                var $image = $elem.children('.image');

                oVal = ((window_bottom - parent_top) / 3);
                $image.css('transform', 'translate3d(0px, ' + oVal + 'px, 0px)');
            }
        });

    }, 6),

    checkScrollForContentTransitions: debounce(function () {
        $('.content-with-opacity').each(function () {
            var $content = $(this);

            if (isElementInViewport($content)) {
                var window_top = $(window).scrollTop();
                opacityVal = 1 - (window_top / 230);

                if (opacityVal < 0) {
                    opacityVal = 0;
                    return;
                } else {
                    $content.css('opacity', opacityVal);
                }

            }
        });
    }, 6),

    initGoogleMaps: function ($elem, lat, lng) {
        var myLatlng = new google.maps.LatLng(lat, lng);

        var mapOptions = {
            zoom: 13,
            center: myLatlng,
            scrollwheel: false, //we disable de scroll over the map, it is a really annoing when you scroll through page
            disableDefaultUI: true,
            styles: [{ "featureType": "administrative", "elementType": "labels", "stylers": [{ "visibility": "on" }, { "gamma": "1.82" }] }, { "featureType": "administrative", "elementType": "labels.text.fill", "stylers": [{ "visibility": "on" }, { "gamma": "1.96" }, { "lightness": "-9" }] }, { "featureType": "administrative", "elementType": "labels.text.stroke", "stylers": [{ "visibility": "off" }] }, { "featureType": "landscape", "elementType": "all", "stylers": [{ "visibility": "on" }, { "lightness": "25" }, { "gamma": "1.00" }, { "saturation": "-100" }] }, { "featureType": "poi.business", "elementType": "all", "stylers": [{ "visibility": "off" }] }, { "featureType": "poi.park", "elementType": "all", "stylers": [{ "visibility": "off" }] }, { "featureType": "road", "elementType": "geometry.stroke", "stylers": [{ "visibility": "off" }] }, { "featureType": "road", "elementType": "labels.icon", "stylers": [{ "visibility": "off" }] }, { "featureType": "road.highway", "elementType": "geometry", "stylers": [{ "hue": "#ffaa00" }, { "saturation": "-43" }, { "visibility": "on" }] }, { "featureType": "road.highway", "elementType": "geometry.stroke", "stylers": [{ "visibility": "off" }] }, { "featureType": "road.highway", "elementType": "labels", "stylers": [{ "visibility": "simplified" }, { "hue": "#ffaa00" }, { "saturation": "-70" }] }, { "featureType": "road.highway.controlled_access", "elementType": "labels", "stylers": [{ "visibility": "on" }] }, { "featureType": "road.arterial", "elementType": "all", "stylers": [{ "visibility": "on" }, { "saturation": "-100" }, { "lightness": "30" }] }, { "featureType": "road.local", "elementType": "all", "stylers": [{ "saturation": "-100" }, { "lightness": "40" }, { "visibility": "off" }] }, { "featureType": "transit.station.airport", "elementType": "geometry.fill", "stylers": [{ "visibility": "on" }, { "gamma": "0.80" }] }, { "featureType": "water", "elementType": "all", "stylers": [{ "visibility": "off" }] }]
        }
        var map = new google.maps.Map($elem, mapOptions);

        var marker = new google.maps.Marker({
            position: myLatlng,
            title: "Hello World!"
        });

        // To add the marker to the map, call setMap();
        marker.setMap(map);
    }

}

// Returns a function, that, as long as it continues to be invoked, will not
// be triggered. The function will be called after it stops being called for
// N milliseconds. If `immediate` is passed, trigger the function on the
// leading edge, instead of the trailing.

function debounce(func, wait, immediate) {
    var timeout;
    return function () {
        var context = this, args = arguments;
        clearTimeout(timeout);
        timeout = setTimeout(function () {
            timeout = null;
            if (!immediate) func.apply(context, args);
        }, wait);
        if (immediate && !timeout) func.apply(context, args);
    };
};


function isElementInViewport(elem) {
    var $elem = $(elem);

    // Get the scroll position of the page.
    var scrollElem = ((navigator.userAgent.toLowerCase().indexOf('webkit') != -1) ? 'body' : 'html');
    var viewportTop = $(scrollElem).scrollTop();
    var viewportBottom = viewportTop + $(window).height();

    // Get the position of the element on the page.
    var elemTop = Math.round($elem.offset().top);
    var elemBottom = elemTop + $elem.height();

    return ((elemTop < viewportBottom) && (elemBottom > viewportTop));
}


var BrowserDetect = {
    init: function () {
        this.browser = this.searchString(this.dataBrowser) || "Other";
        this.version = this.searchVersion(navigator.userAgent) || this.searchVersion(navigator.appVersion) || "Unknown";
    },
    searchString: function (data) {
        for (var i = 0; i < data.length; i++) {
            var dataString = data[i].string;
            this.versionSearchString = data[i].subString;

            if (dataString.indexOf(data[i].subString) !== -1) {
                return data[i].identity;
            }
        }
    },
    searchVersion: function (dataString) {
        var index = dataString.indexOf(this.versionSearchString);
        if (index === -1) {
            return;
        }

        var rv = dataString.indexOf("rv:");
        if (this.versionSearchString === "Trident" && rv !== -1) {
            return parseFloat(dataString.substring(rv + 3));
        } else {
            return parseFloat(dataString.substring(index + this.versionSearchString.length + 1));
        }
    },

    dataBrowser: [
        { string: navigator.userAgent, subString: "Chrome", identity: "Chrome" },
        { string: navigator.userAgent, subString: "MSIE", identity: "Explorer" },
        { string: navigator.userAgent, subString: "Trident", identity: "Explorer" },
        { string: navigator.userAgent, subString: "Firefox", identity: "Firefox" },
        { string: navigator.userAgent, subString: "Safari", identity: "Safari" },
        { string: navigator.userAgent, subString: "Opera", identity: "Opera" }
    ]

};

var better_browser = '<div class="container"><div class="better-browser row"><div class="col-md-2"></div><div class="col-md-8"><h3>We are sorry but it looks like your Browser doesn\'t support our website Features. In order to get the full experience please download a new version of your favourite browser.</h3></div><div class="col-md-2"></div><br><div class="col-md-4"><a href="https://www.mozilla.org/ro/firefox/new/" class="btn btn-warning">Mozilla</a><br></div><div class="col-md-4"><a href="https://www.google.com/chrome/browser/desktop/index.html" class="btn ">Chrome</a><br></div><div class="col-md-4"><a href="http://windows.microsoft.com/en-us/internet-explorer/ie-11-worldwide-languages" class="btn">Internet Explorer</a><br></div><br><br><h4>Thank you!</h4></div></div>';
