<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StockIn_.aspx.cs" Inherits="Foods.StockIn_" %>
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
        <li>
            <a href="#">Stock</a>
            <i class="icon-angle-right"></i>
        </li>
        <li>
            <a href="StockIn_.aspx">Stock In</a>            
        </li>
    </ul>
    
    <!-- imageLoader - START -->

    <img id='HiddenLoadingImage' src="../../img/page-loader.gif" class="LoadingProgress" />

    <!-- imageLoader - END -->
            <div class="row-fluid">
                <div class="box  span12">
                    <div class="box-header" data-original-title>
                        <h2><i class="halflings-icon edit"></i><span class="break"></span> Create Stock In </h2>
                    </div>
                    <div class="box-content">
                        <div class="row-fluid">	
                            <div class="box span12">
                                <div class="box-content">                                 
                                    <div class="span1">
                                        <div class="control-group">
                                            <label class="control-label" for="TBSrhStckIn">Stock In</label>
                                        </div>
                                    </div>
                                    <div class="span10">
                                        <div class="controls">
                                            <div class="input-append">
                                                <asp:TextBox runat="server" class="span12" ID="TBSrhStckIn"></asp:TextBox><asp:LinkButton runat="server" ID="LBStckIn" CssClass="add-on" ><i class="icon-search"></i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span12">
                                        <div class="controls">
                                            <div class="scrollit">
                                                <asp:GridView ID="GVStckIn" runat="server" class="table table-striped table-bordered" PageSize="5" AllowPaging="true" AutoGenerateColumns="False" DataKeyNames="ID" ShowHeaderWhenEmpty="true" OnPageIndexChanging="GVStckIn_PageIndexChanging" OnRowDeleting="GVStckIn_RowDeleting" OnRowCommand="GVStckIn_RowCommand" >
                                                    <Columns>
                                                        <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
                                                        <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                                                        <asp:BoundField DataField="PNO" HeaderText="Purchase NO" SortExpression="PNO" />
                                                        <asp:BoundField DataField="VendorName" HeaderText="Vendor Name" SortExpression="VendorName" />                                                        
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LBtnScrhStckIn" runat="server" CommandName="Select" Text="Select" ></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LBtnDel" runat="server"  CommandName="Delete" Text="Delete" ></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LBtnShw" runat="server"  CommandName="Show" Text="Show" ></asp:LinkButton>
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
                </div>
            </div>
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
                        <div class="span12">
                            <div class="item">
                                <label>Date</label>
                                <asp:TextBox ID="TBdat" runat="server" class="datepicker"></asp:TextBox>
                            </div>
                        </div> 
                        <div class="span12">
                            <div class="item">
                                <label>Vendor</label>
                                <asp:DropDownList ID="ddl_ven" runat="server" data-rel="chosen" ></asp:DropDownList>
                            </div>
                        </div> 
        
                        <div class="clearfix"></div>
                    
                        </div>
                        <div class="row-fluid">
                            <div class="box span12">
                                <div class="box-header" data-original-title>
                                    <h2><i class="halflings-icon th-list"></i><span class="break"></span> Stock Items</h2>
                                </div>
                                <div class="box-content">
                                    <div class="scrollit">
                                        <asp:Panel ID="PnlCrtPurItem" runat="server">
                                            <div class="row-fluid">	
                                                <div class="span12">
                                                    <div class="box-content span12">                            
                                                    <asp:GridView ID="GVStkItems" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Exits!!"  class="table table-striped table-bordered" OnRowDeleting="GVStkItems_RowDeleting" OnRowDataBound="GVStkItems_RowDataBound" >
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ITEMS">  
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlstkItm" runat="server" AutoGRNstBack="true" data-rel="chosen"></asp:DropDownList>
                                                                    <asp:Label ID="lblPurItm" Visible="false" runat="server" Text='<%# Eval("ProNam")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="DESCRIPTIONS">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="ItmDscptin" runat="server" Text='<%# Eval("Dstk_ItmDes")%>' placeholder="Description..."  style="width:120px; height:20px; background:none; border:none;"   ></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="QTY">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="ItmQty" runat="server"  Text='<%# Eval("Dstk_ItmQty")%>' placeholder="0.00" style="width:50px; height:20px; background:none; border:none;"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="WEIGHT">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Tbwght" runat="server" Text='<%# Eval("Dstk_Itmwght")%>'  style="width:50px; height:20px; background:none; border:none;" ></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="UNITS">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Tbunts" runat="server" Text='<%# Eval("Dstk_ItmUnt")%>'  style="width:50px; height:20px; background:none; border:none;" ></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="RATE"> 
                                                                <ItemTemplate>      
                                                                    <asp:TextBox ID="Tbrat" runat="server" Text='<%# Eval("Dstk_rat")%>'  style="width:50px; height:20px; background:none; border:none;" ></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="SALE RATE">
                                                                <ItemTemplate>    
                                                                    <asp:TextBox ID="Tbsalrat" runat="server"  Text='<%# Eval("Dstk_salrat")%>' style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="PURCHASE RATE">
                                                                <ItemTemplate>    
                                                                    <asp:TextBox ID="Tbpurrat" runat="server"  Text='<%# Eval("Dstk_purrat")%>' style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>
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
                                    </div>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="box span12">
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
                                        <asp:Button ID="btnDel" runat="server" Text="Edit" CssClass="btn btn-success"  />
                                        <asp:Button ID="btnReset" runat="server" Text="Cancel" CssClass="btn btn-danger" />
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
</asp:Content>
