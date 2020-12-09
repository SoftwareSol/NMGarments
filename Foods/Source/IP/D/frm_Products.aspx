<%@ Page Title="Products" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
     CodeBehind="frm_Products.aspx.cs" Inherits="Foods.frm_Products" %>
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

        
        /* Modal SalespUp End */
        .completionList {

            border:solid 1px Gray;
            margin:0px;
            padding:3px;
            height: 120px;
            overflow:auto;
            background-color: #FFFFFF;     
        } 

        .listItem {

            color: #191919;
        } 

        .itemHighlighted {

            background-color: #ADD6FF;       
        
        }
         .modalBackground
        {
            position: absolute;
            z-index: 6000 !important;
            top: 0px;
            left: 0px;
            background-color: #000;
            filter: alpha(opacity=60);
            -moz-opacity: 0.6;
            opacity: 0.6;
        }

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
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
                    <div class="box-content">
                        <div class="span12">
                            <div class="span5">
                                <h1><span style="color:black;">Products</span></h1>                                    
                            </div>
                            <div class="span12"></div>
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
                            <asp:GridView ID="GVPro" runat="server" AutoGenerateColumns="False"  class="table table-striped table-bordered" OnRowDeleting="GVPro_RowDeleting" OnRowDataBound="GVPro_RowDataBound" >
                            <Columns>
                                <asp:TemplateField HeaderText="Category">
                                    <ItemTemplate>
                                        <%--<asp:DropDownList ID="DDL_Itmtyp" runat="server" data-rel="chosen" ></asp:DropDownList>--%>
                                        <asp:TextBox ID="TBCat" runat="server" Text='<%# Eval("Category")%>' placeholder="Category..." style="width:100px; height:20px; background:none; border:none;" AutoPostBack="true" OnTextChanged="TBCat_TextChanged" ></asp:TextBox>
                                        <asp:AutoCompleteExtender ServiceMethod="GetCat" CompletionListCssClass="completionList"
                                            CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10" TargetControlID="TBCat" ID="AutoCompleteExtender1"  
                                            runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TBItmCode" runat="server" Text='<%# Eval("Pro_Code")%>' placeholder="Item Code..." style="width:100px; height:20px; background:none; border:none;"  ></asp:TextBox>
                                        <%--<asp:Label ID="lblItmCode" runat="server" Text='<%# Eval("Pro_Code")%>'></asp:Label>--%>
                                    </ItemTemplate>                                        
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Name">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TBDesc" runat="server" Text='<%# Eval("Description")%>' placeholder="Description..." style="width:100px; height:20px; background:none; border:none;" AutoPostBack="true"  OnTextChanged="TBDesc_TextChanged" ></asp:TextBox>
                                    </ItemTemplate>                                        
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Brand" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TBBrnd" runat="server" Text='<%# Eval("Brand")%>' placeholder="Brand..." style="width:100px; height:20px; background:none; border:none;" AutoPostBack="true" OnTextChanged="TBBrnd_TextChanged" ></asp:TextBox>
                                        <asp:AutoCompleteExtender ServiceMethod="GetBrnd" CompletionListCssClass="completionList"
                                            CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10" 
                                            TargetControlID="TBBrnd" ID="AutoCompleteExtender2"  
                                            runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>                                    
                                        <%--<asp:DropDownList ID="DDL_Brnd" runat="server" data-rel="chosen" ></asp:DropDownList>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Origin" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TBOrig" runat="server" Text='<%# Eval("Origin")%>' placeholder="Origin..." style="width:100px; height:20px; background:none; border:none;" AutoPostBack="true" OnTextChanged="TBOrig_TextChanged" ></asp:TextBox>
                                            <asp:AutoCompleteExtender ServiceMethod="Getorign" CompletionListCssClass="completionList"
                                            CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10" 
                                            TargetControlID="TBOrig" ID="AutoCompleteExtender3"  
                                            runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
                                        <%--<asp:DropDownList ID="DDL_Origin" runat="server" data-rel="chosen" ></asp:DropDownList>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Units per Packets" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TBPack" runat="server" Text='<%# Eval("Packing")%>' placeholder="Packing..." style="width:100px; height:20px; background:none; border:none;" AutoPostBack="true" OnTextChanged="TBPack_TextChanged" ></asp:TextBox>
                                            <asp:AutoCompleteExtender ServiceMethod="Getpakgsiz" CompletionListCssClass="completionList"
                                            CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10" 
                                            TargetControlID="TBPack" ID="AutoCompleteExtender4"  
                                            runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
                                        <%--<asp:DropDownList ID="DDL_Pack" runat="server" data-rel="chosen" ></asp:DropDownList>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Units" Visible="false">
                                    <ItemTemplate>
                                        <%--<asp:DropDownList ID="DDL_Unt" runat="server" data-rel="chosen" ></asp:DropDownList>--%>
                                        <asp:TextBox ID="TBUnit" runat="server" Text='<%# Eval("Units")%>' placeholder="Units..." style="width:100px; height:20px; background:none; border:none;"  AutoPostBack="true" OnTextChanged="TBUnit_TextChanged" ></asp:TextBox>
                                            <asp:AutoCompleteExtender ServiceMethod="Getunts" CompletionListCssClass="completionList"
                                            CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10" 
                                            TargetControlID="TBUnit" ID="AutoCompleteExtender5"  
                                            runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Purchase Price">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TBPurPriz" runat="server"  Text='<%# Eval("PURCHASEPRICE")%>' placeholder="Trade Price..." style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>
                                    </ItemTemplate>                                   
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sale Price">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TBSalPric" runat="server"  Text='<%# Eval("SALEPRICE")%>' placeholder="Retail Price..." style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>
                                    </ItemTemplate>                                     
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TBrmks" runat="server"  Text='<%# Eval("RMKS")%>' placeholder="Remarks..." style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>                                                   
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
                            <asp:Label ID="v_gvpro" runat="server" Font-Bold="true" ForeColor="Red" Text=""></asp:Label>         
                        </div>
                        <div class="form-actions" style="padding-right:11%;">
                                <asp:Button runat="server"  ID="btnSave" Text="Save"  CssClass="btn btn-info" OnClick="btnSave_Click" ValidationGroup="VGPro" />
                                <asp:Button runat="server"  ID="btnDel" Text="Delete"  CssClass="btn btn-danger" OnClick="btnDel_Click" />                            
                                <asp:Button runat="server"  ID="btnCancl" Text="Cancel" CssClass="btn" OnClick="btnCancl_Click" />
                                <asp:HiddenField ID="HFProID" runat="server" />
                                
                        </div>  
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
