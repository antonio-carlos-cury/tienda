var imgWidth = 100;
var imgDegs = 0;
var elem = null;
var currentVideo = null;
$(document).ready(function () {
    $('img').not('.img-logo').on('click', function () {
        var src = $(this).attr('src');
        var name = $(this).data('name');
        var fullPath = $(this).data('fp');
        var seconds = $(this).data('seconds');
        var autoPlay = $(this).data('start') == 'true';
        showFull(src, name, fullPath, seconds, autoPlay);
    });
})
function sum(et, sm) {
    return new Number(new Number(et) + sm).toFixed(0).valueOf();
}
function l() {
    let text = [];
    for (let i = 0; i < arguments.length; ++i) {
        if (typeof (arguments[i]) == 'object') {
            for (let prop in arguments[i]) {
                text.push(prop + ' = ' + arguments[i][prop.toString()] + '\r\n');
            }
        }
        text.push(arguments[i] + '(' + typeof (arguments[i]) + ')');
    }
    console.log(text.join(' - '));
}
function toggleMenu() {
    $('nav > ul').toggleClass('visible');
}
function localUrl(controller, action) {
    let basePath = ($('#base-path').val() + controller + '/' + action).replaceAll('//', '');
    return basePath;
}
function formatBytes(bytes, decimals = 2) {
    if (!+bytes) return '0 Bytes'

    const k = 1024
    const dm = decimals < 0 ? 0 : decimals
    const sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB']

    const i = Math.floor(Math.log(bytes) / Math.log(k))

    return `${parseFloat((bytes / Math.pow(k, i)).toFixed(dm))} ${sizes[i]}`
}
function noty(msg) {
    alert(msg)
}
function extractJSON(str) {
    var firstOpen, firstClose, candidate;
    firstOpen = str.indexOf('{', firstOpen + 1);
    do {
        firstClose = str.lastIndexOf('}');
        if (firstClose <= firstOpen) {
            return null;
        }
        do {
            candidate = str.substring(firstOpen, firstClose + 1);
            try {
                var res = JSON.parse(candidate);
                return res;
            }
            catch (e) {
            }
            firstClose = str.substr(0, firstClose).lastIndexOf('}');
        } while (firstClose > firstOpen);
        firstOpen = str.indexOf('{', firstOpen + 1);
    } while (firstOpen != -1);

}
function toggleRow(key, setRowFull = false, obj = null) {
    let isOpenEvent = $('[data-key="' + key + '"]').hasClass('hidden');
    $('[data-key]').addClass('hidden');

    if (isOpenEvent) {
        openRow(key, setRowFull);
    }
    else {
        closeRow(key, setRowFull);
    }
    if (obj != null) {
        obj.scrollIntoView({ block: "start", behavior: "smooth" });
    }
}
function openRow(key, setRowFull = false) {
    $('[data-key="' + key + '"]').removeClass('hidden');
    if (setRowFull) {
        $('[data-hide="true"]').addClass('hidden');
        
    }
}
function closeRow(key, setRowFull = false) {
    $('[data-key="' + key + '"]').addClass('hidden');
    if (setRowFull) {
        $('[data-hide="true"]').removeClass('hidden');
    }
}
function openFull() {

    if (document.fullscreenElement == null) {
        elem = $('#full-screen-modal')[0];
        elem.requestFullscreen();
    }
    else {
        exitFull();
    }
}
function exitFull() {
    $('#full-screen-modal').remove();
    removeCurrentVideo();

    if (elem.exitFullscreen) {
        elem.exitFullscreen();
    } else if (elem.webkitExitFullscreen) {
        elem.webkitExitFullscreen();
    } else if (elem.mozCancelFullScreen) {
        elem.mozCancelFullScreen();
    } else if (elem.msExitFullscreen) {
        elem.msExitFullscreen();
    }
    elem = null;
}
function saveImg(img) {
    let _base64 = img.src.replaceAll('data:image/jpeg;base64,', '');
    let fileName = $(img).data('name');
    $('#img-base64').val(_base64);
    $('#img-name').val(fileName.replaceAll('.mp4', '.jpeg'));
    exitFull();
    $('#download-img').submit();
}
function removeCurrentVideo() {

    $('#current-video').remove();
    if (currentVideo != null) {
        currentVideo.pause();
        currentVideo = null;
    }
}
function zoomIt() {
    let img = $('#img-full');
    imgWidth = imgWidth + 100;
    if (imgWidth > 500) {
        imgWidth = 100;
    }
    img.css('width', imgWidth + '%');
}
function rotateLeft() {
    let img = $('#img-full');
    imgDegs = imgDegs - 90;
    if (imgDegs < -360) {
        imgDegs = 0;
    }
    let newDegs = 'rotate(' + imgDegs + 'deg)';
    img.css('transform', newDegs);
}
function rotateRight() {
    let img = $('#img-full');
    imgDegs = imgDegs + 90;
    if (imgDegs > 360) {
        imgDegs = 0;
    }

    let newDegs = 'rotate(' + imgDegs + 'deg)';
    img.css('transform', newDegs);
}
function showFull(src, name, fp, sec, autoPlay = false) {
    let container = $($('#full-screen-view-mode').html().replaceAll('%%SRC%%', src).replaceAll('%%NAME%%', name).replaceAll('%%FP%%', fp).replaceAll('%%SECONDS%%', sec));
    $('#full-screen-modal').remove();
    $('body').append(container);
    $('#img-full').css('width', '100%');
    imgWidth = 100;
    imgDegs = 0;
    document.addEventListener("fullscreenchange", openFull());
}
function openLoading() {
    $('body').append($('#loading-ui').html());
}
function closeLoading() {
    $('.loading-ui').remove();
}
function navigate(index, form) {
    $('#Page, #PageIndex').val(index);
    $(form).submit();
}
function openPagination(totalPages, form) {
    var pagination = $($('#pagination-full').html());
    $('header').hide();
    for (var i = 1; i <= totalPages; i++) {

        let li = {};

        if (i == totalPages) {
            li = $('<li onclick="closePagination()"><i class="fa fa-times"></i></li>');
        } else
        {
            li = $('<li onclick="navigate(' + (i) + ', \'' + form + '\')" onkeypress="getKeyPagination(this)" tabindex="' + i + '">' + i + '</li>');
        }

        if (i % 2 == 0) {
            li.addClass('odd');
        }

        pagination.append(li);
    }
    
    $('.main-section').append(pagination);
    $(pagination).find('li').first().focus();
}

function closePagination() {
    $('.full-pagination').remove();
    $('header').show();
}

function toTop() {
    window.scrollTo(0, 0);
}

function getKeyPagination(el) {
    if (event.key == 'Enter' || event.code == 'Enter' || event.key == 'Space' || event.code == 'Space') {
        $(el).trigger('click');
    }
}