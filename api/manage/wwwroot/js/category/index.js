
window.onload = function () {
    loadlist();
}
///加载列表
function loadlist() {
    $.ajax({
        url: '/category/getlist',
        type: 'get',
        data: null,
        success: function (info) {
            console.log(info);
            if (info.code == 200) {

                var catelist = "";
                for (var i = 0; i < info.data.length; i++) {
                    catelist += "<tr>";
                    catelist += "<td>" + info.data[i].c_id + "</td>";
                    catelist += "<td>" + info.data[i].c_name + "</td>";
                    catelist += "<td>" + info.data[i].operate_by + "</td>";
                    catelist += "<td><img style='width:50px;height:50px;' src='" + info.data[i].img_path + "'/></td>";
                    if (info.data[i].is_enable == true) {
                        catelist += "<td><a class='label label-success' href='javascript:void(0)' onclick='changestatus(false," + info.data[i].c_id + ");'>启用</a></td>";
                    }
                    else {
                        catelist += "<td><a class='label label-default' href='javascript:void(0)' onclick='changestatus(true," + info.data[i].c_id + ");'>关闭</a></td>";
                    }
                    catelist += "<td>" + info.data[i].operate_at + "</td>";
                    catelist += "</tr>";
                }
                console.log(catelist);
                $("#category_list").html(catelist);
            }
        },
        error: function (err) {
            console.log(err)
        }
    });
}
///更改分类状态
function changestatus(status, id) {
    var msg = "";
    if (status) {
        msg="确定要启用该分类？";
    }
    else {
        msg="确定要关闭该分类？";
    }
    console.log(msg);
    var a = confirm(msg);
    if (a == true) {
        $.ajax({
            url: '/category/changestatus',
            type: 'post',
            data: { is_enable: status, c_id: id },
            success: function (info) {
                console.log(info);
                if (info.code == 200) {
                    loadlist()
                }
                else if (info.code == 100) {
                    alert(info.res);
                }
            },
            error: function (err) {
                console.log(err)
            }
        });
    }
    
}