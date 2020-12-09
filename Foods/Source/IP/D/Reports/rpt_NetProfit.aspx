<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt_NetProfit.aspx.cs" Inherits="Foods.rpt_NetProfit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
			            <h1>Net Profit Sheet</h1>
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
                                <td>Date : <asp:Label ID="lbl_dat" runat="server"></asp:Label></td>
                            </tr>
			            </table>
		            </div>	
		            <div class="clear"></div>	
		            <br/>
		            <div id="Main">
                        <asp:GridView ID="GVProf" runat="server" AutoGenerateColumns="False" CssClass="grid" 
                             >
                            <Columns>
                                <asp:TemplateField HeaderText="Purchase">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_prrat" runat="server"  Text='<%# Eval("PurRat")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sale">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_salrat" runat="server" Text='<%# Eval("SalAmount")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Expence">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_exp" runat="server" Text='<%# Eval("Expence")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                 <asp:TemplateField HeaderText="Profit">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_prof" runat="server" Text='<%# Eval("Profit")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>       
                                <asp:TemplateField HeaderText="Net Profit">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_netprof" runat="server" Text='<%# Eval("NetProf")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <table style="width:200px; height:auto; border:0px; float:right; margin-right:30px;" border="0">
                            <tr>
                                <td>Total Profit  <asp:Label ID="lbl_proff" runat="server" ></asp:Label></td>
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
