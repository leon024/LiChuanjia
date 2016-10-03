<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Finish_Close.aspx.cs" Inherits="WechatSDK.Finish_Close" %>
<!DOCTYPE html>

<html lang="en-gb" dir="ltr">
<head>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <title>故障报修</title>
       <link rel="stylesheet" href="./dist/style/weui.css"/>
    <link rel="stylesheet" href="./dist/example/example.css"/>
          <script src="./dist/example/zepto.min.js"></script>
    <script src="./dist/example/router.min.js"></script>
    <script src="./dist/example/example.js"></script>



        <style type="text/css">
            #xxx {
                height: 99px;
                width: 350px;
                text-align: center;
            }
            #dscr {
                height: 92px;
                width: 419px;
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
        ] // 必填，需要使用的JS接口列表，所有JS接口列表见附录2
            });

            wx.ready(function () {
               

                wx.error(function (res) {
                    alert(res.errMsg);
                });


              

            })
           // wx.closeWindow();
        </script>


     
      
            
                    
                 
        
        
        <div>
  <div class="weui_msg">
    <div class="weui_icon_area"><i class="weui_icon_success weui_icon_msg"></i></div>
    <div class="weui_text_area">
        <h2 class="weui_msg_title">操作成功</h2>
        <p class="weui_msg_desc">维修进展请留意微信通知！</p>
    </div>
    
</div>
        <input id="Button2" class="weui_btn weui_btn_primary" type="button" value="确认！"  onclick="wx.closeWindow();"/>
            </div></form>
     

        <br />
       
       
        
    

        
   
</body> 
</html>
