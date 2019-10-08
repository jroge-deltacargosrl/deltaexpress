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


    function sleep(milliseconds) {
        var start = new Date().getTime();
        for (var i = 0; i < 1e7; i++) {
         if ((new Date().getTime() - start) > milliseconds) {
          break;
         }
        }
       }


    var palabras = ["ABC", "DEF"];
    var letterTime=1000;
    var wordTime=2000;
    var texto=$('#dinamicText');
    for(var i=0;i<=palabras.length-1;i++){
        for(var j=0;j<palabras[i].length;j++){
            var newWord=(palabras[i].substr(0,j+1).toString());
            texto.text(newWord);
            console.log(newWord);
            sleep(letterTime);
        }
        sleep(wordTime);
    }




});