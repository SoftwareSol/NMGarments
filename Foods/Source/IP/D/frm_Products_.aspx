<%@ Page Title="Products" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
     CodeBehind="frm_Products_.aspx.cs" Inherits="Foods.frm_Products_" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <style type="text/css">
            /* Scroller Start */
        .scrollit {
            overflow:scroll;
            height:100%;
	        width:100%;           
	        margin:0px auto;
        }
      /* Scroller End */
      /* Modal SalespUp Start */

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
        .modalSalespup{
                border: 3px solid #000000;
                background-color: #FFFFFF;
                margin-top: 0px;
                color: #000000;
                margin-right: -3px;
                margin-bottom: 0px;
        }

        .modalSalespup1{
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

        /* Modal SalespUp End */
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
             <ContentTemplate>
                <ul class="breadcrumb">
                    <li><i class="icon-home"></i><a href="WellCome.aspx">Home</a><i class="icon-angle-right"></i></li>
                    <li><a href="#">SetUp</a><i class="icon-angle-right"></i></li>
                    <li><a href="frm_Products.aspx">Products</a></li>
                </ul>
                <!-- imageLoader - START -->
                <img id='HiddenLoadingImage' src="../../img/page-loader.gif" class="LoadingProgress" />
                <!-- imageLoader - END -->
                <div class="row-fluid">
                    <div class="box  span12">
                        <%--<div class="box-header" data-original-title>
                            <h3><i class="halflings-icon edit"></i><span class="break"></span> Products </h3>
                        </div>--%>
                        <div class="box-content">
                            <div class="row-fluid">	
                                <div class="span12">
                                    <div class="span5">
                                        <h1><span style="color:black;">Products</span></h1>                                    
                                    </div>
                                    <div class="span12">
                                        <div style="text-align:center">
                                            <asp:Label ID="lbl_err" runat="server" ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="span5">
                                        <div class="control-group">    
                                            Date
                                            <div class="controls">
                                                <asp:TextBox runat="server" class="span10" ID="TBProdat" placeholder="Date..." ></asp:TextBox>
                                                <asp:CalendarExtender ID="CETBFDWise" PopupButtonID="imgPopup" runat="server" TargetControlID="TBProdat" Format="yyyy-MM-dd" />
                                                <asp:RequiredFieldValidator ID="RFDat" runat="server" ForeColor="Red"
                                                    ControlToValidate="TBProdat" ValidationGroup="VGPro"
                                                    ErrorMessage="Please Enter the Date!"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="CVdat" Type="Date" Operator="DataTypeCheck"
                                                    ControlToValidate="TBProdat" runat="server" ValidationGroup="VGPro"
                                                    ForeColor="Red" ErrorMessage="Please Write Correct Date!"></asp:CompareValidator>
                                            </div>
                                        </div>
                                    </div>
                                     <div class="span12">
                                        <div class="control-group">    
                                            Items
                                            <div class="controls">
                                                <asp:DropDownList ID="DDL_Itm" runat="server" data-rel="chosen" AutoPostBack="true" OnSelectedIndexChanged="DDL_Itm_SelectedIndexChanged" ></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="scrollit">
                                        <div class="row-fluid">	
                                            <div class="span12">
                                                <div class="box-content span12">                            
                                                    <asp:GridView ID="GVPro" runat="server" AutoGenerateColumns="False"  class="table table-striped table-bordered" OnRowDeleting="GVPro_RowDeleting" OnRowDataBound="GVPro_RowDataBound" >
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ITEM NAME">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="TBItmNam" runat="server" Text='<%# Eval("ITEMNAME")%>' placeholder="Item Name..." style="width:100px; height:20px; background:none; border:none;" ></asp:TextBox>
                                                                </ItemTemplate>                                        
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ITEM TYPE">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="DDL_Itmtyp" runat="server" data-rel="chosen" ></asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="UNITS">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="DDL_Unt" runat="server" data-rel="chosen" ></asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="PURCHASE PRICE">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="TBPurPrc" runat="server"  Text='<%# Eval("PURCHASEPRICE")%>' placeholder="Purchase Price..." style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>
                                                                </ItemTemplate>                                      
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="SALE PRICE">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="TBSalPrc" runat="server"  Text='<%# Eval("SALEPRICE")%>' placeholder="Sale Price..." style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>
                                                                </ItemTemplate>                                      
                                                            </asp:TemplateField>
                                                            <%--<asp:TemplateField HeaderText="RETAIL PRICE">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="TBRtlPrc" runat="server"  Text='<%# Eval("RETAILPRICE")%>' placeholder="Retail Price..." style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>
                                                                </ItemTemplate>                                      
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="PACKING SIZE">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="TBpksiz" runat="server"  Text='<%# Eval("PACKINGSIZE")%>' placeholder="Packing Size..." style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="PACKETS IN CARTONS">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="TBcartsiz" runat="server"  Text='<%# Eval("PACKINCARTONS")%>' placeholder="Packing In Cartons..." style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="linkbtnadd" OnClick="linkbtnadd_Click"  runat="server" ValidationGroup="Pro" >+</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:CommandField ShowDeleteButton="True" DeleteText="-"  >
                                                                <ControlStyle CssClass="halflings-icon minus-sign" />
                                                            </asp:CommandField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%--<asp:CommandField ShowDeleteButton="True" DeleteText="-"   >
                                                            <ControlStyle CssClass="halflings-icon minus-sign" />
                                                        </asp:CommandField>--%>
                                    <div class="span12"></div>
                                    <div class="span12">
                                        <asp:Button runat="server"  ID="btnSave" Text="Save"  CssClass="btn btn-info" OnClick="btnSave_Click" ValidationGroup="VGPro" />
                                        <asp:Button runat="server"  ID="btnDel" Text="Delete"  CssClass="btn btn-danger" OnClick="btnDel_Click" />                            
                                        <asp:Button runat="server"  ID="btnCancl" Text="Cancel" CssClass="btn" OnClick="btnCancl_Click" />    

                                    </div>  
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="HFProcd" runat="server" />
                <asp:HiddenField ID="HFHead" runat="server" />
                <asp:HiddenField ID="HFSubHead" runat="server" />
                <asp:HiddenField ID="SubHeadCat" runat="server" />
                <asp:HiddenField ID="SubHeadCatFou" runat="server" />
                <asp:HiddenField ID="SubHeadCatFiv" runat="server" />
                <asp:HiddenField ID="HFSubHeadCatFivID" runat="server" />
                <asp:HiddenField ID="HFProID" runat="server" />
                <div id="MyModalDelete" class="modal fade" style="display:none;">
		            <div class="modal-dialog">
		            <div class="modal-content">
			            <div class="modal-header">
				            <h4 class="modal-title">Error!</h4>
			            </div>
			            <!-- dialog body -->
			            <div class="modal-body">
				            <asp:Label ID="lblmodaldelete" runat="server"></asp:Label>
                
			            </div>
			            <!-- dialog buttons -->
                        <div class="modal-footer">
                            <asp:LinkButton ID="linkmodaldelete" runat="server" CssClass="btn btn-success" Text="Yes" OnClick="linkmodaldelete_Click"></asp:LinkButton>
                            <asp:LinkButton ID="linkbtncanceldel" runat="server" CssClass="btn btn-success" Text="No"></asp:LinkButton>
                            <asp:HiddenField ID="HFCustId" runat="server" />
			            </div>
		            </div>
		            </div>
	            </div>
                <div id="ModalAlert" class="modal fade" style="display:none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Error!</h4>
                </div>
                <!-- dialog body -->
                <div class="modal-body">
                    <asp:Label ID="lblalert" runat="server"></asp:Label>                
                </div>
                <!-- dialog buttons -->
                <div class="modal-footer">
                    <asp:LinkButton ID="btnalertOk" runat="server" CssClass="btn btn-success" Text="OK" ></asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
            </ContentTemplate>
         </asp:UpdatePanel>
    
    <script src="Controller/Customers.js"></script>
    <script src="Controller/Common.js"></script>

</asp:Content>
