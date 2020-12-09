<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frm_VDsr.aspx.cs" Inherits="Foods.frm_VDsr" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <ul class="breadcrumb">
	    <li>
		    <i class="icon-home"></i>
		    <a href="WellCome.aspx">Home</a> 
		    <i class="icon-angle-right"></i>
	    </li>
        <li>
            <a href="#">Sales</a> 
		    <i class="icon-angle-right"></i>
        </li>
	    <li><a href="frm_VDsr.aspx">Verify DSR</a></li>
    </ul>
    <div class="row-fluid sortable">
	    <div class="box span12">
			<div class="box-header" data-original-title>
				<h2><i class="halflings-icon align-justify"></i><span class="break"></span>v dsr</h2>
				<div class="box-icon">
				</div>
			</div>
			<div class="box-content">
                <div class="span12">
                    <div style="text-align:center">
                        <asp:Label ID="lbl_err" Style="" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                 </div>
                <div class="span12">
                    <div class="control-group  span10">                                                  
					    <label class="control-label span2" for="appendedInputButton">Search</label>
					    <div class="controls">
                                <div class="input-append span4">
                                    <asp:TextBox ID="TBSDSR" runat="server" size="16" AutoPostBack="true" placeholder="Search DSR..." CssClass="Search"   OnBlur="DisplayLoadingImage();" /><asp:Button ID="SeacrhBtn"  runat="server" CssClass="btn" Text="GO!" OnClick="SeacrhBtn_Click"/>
    						    </div>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <div class="span2">
                                <!--<a href="#" class="btn btn-small btnadd btn-primary"  onclick="ShowRequisitions();" runat="server" id="btnadd" ><i class="icon-plus"></i> <b>Add New Requisitions</b></a>-->
                            </div>
					    </div>
				    </div>
                 </div>
                <div class="box span12">
					<div class="box-header" data-original-title>
				        <h2><i class="halflings-icon align-justify"></i><span class="break"></span>Daily Sales Report</h2>
                        <center><asp:Label ID="lb_error" runat="server" /></center>
				        <div class="box-icon">
					        <a href="#" class="btn-minimize"><i class="halflings-icon chevron-up"></i></a>
					        <a href="#" class="btn-close"><i class="halflings-icon remove"></i></a>
				        </div>
			        </div>	
                    <div class="box-content">                    
                        <div class="scrollit">
                            <div class="div_print">
                                <asp:GridView ID="GVDSR" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="10" ShowHeaderWhenEmpty="True" CssClass="table table-striped table-bordered" DataKeyNames="dsrdat,Username" OnPageIndexChanging="GVDSR_PageIndexChanging" OnRowCommand="GVDSR_RowCommand" OnRowDataBound="GVDSR_RowDataBound" >
                                    <Columns>                                        
                                        <asp:BoundField DataField="dsrdat" HeaderText="Date" SortExpression="dsrdat" />                                       
                                        <asp:BoundField DataField="Username" HeaderText="Booker Name" SortExpression="Username" />                                        
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkshowdsr" CommandName="showdsr" runat="server"  > DSR </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkloadsht" CommandName="loadsht"  runat="server" > Load Sheet </asp:LinkButton>
                                                <asp:HiddenField ID="HFDSRID" runat="server" Value='<%# Eval("dsrdat")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hflock" runat="server" Value='<%# Eval("Isdon")%>' />
                                                <asp:LinkButton ID="lnkDon" CommandName="IsDon"  runat="server" Text="Lock" ></asp:LinkButton>                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>                                        
                                    </Columns>
                                </asp:GridView> 
                            </div>
                        </div>
                    </div>		
                </div>                                                
            </div>
        </div>	    
    </div>
</asp:Content>
