<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="WechatSDK.index" %>

<!DOCTYPE html>

<html lang="en-gb" dir="ltr">
<head>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <title>Leon' Life 
        </title>
       <link rel="stylesheet" href="./dist/style/weui.css"/>
    <link rel="stylesheet" href="./dist/example/example.css"/>
       <link rel="apple-touch-icon-precomposed" href="docs/images/apple-touch-icon.png">
        <link id="data-uikit-theme" rel="stylesheet" href="docs/css/uikit.docs.min.css">
          



            
        <style type="text/css">
            body{margin:0;height:100%;width:100%;position:absolute;}
		#mapContainer{height:200px;
width:100%;
            }
            .auto-style1 {
                width: 329px;
                height: 60px;
            }
            .auto-style2 {
                height: 60px;
            }
            </style>
          
    </head>
<body>
        
           <div class="uk-overflow-container">
         <asp:Repeater ID="Repeater1" runat="server" 
            >
        <HeaderTemplate><div align="center"></HeaderTemplate>
        <ItemTemplate>
              <div class="uk-panel uk-panel-box uk-panel-box-primary" align="center"><%# Eval("Title")%></div>
               <br />
     <div class="weui_cell_bd weui_cell_primary"  align="center">
       
      <a href="http://www.lichuanjia.cn/LeonImage/<%# Eval("FileName")%>"> <img width="150" height="150" alt="" src="http://www.lichuanjia.cn/LeonImage/small_<%# Eval("FileName")%>" title=""/></a>
         </div>
          
            <br />
           
            <%# Eval("ct")%>
            <br />
            <br />
            <div align="right"><%# Eval("Date")%> </div>
          
      </ItemTemplate>
        <FooterTemplate></table></FooterTemplate>
        </asp:Repeater>
           
   </div>
       
       

       <script src="./dist/example/zepto.min.js"></script>
    <script src="./dist/example/router.min.js"></script>
    <script src="./dist/example/example.js"></script> 
   
</body> 
</html>