window.onload = function () {
    loadimg();
}
//�ϴ�ͼƬ
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
///����ͼƬ
function loadimg() {
    if ($("#img_path").val() != "") {
        $("#img").html("<img src='" + $("#img_path").val() + "'/>");
    }
}
function addoredit() {
    if ($("#c_name").val() == "") {
        alert("�������Ʊ���");
        return;
    }
    else if ($("#img_path").val() == "") {
        alert("ͼƬ�ش�");
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
                alert("����������");
            }
        });
    }

}