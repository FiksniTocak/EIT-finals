var counter=document.getElementById("counter")
var start=new Date('27 June, 2024 21:00:00').getTime();
console.log(start)

function countdown(){
    var thisMoment=new Date().getTime();
    var totalSeconds=(start-thisMoment)/1000;
    var d=Math.floor(totalSeconds/3600/24);
    var h=Math.floor(totalSeconds/3600)%24;
    var m=Math.floor(totalSeconds/60)%60;
    var s=Math.floor(totalSeconds)%60;

    counter.textContent=d+" dana, "+h+" sati, "+m+" minuta, "+s+" sekundi."
}

setInterval(countdown,1000);



