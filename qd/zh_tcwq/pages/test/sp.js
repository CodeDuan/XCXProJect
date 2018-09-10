
Page({
  
  onLoad:function(e){
    var that = this//不要漏了这句，很重
    wx.request({
      url: 'https://www.xjbmzxlt.cn/api/Home/GetGategory',
      headers: {
        'Content-Type': 'application/json'
      },
      success: function (res) {
        console.log(res.data.gategory_list);
        that.setData({
          gategory_list: res.data.gategory_list
        })
      }
    })
  }
})
