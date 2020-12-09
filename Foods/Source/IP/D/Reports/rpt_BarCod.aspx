<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt_BarCod.aspx.cs" 
    Inherits="Foods.rpt_BarCod" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Bar Code</title>
    <link href='https://fonts.googleapis.com/css?family=Libre Barcode 39 Extended Text' rel='stylesheet' />
    <style type="text/css">
        body {

            font-family:Arial;
            font-size:12px;
            font-weight:bolder;
        }
        .sticker1
        {
        width:1.2in;
        height:1.2in;       
        float:left;
        margin-top:0px;
        }
        
         .sticker2
        {
        width:1.2in;
        height:1.2in;       
        float:left;
        margin-left:45px;
        margin-top:0px;
        
       
        }
      

        table {
            text-align:center;
            width:2in;
            height:1in;
            /*border:1px dashed #000;*/
            font-size:12px;
           
           
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
    <div>
   

        <div id='printMe'>
            <div class="sticker1" >
               <table>
                   <tr>
                        <td colspan="2">
                            <asp:Label ID="lbl_Comp1" runat="server"></asp:Label>
                        </td>
                    </tr>
                   
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="barcode1" runat="server" Text="Label"></asp:Label>
                            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder> 
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            Rs. <asp:Label id="lbl_pric1" runat="server"></asp:Label> /-
                        </td>
                    </tr>
               </table>
           </div>
            <div class="sticker2" >
                <table>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lbl_comp2" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="barcode2" runat="server" Text="Label"></asp:Label>
                            <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder> 
                        </td>
                    </tr>
                     <tr>
                        <td colspan="2">
                            Rs. <asp:Label id="lbl_pric2" runat="server"></asp:Label> /-
                        </td>
                    </tr>
                </table>
           </div>
            <div class="clear"></div>
        </div>
         <input type="button" value="Print" onclick="printDiv('printMe')" />
    </div>
    </form>
</body>
</html>
