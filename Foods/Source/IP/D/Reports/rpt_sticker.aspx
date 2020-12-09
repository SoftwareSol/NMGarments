<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt_sticker.aspx.cs" Inherits="Foods.rpt_sticker" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report Sticker</title>
    <style type="text/css">
        body {

            font-family:Arial;
            font-size:14px;
            font-weight:bolder;
            margin:0px;
            padding:0px;
        }
        .sticker3
        {
        width:1.2in;
        height:1in;       
        float:left; 
        margin-left:30px;       
        }
        
         .sticker4
        {
        width:1.2in;
        height:1in;       
        float:left;
        margin-left:50px;
      
        }

        table {
            text-align:center;
            width:1.5in;
            height:1in;
            font-size:12px;
            margin-top:0px;
            margin-left:0px;
           
           
        }
        td {
            /*border:1px dashed #000;*/
        }
        #printMe {
            width:100%;
            height:auto;
           
        }
        .clear {
            clear:both;
        }
    </style>
    <script type="text/javascript">

        function printDiv(divName) {
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;
            document.body.innerHTML = printContents;
            window.print();
            document.body.innerHTML = originalContents;
        }
	</script>
</head>
<body>
    <form id="form1" runat="server">
    
        
      

        <div id="printMe">
            <div class="sticker3" >
               <table>
                    <tr>
                        <td colspan="2" ><asp:Label ID="lbl_compnam" runat="server"></asp:Label></td>
                    </tr> 
                    <tr>
                        <td>Product:</td><td><asp:Label ID="lbl_prodesc" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Color</td>
                        <td><asp:Label ID="lblcol" runat="server"></asp:Label></td>

                    </tr>
                    <tr>
                        <td>Size</td>
                        <td><asp:Label ID="lblsiz" runat="server"></asp:Label></td>
                    </tr>

                </table>
            </div>
            <div class="sticker4" >
                <table>
                        <tr>
                        <td colspan="2" ><asp:Label ID="lblcomp2" runat="server"></asp:Label></td>
                    </tr> 
                    <tr>
                        <td>Product:</td><td><asp:Label ID="lbl_prodes2" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Color</td>
                        <td><asp:Label ID="lblcol2" runat="server"></asp:Label></td>

                    </tr>
                    <tr>
                        <td>Size</td>
                        <td><asp:Label ID="lblsiz2" runat="server"></asp:Label></td>
                    </tr>

                </table>
            </div>
            <div class="clear"></div>
        </div>  
        <br />
          <input type="button" value="Print" onclick="printDiv('printMe')" />      
    </form>
</body>
</html>
