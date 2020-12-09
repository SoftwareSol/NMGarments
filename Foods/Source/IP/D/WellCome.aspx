<%@ Page Title="WelCome" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="WellCome.aspx.cs" Inherits="Foods.WellCome" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
            /* Scroller Start */
        .scrollit {
            overflow:scroll;
            height:300px;
	        width:100%;           
	        margin:0px auto;
        }
      /* Scroller End */

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <noscript>
    <div class="alert alert-block span10">
    <h4 class="alert-heading">Warning!</h4>
    <p>You need to have <a href="http://en.wikipedia.org/wiki/JavaScript" target="_blank">JavaScript</a> enabled to use this site.</p>
    </div>
    </noscript>
    <ul class="breadcrumb">
        <li>
        <i class="icon-home"></i>
            <a href="WellCome.aspx">Home</a> 
        <i class="icon-angle-right"></i>
        </li>
        <li><a href="#">Dashboard</a></li>
    </ul>
    <asp:Panel ID="pnlchkusr" runat="server">
    </asp:Panel>
    <div class="row-fluid">
        <%--<asp:GridView ID="GV_Curr" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Exits!!"  class="table table-striped table-bordered" OnRowDeleting="GVItms_RowDeleting" OnRowDataBound="GVItms_RowDataBound" OnRowCommand="GVItms_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="Currency" Visible="false">  
                    <ItemTemplate>
                        <asp:DropDownList ID="DDL_Curr" runat="server" ></asp:DropDownList>
                        <asp:Label ID="lbl_Curr" runat="server" Visible="false" Text='<%# Eval("Currency") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Rate">
                    <ItemTemplate>
                        <asp:TextBox ID="TBCurrRat" runat="server" Text='<%# Eval("CURRRATE")%>' style="width:50px; height:20px; background:none; border:none;"></asp:TextBox>                                                        
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Percentage" Visible="false">
                    <ItemTemplate>
                        <asp:TextBox ID="TBDIS" runat="server" Text='<%# Eval("DIS")%>' style="width:50px; height:20px; background:none; border:none;" AutoPostBack="true" OnTextChanged="TBDIS_TextChanged"></asp:TextBox>                                                        
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="QTY Available">
                    <ItemTemplate>
                        <asp:Label ID="lblItmQty" runat="server" Text='<%# Eval("QTYAVAIL")%>' />                                                        
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>--%>
        <h3>WelCome <asp:Label ID="lblUserName" runat="server"></asp:Label> to <asp:LinkButton ID="lbl_compnam" runat="server"></asp:LinkButton> </h3>
    </div>	
    <asp:Panel ID="pnl_curr" runat="server">
        <div class="row-fluid">
            <div class="span12"><span style="width:100%; height:20px; text-align:center;"><asp:Label ID="lbl_err" ForeColor="Red" runat="server"></asp:Label></span></div>
            <div class="span12">
                <div class="span3"><asp:Label ID="lbl_curr" runat="server" Text="Currency:" ></asp:Label>&nbsp;&nbsp;<asp:DropDownList CssClass="span11" ID="DDL_Curr" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_Curr_SelectedIndexChanged"></asp:DropDownList></div>
                <div class="span3"><asp:Label ID="lbl_Cuurat" runat="server" Text="Rate:" ></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="TBCurrRat" CssClass="span11" runat="server" placeholder="Enter Currency Rate.." ></asp:TextBox></div>
                <div class="span4"><asp:Label ID="lbl_exchrat" runat="server" Text="Exchange Rate:" ></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="TBExchgRat" CssClass="span11" runat="server" placeholder="Enter Exchange Rate.." ></asp:TextBox></div>
                <div class="span2"><asp:Button ID="btn_CurrSav" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btn_CurrSav_Click" /></div>
        </div>
    </asp:Panel>
    </div>
    <div class="row-fluid sortable">		
        <asp:Panel ID="pnlsalchart" runat="server">
            <div class="box span6">
				<div class="box-header">
					<h2><i class="halflings-icon list-alt"></i><span class="break"></span>Monthly Sales</h2>
					<div class="box-icon">
						<a href="#" class="btn-setting"><i class="halflings-icon wrench"></i></a>
						<a href="#" class="btn-minimize"><i class="halflings-icon chevron-up"></i></a>
						<a href="#" class="btn-close"><i class="halflings-icon remove"></i></a>
					</div>
				</div>
				<div class="box-content">
                    <asp:Chart ID="Chart1" runat="server" Height="300px" Width="400px" >
                        <Titles>
                            <asp:Title ShadowOffset="3" Name="Items" />
                        </Titles>
                        <Legends>
                            <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Sales Per Day" LegendStyle="Row" />
                        </Legends>
                        <Series>
                            <asp:Series Name="Sales Per Day" />
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1" BorderWidth="0" />
                        </ChartAreas>
                    </asp:Chart>
				</div>
			</div>
        </asp:Panel>
        <asp:Panel ID="pnlchkpro" runat="server">
            <div class="box black span6 noMargin" onTablet="span12" onDesktop="span6">                
				    <div class="box-header">
					    <h2>Items Less Then 5</h2>
					    <div class="box-icon">
						    <a href="#" class="btn-minimize"><i class="halflings-icon white chevron-up"></i></a>
						    <a href="#" class="btn-close"><i class="halflings-icon white remove"></i></a>
					    </div>
				    </div>
				    <div class="box-content">                    
					    <div class="todo metro">                            
                           
                            <asp:Repeater ID="RepterDetails" runat="server">  
                                <HeaderTemplate>  
                                <ul class="todo-list">
                                </HeaderTemplate>  
                                <ItemTemplate>                                     
                                    <li class="red">
                                        <asp:Label ID="lblUser" runat="server" Font-Bold="true" Text='<%#Eval("ProductName") %>'/> 
                                        <strong><asp:Label ID="lblDate" runat="server" Font-Bold="true" Text='<%#Eval("Dstk_Qty") %>'/></strong>
                                    </li>                                    
                                </ItemTemplate>  
                                <FooterTemplate>  
                                </ul> 
                                </FooterTemplate>  
                            </asp:Repeater>
                        					
                    </div>	
				</div>               
			</div>
        </asp:Panel>
        <asp:Panel ID="pnlDayEnd" runat="server">
			<div class="box span6">
				<div class="box-header" data-original-title>
					<h2><i class="halflings-icon align-justify"></i><span class="break"></span>Day End</h2>
					<div class="box-icon">
					</div>
				</div>
				<div class="box-content">
                    <div class="span8">
                        <div class="control-group  span12">							
						    <div class="controls">                                                                
                                <div class="span5"></div>
                                <div class="span6">
                                    <div class="span5">
                                        <div class="item">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Day End!!..." CssClass="btn btn-primary" OnClick="btnSubmit_Click"  />
					                    </div>
                                    </div>                          
                 		        </div>
				            </div>
                        </div>
                    </div>
                </div>
            </div>		
        </asp:Panel>
	</div><!--/row-->    

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
