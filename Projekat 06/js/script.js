var iframe=document.getElementById("iframe")
var lista=document.getElementById("lista")

lista.onchange=function(){
var izabrano=this.options[this.selectedIndex].value;
iframe.setAttribute('src',izabrano);
}