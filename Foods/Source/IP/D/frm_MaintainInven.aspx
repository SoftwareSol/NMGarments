<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="frm_MaintainInven.aspx.cs" Inherits="Foods.frm_MaintainInven" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        <style type="text/css">
                 /* Modal popUp Start */

        .modalBackground{
                background-color: #000000;
                filter: alpha(opacity=10);
                opacity: 0.7;
        }
         .modalBackgroundSupplier {
                 background-color: #000000;
                filter: alpha(opacity=10);
                opacity: 0.7;
         }
        .modalBackground1{
                width: 500px;
                height: 500px;
                background-color: #000000;
                filter: alpha(opacity=10);
                opacity: 0.6;
        }
        .modalPopup{
                border: 3px solid #000000;
                background-color: #FFFFFF;
                margin-top: 0px;
                color: #000000;
                margin-right: -3px;
                margin-bottom: 0px;
        }

        .modalPopup1{
                width:202px;
                height:140px;
                border: 3px solid #000000;
                background-color: #FFFFFF;
                color: #FF0000;
                margin-right: -3px;
                text-align: center;
                margin-left: -20px;
                margin-top: -80px;
        }
        .closebtn {
                float:right;
                }

        /* Modal popUp End */

            .completionList {

        border:solid 1px Gray;
        margin:0px;
        padding:3px;
        height: 120px;
        overflow:auto;  
        background-color: #FFFFFF;     
    } 
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
            <ul class="breadcrumb">
				<li>
					<i class="icon-home"></i>
					<a href="index.html">Inventory</a> 
					<i class="icon-angle-right"></i>
				</li>
				<li><a href="frm_MaintainInven.aspx">Maintain Inventory</a></li>
			</ul>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>			
			            <div class="row-fluid sortable">
				            <div class="box span12">
					            <div class="box-header" data-original-title>
						            <div class="box-icon">
						            </div>
                                    <p>Maintain Inventory</p>
					            </div>
					            <div class="box-content">
                                    <div class="control-group span12">
                                        <label class="control-label" for="TB_SearchCust">Enter Product Name: </label>
                                        <div class="controls">
                                            <asp:TextBox ID="TB_SearchCust" runat="server" placeholder="Enter Product Name..." AutoPostBack="true" OnTextChanged="TB_SearchCust_TextChanged" ></asp:TextBox>
                                            <asp:AutoCompleteExtender ServiceMethod="Getpro" CompletionListCssClass="completionList"
                                                    CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10" TargetControlID="TB_SearchCust" ID="AutoCompleteExtender3"  
                                                    runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
                                        </div>
                                    </div>
						            <div class="span12"><asp:Label ID="lblerr" runat="server" ForeColor="Red"></asp:Label></div>					         
							        <fieldset>
                                        <div class="scrollit">
                                            <asp:GridView ID="GVSrchCust" ShowHeader="true" AllowPaging="true" PageSize="10" CssClass="table table-striped table-bordered" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" DataKeyNames="Dstk_id" runat="server"  OnRowCancelingEdit="GVSrchCust_RowCancelingEdit" OnRowEditing="GVSrchCust_RowEditing" OnRowUpdating="GVSrchCust_RowUpdating" OnPageIndexChanging="GVSrchCust_PageIndexChanging" OnRowDataBound="GVSrchCust_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField>  
                                                        <ItemTemplate>  
                                                            <asp:Button ID="btn_Edit" CssClass="btn btn-primary" runat="server" Text="Change" CommandName="Edit" />  
                                                        </ItemTemplate>  
                                                        <EditItemTemplate>  
                                                            <asp:Button ID="btn_Update" runat="server" Text="Update" CommandName="Update"/>  
                                                            <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel"/>  
                                                        </EditItemTemplate>  
                                                    </asp:TemplateField>  
                                                    <asp:TemplateField HeaderText="PRODUCT NAME">  
                                                        <ItemTemplate>  
                                                            <asp:Label ID="lbl_pro" runat="server" Text='<%#Eval("ProductName") %>'></asp:Label>  
                                                        </ItemTemplate>  
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lbl_Proid" runat="server" Text='<%#Eval("productid") %>' Visible="false"></asp:Label>                                                            
                                                            <asp:DropDownList ID="DDL_Pro" runat="server" Width="100"></asp:DropDownList>
                                                            <asp:AutoCompleteExtender ServiceMethod="Getpro" CompletionListCssClass="completionList"
                                                                CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10" TargetControlID="TB_SearchCust" ID="AutoCompleteExtender3"  
                                                                runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
                                                        </EditItemTemplate>  
                                                    </asp:TemplateField>  
                                                    <asp:TemplateField HeaderText="RATE">  
                                                        <ItemTemplate>  
                                                            <asp:Label ID="lbl_rat" runat="server" Text='<%#Eval("Dstk_rat") %>'></asp:Label>  
                                                        </ItemTemplate>  
                                                        <EditItemTemplate>  
                                                            <asp:TextBox ID="txt_rat" runat="server" Text='<%#Eval("Dstk_rat") %>' Width="100" ></asp:TextBox>  
                                                        </EditItemTemplate>  
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="QTY">  
                                                        <ItemTemplate>  
                                                            <asp:Label ID="lbl_qty" runat="server" Text='<%#Eval("Dstk_Qty") %>'></asp:Label>  
                                                        </ItemTemplate>  
                                                        <EditItemTemplate>  
                                                            <asp:TextBox ID="txt_qty" runat="server" Text='<%#Eval("Dstk_Qty") %>' Width="100" ></asp:TextBox>  
                                                        </EditItemTemplate>  
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SIZE">  
                                                        <ItemTemplate>  
                                                            <asp:Label ID="lbl_size" runat="server" Text='<%#Eval("Dstk_unt") %>'></asp:Label>  
                                                        </ItemTemplate>  
                                                        <EditItemTemplate>  
                                                            <asp:TextBox ID="tb_size" runat="server" Text='<%#Eval("Dstk_unt") %>' Width="100" ></asp:TextBox>  
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>No Record Available</EmptyDataTemplate> 
                                            </asp:GridView>
                                        </div>
						            </fieldset>
					            </div>
				            </div>
                            <!--/span-->
			            </div>	
                    </ContentTemplate>
                </asp:UpdatePanel>
</asp:Content>
