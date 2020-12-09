<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt_Purchase.aspx.cs" Inherits="Foods.rpt_Purchase" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Purchase Report</title>
    <style type="text/css">
	    body{
	
		    background:#fff;
		    margin:0;
		    padding:20px;
		    font-family:Arial;
		    font-size:12px;
	    }
	    #Container{
		    width:100%;
		    height:940px;
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
            padding:80px 0px 0px;
		    height:auto;
		    float:right;
		    text-decoration:underline;
	    }
	    .logo
	    {
		    width:300px;
		    height:150px;
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
            <div id="Container">
	        <div class="header">
		        <div class="header_left">
                    <h1>NM Garments</h1>
			        <%--<img src="../../../../img/logo.png" alt="logo" class="logo" />--%>
		        </div>
		        <div class="header_right">
                    <b></b>
		        </div>
	        </div>
	        <div class="clear"></div>
	        <div class="content">
		        <div class="heading">
			        <h1>PURCHASE INVOICE</h1>
		        </div>
		        <div class="left">
			        <h4>Supplier:</h4>
			        <div class="custadd">
                        <b>Name:</b>
    			        <asp:Label ID="LBLShpTo" runat="server"></asp:Label>
                        <br />
                        <b>Address:</b>
    			        <asp:Label ID="lbladd" runat="server"></asp:Label>
			        </div>
		        </div>
		        <div class="right">
			        <table style="float:right; width:50%; height:auto;">
				        <tr>
					        <td>Voucher</td>
					        <td><asp:Label ID="lbl_vouc" runat="server" Text=""></asp:Label></td>
				        </tr>
				        <tr>
					        <td>Date</td>
					        <td><asp:Label ID="lbl_dat" runat="server" Text=""></asp:Label></td>
				        </tr>
			        </table>
		        </div>	
		        <div class="clear"></div>	
		        <br/>
		        <div id="Main">
                    <asp:GridView ID="GVShowpurItms" CssClass="table" style="text-align:center;" runat="server" EmptyDataText="Sorry No Record Exits" AutoGenerateColumns="false" DataKeyNames="MPurID,ProductID" >
                        <Columns>
                            <asp:BoundField DataField="productname" HeaderText="Description" SortExpression="productname" ReadOnly="True" />
                            <asp:BoundField DataField="Rate" HeaderText="Rate" SortExpression="Rate" ReadOnly="True" />
                            <%--<asp:BoundField DataField="Brand" HeaderText="Brand" SortExpression="Brand" ReadOnly="True" />--%>
                            <asp:BoundField DataField="Qty" HeaderText="Quantity in Pcs" SortExpression="Qty" ReadOnly="True" />
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblamt" runat="server" Text='<%# Eval("Amount")%>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
		        </div>	
		        <br />
                <table style="width:200px; height:auto; float:right">
                    <%-- <tr>
                         <td><label>Transport:</label></td>
                         <td><asp:Label ID="lbl_Fright" runat="server" Text="0"></asp:Label>&nbsp;Only/=</td>
                     </tr>
                     <tr>
                         <td><label>Other:</label></td>
                         <td><asp:Label ID="lbloth" runat="server" Text="0"></asp:Label>&nbsp;Only/=</td>
                     </tr>
				    <tr>
					    <td><label>Net Total:</label></td>
					    <td style="border-bottom:1px solid #000">
                            <asp:Label ID="lbl_ttl" runat="server" Text="0"></asp:Label>&nbsp;Only/=
					    </td>
				    </tr>--%>				
				    <tr>
					    <td><label>Gross Total:</label></td>
					    <td style="border-bottom:1px solid #000">
                            <asp:Label ID="lblgrssttl" runat="server" Text="0"></asp:Label>&nbsp;Only/=
					    </td>
				    </tr>				
                </table>
		        <br />
		        <br />
		        <div class="low_left">
		        </div>
		        <div class="low_right">
                    <b>Verified By..</b><br />
                    <u><asp:Label ID="lbl_usr" runat="server" Text=""></asp:Label></u>
		            <br />
                    <asp:Label ID="lbl_mob" runat="server" Text=""></asp:Label>
		        </div>
		        <div class="clear"></div>	
	        </div>
	        <br />
	        <br />
	        <br />
		        <div id="Footer">
		        <p></p>
		        <p></p>
		        <div class="end"></div>
	        </div>
        </div>
    </form>
</body>
</html>
