<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt_Profit.aspx.cs" Inherits="Foods.rpt_Profit" %>

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
			            <img src="../../../../img/logo.png" alt="logo" class="logo" />
		            </div>
		            <div class="header_right">
                        <b>NTN# 7592966-1</b>
		            </div>
	            </div>
	            <div class="clear"></div>
	            <div class="content">
		            <div class="heading">
			            <h1>Profit Sheet</h1>
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
                        <asp:GridView ID="GVProf" runat="server" AutoGenerateColumns="False" CssClass="grid" DataKeyNames="MSal_id,MPurID,ProductID"  >
                            <Columns>
                                <asp:TemplateField HeaderText="Product">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_pro" runat="server"  Text='<%# Eval("productname")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sale Invoice No">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_salid" runat="server" Text='<%# Eval("MSal_id")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>       
                                <asp:TemplateField HeaderText="Sale Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_satrat" runat="server" Text='<%# Eval("rat")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Disc %">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_disc" runat="server" Text='<%# Eval("disc")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Disc Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_discamt" runat="server" Text='<%# Eval("disamt")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Purchase Price">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_purpric" runat="server" Text='<%# Eval("dstk_rat")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Diff.">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_diff" runat="server" Text='<%# Eval("Diff")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <table style="width:200px; height:auto; border:0px; float:right; margin-right:30px;" border="0">
                            <tr>
                                <td>Total  <asp:Label ID="lblttl" runat="server" ></asp:Label></td>
                            </tr>     
                            <tr>
                                <td>Discount  <asp:Label ID="lbldisc" runat="server" ></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Discount Amount  <asp:Label ID="lbl_discamt" runat="server" ></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Total Difference  <asp:Label ID="lbl_diff" runat="server" ></asp:Label></td>
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
