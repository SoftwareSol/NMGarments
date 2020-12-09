<%@ Page Title="Purchase" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frm_SPurchase.aspx.cs"
Inherits="Foods.frm_SPurchase" %>
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
    </style>
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
                            <div class="span12">
                                <div class="controls">
                                    <div class="scrollit">
                                        <asp:GridView ID="GVScrhMPur" runat="server" class="table table-striped table-bordered" AllowPaging="True" PageSize="4" AutoGenerateColumns="False" DataKeyNames="MPurID" OnPageIndexChanging="GVScrhMPur_PageIndexChanging" OnRowDeleting="GVScrhMPur_RowDeleting" OnRowCommand="GVScrhMPur_RowCommand" OnSelectedIndexChanging="GVScrhMPur_SelectedIndexChanging">
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
                                                        <asp:LinkButton ID="lnkShow" CommandName="Show"  runat="server" Text="Show" ></asp:LinkButton>                                                
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
                            <div class="box span12">
                                <div class="box-header" data-original-title>
                                    <h2><i class="halflings-icon calendar"></i><span class="break"></span>Date</h2>
                                </div>
                                <div class="box-content">
                                    <asp:TextBox runat="server" class="span10 datepicker" ID="TBPurDat" ></asp:TextBox>
                                </div>
                            </div>
                        </div>                               
                        <div  class="span2">
                            <div class="box  span12">
                                <div class="box-header" data-original-title>
                                    <h2><i class="halflings-icon edit"></i><span class="break"></span>Pur. No.</h2>
                                </div>
                                <div class="box-content">
                                    <asp:Label runat="server" class="span2" ID="TBPONum" ></asp:Label>
                                </div>
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
                                    <div class="span5" >
                                        <div class="control-group">
                                            <label style="color:black" for="DDL_Vchtyp" >Voucher Type</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="DDL_Vchtyp" runat="server" data-rel="chosen" CssClass="span12" AutoPostBack="True" OnSelectedIndexChanged="DDL_Vchtyp_SelectedIndexChanged">                                               
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span3" >
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
                                    <div class="span12">
                                        <div class="control-group">
                                            <label style="color:black" for="TBRmk" >Remarks</label>
                                            <div class="controls">
                                                <asp:TextBox runat="server" TextMode="MultiLine" class="span12" ID="TBRmk" placeholder="Remarks..."></asp:TextBox>                                    
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
                                            <asp:TemplateField HeaderText="ITEMS">  
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPurItm" runat="server" Text='<%# Eval("productid") %>' Visible = "false" />
                                                    <asp:DropDownList ID="ddlPurItm" runat="server" AutoPostBack="true" data-rel="chosen" OnSelectedIndexChanged="ddlPurItm_SelectedIndexChanged"></asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DESCRIPTIONS">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TbaddPurItmDscptin" Text='<%# Eval("ProDes")%>' runat="server" placeholder="Description..."  style="width:120px; height:20px; background:none; border:none;" ></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="QTY">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TbAddPurItmQty" Text='<%# Eval("QTY")%>' runat="server"   style="width:50px; height:20px; background:none; border:none;"  ></asp:TextBox>
                                                    <asp:HiddenField ID="HFCost" runat="server" /> 
                                                    <%--OnTextChanged="TbAddPurItmQty_TextChanged"--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="Weight">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="Tbwght" runat="server" Text='<%# Eval("Weight")%>'   style="width:50px; height:20px; background:none; border:none;" ></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="Tbrat" runat="server" Text='<%# Eval("Rate")%>'   style="width:50px; height:20px; background:none; border:none;" ></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                          <asp:TemplateField HeaderText="UNIT" Visible="false">
                                                <ItemTemplate>      
                                                    <asp:DropDownList ID="ddlPurUnit" runat="server" >
                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                    <asp:ListItem Value="kg" > Kg </asp:ListItem>
                                                    <asp:ListItem Value="Ltrs" > Ltrs </asp:ListItem>
                                                    <asp:ListItem Value="Pcs" > Pcs </asp:ListItem>
                                                    </asp:DropDownList>                           
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cost">
                                                <ItemTemplate>    
                                                    <asp:TextBox ID="TbAddCosts" runat="server" Text='<%# Eval("Cost")%>'  style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sales Tax">
                                                <ItemTemplate>    
                                                    <asp:TextBox ID="TbSalTax" runat="server" Text='<%# Eval("Sales Tax")%>'   style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Trade Price">
                                                <ItemTemplate>    
                                                    <asp:TextBox ID="Tbtrdpric" runat="server" Text='<%# Eval("TPrice")%>'  style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Trade Offer">
                                                <ItemTemplate>    
                                                    <asp:TextBox ID="Tbtrdoff" runat="server" Text='<%# Eval("TOffer")%>' style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Discount">
                                                <ItemTemplate>    
                                                    <asp:TextBox ID="Tbdis" runat="server" AutoPostBack="true" OnTextChanged="Tbdis_TextChanged" Text='<%# Eval("Discount")%>' style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="NET TOTAL">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Flag" Visible="false" runat="server" Text="0" />
                                                    <asp:TextBox ID="TbAddPurNetTtl" runat="server" Text='<%# Eval("NetTotal")%>' style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>
                                                    <asp:HiddenField runat="server" ID="HFDPur" Value='<%# Eval("DPurID")%>'  />
                                                </ItemTemplate>
                                            </asp:TemplateField>        
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnadd" OnClick="linkbtnadd_Click" runat="server"><i class="halflings-icon plus-sign" ></i></asp:LinkButton>
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
                    <%--<asp:Panel ID="PnlPurEdt" runat="server">
                <div class="row-fluid" >	
                <div class="span12">
                <div class="box-content span12">
                <asp:GridView ID="GVAddEdtPur" runat="server" AutoGenerateColumns="False" DataKeyNames="DPurID"  class="table table-striped table-bordered" OnRowCommand="GVAddEdtPur_RowCommand">
                <Columns>
                <asp:TemplateField HeaderText="ITEMS">
                <ItemTemplate>
                <asp:DropDownList ID="ddl_EdtPurProNam" runat="server"  style="width:70px; height:20px; background:none; border:none;" ></asp:DropDownList>
                </ItemTemplate>
                <edititemtemplate>
                <asp:DropDownList ID="ddl_EdtPurProNam" runat="server"  style="width:70px; height:20px; background:none; border:none;" ></asp:DropDownList>
                </edititemtemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DESCRIPTIONS">
                <ItemTemplate>
                <asp:TextBox ID="TbEdtPurItmsDes" runat="server"  Text='<%# Eval("ProDes")%>' style="width:120px; height:20px; background:none; border:none;" ></asp:TextBox>
                </ItemTemplate>
                <edititemtemplate>
                <asp:TextBox ID="TbEdtPurItmsDes" runat="server"   Text='<%# Eval("ProDes")%>' style="width:120px; height:20px; background:none; border:none;" ></asp:TextBox>
                </edititemtemplate>
                </asp:TemplateField>                        
                <asp:TemplateField HeaderText="QTY">
                <ItemTemplate>
                <asp:TextBox ID="TbEdtQty" runat="server"  Text='<%# Eval("QTY")%>' style="width:50px; height:20px; background:none; border:none;" ></asp:TextBox>
                </ItemTemplate>
                <edititemtemplate>
                <asp:TextBox ID="TbEdtQty" runat="server"   Text='<%# Eval("QTY")%>' style="width:50px; height:20px; background:none; border:none;" ></asp:TextBox>
                </edititemtemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="UNIT">
                <ItemTemplate>
                <asp:TextBox ID="TbEdtPurUnt" runat="server"  Text='<%# Eval("Unit")%>' style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>
                </ItemTemplate>
                <edititemtemplate>
                <asp:TextBox ID="TbEdtPurUnt" runat="server"   Text='<%# Eval("Unit")%>' style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>
                </edititemtemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="COST">
                <ItemTemplate>
                <asp:TextBox ID="TbEdtPurCst" runat="server"  Text='<%# Eval("Cost")%>' style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>
                </ItemTemplate>
                <edititemtemplate>
                <asp:TextBox ID="TbEdtPurCst" runat="server"   Text='<%# Eval("Cost")%>' style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>
                </edititemtemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="NET TOTAL">
                <ItemTemplate>
                <asp:TextBox ID="TbEdtPurNetTotl" runat="server"  Text='<%# Eval("NetTotal")%>' style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>
                </ItemTemplate>
                <edititemtemplate>
                <asp:TextBox ID="TbEdtPurNetTotl" runat="server"   Text='<%# Eval("NetTotal")%>' style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>
                </edititemtemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                <ItemTemplate>
                <asp:LinkButton ID="linkbtnedit" runat="server" ><i class="halflings-icon plus-sign" ></i></asp:LinkButton>
                <asp:HiddenField ID="HFmPurID" runat="server" Value='<%# Eval("mPurID")%>' />                                           
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
                </asp:Panel>--%>
                </div>
            </div>
        </div>
        <div class="row-fluid">
            <div class="box span12">
                <div class="box-content">
                    <div class="row-fluid">
                        <div class="span12">
                            <div class="span3"></div>
                            <div  class="span3">
                                <h5><span class="break"></span></h5>                              
                            </div>
                            <div  class="span3">
                                <h5><span class="break"></span></h5>
                            </div>
                            <div  class="span2">
                                <h5><span class="break"></span>Grand Total:</h5>
                                <asp:TextBox runat="server" class="span12" ID="TBGrssTotal" Text="0.00"></asp:TextBox>
                            </div>
                        </div>
                        <div class="span12">
                            <div class="span3"></div>
                            <div  class="span4">
                                <h5><span class="break"></span></h5>                              
                            </div>
                            <div  class="span2">
                                <h5><span class="break"></span>Previous Balance</h5>
                                <asp:TextBox ID="txt_outstand" runat="server" Text="0.00" style="width:100px; height:30px;"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFVPreBal" runat="server" ControlToValidate="txt_outstand" ValidationGroup="pur" ErrorMessage="Please Enter in Previous Balance!"></asp:RequiredFieldValidator>
                            </div>
                            <div  class="span2">
                                <h5><span class="break"></span>Further Out Standing:</h5>
                                <asp:TextBox runat="server" class="span12" ID="TBOutstand" Text="0.00" AutoPostBack="true" OnTextChanged="TBOutstand_TextChanged"></asp:TextBox>
                            </div>
                        </div>	
                        <div class="span12">
                            <div class="span3"></div>
                            <div  class="span3">
                                <h5><span class="break"></span></h5>
                                <%--<asp:TextBox runat="server" class="span12" ID="TBDBAmtttl" Text="0.00" ></asp:TextBox>--%>
                            </div>
                            <div  class="span3">
                                <h5><span class="break"></span></h5>
                                <%--<asp:TextBox runat="server" class="span12" ID="TBCRAmtttl" Text="0.00" ></asp:TextBox>--%>
                            </div>
                            <div  class="span2">
                                <h5><span class="break"></span>Total:</h5>
                                <asp:TextBox runat="server" class="span12" ID="TBTtl" Text="0.00"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row-fluid">	
                        <div class="span12">
                            <div class="span2">
                                <asp:CheckBox runat="server" ID="ckprntd" Text="To Be Printed" TextAlign="Left" Visible="false"/> 
                            </div>
                            <div class="span1">
                                <%--<asp:CheckBox runat="server" ID="chkClosed" Text="Closed" TextAlign="Left" />--%>
                            </div>
                            <div class="span3"></div>
                            <div  class="span2"></div>
                        </div>
                    </div>
                    <div class="row-fluid">	
                        <div class="span12">
                            <div class="span6">
                                <%--<h2><span class="break"></span>Memo</h2>
                                <asp:TextBox runat="server" class="span12" ID="TBMemo" placeholder="Memo"></asp:TextBox>
                                --%>         
                            </div>
                            <div class="span2"></div>
                            <div class="span2"></div>
                            <div class="span2"></div>
                            <div class="span4"></div>
                            <div class="span4">                                   
                                <asp:Button runat="server"  ID="btnSaveClose" Text="Save"  CssClass="btn btn-info"  ValidationGroup="pur" OnClick="btnSaveClose_Click" />   
                                <asp:Button runat="server"  ID="btnRevert" Text="Cancel" CssClass="btn" OnClick="btnRevert_Click"  />       
                            </div>                                   
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
