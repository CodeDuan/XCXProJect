window.onload = function () {
    loadimg();
}
//上传图片
function upimg() {
    var formdata = new FormData();
    formdata.append('uploadImage', $('#upload').get(0).files[0]);
    $.ajax({
        url: '/Common/uploadimg',
        type: 'post',
        contentType: false,
        data: formdata,
        processData: false,
        success: function (info) {
            console.log(info);
            $("#img_path").val(info.message);
            loadimg();
        },
        error: function (err) {
            console.log(err)
        }
    });
}
///加载图片
function loadimg() {
    if ($("#img_path").val() != "") {
        $("#img").html("<img src='" + $("#img_path").val() + "'/>");
    }
}
function addoredit() {
    if ($("#c_name").val() == "") {
        alert("分类名称必填");
        return;
    }
    else if ($("#img_path").val() == "") {
        alert("图片必传");
        return;
    }
    else {
        $.ajax({
            url: '/Category/AddOrEdit',
            type: 'post',
            data: {
                "c_id": $("#c_id").val(),
                "c_name": $("#c_name").val(),
                "img_path":$("#img_path").val()
            },
            success: function (info) {
                if (info.code == 200) {
                    window.location.href = '/Category/Index';
                }
                else {
                    alert(info.message);
                }
                
            },
            error: function (info) {
                console.log(info);
                alert("服务器错误");
            }
        });
    }

}