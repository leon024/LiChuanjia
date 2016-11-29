<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add_Image.aspx.cs" Inherits="WechatSDK.Add_Image" %>
<!DOCTYPE html>

<html lang="en-gb" dir="ltr">
<head>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <title>Add Photo</title>
       <link rel="stylesheet" href="./dist/style/weui.css"/>
    <link rel="stylesheet" href="./dist/example/example.css"/>
        <link rel="apple-touch-icon-precomposed" href="docs/images/apple-touch-icon.png">
        <link id="data-uikit-theme" rel="stylesheet" href="docs/css/uikit.docs.min.css">
          



        <style type="text/css">
            #xxx {
                height: 99px;
                width: 350px;
                text-align: center;
            }
            #dscr {
                height: 92px;
                width: 419px;
                text-align: left;
            }
            #form1 {
                text-align: center;
            }
            #Text1 {
                width: 408px;
            }
            #pickfiles {
                height: 44px;
                width: 141px;
            }
            </style>
    </head>
<body>
          <div class="weui_cells weui_cells_form">
        <div class="weui_cell">
            <div class="weui_cell_bd weui_cell_primary">
                <div class="weui_uploader">
                     <span class="desc">
                <div class="weui_cell_bd weui_cell_primary" >上传图片</div>
                         </span>
                    <div class="weui_uploader_bd" id="addimage">
                        <ul class="weui_uploader_files" >
           
                             <asp:Image ID="Image1" runat="server" Height="75" Width="75"  ImageUrl="dist/example/images/plus.png"/>
                           
                           
                      
                        </ul>
                        
                    </div>
                </div>
            </div>
        </div>
    </div>
    <form id="form1" runat="server">
        
        <script type="text/javascript" src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.2.min.js"></script>
        <script src="http://res.wx.qq.com/open/js/jweixin-1.1.0.js" type="text/javascript"></script>
        <script>
            var appId = '<%=appId %>'
               , nonceStr = '<%=nonceStr %>'
                , signature = '<%=signature %>'
                , timestamp = '<%=timestamp %>';
            wx.config({
                debug: false, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
                appId: appId, // 必填，公众号的唯一标识
                timestamp: timestamp, // 必填，生成签名的时间戳
                nonceStr: nonceStr, // 必填，生成签名的随机串
                signature: signature, // 必填，签名，见附录1
                jsApiList: ['checkJsApi',
        'onMenuShareTimeline',
        'onMenuShareAppMessage',
        'onMenuShareQQ',
        'onMenuShareWeibo',
        'hideMenuItems',
        'showMenuItems',
        'hideAllNonBaseMenuItem',
        'showAllNonBaseMenuItem',
        'translateVoice',
        'startRecord',
        'stopRecord',
        'onRecordEnd',
        'playVoice',
        'pauseVoice',
        'stopVoice',
        'uploadVoice',
        'downloadVoice',
        'chooseImage',
        'previewImage',
        'uploadImage',
        'downloadImage',
        'getNetworkType',
        'openLocation',
        'getLocation',
        'hideOptionMenu',
        'showOptionMenu',
        'closeWindow',
        'scanQRCode',
        'chooseWXPay',
        'openProductSpecificView',
        'addCard',
        'chooseCard',
        'openCard'] // 必填，需要使用的JS接口列表，所有JS接口列表见附录2
            });

            wx.ready(function () {
                // 1 判断当前版本是否支持指定 JS 接口，支持批量判断
                // document.querySelector('#Button3').onclick = function () {
                //    wx.closeWindow();
                // }


                // 5 图片接口
                // 5.1 拍照、本地选图
                var images = {
                    localId: [],
                    serverId: []
                };
                document.querySelector('#addimage').onclick = function () {
                    wx.chooseImage({
                        count: 1,
                        success: function (res) {
                            images.localId = res.localIds;
                            //alert('已选择 ' + res.localIds);
                            document.getElementById('hide_file').value = res.localIds;
                            var i = 0, length = images.localId.length;
                            images.serverId = [];
                            function upload() {
                                wx.uploadImage({
                                    localId: images.localId[i],
                                    success: function (res) {
                                        i++;
                                        //alert('已上传：' + res.serverId );
                                        document.getElementById('serverid').value = res.serverId;
                                        document.getElementById("Button1").click();
                                        images.serverId.push(res.serverId);
                                        if (i < length) {
                                            upload();

                                        }
                                    },
                                    fail: function (res) {
                                        alert(JSON.stringify(res));
                                    }
                                });
                            }
                            upload();

                        }
                    });
                };


                wx.error(function (res) {
                    alert(res.errMsg);
                });




            })

        </script>


     
      
            
                    
                 
            <div class="weui_cells weui_cells_form">
                <div class="weui_cell">
            
            <div class="weui_cell_bd weui_cell_primary">
                <input class="weui_input" type="text" placeholder="请输主题" id="title" runat="server"/>
            </div>
                  
                    
           </div>
                  </div>
        <br />
        <br />
       
         <div class="weui_cells weui_cells_form">
        <div class="weui_cell">
            <div class="weui_cell_bd weui_cell_primary">
                <textarea class="weui_textarea" placeholder="请输入内容描述" rows="3" id="dscr" runat="server"></textarea>
                
            </div>
        </div>
                <br />
            </div>
        

        <input id="hide_file" runat="server" hidden="hidden" />
         <input id="serverid" runat="server" hidden="hidden" />

         <asp:Button ID="Button1" runat="server" OnClick="Button1_Click1" Text="Button"  Width="0" Height="0" />
         <span class="desc">
    <br />
   
          <asp:Button class="weui_btn weui_btn_primary" ID="Button2" runat="server" Text="提交" OnClick="Button2_Click" />
           
        <br />
           
    </span> 
        </form>
     

        <br />
       
       

       <script src="./dist/example/zepto.min.js"></script>
    <script src="./dist/example/router.min.js"></script>
    <script src="./dist/example/example.js"></script> 
   
</body> 
</html>