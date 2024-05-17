var login_btn=document.getElementById("login-btn")
var pop_up=document.getElementById("pop-up")


login_btn.addEventListener('click',function(){
    pop_up.classList.toggle("active");
})