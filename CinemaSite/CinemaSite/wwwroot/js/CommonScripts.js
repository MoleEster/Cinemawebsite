jQuery(document).ready(function () {
    $(window).on('load', function () {
        var $preloader = $('#p_prldr'),
            $preloader_logo = $preloader.find('.preloader-logo');
        $preloader_logo.delay(600).fadeOut('slow');
        $preloader.delay(1200).fadeOut('slow');
    })
});
