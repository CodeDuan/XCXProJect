var app = getApp();

Page({
    data: {
        luntext: [ "最新收录", "热门推荐", "附近发现" ],
        activeIndex: 0,
        sliderOffset: 0,
        sliderLeft: 35,
        currentTab: 0,
        swiperCurrent: 0,
        indicatorDots: !1,
        autoplay: !0,
        interval: 5e3,
        duration: 1e3
    },
    swiperChange: function(t) {
        this.setData({
            swiperCurrent: t.detail.current
        });
    },
    onLoad: function(t) {
        var e = wx.getStorageSync("url");
        this.setData({
            url: e
        }), this.refresh();
    },
    refresh: function(t) {
        var f = this, e = wx.getStorageSync("users").id;
        app.util.request({
            url: "entry/wxapp/MyYellowPage",
            cachetime: "0",
            data: {
                user_id: e
            },
            success: function(t) {
                for (var e in console.log(t), t.data) {
                    var n = t.data[e].coordinates.split(",");
                    t.data[e].lat2 = Number(wx.getStorageSync("Location").latitude), t.data[e].lng2 = Number(wx.getStorageSync("Location").longitude);
                    var a = Number(wx.getStorageSync("Location").latitude), o = Number(wx.getStorageSync("Location").longitude), i = n[0], r = n[1], u = a * Math.PI / 180, s = i * Math.PI / 180, c = u - s, l = o * Math.PI / 180 - r * Math.PI / 180, d = 2 * Math.asin(Math.sqrt(Math.pow(Math.sin(c / 2), 2) + Math.cos(u) * Math.cos(s) * Math.pow(Math.sin(l / 2), 2)));
                    d *= 6378.137;
                    d = (d = Math.round(1e4 * d) / 1e4).toFixed(2);
                    t.data[e].distance = d;
                }
                f.setData({
                    yellow_list: t.data
                });
            }
        });
    },
    yellow_info: function(t) {
        var e = t.currentTarget.dataset.id;
        wx.navigateTo({
            url: "yellowinfo?id=" + e,
            success: function(t) {},
            fail: function(t) {},
            complete: function(t) {}
        });
    },
    store_type_id: function(t) {
        var e = t.currentTarget.dataset.id;
        wx.navigateTo({
            url: "../store/business?id=" + e,
            success: function(t) {},
            fail: function(t) {},
            complete: function(t) {}
        });
    },
    shouye: function(t) {
        wx.switchTab({
            url: "../index/index"
        });
    },
    yellow: function(t) {
        wx.reLaunch({
            url: "yellow"
        });
    },
    settled: function(t) {
        wx.navigateTo({
            url: "settled"
        });
    },
    onReady: function() {},
    onShow: function() {},
    onHide: function() {},
    onUnload: function() {},
    onPullDownRefresh: function() {
        this.reload(), wx.stopPullDownRefresh();
    },
    onReachBottom: function() {},
    onShareAppMessage: function() {}
});