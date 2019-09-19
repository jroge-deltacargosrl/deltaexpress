$(document).ready(function () {
    $('.tabContents article').hide();
    $('.img').hide();
    $('#tab1').show();
    $('#tab1img').show();
    $('#li1 a').addClass('active');
    $('.divOptions .tabs li a').click(function () {
        $('.divOptions .tabs li a').removeClass('active');
        $(this).addClass('active');
        var activeTab = $(this).attr('href');
        $('.tabContents article').hide();
        $('.img').hide();
        $(activeTab).show();
        var activeImg = activeTab + "img";
        $(activeImg).show();
        return false;
    });
});