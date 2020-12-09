<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt_BarCod.aspx.cs" 
    Inherits="Foods.rpt_BarCod" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Bar Code</title>
    <style type="text/css">
        body {

            font-family:Arial;
            font-size:20px;
            font-weight:bolder;
        }
        .sticker1
        {
        width:1.9in;
        height:1.5in;       
        float:left;
        margin-top:20px;
        }
        
         .sticker2
        {
        width:1.9in;
        height:1.5in;       
        float:left;
        margin-left:90px;
        margin-top:20px;
        
       
        }
      

        table {
            text-align:center;
            width:2in;
            height:1in;
            /*border:1px dashed #000;*/
            font-size:20px;
           
           
        }
        td {
            /*border:1px dashed #000;*/
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
    <input type="button" value="Print" onclick="printDiv('printMe')" />

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
        </div>
    </div>
    </form>
</body>
</html>
