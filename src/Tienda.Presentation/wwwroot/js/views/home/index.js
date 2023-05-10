var toShow = {};
var imageContainer = {};
var hideContainer = {};
var imgsAll = [];
var videosAll = [];

$(document).ready(function () {
    $('.inline-code').each(function () {

        let jObject = extractJSON($(this).text());
        let formatedObject = JSON.stringify(jObject, null, 4);
        formatedObject = js_beautify(formatedObject, { indent_size: 2, space_in_empty_paren: true });
        if (formatedObject == 'null' || formatedObject == '{}' || formatedObject.length < 1) {
            $(this).text(js_beautify($(this).text(), { indent_size: 2, space_in_empty_paren: true }))
        }
        else {
            $(this).text(formatedObject);
        }
    });

    let hasCode = $('#BodyContents').val() > 0;
    if (hasCode) {
        closeRow('filter');
        openRow('code');
    }
    else {
        closeRow('code');
        openRow('filter');
    }

    let hasResults = $('.img-grid, .img-full').length > 0;
    if (hasResults) {
        openRow('results');
        closeRow('filter');
        $.each($('[data-src]'), function (i, el) {
            $(el).attr('src', $(el).data('src'));
        })
    }
    else {
        openRow('filter');
        closeRow('results');
    }

});

var isPlaying = false;
var isFullScreen = false;
function showVideo(video) {
    if (!isPlaying) {
        video.play();
        isPlaying = true;
        if (!isFullScreen) {
            video.requestFullscreen();
        }
    }
    else {
        video.pause();
        isPlaying = false;
    }
}

function playing(video) {

    //console.log('executando', video, isPlaying);

}

function onEnded(video) {
    isPlaying = false;
    elem = video;
    exitFull();
    isFullScreen = false;
}

function loadImages(container, key) {
    container.each(function (i, el) {

        if ($('[data-ref="' + key + '"]').length < 1) {
            openLoading();
            imageContainer = $('<div class="image-container" data-ref="' + key + '"></div>');
            hideContainer.append(imageContainer);

            $.each($(el).find('.file-to-show'), function (ind, fl) {
                var newImage = $(fl).find('[data-src]');
                newImage.attr('src', newImage.data('src'));
                newImage.removeAttr('data-src');
                newImage.on('load', function () {
                    loadImage(imgsAll[ind + 1], ind + 1);
                });
                imgsAll.push(newImage);
            });

            imageContainer.append(imgsAll[0]);
            $('.file-to-show').remove();
        }
    });
}
function loadImage(img, ind) {
    imageContainer.append(img);
    if (imgsAll.length > (ind + 1)) {
        loadImage(imgsAll[ind + 1], ind + 1);
    }
    else {
        closeLoading();
    }
}

function breakLines() {
    $('#code').toggleClass('break-pre');
    $('#code').toggleClass('break-pre-line');
}
function textSize() {
    let currentSize = new Number($('#code').css('font-size').replaceAll('px', '')).valueOf();
    if (currentSize + 2 == 24) {
        currentSize = 8;
    }
    $('#code').css('font-size', (currentSize + 2) + 'px');
}
function copyText(text, el) {

    if (text == null) {
        text = $('#BodyContents').text();
    }
    if (text.length > 0) {
        var inpt = document.getElementById("copy-text");
        inpt.value = text;
        inpt.select();
        inpt.setSelectionRange(0, 99999);
        navigator.clipboard.writeText(inpt.value);
        el.removeClass('fa-copy').addClass('fa-check success');
        setTimeout(function () {
            el.removeClass('fa-check success').addClass('fa-copy');
        }, 2000);
    }
}
function getJson() {
    $('#separate-form').submit();
}