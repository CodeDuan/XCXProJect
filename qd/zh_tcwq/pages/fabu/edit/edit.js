var app = getApp(), _imgArray = [];

Page({
    data: {
        know: !1,
        num: 1,
        disabled: !1,
        upimgurl:"https://www.xjbmzxlt.cn/api/common/uploadimg"
    },
    bindMultiPickerChange: function(e) {
        this.setData({
            multiIndex: e.detail.value
        });
    },
    bindPickerChange: function(e) {
        var t = this.data.stock[e.detail.value];
        this.setData({
            index: e.detail.value,
            text: t
        });
    },
    onLoad: function(e) {
        console.log(e);
        this.setData({
          category_id:e.id,
          category_name:e.name
        });
        
        var i = this, t = wx.getStorageSync("users").id;
         wx.setNavigationBarColor({
            frontColor: "#ffffff",
            backgroundColor: wx.getStorageSync("color"),
            animation: {
                duration: 0,
                timingFunc: "easeIn"
            }
        })
        
      console.log(wx.getStorageSync("users")),  wx.getLocation({
            type: "wgs84",
            success: function(e) {
                var t = e.latitude + "," + e.longitude;
            }
        }) 
    },
    selected: function(e) {
        var t = e.currentTarget.id, a = this.data.stick;
        this.setData({
            stick_info: a[t].array,
            money1: a[t].money,
            type: a[t].type,
            checked: !1,
            stick_none: !0
        });
    },
    add: function(e) {
        var a = this;
        wx.chooseLocation({
            type: "wgs84",
            success: function(e) {
                e.latitude, e.longitude, e.speed, e.accuracy;
                var t = e.latitude + "," + e.longitude;
                a.setData({
                    address: e.address,
                    coordinates: t
                });
            }
        });
    },
    label: function(e) {
        var t = this.data.label, a = e.currentTarget.dataset.inde;
        "selected1" == t[a].click_class ? t[a].click_class = "selected2" : "selected2" == t[a].click_class && (t[a].click_class = "selected1"), 
        this.setData({
            label: t
        });
    },
    know: function(e) {
        var t = this.data.know;
        1 == t ? this.setData({
            know: !1
        }) : this.setData({
            know: !0
        });
    },
    imgArray1: function(e) {
      console.log("1");
        var a = this;
        //var n = wx.getStorageSync("uniacid");
        var t = 9 - _imgArray.length;
        0 < t && t <= 9 ? wx.chooseImage({
            count: t,
            sizeType: [ "compressed" ],
            sourceType: [ "album", "camera" ],
            success: function(e) {
                wx.showToast({
                    icon: "loading",
                    title: "正在上传"
                });
                var t = e.tempFilePaths;
                a.uploadimg({
                    url: a.data.upimgurl,
                    path: t
                });
            }
        }) : wx.showModal({
            title: "上传提示",
            content: "最多上传9张图片",
            showCancel: !0,
            cancelText: "取消",
            confirmText: "确定",
            success: function(e) {},
            fail: function(e) {},
            complete: function(e) {}
        });
    },
    uploadimg: function(e) {
        var t = this, a = e.i ? e.i : 0, n = e.success ? e.success : 0, i = e.fail ? e.fail : 0;
        wx.uploadFile({
            url: e.url,
            filePath: e.path[a],
            name: "img",
            formData: null,
            success: function(e) {
              var res=JSON.parse(e.data);
              console.log(res);
              "" != e.data ? (n++, _imgArray.push("https://www.xjbmzxlt.cn/"+res.msg), t.setData({
                  imgArray1: _imgArray
                })) : wx.showToast({
                    icon: "loading",
                    title: "请重试"
                });
            },
            fail: function(e) {
                i++;
            },
            complete: function() {
                ++a == e.path.length ? (t.setData({
                    images: e.path
                }), wx.hideToast()) : (e.i = a, e.success = n, e.fail = i, t.uploadimg(e));
            }
        });
    },
    delete: function(e) {
        Array.prototype.indexOf = function(e) {
            for (var t = 0; t < this.length; t++) if (this[t] == e) return t;
            return -1;
        }, Array.prototype.remove = function(e) {
            var t = this.indexOf(e);
            -1 < t && this.splice(t, 1);
        };
        var t = e.currentTarget.dataset.inde;
        _imgArray.remove(_imgArray[t]), this.setData({
            imgArray1: _imgArray
        });
    },

    formSubmit: function(e) {
        console.log("这是保存formid2");
        var t = this, a = wx.getStorageSync("city");
        var m = wx.getStorageSync("openid"), p = (e.detail.formId, e.detail.value.content.replace("\n", "↵")), y = e.detail.value.name, f = e.detail.value.tel;
        console.log(f);
        var S="";//验证信息
        if ("" == p ? S = "内容不能为空" : 540 <= p.length ? S = "内容超出了" : "" == y ? S = "姓名不能为空" : "" == f ? S = "电话不能为空" : 1 == 1, "" != S) {
          wx.showModal({
            title: "提示",
            content: S,
            success: function(e) {},
            fail: function(e) {},
            complete: function(e) {}
          });
        } else {
            var o = wx.getStorageSync("System");
            console.log(1);
            console.log(o);
            if (0 == _imgArray.length) var I = ""; else I = _imgArray.join(",");
            console.log(I);
                wx.request({
                  method: 'POST',
                  url: "https://www.xjbmzxlt.cn/api/message/addusermessage",
                data: {
                    content:p,
                    name:y,
                    phone:f
                },
                success: function(e) {
                  console.log(e);
                    wx.showToast({
                        title: "发布成功",
                        icon: "",
                        image: "",
                        duration: 2e3,
                        mask: !0,
                        success: function(e) {},
                        fail: function(e) {},
                        complete: function(e) {}
                    }), setTimeout(function() {
                        wx.switchTab({
                            url: "../../index/index",
                            success: function(e) {},
                            fail: function(e) {},
                            complete: function(e) {}
                        });
                    }, 2e3);
                }
            })
        }
    },
    cancel: function(e) {
        this.setData({
            money1: 0,
            type: 0,
            checked: !1,
            stick_none: !1,
            iszdchecked: !1
        });
    },
    onReady: function() {},
    onShow: function() {},
    onHide: function() {},
    onUnload: function() {
        console.log(this.data), _imgArray.splice(0, _imgArray.length);
    },
    onPullDownRefresh: function() {},
    onReachBottom: function() {},
    onShareAppMessage: function() {}
});
