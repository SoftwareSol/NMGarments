<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt_Rsticker.aspx.cs" Inherits="Foods.rpt_Rsticker" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report Sticker</title>
    <style type="text/css">
        body {

            font-family:Arial;
            font-size:16px;
            font-weight:bolder;
        }
        .sticker
        {
        width:2in;
        height:1.5in;       
        float:left;
        margin-left:30px;
        }
        .sticker1
        {
        width:1.7in;
        height:1.5in;       
        float:left;        
        }
        
         .sticker2
        {
        width:1.8in;
        height:1.5in;       
        float:left;
        margin-left:90px;
      
        }

        table {
            text-align:center;
            width:100%;
            height:1.5in;
           
        }
        td {
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="sticker1">
            <table>
                <tr>
                    <td colspan="2" ><asp:Label ID="lbl_compnam" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder> 
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lbl_barcd" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2"><asp:Label ID="lbl_prodesc" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="2">Rs <asp:Label ID="lblpric" runat="server"></asp:Label> /-</td>
                </tr>

            </table>
        </div>
        <div class="sticker2">
            <table>
                <tr>
                    <td colspan="2" ><asp:Label ID="lbl_comp1" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder> 
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbl_barcd1" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2"><asp:Label ID="lbl_prodesc1" runat="server"></asp:Label></td>
                </tr>   
                <tr>
                   <td colspan="2">Rs <asp:Label ID="lblpric1" runat="server"></asp:Label> /-</td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
