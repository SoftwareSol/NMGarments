<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt_salinv.aspx.cs" Inherits="Foods.rpt_salinv" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sales Invoice</title>
    <style type="text/css">
	    body{
	
		    background:#fff;
		    margin:0;
		    padding:20px;
		    font-family:Arial;
		    font-size:12px;
	    }
        .grid {
            text-align:center;
        }
	    #Container{
		    width:100%;
		    height:800px auto;
		    border:1px solid #000;
	    }
	    .header{
		    width:100%;
		    height:auto;
	    }
	    .header_left
	    {
		    width:50%;
		    height:auto;
		    float:left;
	    }
	    .header_right
	    {
		    width:30%;
            padding:80px 0px 0px 40px;
		    height:auto;
		    float:right;
		    text-decoration:underline;
	    }
	    .logo
	    {
		    width:300px;
		    height:180px;
	    }
	    .small{
	
		    width:100%;
		    height: 30px;
		    font-size:12px;
		    background:#bfbfbf;
	    }
	    .content
	    {
		    width:94%;
		    height:auto;
		    padding:30px;
	    }
	    .heading
	    {
		    width:100%;
		    height:auto;
		    text-decoration:underline;
		    text-align:center;
		    font-style:italic;
	    }
	    .left{
		    width:49%;
		    height:auto;
		    float:left;
	    }
	    .low_left{
		    width:50%;
		    height:auto;
		    float:left;
	    }
	    .custadd
	    {
		    width:300px;
		    height:80px;		 
	    }
	    .right{
		    width:49%;
		    height:auto;
		    float:right;
	    }
	    .low_right{
		    width:29%;
		    height:auto;
		    float:right;
		    padding:180px 0px 40px 130px;
	    }
	    .clear{
		    clear:both;
	    }
	    table{
		    width:100%;
		    height:auto;
		    font-size:12px;
		    border:1px solid #000;
	    }
	    tr td {
		    font-size:12px;	
		    border:1px solid #000;
	    }
	    #Main
	    {
		    width:100%;
		    height:auto;
	    }
	    #Footer
	    {
		    width:100%;
		    font-size:14px;
		    height:auto;
		    text-align:center;
	    }
	    .end
	    {
		    width:100%;
		    height:30px;
		    background:green;
	    }
	    p{
		    padding:0px;
		    margin:0px;
	    }

    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="Div1">
	        <div class="header">
		        <div class="header_left">
                    <h1>NM Garments</h1>
		        </div>
		        <div class="header_right">
                    <b></b>
		        </div>
	        </div>
	        <div class="clear"></div>
	        <div class="content">
		        <div class="heading">
			        <h1>Sales Invoice</h1>
		        </div>
		        <div class="left">
			        <h4></h4>
                    <div class="custadd">
                        <br />
	                    <br />
                        Customer:<u><asp:Label ID="lbl_intro" runat="server" ></asp:Label></u>
                        <br />
                        Address:<u><asp:Label ID="lbladd" runat="server" ></asp:Label></u>
                        <br />
                        Mobile No:<u><asp:Label ID="lblph" runat="server" ></asp:Label></u>
			        </div>
		        </div>
		        <div class="right">
			        <table style="float:right; width:50%; height:auto;">
				        <tr>
                            <td>Bill No : <asp:Label ID="lblbilno" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td> Date : <asp:Label ID="lblsaldat" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td> Sales Return : <asp:Label ID="lbl_salreturn" runat="server"></asp:Label></td>
                        </tr>
			        </table>
		        </div>	
		        <div class="clear"></div>	
		        <br/>
		        <div id="Main">
                <asp:GridView ID="GVSal" runat="server" AutoGenerateColumns="False" CssClass="grid" DataKeyNames="MSal_id" >
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="SNO" ReadOnly="True" SortExpression="ID" />
                        <asp:BoundField DataField="Products" HeaderText="Product Code / Description" ReadOnly="True" SortExpression="Products" />
                        <asp:BoundField DataField="Pcs" HeaderText="Qty" SortExpression="Pcs" />
                        <%--<asp:BoundField DataField="Box" HeaderText="Box" ReadOnly="True" SortExpression="Box" />
                        <asp:BoundField DataField="CTNSize" HeaderText="CTNSize" ReadOnly="True" SortExpression="CTNSize" />--%>
                        <asp:BoundField DataField="rat" HeaderText="Rate" SortExpression="rat" />
                        <asp:TemplateField HeaderText="Net Amount">
                            <ItemTemplate>
                                <asp:Label ID="lbl_afterdis" runat="server"  Text='<%# Eval("NetAmount")%>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
		        </div>	
                <div class="left">
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    ____________________ <br />
                    ShopKeeper Signature
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    ____________________ <br />
                    Amount Recieved by<br />
                    SalesMan Signature
                    </div>
                </div>
                <div class="right">                    
                    <table style="width:200px; height:auto; border:0px; float:right; margin-right:30px;" border="0">
                        <tr>
                            <td>Gross Amount:  <asp:Label ID="lbl_grrsttl" runat="server"  Text="0"></asp:Label> Only-</td>
                        </tr>
                        <tr>
                            <td>Discount:  <asp:Label ID="lbldisc" runat="server" Text="0" ></asp:Label> %</td>
                        </tr>     
                        <tr>
                            <td>Discount Amount:  <asp:Label ID="lbldiscamt" runat="server" Text="0" ></asp:Label> Only-</td>
                        </tr>
                        <tr>
                            <td>Current Net Payable:  <asp:Label ID="lb_currnetpay" runat="server" Text="0" ></asp:Label> Only</td>
                        </tr>                        
                        <tr>
                            <td>Previous Outstanding: <asp:Label ID="lb_preout" runat="server"  Text="0" ></asp:Label> Only-</td>
                        </tr>
                    </table>
                </div>		
		        <br />
		        <br />
		        <div class="clear"></div>	
	        <br />
	        <br />
	        <br />
	        <br />
	        <br />
	        <br />
	        <br />
	        <br />
	        <br />
            <div id="Footer">
		        <div class="end"></div>
	        </div>
        </div>            
    </div>
    </form>
</body>
</html>
