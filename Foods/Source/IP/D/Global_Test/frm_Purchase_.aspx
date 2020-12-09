<%@ Page Title="Purchase" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frm_Purchase_.aspx.cs"
Inherits="Foods.frm_Purchase" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
/* Scroller Start */
        .scrollit {
        overflow:scroll;
        height:200px;
        width:100%;           
        margin:0px auto;
        }
        /* Scroller End */
        
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
        .calender {
            border:solid 1px Gray;
            margin:0px;
            padding:3px;
            height: 200px;
            overflow:auto;
            background-color: #FFFFFF;     
            z-index:20000 !important;
            position:absolute;
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

        .listItem {
            color: #191919;
        } 

        .itemHighlighted {
            background-color: #ADD6FF;               
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel  ID="UpdatePanel1" runat="server">
        <ContentTemplate>  
           
    <ul class="breadcrumb">
        <li>
            <i class="icon-home"></i>
            <a href="WellCome.aspx">Home</a> 
            <i class="icon-angle-right"></i>
        </li>
        <li><a href="frm_Purchase.aspx">Purchase</a></li>
    </ul>

    <!-- imageLoader - START -->

    <img id='HiddenLoadingImage' src="../../img/page-loader.gif" class="LoadingProgress" />

    <!-- imageLoader - END -->
            
        
    <div class="row-fluid">
        <div class="box  span12">
            <div class="box-header" data-original-title>
                <h2><i class="halflings-icon edit"></i><span class="break"></span> Create Purchase </h2>
            </div>
            <div class="box-content">
                <asp:Panel ID="PanelShowClosed" runat="server">
                    <div class="row-fluid">	
                        <div class="box span12">
                            <div class="box-header" data-original-title>
                            <div class="centerClosed">    
                                <P>Closed!</P>
                            </div>
                        </div>
                    </div>
                </div>
                </asp:Panel>
                <div class="row-fluid">	
                    <div class="box span12">
                        <div class="box-content">                                 
                            <div class="span1">
                                <div class="control-group">
                                    <label class="control-label" for="TBSearchPONum">PO Num</label>
                                </div>
                            </div>
                            <div class="span10">
                                <div class="controls">
                                    <div class="input-append">
                                        <asp:TextBox runat="server" class="span12" ID="TBSearchPONum" AutoPostBack="true" OnTextChanged="TBSearchPONum_TextChanged" ></asp:TextBox><asp:LinkButton runat="server" ID="LinkButton1" CssClass="add-on" ><i class="icon-search"></i></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="span12" style="text-align:center;">
                                <asp:Label ID="lblmssg" runat="server" ForeColor="Red" ></asp:Label>
                            </div>
                            <div class="span12">
                                <div class="controls">
                                    <div>
                                        <asp:GridView ID="GVScrhMPur" runat="server" class="table table-striped table-bordered"
                                             AllowPaging="True" PageSize="4" AutoGenerateColumns="False" DataKeyNames="MPurID" OnPageIndexChanging="GVScrhMPur_PageIndexChanging" OnRowDeleting="GVScrhMPur_RowDeleting" OnRowCommand="GVScrhMPur_RowCommand" OnSelectedIndexChanging="GVScrhMPur_SelectedIndexChanging">
                                            <Columns>
                                                <asp:BoundField DataField="MPurID" HeaderText="ID" SortExpression="MPurID" ReadOnly="True" />
                                                <asp:BoundField DataField="PurNo" HeaderText="Purchase No." SortExpression="PurNo" />
                                                <asp:BoundField DataField="suppliername" HeaderText="Vender" SortExpression="suppliername" />
                                                <asp:BoundField DataField="MPurDate" HeaderText="Date" SortExpression="MPurDate" />
                                                <asp:BoundField DataField="CreatedBy" HeaderText="Created By" SortExpression="CreatedBy" />                                            
                                                <asp:BoundField DataField="CreatedAt" HeaderText="Created At" SortExpression="CreatedAt" />                                            
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LBtnScrhMPur" runat="server" CommandName="Select" ForeColor="Green" > Select </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LBtnDel" runat="server" CommandName="Delete" ForeColor="Red" > Delete </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkShow" CommandName="Show"  runat="server" Text="Invoice" ></asp:LinkButton>                                                
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
                <div class="row-fluid">	
                    <div class="span12">
                        <div class="span3">
                            <h1><span style="color:black;"> Purchase</span></h1>                                    
                        </div>
                        <div class="span5"></div>
                        <div class="span2">
                            <div class="span12">
                                <h2><span class="break"></span>Date</h2>
                                    <asp:TextBox runat="server" class="span10"  ID="TBPurDat" ></asp:TextBox>
                                    <asp:CalendarExtender ID="Calendar1" CssClass="calender" PopupButtonID="imgPopup" runat="server" 
                                        TargetControlID="TBPurDat" Format="dd/MM/yyyy"> </asp:CalendarExtender>  
                            </div>
                        </div>                               
                        <div  class="span2">
                            <div class="span12">
                                <h2><span class="break"></span>Voucher No.</h2>
                                    <asp:Label runat="server" class="span2" ID="TBPONum" ForeColor="Red" ></asp:Label>
                            </div>
                        </div>
                        <div class="row-fluid">	
                            <div class="box span12">
                                <div class="box-content">
                                <div class="span1">&nbsp;</div>
                                    <div class="span2">
                                        <div class="control-group">                                            
                                            <div class="controls">
                                                <asp:CheckBox ID="chk_Act" runat="server" Text="Active" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span2">
                                        <div class="control-group">                                            
                                            <div class="controls">
                                                <asp:CheckBox ID="chk_prtd" runat="server" Checked="true" Text="Printed" />
                                            </div>
                                        </div>
                                    </div>                                
                                    <%--<div class="span12" style="visibility:hidden;">
                                    <div class="control-group">
                                    <label style="color:black" for="DDl_Req" >Requisition</label>
                                    <div class="controls">
                                    <asp:DropDownList ID="DDl_Req" runat="server" data-rel="chosen" CssClass="span12"  AutoPostBack="true" OnSelectedIndexChanged="DDl_Req_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    </div>
                                    </div>
                                    <div class="span12" style="visibility:hidden;">
                                    <div class="control-group">
                                    <label style="color:black" for="DDl_PO">Purchase Order</label>
                                    <div class="controls">
                                    <asp:DropDownList ID="DDl_PO" runat="server" data-rel="chosen"  AutoPostBack="True" CssClass="span12" OnSelectedIndexChanged="DDl_PO_SelectedIndexChanged" ></asp:DropDownList>
                                    </div>
                                    </div>
                                    </div>--%>
                                    <div class="span12"></div>
                                   
                                    <div class="span5">
                                    <div class="span12">
                                        <div class="control-group">
                                            <label style="color:black" for="TBVenNam" >Vendor</label>
                                            <div class="controls">
                                                <asp:DropDownList id="ddlVenNam" runat="server" data-rel="chosen" CssClass="span12" OnSelectedIndexChanged="ddlVenNam_SelectedIndexChanged" AutoPostBack="True" ></asp:DropDownList>                                           
                                            </div>
                                        </div>
                                    </div>
                                    
                                    
                                    <%-- <div class="span12" style="visibility:hidden;">
                                    <div class="control-group">
                                    <label style="color:black" for="DDL_Cust" >Customer</label>
                                    <div class="controls">
                                    <asp:DropDownList ID="DDL_Cust" runat="server" data-rel="chosen" CssClass="span12"></asp:DropDownList>
                                    </div>
                                    </div>
                                    </div>--%>
                                    <div class="span6" >
                                        <div class="control-group">
                                            <label style="color:black" for="DDL_Vchtyp" >Voucher Type</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="DDL_Vchtyp" runat="server" data-rel="chosen" CssClass="span12" AutoPostBack="True" OnSelectedIndexChanged="DDL_Vchtyp_SelectedIndexChanged">                                               
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span5" >
                                        <div class="control-group">
                                            <label style="color:black" for="DDL_Paytyp" >Payment Type</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="DDL_Paytyp" runat="server" data-rel="chosen" CssClass="span12" AutoPostBack="True" OnSelectedIndexChanged="DDL_Paytyp_SelectedIndexChanged">                                               
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:Panel ID="pnl_bnk" runat="server" CssClass="span12" >
                                        <div class="span6" >
                                            <div class="control-group">
                                                <label style="color:black" for="DDL_Bnk" >Bank</label>
                                                <div class="controls">
                                                    <asp:DropDownList ID="DDL_Bnk" runat="server" data-rel="chosen" CssClass="span12">                                               
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="pnl_Chqno" runat="server" CssClass="span12" >
                                        <div class="span6" >
                                            <div class="control-group">
                                                <label style="color:black" for="DDL_ChqNo" >Cheque No.</label>
                                                <div class="controls">
                                                    <asp:TextBox ID="TBChqNo" runat="server" CssClass="span12"></asp:TextBox>
                                                    <%--<asp:DropDownList ID="DDL_ChqNo" runat="server" data-rel="chosen" CssClass="span12">                                               
                                                    </asp:DropDownList>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="pnl_chqamt" runat="server" CssClass="span12">
                                        <div class="span6" >
                                            <div class="control-group">
                                                <label style="color:black" for="TB_ChqAmt" >Cheque Amount</label>
                                                <div class="controls">
                                                    <asp:TextBox ID="TB_ChqAmt" runat="server" CssClass="span12" placeholder="Cheque Amount"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="pnl_cshamt" runat="server" CssClass="span12">
                                        <div class="span12" >
                                            <div class="control-group">
                                                <label style="color:black" for="TB_CshAmt" >Cash Amount</label>
                                                <div class="controls">
                                                    <asp:TextBox ID="TB_CshAmt" runat="server" CssClass="span12" placeholder="Cash Amount"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <%--<div class="span12" >
                                        <div class="control-group">
                                            <label style="color:black" for="DDL_Acct" >Account</label>
                                            <div class="controls">                                                
                                                <asp:DropDownList ID="DDL_Acct" runat="server" data-rel="chosen" CssClass="span12">                                               
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>--%>
                                    
                                    </div>
                                    <div class="span6">
                                        <div class="span5">
                                            <div class="span12">
                                                <div class="control-group">
                                                    <label style="color:black" for="DDL_Currency" >Currency</label>
                                                    <div class="controls">
                                                        <asp:DropDownList id="DDL_Currency" runat="server" data-rel="chosen" CssClass="span12" AutoPostBack="True" OnSelectedIndexChanged="DDL_Currency_SelectedIndexChanged"  ></asp:DropDownList>                                           
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span2">
                                            <div class="span12">
                                                <div class="control-group">
                                                    <label style="color:black" for="lbl_currrat" >Rate</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="TBCurrRat" CssClass="span11" runat="server" placeholder="Enter Currency Rate.." ></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>                                            
                                        </div>  
                                        <div class="span2">
                                            <div class="span12">
                                                <div class="control-group">
                                                    <label style="color:black" for="lbl_exchrat" >Ex. Rate</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="TBExchgRat" CssClass="span11" runat="server" placeholder="Enter Exchange Rate.." ></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>                                            
                                        </div>
                                        <div class="span2">
                                            <div class="span12">
                                                <div class="control-group">
                                                    <br />
                                                    <label style="color:black" for="btn_CurrSav" ></label>
                                                    <div class="controls">
                                                        <asp:Button ID="btn_CurrSav" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btn_CurrSav_Click" /></div>  
                                                    </div>
                                                </div>
                                            </div> 
                                        </div>
                                     <div class="span5">
                                            <div class="row-fluid">	
                                                <div class="box-header" data-original-title>
                                                    <h2><i class="halflings-icon edit"></i><span class="break"></span> Other Information </h2>
                                                </div>
                                                <div class="box span12">
                                                    <div class="box-content">  
                                                        <div class="span12">
                                                            <div class="controls">
                                                                <div class="scrollit">
                                                                    
                                                                    <div class="span12">
                                                                        <div class="control-group">
                                                                            <label style="color:black" for="TBDCNo" >DC No</label>
                                                                            <div class="controls">
                                                                                <asp:TextBox ID="TBDCNo" runat="server" placeholder="DC No.." ></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="span12">
                                                                        <div class="control-group">
                                                                            <label style="color:black" for="TBDatTim" >Date & Time</label>
                                                                            <div class="controls">
                                                                                <asp:TextBox ID="TBDatTim" runat="server" placeholder="Date: 12/2/2019.." ></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="span12">
                                                                        <div class="control-group">
                                                                            <label style="color:black" for="TBBiltyNo" >Bilty No.</label>
                                                                            <div class="controls">
                                                                                <asp:TextBox ID="TBBiltyNo" runat="server" placeholder="Bilty No.." ></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="span12">
                                                                        <div class="control-group">
                                                                            <label style="color:black" for="TBVNo" >Vehical No.</label>
                                                                            <div class="controls">
                                                                                <asp:TextBox ID="TBVNo" runat="server" placeholder="Vehicle No.." ></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    
                                                                    <div class="span12">
                                                                        <div class="control-group">
                                                                            <label style="color:black" for="TBDrvNam" >Diver Name</label>
                                                                            <div class="controls">
                                                                                <asp:TextBox ID="TBDrvNam" runat="server" placeholder="Driver Name.." ></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="span12">
                                                                        <div class="control-group">
                                                                            <label style="color:black" for="TBDrvMobNo" >Driver Mobile No</label>
                                                                            <div class="controls">
                                                                                <asp:TextBox ID="TBDrvMobNo" runat="server" placeholder="Driver Mobile No.." ></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="span12">
                                                                        <div class="control-group">
                                                                            <label style="color:black" for="TBTrans" >Transporter</label>
                                                                            <div class="controls">
                                                                                <asp:TextBox ID="TBTrans" runat="server" placeholder="Transporter.." ></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="span12">
                                                                        <div class="control-group">
                                                                            <label style="color:black" for="TbStaton" >Station</label>
                                                                            <div class="controls">
                                                                                <asp:TextBox ID="TbStaton" runat="server" placeholder="Stations.." ></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="span12">
                                                                        <div class="control-group">
                                                                            <label style="color:black" for="TBDelivOrd" >Delivery Order</label>
                                                                            <div class="controls">
                                                                                <asp:TextBox ID="TBDelivOrd" runat="server" placeholder="Date: 12/2/2019.." ></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="span12">
                                                                        <div class="control-group">
                                                                            <label style="color:black" for="TBFright" >Frieght</label>
                                                                            <div class="controls">
                                                                                <asp:TextBox ID="TBFright" Text="0" runat="server" placeholder="Date: 12/2/2019.." ></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>            
    <div class="row-fluid">
        <div class="box span12">
            <div class="box-header" data-original-title>
                <h2><i class="halflings-icon th-list"></i><span class="break"></span> Purchase Items</h2>
            </div>
            <div class="box-content">
                <div class="scrollit">
                    <asp:Panel ID="PnlCrtPurItem" runat="server">
                        <div class="row-fluid">	
                            <div class="span12">
                                <div class="box-content span12">                            
                                    <asp:GridView ID="GVPurItems" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Exits!!"  class="table table-striped table-bordered" OnRowDeleting="GVPurItems_RowDeleting" >
                                        <Columns>
                                            <asp:TemplateField HeaderText="Category">  
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPurItm" runat="server" Text='<%# Eval("Category") %>' Visible = "false" />
                                                   <asp:TextBox ID="TBcat" runat="server" Text='<%# Eval("Category") %>' placeholder="Category..."  style="width:80px; height:20px; background:none; border:none;"  AutoPostBack="true" OnTextChanged="TBcat_TextChanged" ></asp:TextBox>
                                                        <asp:AutoCompleteExtender ServiceMethod="GetCat" CompletionListCssClass="completionList"
                                                        CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                        MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10"
                                                        TargetControlID="TBcat" ID="AutoCompleteExtender1"  
                                                        runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TbItmDscptin" Text='<%# Eval("Description")%>' runat="server" placeholder="Description..."  style="width:120px; height:20px; background:none; border:none;" AutoPostBack="true" OnTextChanged="TbItmDscptin_TextChanged" ></asp:TextBox>
                                                    <asp:AutoCompleteExtender ServiceMethod="GetPro" CompletionListCssClass="completionList"
                                                    CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                    MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10"
                                                    TargetControlID="TbItmDscptin" ID="AutoCompleteExtender2"  
                                                    runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Brand">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="Tbbrnd" Text='<%# Eval("Brand")%>' placeholder="Brand..."  runat="server" style="width:50px; height:20px; background:none; border:none;" OnTextChanged="Tbbrnd_TextChanged" AutoPostBack="true"  ></asp:TextBox>
                                                    <asp:AutoCompleteExtender ServiceMethod="GetBrnd" CompletionListCssClass="completionList"
                                                    CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                    MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10"
                                                    TargetControlID="Tbbrnd" ID="AutoCompleteExtender3"  
                                                    runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Origin">
                                                <ItemTemplate>    
                                                    <asp:TextBox ID="Tborgn" runat="server" Text='<%# Eval("Origin")%>' placeholder="Origin..."  style="width:80px; height:20px; background:none; border:none;" OnTextChanged="Tborgn_TextChanged" AutoPostBack="true" ></asp:TextBox>
                                                    <asp:AutoCompleteExtender ServiceMethod="Getorign" CompletionListCssClass="completionList"
                                                    CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                    MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10"
                                                    TargetControlID="Tborgn" ID="AutoCompleteExtender4"  
                                                    runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Packing Size">
                                                <ItemTemplate>    
                                                    <asp:TextBox ID="TbPacksiz" runat="server" Text='<%# Eval("PackingSize")%>' placeholder="Packing size..."  style="width:80px; height:20px; background:none; border:none;" AutoPostBack="true" OnTextChanged="TbPacksiz_TextChanged" ></asp:TextBox>
                                                    <asp:AutoCompleteExtender ServiceMethod="Getpakgsiz" CompletionListCssClass="completionList"
                                                    CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                    MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10"
                                                    TargetControlID="TbPacksiz" ID="AutoCompleteExtender5"  
                                                    runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>    
                                                    <asp:TextBox ID="TB_rat" Text='<%# Eval("Rate")%>' runat="server" placeholder="Rate..."  style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty">
                                                    <ItemTemplate>    
                                                        <asp:TextBox ID="Tbqty" runat="server" Text='<%# Eval("Qty")%>' AutoPostBack="true" placeholder="Quantity..." style="width:80px; height:20px; background:none; border:none;" OnTextChanged="Tbqty_TextChanged" ></asp:TextBox>
                                                    </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>    
                                                    <asp:TextBox ID="Tbunt" runat="server" Text='<%# Eval("Unit")%>' placeholder="Unit..." style="width:80px; height:20px; background:none; border:none;" AutoPostBack="true" OnTextChanged="Tbunt_TextChanged" ></asp:TextBox>
                                                    <asp:AutoCompleteExtender ServiceMethod="Getunts" CompletionListCssClass="completionList"
                                                    CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                    MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10"
                                                    TargetControlID="Tbunt" ID="AutoCompleteExtender6"  
                                                    runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Flag"  runat="server" Text="0" Visible="false" />
                                                    <asp:TextBox ID="Tbamt" runat="server" Text='<%# Eval("Amount")%>' placeholder="Amount..." style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>
                                                    <asp:HiddenField runat="server" ID="HFDPur" Value='<%# Eval("DPurID")%>'  />
                                                </ItemTemplate>
                                            </asp:TemplateField>        
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnadd" OnClick="linkbtnadd_Click" runat="server">+</asp:LinkButton>
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
                    </asp:Panel>
                </div>
            </div>
        </div>
        <div class="row-fluid">
            <div class="row-fluid">
            <div class="box span12">
                <div class="box-content">
                    
                <div class="span12">
                    <div  class="span2">
                        <h5><span class="break"></span>Previous Balance</h5>
                        <asp:TextBox ID="txt_outstand" runat="server" Text="0.00" style="width:100px;"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVPreBal" runat="server" ControlToValidate="txt_outstand" ValidationGroup="pur" ErrorMessage="Please Enter in Previous Balance!"></asp:RequiredFieldValidator>
                    </div>
                    <div  class="span3">
                        <h5><span class="break"></span>Further Out Standing:</h5>
                        <asp:TextBox runat="server" class="span12" ID="TBOutstand" Text="0.00" AutoPostBack="true" OnTextChanged="TBOutstand_TextChanged"></asp:TextBox>
                    </div>
                    <div  class="span2">
                        <h5><span class="break"></span>Amount Paid:</h5>
                        <asp:TextBox runat="server" class="span12" ID="TBAmtPaid" Text="0.00" AutoPostBack="true" OnTextChanged="TBAmtPaid_TextChanged"></asp:TextBox>
                    </div>
                    <div  class="span2">
                        <h5><span class="break"></span></h5>
                    </div>
                    <div  class="span2">
                        <h5><span class="break"></span>Gross Total:</h5>
                        <asp:TextBox runat="server" class="span12" ID="TBTtl" Text="0.00"></asp:TextBox>
                    </div>
                    <div class="span2"></div>
                    <div class="span3">
                        <h5><span class="break"></span></h5>
                        <asp:TextBox ID="TBWidholdtx" class="span12" Visible="false" runat="server" placeholder="Enter with Holding Tax..."></asp:TextBox>
                    </div>
                    <div  class="span2">
                        <h5><span class="break"></span>Discount %</h5>
                        <asp:TextBox runat="server" class="span12" ID="TBDiscPer" Text="0.00" AutoPostBack="true" OnTextChanged="TBDiscPer_TextChanged"></asp:TextBox>
                    </div>
                    <div  class="span2">
                        <h5><span class="break"></span>Discount:</h5>
                        <asp:TextBox runat="server" class="span12" ID="TBDisc" Text="0.00"></asp:TextBox>
                    </div>
                    <div class="span9">
                        <h5><span class="break"></span>Payment Via A/c #:</h5>
                        <asp:DropDownList ID="ddl_acc" class="span12" runat="server"></asp:DropDownList>
                    </div>
                    <div  class="span2">
                        <h5><span class="break"></span>Net Total:</h5>
                        <asp:TextBox runat="server" class="span12" ID="TBGrssTotal" Text="0.00"></asp:TextBox>
                    </div>
                </div>
                <div class="row-fluid">	
                        <div class="span12">
                            <div class="span2">
                                <asp:CheckBox runat="server" ID="ckprntd" Text="To Be Printed" TextAlign="Left" Visible="false"/> 
                            </div>
                        </div>
                    </div> 
                    <div class="span12">
                        <div class="control-group">
                            <label style="color:black" for="TBRmk" >Note!</label>
                            <div class="controls">
                                <asp:TextBox runat="server" TextMode="MultiLine" class="span12" ID="TBRmk" placeholder="Remarks..."></asp:TextBox>                                    
                            </div>
                        </div>
                    </div>                   
                    <div class="span12">
                        <div class="span5">                                   
                            <asp:Button runat="server"  ID="btnSaveClose" Text="Save"  CssClass="btn btn-info"  ValidationGroup="pur" OnClick="btnSaveClose_Click" />   
                            <asp:Button runat="server"  ID="btnRevert" Text="Cancel" CssClass="btn" OnClick="btnRevert_Click"  />       
                        </div>                                   
                    </div>
                </div>
            </div>
            <asp:LinkButton ID="LinkBtnDialogue" runat="server" class="btn btn-info btn-setting" Text="Click for dialog" Visible="false"></asp:LinkButton>
            <asp:HiddenField ID="HFMPur" runat="server" Value="0" />
            <asp:HiddenField ID="HFMStck" runat="server" Value="0" />  
            <asp:HiddenField ID="HFDStck" runat="server" Value="0" />  
            <asp:HiddenField ID="HFMvch" runat="server" Value="0" />  
            <asp:HiddenField ID="HFDvch" runat="server" Value="0" />  
        </div>
        </div>
    </div>
             </ContentTemplate>
    </asp:UpdatePanel>
    <div id="ModalAlert" class="modal fade" style="display:none;">
    <div class="modal-dialog">
    <div class="modal-content">
    <div class="modal-header">
    <h4 class="modal-title"><asp:Label ID="lbl_Heading" runat="server"></asp:Label></h4>
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
    <script src="Controller/Common.js"></script>
</asp:Content>
