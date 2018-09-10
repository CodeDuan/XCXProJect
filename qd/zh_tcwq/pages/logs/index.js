var app = getApp(), imgArray = [], uploaded = [];

Page({
    data: {
        hidden1: !1,
        hidden2: !0,
        hidden3: !0,
        index_two: 0,
        prompt: !1,
        choise: !1,
        images: [],
        upload_img: [],
        upload_img_length: 0,
        getmsg: "获取验证码",
        tel_code: !1,
        items: [ {
            name: "刷卡支付",
            value: "刷卡支付"
        }, {
            name: "免费wifi",
            value: "免费wifi"
        }, {
            name: "免费停车",
            value: "免费停车"
        }, {
            name: "禁止吸烟",
            value: "禁止吸烟"
        }, {
            name: "提供包间",
            value: "提供包间"
        }, {
            name: "沙发休闲",
            value: "沙发休闲"
        } ]
    },
    bindMultiPickerChange: function(e) {
        console.log("picker发送选择改变，携带值为", e.detail.value), this.setData({
            multiIndex: e.detail.value
        });
    },
    cost1: function(e) {
        var t = this;
        if (console.log(t.data), console.log(e), 2 == t.data.store_info.time_over) wx.showModal({
            title: "提示",
            content: "入驻时间不可以修改喔",
            showCancel: !0,
            cancelText: "取消",
            confirmText: "确定",
            success: function(e) {},
            fail: function(e) {},
            complete: function(e) {}
        }); else {
            var a = t.data.stick, o = e.currentTarget.id;
            for (var n in a) a[n].hidden1 = n != o;
            t.setData({
                stick: a,
                type: a[o].type,
                inmoney: a[o].money
            });
        }
    },
    onLoad: function(e) {
        console.log(e);
        var c = this, t = getCurrentPages(), a = (t[t.length - 1], t[t.length - 2]);
        console.log(a), a.setData({
            Return: !0
        }), wx.setNavigationBarColor({
            frontColor: "#ffffff",
            backgroundColor: wx.getStorageSync("color"),
            animation: {
                duration: 0,
                timingFunc: "easeIn"
            }
        }), console.log(c.data);
        var o = wx.getStorageSync("url2"), n = wx.getStorageSync("url"), i = wx.getStorageSync("openid"), s = e.user_id;
        console.log("用户的openid为 " + i + " 用户的user_id为 " + s), c.setData({
            user_id: s,
            openid: i,
            url: o,
            url1: n
        }), app.util.request({
            url: "entry/wxapp/MyStore",
            cachetime: "0",
            data: {
                user_id: s
            },
            success: function(e) {
                if (console.log(e), 0 == e.data) c.setData({
                    mystore: !0
                }); else {
                    for (var t in e.data.img = e.data.img.split(","), e.data.ad = e.data.ad.split(","), 
                    e.data.ad) imgArray.push(e.data.ad[t]);
                    for (var a in e.data.img) uploaded.push(e.data.img[a]);
                    console.log(imgArray), console.log(uploaded);
                    var o = c.data.items, i = e.data;
                    1 == i.time_over && wx.showModal({
                        title: "提示",
                        content: "您的入驻已经到期了喔,赶紧去续费吧",
                        showCancel: !0,
                        cancelText: "取消",
                        confirmText: "确定",
                        success: function(e) {},
                        fail: function(e) {},
                        complete: function(e) {}
                    }), i.yy_time = i.yy_time.split("-"), console.log(i.yy_time);
                    for (var n = 0; n < o.length; n++) "刷卡支付" == o[n].value ? 1 == i.skzf ? o[n].checked = !0 : o[n].checked = !1 : "免费wifi" == o[n].value ? 1 == i.wifi ? o[n].checked = !0 : o[n].checked = !1 : "免费停车" == o[n].value ? 1 == i.mftc ? o[n].checked = !0 : o[n].checked = !1 : "禁止吸烟" == o[n].value ? 1 == i.jzxy ? o[n].checked = !0 : o[n].checked = !1 : "提供包间" == o[n].value ? 1 == i.tgbj ? o[n].checked = !0 : o[n].checked = !1 : "沙发休闲" == o[n].value && (1 == i.sfxx ? o[n].checked = !0 : o[n].checked = !1);
                    console.log(o);
                    var s = i.coordinates, l = i.address;
                    c.setData({
                        time: i.start_time,
                        time1: i.end_time,
                        store_info: i,
                        address: l,
                        coordinates: s,
                        lunbo_len: imgArray.length,
                        imgArray: imgArray,
                        items: o,
                        type: i.type,
                        uploaded: uploaded,
                        upload_img_length: uploaded.length,
                        upload_img: e.data.img,
                        hotel_logo: i.logo,
                        wechat: i.weixin_logo
                    }), app.util.request({
                        url: "entry/wxapp/Area",
                        cachetime: "0",
                        success: function(e) {
                            for (var t in console.log(e), e.data) e.data[t].id == i.area_id && c.setData({
                                inde: t
                            });
                            var a = [];
                            e.data.map(function(e) {
                                var t;
                                t = e.area_name, a.push(t);
                            }), console.log(a), c.setData({
                                area: e.data,
                                city: a
                            });
                        }
                    }), app.util.request({
                        url: "entry/wxapp/StoreType",
                        cachetime: "0",
                        success: function(e) {
                            console.log(e);
                            var t = e.data, a = [];
                            t.map(function(e) {
                                var t;
                                t = e.type_name, a.push(t);
                            }), console.log(a), console.log(c.data);
                            var o = i.type_name;
                            for (var n in a) a[n] == o && (console.log(a[n]), c.setData({
                                index: n
                            }));
                            c.setData({
                                store: t,
                                store_type: a
                            }), app.util.request({
                                url: "entry/wxapp/StoreType2",
                                cachetime: "0",
                                data: {
                                    type_id: i.storetype_id
                                },
                                success: function(e) {
                                    console.log(e);
                                    var a = [];
                                    e.data.map(function(e) {
                                        var t;
                                        t = e.name, a.push(t);
                                    });
                                    var t = i.type2_name;
                                    for (var o in a) a[o] == t && (console.log(a[o]), c.setData({
                                        index_two: o
                                    }));
                                    c.setData({
                                        store2: e.data,
                                        store_type_two: a
                                    });
                                }
                            });
                        }
                    }), app.util.request({
                        url: "entry/wxapp/InMoney",
                        cachetime: "0",
                        success: function(e) {
                            console.log(e);
                            var t = e.data;
                            for (var a in t) if (t[a].hidden1 = !0, 0 != t[a].money ? t[a].money1 = "（收费" + t[a].money + "元）" : t[a].money1 = "  免费", 
                            1 == t[a].type ? t[a].array = "一周" + t[a].money1 : 2 == t[a].type ? t[a].array = "半年" + t[a].money1 : 3 == t[a].type && (t[a].array = "一年" + t[a].money1), 
                            t[a].type == i.type) {
                                var o = t[a].type, n = t[a].money;
                                t[a].hidden1 = !1;
                            }
                            c.setData({
                                stick: t,
                                type: o,
                                inmoney: n
                            });
                        }
                    });
                }
            }
        }), app.util.request({
            url: "entry/wxapp/IsSms",
            cachetime: "0",
            success: function(e) {
                console.log(e), 1 == e.data ? c.setData({
                    send: !1,
                    sms: !0,
                    tel_code: !1
                }) : c.setData({
                    send: !0,
                    sms: !1,
                    tel_code: !1
                });
            }
        });
    },
    user_name: function(e) {
        console.log(e);
        var t = e.detail.value;
        this.setData({
            name: t
        });
    },
    user_code: function(e) {
        console.log(e);
        var t = e.detail.value;
        t != this.data.num ? wx.showToast({
            title: "验证码错误",
            icon: "",
            image: "",
            duration: 2e3,
            mask: !0,
            success: function(e) {},
            fail: function(e) {},
            complete: function(e) {}
        }) : this.setData({
            tel_code: !0,
            yz_code: t
        });
    },
    sendmessg: function(e) {
        var t = this;
        console.log(t.data);
        var a = t.data.name;
        if ("" == a || null == a) wx.showToast({
            title: "请输入手机号",
            icon: "",
            image: "",
            duration: 2e3,
            mask: !0,
            success: function(e) {},
            fail: function(e) {},
            complete: function(e) {}
        }); else if ("获取验证码" == t.data.getmsg) {
            for (var o = "", n = 0; n < 6; n++) o += Math.floor(10 * Math.random());
            console.log(o), app.util.request({
                url: "entry/wxapp/sms",
                cachetime: "0",
                data: {
                    code: o,
                    tel: a
                },
                success: function(e) {
                    console.log(e);
                }
            }), t.setData({
                num: o
            });
            var i = 60, s = setInterval(function() {
                t.setData({
                    getmsg: i + "s后重新发送",
                    send: !0
                }), --i <= 0 && (clearInterval(s), t.setData({
                    getmsg: "获取验证码",
                    send: !1,
                    num: 0
                }));
            }, 1e3);
        }
    },
    getPhoneNumber: function(e) {
        var t = this, a = wx.getStorageSync("key"), o = e.detail.iv, n = e.detail.encryptedData;
        console.log(a), console.log(o), console.log(n), app.util.request({
            url: "entry/wxapp/jiemi",
            cachetime: "0",
            data: {
                sessionKey: a,
                iv: o,
                data: n
            },
            success: function(e) {
                console.log(e), t.setData({
                    num: e.data.phoneNumber,
                    tel_code: !0
                });
            }
        });
    },
    bindTimeChange: function(e) {
        console.log("picker发送选择改变，携带值为", e.detail.value), this.setData({
            time: e.detail.value
        });
    },
    bindTimeChange1: function(e) {
        var t = this.data.time, a = e.detail.value;
        "00：00" == a ? wx.showModal({
            title: "提示",
            content: "营业结束时间不能为00:00",
            showCancel: !0,
            cancelText: "取消",
            confirmText: "确定",
            success: function(e) {},
            fail: function(e) {},
            complete: function(e) {}
        }) : a <= t ? wx.showModal({
            title: "提示",
            content: "营业结束时间不能为小于或等于营业开始时间",
            showCancel: !0,
            cancelText: "取消",
            confirmText: "确定",
            success: function(e) {},
            fail: function(e) {},
            complete: function(e) {}
        }) : this.setData({
            time1: e.detail.value
        });
    },
    add: function(e) {
        var o = this;
        wx.chooseLocation({
            type: "wgs84",
            success: function(e) {
                console.log(e);
                e.latitude, e.longitude, e.speed, e.accuracy;
                var t = e.latitude + "," + e.longitude, a = o.data.store_info;
                a.address = e.address, a.coordinates = t, o.setData({
                    store_info: a
                });
            }
        });
    },
    bindPickerChanges: function(e) {
        var t = this, a = t.data.store, o = e.detail.value;
        this.setData({
            index: o,
            index_two: 0
        }), app.util.request({
            url: "entry/wxapp/StoreType2",
            cachetime: "0",
            data: {
                type_id: a[o].id
            },
            success: function(e) {
                console.log(e);
                var a = [];
                e.data.map(function(e) {
                    var t;
                    t = e.name, a.push(t);
                }), t.setData({
                    store2: e.data,
                    store_type_two: a
                });
            }
        });
    },
    bindchange_two: function(e) {
        console.log("picker发送选择改变，携带值为", e.detail.value), this.setData({
            index_two: e.detail.value
        });
    },
    bindRegionChange: function(e) {
        this.setData({
            inde: e.detail.value
        });
    },
    choice: function(e) {
        this.setData({
            choice: !0
        });
    },
    previewImage: function(e) {
        var t = e.currentTarget.dataset.index, a = this.data.lunbo;
        wx.previewImage({
            current: a[t],
            urls: a
        });
    },
    previewImage1: function(e) {
        var t = e.currentTarget.dataset.index, a = this.data.lunbo1;
        wx.previewImage({
            current: a[t],
            urls: a
        });
    },
    lunbo1: function(e) {
        var t = this;
        wx.getStorageSync("uniacid");
        console.log(t.data);
        var a = t.data.lunbo;
        a = null == a ? [] : t.data.lunbo;
        t.data.url;
        wx.chooseImage({
            count: 9,
            sizeType: [ "original", "compressed" ],
            sourceType: [ "album", "camera" ],
            success: function(e) {
                console.log(e), Array.prototype.push.apply(a, e.tempFilePaths), a.length <= 9 || (a = a.slice(0, 9)), 
                t.setData({
                    lunbo1: a,
                    lunbo_len1: a.length,
                    log: !0
                });
            }
        });
    },
    choose: function(e) {
        var a = this, o = a.data.url, n = wx.getStorageSync("uniacid");
        console.log(o), console.log(n), wx.chooseImage({
            count: 1,
            sizeType: [ "original", "compressed" ],
            sourceType: [ "album", "camera" ],
            success: function(e) {
                console.log(e);
                var t = e.tempFilePaths[0];
                wx.uploadFile({
                    url: o + "app/index.php?i=" + n + "&c=entry&a=wxapp&do=Upload&m=zh_tcwq",
                    filePath: t,
                    name: "upfile",
                    formData: {},
                    success: function(e) {
                        console.log(e), a.setData({
                            hotel_logo: e.data
                        });
                    },
                    fail: function(e) {
                        console.log(e);
                    }
                });
            }
        });
    },
    choose1: function(e) {
        var a = this, o = a.data.url, n = wx.getStorageSync("uniacid");
        wx.getStorageSync("openid");
        wx.chooseImage({
            count: 1,
            sizeType: [ "original", "compressed" ],
            sourceType: [ "album", "camera" ],
            success: function(e) {
                console.log(e);
                var t = e.tempFilePaths[0];
                wx.uploadFile({
                    url: o + "app/index.php?i=" + n + "&c=entry&a=wxapp&do=Upload&m=zh_tcwq",
                    filePath: t,
                    name: "upfile",
                    formData: {},
                    success: function(e) {
                        console.log(e), a.setData({
                            wechat: e.data
                        });
                    },
                    fail: function(e) {
                        console.log(e);
                    }
                });
            }
        });
    },
    checkboxChange: function(e) {
        console.log(e);
        var t = e.detail.value;
        this.setData({
            facilities: t
        });
    },
    chooseImage2: function() {
        console.log(e);
        var a = this, e = a.data.imgArray;
        console.log(a.data);
        var o = wx.getStorageSync("uniacid"), t = 9 - e.length;
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
                    url: a.data.url + "app/index.php?i=" + o + "&c=entry&a=wxapp&do=Upload&m=zh_tcwq",
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
        var t = this, a = e.i ? e.i : 0, o = e.success ? e.success : 0, n = e.fail ? e.fail : 0;
        wx.uploadFile({
            url: e.url,
            filePath: e.path[a],
            name: "upfile",
            formData: null,
            success: function(e) {
                "" != e.data ? (console.log(e), o++, imgArray.push(e.data), t.setData({
                    imgArray: imgArray,
                    lunbo_len: imgArray.length
                }), console.log(a), console.log("上传商家轮播图时候提交的图片数组", imgArray)) : wx.showToast({
                    icon: "loading",
                    title: "请重试"
                });
            },
            fail: function(e) {
                n++, console.log("fail:" + a + "fail:" + n);
            },
            complete: function() {
                console.log(a), ++a == e.path.length ? (t.setData({
                    images: e.path
                }), wx.hideToast(), console.log("执行完毕"), console.log("成功：" + o + " 失败：" + n)) : (console.log(a), 
                e.i = a, e.success = o, e.fail = n, t.uploadimg(e));
            }
        });
    },
    delete: function(e) {
        console.log(this.data), console.log(imgArray), Array.prototype.indexOf = function(e) {
            for (var t = 0; t < this.length; t++) if (this[t] == e) return t;
            return -1;
        }, Array.prototype.remove = function(e) {
            var t = this.indexOf(e);
            -1 < t && this.splice(t, 1);
        };
        var t = e.currentTarget.dataset.index, a = this.data.images;
        imgArray.remove(imgArray[t]), a.remove(a[t]), console.log(a), this.setData({
            images: a,
            imgArray: imgArray,
            lunbo_len: imgArray.length
        });
    },
    upload_image: function() {
        var a = this;
        uploaded = a.data.uploaded;
        var o = wx.getStorageSync("uniacid"), e = 9 - uploaded.length;
        0 < e && e <= 9 ? wx.chooseImage({
            count: e,
            sizeType: [ "compressed" ],
            sourceType: [ "album", "camera" ],
            success: function(e) {
                wx.showToast({
                    icon: "loading",
                    title: "正在上传"
                });
                var t = e.tempFilePaths;
                a.already({
                    url1: a.data.url + "app/index.php?i=" + o + "&c=entry&a=wxapp&do=Upload&m=zh_tcwq",
                    path1: t
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
    already: function(e) {
        var t = this, a = e.j ? e.j : 0, o = e.success ? e.success : 0, n = e.fail ? e.fail : 0;
        wx.uploadFile({
            url: e.url1,
            filePath: e.path1[a],
            name: "upfile",
            formData: null,
            success: function(e) {
                "" != e.data ? (console.log(e), o++, uploaded.push(e.data), t.setData({
                    uploaded: uploaded,
                    upload_img_length: uploaded.length
                }), console.log(a), console.log("上传商家介绍时候提交的图片数组", uploaded)) : wx.showToast({
                    icon: "loading",
                    title: "请重试"
                });
            },
            fail: function(e) {
                n++, console.log("fail:" + a + "fail:" + n);
            },
            complete: function() {
                console.log(a), ++a == e.path1.length ? (t.setData({
                    upload_img: e.path1
                }), wx.hideToast(), console.log("执行完毕"), console.log("成功：" + o + " 失败：" + n)) : (console.log(a), 
                e.j = a, e.success = o, e.fail = n, t.already(e));
            }
        });
    },
    delete2: function(e) {
        Array.prototype.indexOf = function(e) {
            for (var t = 0; t < this.length; t++) if (this[t] == e) return t;
            return -1;
        }, Array.prototype.remove = function(e) {
            var t = this.indexOf(e);
            -1 < t && this.splice(t, 1);
        };
        var t = e.currentTarget.dataset.index, a = this.data.upload_img;
        uploaded.remove(uploaded[t]), a.remove(a[t]), console.log(a), this.setData({
            uploaded: uploaded,
            upload_img_length: uploaded.length
        });
    },
    formSubmit: function(e) {
        var t = this;
        if (console.log(e), console.log(t.data), null == t.data.inmoney) var a = 0; else a = Number(t.data.inmoney);
        var o = t.data.type;
        console.log(a + " " + o);
        var n = t.data.store_info.id;
        console.log(n);
        var i = wx.getStorageSync("openid"), s = t.data.facilities, l = t.data.hotel_logo, c = t.data.wechat, u = e.detail.value.business_name, r = e.detail.value.business_keyword, d = t.data.store_info.address, p = (t.data.time, 
        t.data.time1, t.data.time1), g = t.data.time, f = e.detail.value.vr_demo, m = e.detail.value.business_number, h = e.detail.value.business_notice, y = e.detail.value.textarea, v = t.data.store_info.coordinates, w = t.data.store, x = t.data.store2, _ = t.data.index, T = t.data.index_two, D = w[_], k = x[T], S = w[_].id;
        if (0 < x.length) var b = x[T].id; else b = "";
        console.log(w, x, D, k, S, b);
        var A = t.data.tel_code, z = t.data.num, q = (T = t.data.index_two, imgArray.join(",")), C = uploaded.join(",");
        t.data.city;
        "" != y && null != y || (y = ""), t.data.sms;
        var P = "";
        if ("" == u ? P = "请输入商家名称" : "" == r ? P = "请输入关键字" : "" == d ? P = "请输入详细地址" : "" == m ? P = "请输入联系电话" : "00:00" == g ? P = "请输入营业开始时间" : "00:00" == p ? P = "请输入营业结束时间" : "" == h ? P = "请输入公告说明" : 0 == t.data.sms ? "" == z && (P = "请进行手机号验证") : 1 == t.data.sms && null == t.data.yz_code && (P = "请进行手机号验证"), 
        "" != P) wx.showModal({
            title: "提示",
            content: P,
            showCancel: !0,
            cancelText: "取消",
            confirmText: "确定",
            success: function(e) {},
            fail: function(e) {},
            complete: function(e) {}
        }); else {
            var I = 0, j = 0, M = 0, F = 0, U = 0, L = 0;
            console.log(t.data.items);
            var R = t.data.items;
            if (null == s) {
                for (var N in R) if (1 == R[N].checked) if ("刷卡支付" == R[N].value) I = 1; else if ("免费wifi" == R[N].value) j = 1; else if ("免费停车" == R[N].value) M = 1; else if ("禁止吸烟" == R[N].value) F = 1; else if ("提供包间" == R[N].value) U = 1; else if ("沙发休闲" == R[N].value) L = 1;
            } else for (var O = 0; O < s.length; O++) if ("刷卡支付" == s[O]) I = 1; else if ("免费wifi" == s[O]) j = 1; else if ("免费停车" == s[O]) M = 1; else if ("禁止吸烟" == s[O]) F = 1; else if ("提供包间" == s[O]) U = 1; else if ("沙发休闲" == s[O]) L = 1;
            if (1 == A) if (2 == t.data.store_info.time_over) app.util.request({
                url: "entry/wxapp/UpdStore",
                cachetime: "0",
                data: {
                    id: n,
                    user_id: t.data.user_id,
                    store_name: u,
                    address: d,
                    announcement: h,
                    storetype_id: S,
                    storetype2_id: b,
                    start_time: g,
                    end_time: p,
                    keyword: r,
                    skzf: I,
                    wifi: j,
                    mftc: M,
                    jzxy: F,
                    tgbj: U,
                    sfxx: L,
                    tel: m,
                    logo: l,
                    weixin_logo: c,
                    ad: q,
                    img: C,
                    money: B,
                    details: y,
                    coordinates: v,
                    vr_link: f
                },
                success: function(e) {
                    console.log("这里是发布成功"), console.log(e), 1 == e.data && (wx.showToast({
                        title: "修改成功",
                        icon: "",
                        image: "",
                        duration: 2e3,
                        mask: !0,
                        success: function(e) {},
                        fail: function(e) {},
                        complete: function(e) {}
                    }), setTimeout(function() {
                        wx.reLaunch({
                            url: "../logs/logs",
                            success: function(e) {},
                            fail: function(e) {},
                            complete: function(e) {}
                        });
                    }, 2e3));
                }
            }); else {
                var B = a;
                B <= 0 ? app.util.request({
                    url: "entry/wxapp/UpdStore",
                    cachetime: "0",
                    data: {
                        id: n,
                        user_id: t.data.user_id,
                        store_name: u,
                        address: d,
                        announcement: h,
                        storetype_id: S,
                        storetype2_id: b,
                        start_time: g,
                        end_time: p,
                        keyword: r,
                        skzf: I,
                        wifi: j,
                        mftc: M,
                        jzxy: F,
                        tgbj: U,
                        sfxx: L,
                        tel: m,
                        logo: l,
                        weixin_logo: c,
                        ad: q,
                        img: C,
                        money: B,
                        details: y,
                        coordinates: v,
                        type: o,
                        vr_link: f
                    },
                    success: function(e) {
                        console.log("这里是发布成功"), console.log(e), 1 == e.data && (wx.showToast({
                            title: "入驻成功",
                            icon: "",
                            image: "",
                            duration: 2e3,
                            mask: !0,
                            success: function(e) {},
                            fail: function(e) {},
                            complete: function(e) {}
                        }), setTimeout(function() {
                            wx.reLaunch({
                                url: "../logs/logs",
                                success: function(e) {},
                                fail: function(e) {},
                                complete: function(e) {}
                            });
                        }, 2e3));
                    }
                }) : app.util.request({
                    url: "entry/wxapp/Pay",
                    cachetime: "0",
                    data: {
                        openid: i,
                        money: B
                    },
                    success: function(e) {
                        wx.requestPayment({
                            timeStamp: e.data.timeStamp,
                            nonceStr: e.data.nonceStr,
                            package: e.data.package,
                            signType: e.data.signType,
                            paySign: e.data.paySign,
                            success: function(e) {
                                console.log("这里是支付成功"), console.log(e), app.util.request({
                                    url: "entry/wxapp/UpdStore",
                                    cachetime: "0",
                                    data: {
                                        id: n,
                                        user_id: t.data.user_id,
                                        store_name: u,
                                        address: d,
                                        announcement: h,
                                        storetype_id: S,
                                        storetype2_id: b,
                                        start_time: g,
                                        end_time: p,
                                        keyword: r,
                                        skzf: I,
                                        wifi: j,
                                        mftc: M,
                                        jzxy: F,
                                        tgbj: U,
                                        sfxx: L,
                                        tel: m,
                                        logo: l,
                                        weixin_logo: c,
                                        ad: q,
                                        img: C,
                                        money: B,
                                        details: y,
                                        coordinates: v,
                                        type: o,
                                        vr_link: f
                                    },
                                    success: function(e) {
                                        console.log("这里是发布成功"), console.log(e), 1 == e.data && (wx.showToast({
                                            title: "入驻成功",
                                            icon: "",
                                            image: "",
                                            duration: 2e3,
                                            mask: !0,
                                            success: function(e) {},
                                            fail: function(e) {},
                                            complete: function(e) {}
                                        }), setTimeout(function() {
                                            wx.reLaunch({
                                                url: "../logs/logs",
                                                success: function(e) {},
                                                fail: function(e) {},
                                                complete: function(e) {}
                                            });
                                        }, 2e3));
                                    }
                                });
                            },
                            fail: function(e) {
                                console.log(e), wx.showToast({
                                    title: "支付失败",
                                    duration: 1e3
                                });
                            }
                        });
                    }
                });
            } else wx.showToast({
                title: "手机验证错误",
                icon: "",
                image: "",
                duration: 2e3,
                mask: !0,
                success: function(e) {},
                fail: function(e) {},
                complete: function(e) {}
            });
        }
    },
    reset: function(e) {},
    onReady: function() {},
    onShow: function() {},
    onHide: function() {},
    onUnload: function() {
        console.log(this.data), imgArray.splice(0, imgArray.length), uploaded.splice(0, uploaded.length);
    },
    onPullDownRefresh: function() {},
    onReachBottom: function() {},
    onShareAppMessage: function() {}
});