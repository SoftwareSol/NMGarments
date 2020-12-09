<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt_salinv.aspx.cs" Inherits="Foods.rpt_salinv" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sales Invoice</title>
    <style type="text/css">
        #container{
	        width:100%;
	        height:100%;
	        font-family:CordiaUPC;
            font-size:18px; 
        }
        .left{
	        width:45%;
	        height:auto;
	        float:left;
	        margin-left:20px;

        }
        .right{
	        width:49%;
	        height:auto;
	        float:right;
	        text-align:center;

        }
        .gv{
	        width:100%;
	        height:300px;
        }
        .clear{
        clear: both;
        }
        .shpkep{
	        width:50%;
	        height:auto;
	        text-align:center;
	        float:left;
        }
        .salemansig{
	        width:49%;
	        text-align:center;
	        height:auto;
	        float:right;
        }

    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <!--	border:1px solid #000;-->

        <div id="container">
            <div class="left">
	            <b>Ham Brothers</b><br />
	            KARACHI /<br />
	            <b>CASH MEMO</b><br />	            <br />
	            <br />
                <asp:Label ID="lbl_intro " runat="server" ></asp:Label>
	            <%--T001-566/SWEET MADINA CONFECTIONAY SHOP
	            SHOP # 29 MALIR TANKI / 03435454345--%>	
            </div>
            <div class="right">
                Bill No : <br />
                Order / Delv Date : <br />
                Booker / SalesMan : <br />
            </div>
            <div class="clear"></div>
            <div class="gv">
                
                <asp:GridView ID="GVSal" runat="server" AutoGenerateColumns="False" DataKeyNames="MSal_id" >
                    <Columns>
                        <asp:BoundField DataField="MSal_id" HeaderText="MSal_id" ReadOnly="True" SortExpression="MSal_id" />
                        <asp:BoundField DataField="Product Code / Description" HeaderText="Product Code / Description" ReadOnly="True" SortExpression="Product Code / Description" />
                        <asp:BoundField DataField="Pcs" HeaderText="Pcs" SortExpression="Pcs" />
                        <asp:BoundField DataField="Box" HeaderText="Box" ReadOnly="True" SortExpression="Box" />
                        <asp:BoundField DataField="CTNSize" HeaderText="CTNSize" ReadOnly="True" SortExpression="CTNSize" />
                        <asp:BoundField DataField="Trade Price" HeaderText="Trade Price" SortExpression="Trade Price" />
                        <asp:BoundField DataField="Dis" HeaderText="Dis" ReadOnly="True" SortExpression="Dis" />
                        <asp:BoundField DataField="After Discount" HeaderText="After Discount" ReadOnly="True" SortExpression="After Discount" />
                    </Columns>
                </asp:GridView>

                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:HumBrosConnectionString2 %>" SelectCommand="SELECT * FROM [v_salDrecipt]"></asp:SqlDataSource>

            </div>
            <div class="left">
                <div class="shpkep">
                ____________________ <br />
                ShopKeeper Signature
                </div>
                <div class="salemansig">
                ____________________ <br />
                Amount Recieved by<br />
                SalesMan Signature
                </div>
            </div>
            <div class="right">
	            Current Net Payable:<br /> 
	            Previous Outstanding: 
            </div>		
        </div>
    </div>
    </form>
</body>
</html>
