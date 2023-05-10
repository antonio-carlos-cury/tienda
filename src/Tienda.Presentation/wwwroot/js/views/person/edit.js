function viewImages(url) {
    if ($('.image-container').find('img').length < 1) {

        $.get(url, function (response) {
            $.each(response.results, function (i, img) {
                var html = $('#image-default').html().replaceAll('%%IMGSRC%%', img.base64Value).replaceAll('%%CAPTION%%', img.source);
                $('.image-container').append(html);
            });
        })
    }
}