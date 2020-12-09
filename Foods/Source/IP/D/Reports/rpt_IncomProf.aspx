<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt_IncomProf.aspx.cs"
     Inherits="Foods.rpt_IncomProf" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

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
		    height:800px auto;
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
			            <h1>General Ledger</h1>
		            </div>
		            <div class="left">
			            <h4></h4>
                        <div class="custadd">
                            <br />
	                        <br />
			            </div>
		            </div>
		            <div class="right">
			            <table style="float:right; width:50%; height:auto;">
				            <tr>
                                <td>From Date : <asp:Label ID="lblfrmdat" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>To Date : <asp:Label ID="lbltodat" runat="server"></asp:Label></td>
                            </tr>
			            </table>
		            </div>	
		            <div class="clear"></div>	
		            <br/>
		            <div id="Main">
                        <asp:GridView ID="GVIncomExp" runat="server" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found.." AutoGenerateColumns="False" CssClass="grid" DataKeyNames="accno" >
                            <Columns>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_dat" runat="server" Text='<%# Eval("expensesdat")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Account No">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_accno" runat="server" Text='<%# Eval("accno")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Account Title">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_accttl" runat="server" Text='<%# Eval("acctitle")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Recived">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_pay" runat="server" Text='<%# Eval("PaymentIn")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payments">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_incom" runat="server" Text='<%# Eval("PaymentOut")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>     
                                <asp:TemplateField HeaderText="Opening Balance">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_openbal" runat="server" Text='<%# Eval("openingBal")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                         
                                <asp:TemplateField HeaderText="Current Balance">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_availbal" runat="server" Text='<%# Eval("OpenBal")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <br />
                        <table style="width:200px; height:auto; border:0px; float:right; margin-right:30px;" border="0">
                            <tr>
                                <td><asp:Label ID="lbl_ttl" runat="server"></asp:Label> : <asp:Label ID="lblttl" runat="server" ></asp:Label></td>
                            </tr>     
                        </table>
		            </div>	
                    <div class="left">
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        ____________________ <br />
                        Verified By
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        </div>
                    </div>
                    <div class="right">                    
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
		            <p><b>OFFICE ADDRESS: </b> Office # 208 2nd floor City Center, Bombay Baza,r Ameer Aslam Guest House Karachi, Pakistan.</p>
		            <p>arif@smpgrouppk.com&nbsp;&nbsp;&nbsp;&nbsp;Mob 1 # +923212382306&nbsp;&nbsp;&nbsp;&nbsp;Mob 2 # +923219237308</p>
		            <div class="end"></div>
	            </div>
            </div>                

    </div> 
    </form>
</body>
</html>
