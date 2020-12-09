<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt_Rsticker.aspx.cs" Inherits="Foods.rpt_Rsticker" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report Sticker</title>
    <link href='https://fonts.googleapis.com/css?family=Libre Barcode 39 Extended Text' rel='stylesheet' />
    <style type="text/css">
        body {

            font-family: Arial;
            font-size: 10px;
            font-weight: bolder;
        }
        #printMe {
            width:100%;
            height:150px;
        }
        .sticker
        {
        width:1.9in;
        height:1.5in;       
        float:left;
        margin-left:30px;
        font-weight:bolder;
        }
        .sticker1
        {
            width: 1.2in;
            height: 1in;
            float: left;
            font-weight: bolder;
            margin: 0px 0px 0px 40px;   
        }
        
         .sticker2
        {
           width: 1.2in;
            height: 1.5in;
            float: left;
            margin-left: 60px;
            font-weight: bolder;
      
        }

        table {
       
            width:100%;
            height:1.5in;
            font-weight:bolder;
           
        }
        td {
        }
        .upper {
            width: 100%;
            height: auto;
            background-color: black;
            color: #fff;
            margin-right: 50px;
            text-align: center;

        }
        
        .barnum {
            width: 100%;
            width: 100%;
            height: auto;
            font-size: 10px;
        }
        .Brand {
            width: 100%;
            height: auto;
            font-size: 10px;
        }
        .BrandName {
        font-size: 10px;
        font-weight: bolder;
        }
        .price {
           font-size: 12px;
        }
        @media print {
            .upper {
                background-color:black;
            }
            .barcode {
            width:100%; height: 30px;  font-size:18px;
        }
        }
    </style>
     <script type="text/javascript" language="javascript">
       

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
            <div class="sticker1">
                <div class="upper">
                    <asp:Label ID="lbl_compnam" runat="server"></asp:Label>
                </div>
                <div class="barcode">
                    <asp:Label ID="barcode1" runat="server"></asp:Label>
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder> 
                 </div>
                <div class="barnum">
                    <asp:Label ID="lbl_barcd"  runat="server"></asp:Label>
                </div>
                <div class="Brand">
                    <div class="BrandName">
                    
                    
                        <asp:Label ID="lbl_prodesc" runat="server"></asp:Label>
                    </div>
                    <div class="price">
                    Rs <asp:Label ID="lblpric"  runat="server"></asp:Label> /-
                    </div>
                </div>
           
            </div>
            <div class="sticker2">
                <div class="upper">
                    <asp:Label ID="lbl_comp1" runat="server"></asp:Label>
                </div>
                <div class="barcode">
                    <asp:Label ID="barcode2" runat="server"></asp:Label>
                    <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder> 
                </div>
                <div class="barnum">
                    <asp:Label ID="lbl_barcd1" runat="server"></asp:Label>
                </div>
                <div class="Brand">
                    <div class="BrandName">
                        <asp:Label ID="lbl_prodesc1" runat="server"></asp:Label>
                    </div>
                    <div class="price">
                    Rs <asp:Label ID="lblpric1"  runat="server"></asp:Label> /-
                    </div>
                </div>
               <%-- <table>
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
                </table>--%>
            </div>
        </div>

          <input type="button" value="Print" onclick="printDiv('printMe')" />
    </div>
    </form>
</body>
</html>
