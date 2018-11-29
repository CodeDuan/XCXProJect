// zh_tcwq/pages/List/List.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    page_index:1,
    page_size:10,
    category:0
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    var that = this//不要漏了这句，很重
    wx.request({
      url: 'https://www.xjbmzxlt.cn/api/message/getmessage',
      data:{
        page_index:that.data.page_index, 
        page_size:that.data.page_size,
        category:that.data.category
      },
      headers: {
        'Content-Type': 'application/json'
      },
      success: function (res) 
      {

        var tz = res.data;
        that.setData({
          classification_info:res.data.list,
          allpage:res.data.allpage
        })
      }
    });
  },

  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function () {

  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function () {

  },

  /**
   * 生命周期函数--监听页面隐藏
   */
  onHide: function () {

  },

  /**
   * 生命周期函数--监听页面卸载
   */
  onUnload: function () {

  },

  /**
   * 页面相关事件处理函数--监听用户下拉动作
   */
  onPullDownRefresh: function () {
  },

  /**
   * 页面上拉触底事件的处理函数
   */
  onReachBottom: function () {
    console.log(123);
    var that=this;
    if(that.data.page_index<that.data.allpage)
    {
      wx.showLoading({
        title: '加载中',
      });
      that.setData({
        page_index: that.data.page_index + 1
      });
      wx.request({
        url: 'https://www.xjbmzxlt.cn/api/message/getmessage',
        data: {
          page_index: that.data.page_index,
          page_size: that.data.page_size,
          category: that.data.category
        },
        headers: {
          'Content-Type': 'application/json'
        },
        success: function (res) {
          console.log(res.data);
          wx.hideLoading();
          var tz = res.data;
          that.setData({
            //classification_info: res.data.list
            classification_info: that.data.classification_info.concat(res.data.list)
          })
        }
      });
    }
  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function () {

  }
})