let change=true;
$('#spam-image').click(ChangeColour);

function ChangeColour(){
    if(change){
        $("#text").css("color", "red");
        $("body").css("background-color", "white");
        change=false;
    }
    else{
        $("#text").css("color", "black");
        $("body").css("background-color", "#FF6EBF");
        change=true;
    }
}

$('body').append("<p id='apended'>text text text </p>");