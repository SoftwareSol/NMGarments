<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StockIn.aspx.cs" Inherits="Foods.StockIn" %>
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

        .calender {
            border:solid 1px Gray;
            margin:0px;
            padding:3px;
            height: 200px;
            overflow:auto;
            background-color: #FFFFFF;     
            z-index:12000 !important;
            position:absolute;
        }

        .completionList {
            border:solid 1px Gray;
            margin:0px;
            padding:3px;
            height: 120px;
            overflow:auto;
            background-color: #FFFFFF;   
            z-index:12000 !important;  
            position:absolute;
        } 

        .listItem {
            color: #191919;
        } 

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="pnl" runat="server">
        <ContentTemplate>
    <ul class="breadcrumb">
        <li>
            <i class="icon-home"></i>
            <a href="WellCome.aspx">Home</a> 
            <i class="icon-angle-right"></i>
        </li>
        <li>
            <a href="#">Stock</a>
            <i class="icon-angle-right"></i>
        </li>
        <li>
            <a href="StockIn.aspx">Stock In</a>            
        </li>
    </ul>
    
    <!-- imageLoader - START -->

    <img id='HiddenLoadingImage' src="../../img/page-loader.gif" class="LoadingProgress" />

    <!-- imageLoader - END -->
    <div class="row-fluid sortable">
        <div class="box span12">
            <div class="box-header" data-original-title>
                <h2><i class="halflings-icon align-justify"></i><span class="break"></span> StockIn </h2>
                <div class="box-icon">
                    <a href="#" class="btn-minimize"><i class="halflings-icon chevron-up"></i></a>						                
                </div>
            </div>
            <div class="box-content">
                <div class="box">
                <div class="span12">&nbsp;</div>
                <%--<div class="alert alert-error" id="alerts" style="display:none;">
                <strong>Error!</strong>
                <label id="lblerrorSupplierName"> Please Write some thing in the Supplier Name!!</label>                                               
                <label id="lblPhone">Please write The Valid Phone Num!!</label>
                <label id="lblCellNo">Please Write Cell Number!!</label>                                               
                <label id="lblInt">Please Write Number Only!!</label>
                <label id="lblDesignation">Please Write Designation!!</label>
                <label id="lblAddressOne">Please Write Address One!!</label>                                                
                <label id="lblNIC">Please Write CNIC!!</label>
                <label id="lblBusinessNature">Please Write Business Nature!!</label>                                              
                </div>--%>
                <asp:Panel ID="pnlpurchase" runat="server">
                    <div class="span12">
                        <div class="span2">
                            <div class="item">
                                <label>Active</label>
                                <asp:CheckBox ID="chk_act" runat="server" />
                            </div>
                        </div>
                        <div class="span2">
                            <div class="item">
                                <label>Print</label>
                                <asp:CheckBox ID="chk_prt" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="span12">
                        <div class="span6">
                            <div class="item">
                                <label>Purchase</label>
                                <asp:DropDownList ID="ddlstckIn" runat="server" data-rel="chosen" AutoPostBack="True" OnSelectedIndexChanged="ddlstckIn_SelectedIndexChanged" ></asp:DropDownList>
                            </div>
                        </div>
                        <div class="span6">
                            <div class="item">
                                <label>Purchase Date</label>
                                <asp:TextBox ID="TBPurdat" runat="server" class="datepicker"></asp:TextBox>
                            </div>
                        </div>                                        
                    </div>
                </asp:Panel>
                <div class="span12">
                    <div class="span9">
                        <div class="item">
                            <label>Date</label>
                            <asp:TextBox ID="TBdat" runat="server" class="datepicker"></asp:TextBox>
                        </div>
                    </div>
                    <div class="span2">
                        <div class="item">
                            <label>Defected</label>
                            <asp:CheckBox ID="chk_def" runat="server" />
                        </div>
                    </div>                    
                </div> 
                <asp:Panel ID="pnlvendor" runat="server" Visible="false">
                    <div class="span12">
                        <div class="item">
                            <label>Vendor</label>
                            <asp:DropDownList ID="ddl_ven" runat="server" CssClass="span12" data-rel="chosen" ></asp:DropDownList>
                        </div>
                    </div> 
                </asp:Panel>        
                <div class="clearfix"></div>
                    
                </div>
                <div class="row-fluid">
                    <asp:Label ID="lbl_openbalance1" runat="server" ></asp:Label>
                    <asp:Label ID="lbl_Openbalance" runat="server" ></asp:Label>
                    
                    <div class="box span12">
                        <div class="box-header" data-original-title>
                            <h2><i class="halflings-icon th-list"></i><span class="break"></span> Items</h2>
                        </div>
                        <div class="box-content">
                            <div class="scrollit">
                                <asp:Panel ID="PnlCrtPurItem" runat="server">
                                    <div class="row-fluid">	
                                        <div class="span12">
                                            <div class="box-content span12">
                                                <table  style="text-align:center; font-weight:bold;" class="table table-striped table-bordered">
                                                    <thead>
                                                        <tr>
                                                            <td>Category</td>
                                                            <td>Items</td>
                                                            <td>Sizes</td>
                                                            <td>Qty</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>Rate</td>
                                                            <td>Total Qty</td>
                                                            <td>Amount</td>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="TBCat" CssClass="span10" runat="server" placeholder="Category..." AutoPostBack="true" OnTextChanged="TBCat_TextChanged"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ServiceMethod="GetCat" CompletionListCssClass="completionList"
                                                                CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                                MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10"
                                                                TargetControlID="TBCat" ID="AutoCompleteExtender6"  
                                                                runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TBItms" CssClass="span10" runat="server" AutoPostBack="true" placeholder="Items..." OnTextChanged="TBItms_TextChanged"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ServiceMethod="GetPro" CompletionListCssClass="completionList"
                                                                CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                                MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10"
                                                                TargetControlID="TBItms" ID="AutoCompleteExtender1"  
                                                                runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>

                                                            </td>
                                                            <td colspan="4">
                                                                <asp:GridView ID="GVStkItems" runat="server" class="table table-striped table-bordered" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Exits!!" ShowHeader="false"    OnRowDeleting="GVStkItems_RowDeleting" OnRowDataBound="GVStkItems_RowDataBound" >
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="itmsiz" runat="server" Text='<%# Eval("Dstk_sizes")%>' placeholder="Size..."  style="width:30px; height:20px; background:none; border:none;"   ></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="ItmQty" runat="server"  Text='<%# Eval("Dstk_ItmQty")%>' placeholder="0.00" style="width:30px; height:20px; background:none; border:none;" AutoPostBack="true" OnTextChanged="ItmQty_TextChanged"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkbtnadd" OnClick="linkbtnadd_Click" runat="server">+<i class="halflings-icon plus-sign" ></i></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>                                     
                                                                        <asp:CommandField ShowDeleteButton="True" DeleteText="-"  >
                                                                        <ControlStyle CssClass="halflings-icon minus-sign" />
                                                                        </asp:CommandField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TBRat" CssClass="span10" runat="server" placeholder="Rate..." AutoPostBack="true" OnTextChanged="TBRat_TextChanged"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TBttlqty" CssClass="span10" runat="server" placeholder="Total Quantity..."></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TBAmt" CssClass="span10" runat="server" placeholder="Amount..."></asp:TextBox>
                                                            </td>
                                              
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>     
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="box span12">
                                    
                            <asp:TextBox ID="TBrmks" runat="server" TextMode="MultiLine" placeholder="Remarks.." CssClass="span12" ></asp:TextBox>
                            <asp:HiddenField ID="HFMStckIn" runat="server"  />
                            <asp:HiddenField ID="HFDStckOut" runat="server" />
                            <asp:HiddenField ID="HFStkSONO" runat="server"  />
                            <asp:HiddenField ID="HFStckIn" runat="server"  />
                        </div>
                    </div>
                    <div class="form-actions">
                        <div class="span4"></div>
                            <div class="span6">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"  OnClick="btnSave_Click" />
                                <asp:Button ID="btnReset" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnReset_Click" />
                            </div>                                            
                        <div class="span2">
                            <asp:HiddenField ID="HFstckInId" runat="server" />
                        </div>
                    </div>
                    <div class="span12"></div>
                </div>                
            </div>
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

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
