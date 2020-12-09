<%@ Page Title="Sales Return" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
     CodeBehind="frm_mdn.aspx.cs" Inherits="Foods.frm_mdn" MaintainScrollPositionOnPostback="true" %>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <ul class="breadcrumb">
            <li>
                <i class="icon-home"></i>
                <a href="WellCome.aspx">Home</a> 
                <i class="icon-angle-right"></i>
            </li>
            <li>
                <i class="icon-angle-right"></i>
                <a href="#">Sales</a>
            </li>
            <li>
                <i class="icon-angle-right"></i>
                <a href="frm_mdn.aspx">Sales Return</a>
            </li>
        </ul>    
            <!-- imageLoader - START -->

            <img id='HiddenLoadingImage' src="../../img/page-loader.gif" class="LoadingProgress" />

            <!-- imageLoader - END -->

            <div class="row-fluid">
                <div class="box  span12">
                    <div class="box-header" data-original-title>
                        <h2><i class="halflings-icon edit"></i><span class="break"></span> Create Sales Return</h2>
                    </div>
                    <div class="box-content">
                        <div class="row-fluid">	
                            <div class="box span12">
                                <div class="box-content">                                 
                                    <div class="span1">
                                        <div class="control-group">
                                            <label class="control-label" for="TBSearchDebtNot">Sales Return</label>
                                        </div>
                                    </div>
                                    <div class="span10">
                                        <div class="controls">
                                            <div class="input-append">
                                                <asp:TextBox runat="server" class="span12" ID="TBSearchDebtNot" AutoPostBack="true" OnTextChanged="TBSearchDebtNot_TextChanged" ></asp:TextBox><asp:LinkButton runat="server" ID="LnkBtn_MDN" CssClass="add-on" ><i class="icon-search"></i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span12">
                                        <div class="controls">
                                            <div class="scrollit">
                                                <asp:GridView ID="GVScrhMDN" runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found!"  class="table table-striped table-bordered" AllowPaging="True" PageSize="4" AutoGenerateColumns="False" DataKeyNames="Mdn_id" OnPageIndexChanging="GVScrhMDN_PageIndexChanging" OnRowCommand="GVScrhMDN_RowCommand" OnRowDeleting="GVScrhMDN_RowDeleting" >
                                                    <Columns>
                                                        <asp:BoundField DataField="Mdn_id" HeaderText="ID" SortExpression="Mdn_id" />
                                                        <asp:BoundField DataField="CustomerName" HeaderText="Customer" SortExpression="CustomerName" />
                                                        <asp:BoundField DataField="Mdn_dat" HeaderText="Date" SortExpression="Mdn_dat" />
                                                        <asp:BoundField DataField="CreatedBy" HeaderText="Created By" SortExpression="CreatedBy" />                                            
                                                        <asp:BoundField DataField="CreatedAt" HeaderText="Created At" SortExpression="CreatedAt" />                                            
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LBtnScrhMSal" runat="server" ForeColor="Green" CommandName="Select" >Select</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LBtnDel" runat="server" ForeColor="Red" CommandName="Delete" >Delete</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LBtnShow" runat="server" ForeColor="Orange" CommandName="Show" >Show</asp:LinkButton>
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
                                    <h1><span style="color:black;"> Sales Return</span></h1>                                    
                                </div>
                            <div class="span6"></div>
                            <div class="span3">
                                <div class="box span12">
                                    <div class="box-header" data-original-title>
                                        <h2><i class="halflings-icon calendar"></i><span class="break"></span>Date</h2>
                                    </div>
                                    <div class="box-content">
                                        <asp:TextBox runat="server" class="span10 datepicker" ID="TBDNDat" placeholder="02/16/12" ></asp:TextBox>
                                        <asp:Label runat="server" class="span12" ID="TBDebitNot" Visible="False" Text="0" ></asp:Label>
                                    </div>
                                </div>
                            </div>    
                            <div class="row-fluid">	
                                <div class="box span12">
                                    <div class="box-content">
                                        <div class="span1">&nbsp;</div>
                                        <asp:Panel ID="pnl_act" runat="server">
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
                                                        <asp:CheckBox ID="chk_prtd" runat="server" Text="Printed" />
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <div class="span12">
                                            <div class="control-group">
                                              <label style="color:black" for="TB_Sal" >Sales</label>
                                                <div class="controls">
                                                    <asp:TextBox ID="TB_Sal" runat="server" CssClass="span12" AutoPostBack="true"  placeholder="Sales Invoices.." OnTextChanged="TB_Sal_TextChanged2"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ServiceMethod="GetSalNo" CompletionListCssClass="completionList"
                                                        CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                          MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" 
                                                         CompletionSetCount="10" TargetControlID="TB_Sal" ID="AutoCompleteExtender2"  
                                                        runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
                                                    <asp:DropDownList id="DDL_Sal" Visible="false" runat="server" data-rel="chosen" CssClass="span12" ></asp:DropDownList>
                                                    <asp:Label ID="v_sal" runat="server" ForeColor="Red" Font-Bold="true" Text=""></asp:Label>                                           
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span9"></div>
                                        <div class="span12">
                                            <asp:Panel ID="pnlsaldat" runat="server">
                                                <div class="span3">
                                                   <div class="control-group">
                                                        <label style="color:black" for="TBSalDat" >Sale Date</label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="TBSalDat" runat="server" CssClass="span12"></asp:TextBox>
                                                        </div>
                                                   </div>
                                                </div>
                                            </asp:Panel>
                                        </div>    
                                        <div class="span12">
                                            <div class="control-group">
                                              <label style="color:black" for="ddl_Cust" >Customer</label>
                                                <div class="controls">
                                                    <asp:DropDownList id="ddl_Cust" runat="server" data-rel="chosen" CssClass="span12"></asp:DropDownList>                                           
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Panel ID="pnl_supbook" runat="server">
                                            <div class="span12">
                                                <div class="control-group">
                                                  <label style="color:black" for="ddl_sup" >Supplier</label>
                                                    <div class="controls">
                                                        <asp:DropDownList id="ddl_sup" runat="server" data-rel="chosen" CssClass="span12"></asp:DropDownList>                                           
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="span12">
                                                <div class="control-group">
                                                  <label style="color:black" for="ddl_bokr" >Booker</label>
                                                    <div class="controls">
                                                        <asp:DropDownList id="ddl_bokr" runat="server" data-rel="chosen" CssClass="span12"></asp:DropDownList>                                           
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <div class="span12">
                                            <div class="control-group">
                                                <label style="color:black" for="DDLRetntyp" >Return Type</label>
                                                <div class="controls">
                                                    <asp:DropDownList data-rel="chosen" ID="DDLRetntyp" runat="server" CssClass="span12" OnTextChanged="DDLRetntyp_TextChanged">
                                                        <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                                        <asp:ListItem Value="Exchange">Exchange</asp:ListItem>
                                                        <asp:ListItem Value="Defected">Defected</asp:ListItem>
                                                        <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
                                                        <asp:ListItem Value="Return">Return</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:Label ID="v_type" runat="server" ForeColor="red" Font-Bold="true" Text=""></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span12">
                                            <div class="control-group">
                                                <label style="color:black" for="TBReason" >Reasons</label>
                                                <div class="controls">
                                                    <asp:TextBox runat="server" class="span4" ID="TBReason" AutoPostBack="true" OnTextChanged="TBReason_TextChanged"></asp:TextBox>  
                                                    <asp:AutoCompleteExtender ServiceMethod="GetReason" CompletionListCssClass="completionList"
                                                        CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                         MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10" 
                                                        TargetControlID="TBReason" ID="AutoCompleteExtender1"  
                                                        runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>                                  
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span12">
                                            <div class="control-group">
                                                <label style="color:black" for="TBRmk" >Remarks</label>
                                                <div class="controls">
                                                    <asp:TextBox runat="server" class="span12" ID="TBRmk"></asp:TextBox>                                    
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
                        <h2><i class="halflings-icon th-list"></i><span class="break"></span> Items</h2>
                    </div>
                    <div class="box-content">
                        <div class="scrollit">
                            <asp:Panel ID="PnlCrtSalItem" runat="server">
                                <div class="row-fluid">	
                                    <div class="span12">
                                        <div class="box-content span12">                            
                                        <asp:GridView ID="GVDNItm" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="true" EmptyDataText="No Record Exits!!"  class="table table-striped table-bordered" OnRowDeleting="GVDNItm_RowDeleting" OnRowDataBound="GVDNItm_RowDataBound" >
                                            <Columns>
                                                <asp:TemplateField HeaderText="ITEMS">  
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddl_Itm" runat="server"  data-rel="chosen" ></asp:DropDownList>
                                                        <asp:Label ID="lbl_itm" Visible="false" runat="server" Text='<%# Eval("ITEMS")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DESCRIPTIONS">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TbaddSalItmDscptin" runat="server" Text='<%# Eval("DESCRIPTIONS")%>' style="width:120px; height:20px; background:none; border:none;" ></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="QTY">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TbAddSalItmQty" runat="server" Text='<%# Eval("QTY")%>' AutoPostBack="true"  style="width:50px; height:20px; background:none; border:none;" OnTextChanged="TbAddSalItmQty_TextChanged"  ></asp:TextBox>
                                                        <asp:Label ID="lblckqty" runat="server" Font-Size="Smaller" ForeColor="Red"></asp:Label>
                                                        <asp:Label ID="lblqty" runat="server" Text='<%# Eval("QTY")%>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SIZE">
                                                    <ItemTemplate>                                              
                                                        <asp:TextBox ID="TbAddSalUnit" runat="server" Text='<%# Eval("UNIT")%>'  style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>                    
                                                    </ItemTemplate>
                                                </asp:TemplateField>                     
                                                <asp:TemplateField HeaderText="RATE">
                                                    <ItemTemplate>                                              
                                                        <asp:TextBox ID="TBTtl" runat="server" Text='<%# Eval("rat")%>'  style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>                    
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>                                              
                                                        <asp:TextBox ID="TBamt" runat="server" Text='<%# Eval("Amount")%>'  style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>                    
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="REMARKS">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TBDDNRmk" runat="server" Text='<%# Eval("REMARKS")%>' style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>
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
                        <div class="span12"></div>
                        <div class="span12">
                            <asp:Label ID="Labelttl" runat="server" Text="Previous Balance:" ></asp:Label>
                            <asp:Label ID="lbl_ttl" runat="server" Text="0" ></asp:Label>
                        </div>
                        <div class="span12"></div>
                        <div class="span12">
                            <asp:Button runat="server"  ID="btnSave" Text="Save"  CssClass="btn btn-info" OnClick="btnSave_Click" />   
                            <asp:Button runat="server"  ID="btnCancl" Text="Cancel" CssClass="btn" OnClick="btnCancl_Click"  />       
                        </div>  
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="HFSAL" runat="server" />
            <asp:HiddenField ID="HFMDN" runat="server" />
            <asp:HiddenField ID="HFDDN" runat="server" />
            <asp:HiddenField ID="HFDebtNot" runat="server" />
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

            <script src="Controller/Common.js"></script>
        </ContentTemplate>
    </asp:UpdatePanel>
  
</asp:Content>
