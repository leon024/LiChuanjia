<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Log_Edit.aspx.cs" Inherits="WechatSDK.Log_Edit" %>

<html lang="en-gb" dir="ltr">
<head>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <title>Add Photo</title>
       <link rel="stylesheet" href="./dist/style/weui.css"/>
    <link rel="stylesheet" href="./dist/example/example.css"/>
          



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
    
    <form id="form1" runat="server">
        
        <script type="text/javascript" src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.2.min.js"></script>
        <script src="http://res.wx.qq.com/open/js/jweixin-1.1.0.js" type="text/javascript"></script>
          
            <div class="weui_cell_bd weui_cell_primary">
               
            
           
                <input class="weui_input" type="text" placeholder="请输主题" id="title" runat="server"/>
            
         
                  </div>
        <hr />
        <br />
        <br />
       
      
            <div class="weui_cell_bd weui_cell_primary">
                <textarea class="weui_textarea" placeholder="请输入内容描述" rows="3" id="dscr" runat="server"></textarea>
                
          
                <br />
            </div>
        

       

        
   
          <asp:Button class="weui_btn weui_btn_primary" ID="Button2" runat="server" Text="提交" OnClick="Button2_Click" />
           
        <br />
           
  
        </form>
     

        <br />
       
       

       <script src="./dist/example/zepto.min.js"></script>
    <script src="./dist/example/router.min.js"></script>
    <script src="./dist/example/example.js"></script> 
   
</body> 
</html>