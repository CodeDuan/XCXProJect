var app = getApp();

Page({
    data: {},
    onLoad: function(n) {
        this.Refresh();
    },
    refresh1: function() {
        this.Refresh();
    },
    Refresh: function(n) {
        var i = this;
        wx.setNavigationBarColor({
            frontColor: "#ffffff",
            backgroundColor: wx.getStorageSync("color"),
            animation: {
                duration: 0,
                timingFunc: "easeIn"
            }
        });
        var t = wx.getStorageSync("users").id;
        app.util.request({
            url: "entry/wxapp/GetUserInfo",
            cachetime: "0",
            data: {
                user_id: t
            },
            success: function(n) {
                console.log(n);
                var a = n.data;
                app.util.request({
                    url: "entry/wxapp/MyTiXian",
                    cachetime: "0",
                    data: {
                        user_id: n.data.id
                    },
                    success: function(n) {
                        console.log(n);
                        var t = 0;
                        for (var o in n.data) t += Number(n.data[o].tx_cost);
                        console.log(t);
                        var e = Number(a.money);
                        e = e.toFixed(2), console.log(e), i.setData({
                            money: e
                        });
                    }
                });
            }
        });
    },
    detailed2: function(n) {
        wx.navigateTo({
            url: "detailed?state=2",
            success: function(n) {},
            fail: function(n) {},
            complete: function(n) {}
        });
    },
    detailed3: function(n) {
        wx.navigateTo({
            url: "detailed?state=1",
            success: function(n) {},
            fail: function(n) {},
            complete: function(n) {}
        });
    },
    cash: function(n) {
        wx.navigateTo({
            url: "cash?state=1",
            success: function(n) {},
            fail: function(n) {},
            complete: function(n) {}
        });
    },
    onReady: function() {},
    onShow: function() {},
    onHide: function() {},
    onUnload: function() {},
    onPullDownRefresh: function() {},
    onReachBottom: function() {}
});