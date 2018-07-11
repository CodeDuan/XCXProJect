
function login(){
        if($("#username").val()=="" || $("#pwd").val()==""){
            alert("用户名或密码为空，请输入");
            $("#username").val()="";
            $("#pwd").val()=""
            $("username").focus();
       }else{
            $.ajax({
               type:"post",
               url:"/Login/Login",
                data: {
                    username: $("#username").val(),
                    pwd: $("#pwd").val(),
                    Answer:$("#logyzm").val(),
                    Captcha: $.cookie('Captcha')
               }, 
               //contentType: "application/json; charset=utf-8",
               error:function(request) {
                   alert("服务器错误");
               },
                success: function (data) {
                    if (data.code == 100) {
                        window.location.href = "/Home";
                    }
                    else {
                        alert(data.message);
                    }
               }
           }); 
       }
   }

   function changeVeryfy(){
       var verifycodeurl="/Login/verifycode?tm="+Math.floor(Math.random()*(100-1+1)+1);
       console.log(verifycodeurl);
        $('#verifycode').attr("src", verifycodeurl);
   }