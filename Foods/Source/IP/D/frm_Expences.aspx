<%@ Page Title="Transactions" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="frm_Expences.aspx.cs" Inherits="Foods.frm_Expences" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        <style type="text/css">
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
                <li><a href="frm_Expences.aspx">Transactions</a></li>
            </ul>
            <!-- imageLoader - START -->
            <img id='HiddenLoadingImage' src="../../img/page-loader.gif" class="LoadingProgress" />
            <!-- imageLoader - END -->
            <div class="row-fluid">
                <div class="box  span12">
                    <div class="box-header" data-original-title>
                        <h2><i class="halflings-icon edit"></i><span class="break"></span> Create Transactions </h2>
                    </div>
                    <div class="box-content">
                      
                        <div class="row-fluid">	
                            <div class="box span12">
                                <div class="box-content">                                                                     
                                    <div class="span12" style="text-align:center;">
                                        <asp:Label ID="lblmssg" runat="server" ForeColor="Red" ></asp:Label>
                                    </div>
                                     <div class="span12">
                                         <asp:Label ID="Label1" Font-Bold="true" Font-Size="Large"  runat="server" Text="Opening Balance:"></asp:Label>
                                         <asp:Label ID="lbl_Openbalance" runat="server" Text="0"></asp:Label>
                                     </div>
                                    <div class="span12">
                                                <div class="span5">
                                                     <asp:Label ID="lbl_openbal" runat="server" Text="Amount Variation"></asp:Label>
                                                     <asp:TextBox ID="TBOpeBal" AutoPostBack="true" OnTextChanged="TBOpeBal_TextChanged" Text="0" runat="server" Height="20px" Width="190px" placeholder="Enter OPening Balance.."></asp:TextBox>
                                                </div>
                                            </div>
                                     <div class="span12" style="text-align:center;">                                      
                                       <asp:Button ID="btn_pay" runat="server" Text="Payments" CssClass="btn btn-info" OnClick="btn_pay_Click" />
                                       <asp:Button ID="btn_incm" runat="server" Text="Recieve" CssClass="btn btn-success" OnClick="btn_incm_Click" />
                                       <asp:Button ID="btn_CashBook" runat="server" Text="Cash Book" CssClass="btn btn-danger" OnClick="btn_CashBook_Click"  />
                                    </div>
                                    
                                    <div style="text-align:center; margin-top:50px; margin-left:20px">
                                        <asp:Label ID="v_choice" runat="server" ForeColor="Black" Font-Bold="true" Text="" Font-Size="Medium"></asp:Label> 
                                    </div>
                                    <asp:Panel ID="pnl_pay" runat="server">
                                        <fieldset>
                                            <legend>Payments</legend>
                                            <div class="span12">
                                                <div class="span5">
                                                     <asp:Label ID="Label3" runat="server" Text=" DATE" Height="30px" Width="69px"></asp:Label>
                                                     <asp:TextBox ID="date" runat="server" Height="20px" placeholder="Enter Date.."></asp:TextBox>
                                                     <asp:CalendarExtender ID="Calendar1" CssClass="calender" PopupButtonID="imgPopup" 
                                                runat="server" TargetControlID="date" Format="dd/MM/yyyy"> </asp:CalendarExtender>  
                                                </div>
                                            </div>
                                            <div class="span12">
                                                <div class="controls">
                                                    <div>
                                                       <asp:Label ID="Label7" runat="server" Height="30px" Text="Accounts" Width="70px"></asp:Label>
                                                         <asp:DropDownList ID="DDL_ACC" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDL_ACC_SelectedIndexChanged">
                                                         </asp:DropDownList>
                                                         <asp:TextBox ID="dllexptyp" runat="server" Width="341px" placeholder="--Select Account--" AutoPostBack="true" OnTextChanged="dllexptyp_TextChanged" ></asp:TextBox>
                                                         <asp:AutoCompleteExtender ServiceMethod="GetAcc" CompletionListCssClass="completionList"
                                                            CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                            MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10"
                                                            TargetControlID="dllexptyp" ID="AutoCompleteExtender2"  
                                                            runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:Panel ID="pnl_bill"  runat="server">
                                                <div class="span12">
                                                    <div class="span5">
                                                        <asp:Label ID="Label4" runat="server" Text="Bill No" Height="30px" Width="69px"></asp:Label>
                                                        <asp:TextBox ID="bilno" runat="server" Height="20px" placeholder="Enter Bill No.."></asp:TextBox>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlvtyp" runat="server">
                                                <div class="span6" >
                                                    <div class="control-group">
                                                        <label style="color:black" for="DDL_Vchtyp" >Voucher Type</label>
                                                        <div class="controls">
                                                            <asp:DropDownList ID="DDL_Vchtyp" runat="server" data-rel="chosen"
                                                                    CssClass="span12" AutoPostBack="True" OnSelectedIndexChanged="DDL_Vchtyp_SelectedIndexChanged">                                               
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="span5" >
                                                    <div class="control-group">
                                                        <label style="color:black" for="DDL_Paytyp" >Payment Type</label>
                                                        <div class="controls">
                                                            <asp:DropDownList ID="DDL_Paytyp" runat="server" data-rel="chosen" CssClass="span12" AutoPostBack="True" OnSelectedIndexChanged="DDL_Paytyp_SelectedIndexChanged" >
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnl_bnk" runat="server" CssClass="span12" >
                                                <div class="span6" >
                                                    <div class="control-group">
                                                        <div class="controls">
                                                             <asp:GridView ID="GVPay" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true"
                                                                  EmptyDataText="No Record Exits!!"  class="table table-striped table-bordered"
                                                                  OnRowDeleting="GVPay_RowDeleting" OnRowDataBound="GVPay_RowDataBound"  >
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Bank">  
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblbnk" runat="server" Text='<%# Eval("bank") %>' Visible = "false" />
                                                                            <asp:DropDownList ID="DDL_Bnk" runat="server" data-rel="chosen" CssClass="span12" style="width:120px; height:35px;" ></asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Cheque No">  
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TBChqnum" runat="server" Text='<%# Eval("CheqNum") %>' style="width:120px; height:35px;" placeholder="chequqe No"></asp:TextBox>                                                                        
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Amount">  
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TBChqAmnt" runat="server" Text='<%# Eval("CheqAmnt") %>' style="width:120px; height:35px;" placeholder="chequqe Amount"></asp:TextBox>                                                                        
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Date">  
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TBChqDate" runat="server" Text='<%# Eval("CheqDate") %>' style="width:120px; height:35px;" placeholder="chequqe Date"></asp:TextBox>                                                                        
                                                                            <asp:CalendarExtender ID="Calendar1" CssClass="calender" PopupButtonID="imgPopup" 
                                                                                runat="server" TargetControlID="TBChqDate" Format="M/dd/yyyy"> </asp:CalendarExtender>  
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkbtnaddbnk" OnClick="linkbtnaddbnk_Click" runat="server">+</asp:LinkButton>
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
                                            <asp:Panel ID="pnl_chqamt" runat="server" CssClass="span12">
                                                <div class="span6" >
                                                    <div class="control-group">
                                                        <label style="color:black" for="TB_ChqAmt" >Cheque Amount</label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="TB_ChqAmt" runat="server" CssClass="span12" Text="0" placeholder="Cheque Amount"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnl_cshamt" runat="server" CssClass="span12" >
                                                <div class="span12" >
                                                    <div class="control-group">
                                                        <label style="color:black" for="TB_CshAmt" >Cash Amount</label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="TB_CshAmt" runat="server" CssClass="span12" Text="0" placeholder="Cash Amount"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <div class="span12">
                                                <div class="controls">
                                                    <div class="span10">
                                                      <asp:Label ID="Label5" runat="server" Text="Amount Paid"></asp:Label>
                                                      <asp:TextBox ID="amtpad" runat="server" Height="20px" AutoPostBack="true" Text="0"  placeholder="Enter Amount Paid.." OnTextChanged="amtpad_TextChanged"></asp:TextBox>
                                                        <asp:Panel ID="pnl_loan" runat="server">
                                                            <asp:CheckBox ID="chkloan" runat="server" /><asp:Label ID="lbl_loan" runat="server" Text="Loan"></asp:Label>
                                                        </asp:Panel>
                                                      <asp:Panel ID="amtremn" runat="server">
                                                        Balance:
                                                        <asp:Label ID="lbl_amtremain" runat="server" Text="0" ></asp:Label>
                                                      </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="span12">
                                                <div class="controls">
                                                    <div>
                                                      <asp:Label ID="lbl_Prebal" runat="server" Text="Previous Balance"></asp:Label>
                                                      <asp:TextBox ID="TBPrebal" runat="server" Height="20px" Text="0" placeholder="Previous Balance..."></asp:TextBox>
                                                   </div>
                                                </div>
                                            </div>
                                            <div class="span12">
                                                <div class="controls">
                                                    <div>
                                                        <asp:Label ID="Label6" runat="server" Text="Expense Remarks"></asp:Label>
                                                        <asp:TextBox ID="exprmk" runat="server" Height="20px" TextMode="MultiLine" placeholder="Enter Remarks.."></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="span12">
                                        <div class="controls">
                                            <div>
                                                <asp:Button ID="Button1"  CssClass="btn btn-info" runat="server" OnClick="Button1_Click" Text="Save" />
                                            </div>
                                        </div>
                                    </div>
                                        </fieldset>
                                    </asp:Panel>
                                    <asp:Panel ID="Panel_incom" runat="server">
                                        <fieldset>
                                            <legend>Recieve</legend>
                                            <div class="span12">
                                                <div class="span5">
                                                     <asp:Label ID="Label2" runat="server" Text=" DATE" Height="30px" Width="69px"></asp:Label>
                                                     <asp:TextBox ID="TBIncodat" runat="server" Height="20px" placeholder="Enter Date.."></asp:TextBox>
                                                     <asp:CalendarExtender ID="CalendarExtender1" CssClass="calender" PopupButtonID="imgPopup" 
                                                runat="server" TargetControlID="TBIncodat" Format="M/dd/yyyy"> </asp:CalendarExtender>  
                                                </div>
                                            </div>
                                            <div class="span12">
                                                <div class="controls">
                                                    <div>
                                                       <asp:Label ID="Label8" runat="server" Height="30px" Text="Accounts" Width="70px"></asp:Label>
                                                         <asp:DropDownList ID="DDL_IncomAcc" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDL_IncomAcc_SelectedIndexChanged">
                                                         </asp:DropDownList>
                                                         <asp:TextBox ID="TBincomacc" runat="server" Width="341px" placeholder="--Select Account--" AutoPostBack="true" OnTextChanged="TBincomacc_TextChanged" ></asp:TextBox>
                                                         <asp:AutoCompleteExtender ServiceMethod="GetIncomAcc" CompletionListCssClass="completionList"
                                                            CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                            MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10"
                                                            TargetControlID="TBincomacc" ID="AutoCompleteExtender1"  
                                                            runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:Panel ID="icombillpnl" runat="server">
                                                <div class="span12">
                                                        <div class="span5">
                                                            <asp:Label ID="Label9" runat="server" Text="Bill No" Height="30px" Width="69px"></asp:Label>
                                                            <asp:TextBox ID="TBincomBill" runat="server" Height="20px" placeholder="Enter Bill No.."></asp:TextBox>
                                                        </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlvtypincom" runat="server">
                                                <div class="span6" >
                                                    <div class="control-group">
                                                        <label style="color:black" for="DDL_incomvtyp" >Voucher Type</label>
                                                        <div class="controls">
                                                            <asp:DropDownList ID="DDL_incomvtyp" runat="server" data-rel="chosen"
                                                                    CssClass="span12" AutoPostBack="True" OnSelectedIndexChanged="DDL_incomvtyp_SelectedIndexChanged">                                               
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="span5" >
                                                    <div class="control-group">
                                                        <label style="color:black" for="DDL_Paytypincom" >Payment Type</label>
                                                        <div class="controls">
                                                            <asp:DropDownList ID="DDL_Paytypincom" runat="server" data-rel="chosen" CssClass="span12" AutoPostBack="True" OnSelectedIndexChanged="DDL_Paytypincom_SelectedIndexChanged" >
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnl_incombnk" runat="server" CssClass="span12" >
                                                <div class="span6" >
                                                    <div class="control-group">
                                                        <div class="controls">
                                                             <asp:GridView ID="GVbkExp" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true"
                                                                  EmptyDataText="No Record Exits!!"  class="table table-striped table-bordered"
                                                                  OnRowDeleting="GVbkExp_RowDeleting" OnRowDataBound="GVbkExp_RowDataBound" >
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Bank">  
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblincombnk" runat="server" Text='<%# Eval("icombank") %>' Visible = "false" />
                                                                            <asp:DropDownList ID="DDL_incombnk" runat="server" style="width:120px; height:35px;">                                               
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Cheque No">  
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TBChqno" runat="server" Text='<%# Eval("CheqNo") %>' style="width:120px; height:35px;" placeholder="chequqe No"></asp:TextBox>                                                                        
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Amount">  
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TBChqAmt" runat="server" Text='<%# Eval("CheqAmt") %>' style="width:120px; height:35px;" placeholder="chequqe Amount"></asp:TextBox>                                                                        
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Date">  
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TBChqDat" runat="server" Text='<%# Eval("CheqDat") %>' style="width:120px; height:35px;" placeholder="chequqe Date"></asp:TextBox>                                                                        
                                                                            <asp:CalendarExtender ID="Calendar1" CssClass="calender" PopupButtonID="imgPopup" 
                                                                                runat="server" TargetControlID="TBChqDat" Format="M/dd/yyyy"> </asp:CalendarExtender>  
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
                                            <asp:Panel ID="pnl_chqincomamt" runat="server" CssClass="span12">
                                                <div class="span6" >
                                                    <div class="control-group">
                                                        <label style="color:black" for="TB_ChqAmtincom" >Cheque Amount</label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="TB_ChqAmtincom" runat="server" CssClass="span12" Text="0" placeholder="Cheque Amount"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnl_cshamtincom" runat="server" CssClass="span12">
                                                <div class="span12" >
                                                    <div class="control-group">
                                                        <label style="color:black" for="TB_CshAmtincom" >Cash Amount</label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="TB_CshAmtincom" runat="server" CssClass="span12" Text="0" placeholder="Cash Amount"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <div class="span12">
                                                <div class="controls">
                                                    <div>
                                                      <asp:Label ID="Label10" runat="server" Text="Amount Paid"></asp:Label>
                                                      <asp:TextBox ID="TB_incompaids" runat="server" Height="20px" AutoPostBack="true" Text="0"  placeholder="Enter Amount Paid.." OnTextChanged="TB_incompaid_TextChanged"></asp:TextBox>
                                                      <asp:Panel ID="Panel6" runat="server">
                                                        Balance:
                                                        <asp:Label ID="lbl_amtremainincom" runat="server" Text="0" ></asp:Label>
                                                      </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="span12">
                                                <div class="controls">
                                                    <div>
                                                      <asp:Label ID="Label12" runat="server" Text="Previous Balance"></asp:Label>
                                                      <asp:TextBox ID="TBPrebalincom" runat="server" Height="20px" Text="0" placeholder="Previous Balance..."></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="span12">
                                                <div class="controls">
                                                    <div>
                                                        <asp:Label ID="Label13" runat="server" Text="Expense Remarks"></asp:Label>
                                                        <asp:TextBox ID="TbincomRmks" runat="server" Height="20px" TextMode="MultiLine" placeholder="Enter Remarks.."></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="span12">
                                        <div class="controls">
                                            <div>
                                                <asp:Button ID="btn_incom"  CssClass="btn btn-info" runat="server" OnClick="btn_incom_Click" Text="Save" />
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                    </asp:Panel>
                                <asp:Panel ID="Panel_CashBook" runat="server">
                                        <fieldset>
                                            <legend>Cash Book</legend>
                                            
                                            <div class="span12">
                                                <div class="controls">
                                                    <div>
                                                     <asp:Label ID="lbl_ttlsales" runat="server" Text="Total Earning:"></asp:Label>
                                                       <asp:Label ID="lbl_totalSal" runat="server" Height="30px" Width="70px"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="span12">
                                                <div class="controls">
                                                    <div>
                                                       <asp:Label ID="lbl_ttlExpes" runat="server" Text="Total Expences:"></asp:Label>
                                                       <asp:Label ID="lbl_ttlExp" runat="server" Height="30px" Width="70px"></asp:Label>
                                                    </div>
                                                </div>
                                            </div><div class="span12">
                                                <div class="controls">
                                                    <div>
                                                       <asp:Label ID="lbl_CLosBal" runat="server" Text="Closing Balance:"></asp:Label>
                                                       <asp:Label ID="lbl_ClsBal" runat="server" Height="30px"  Width="70px"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                </asp:Panel>
                                </div>
                            </div>
                        </div>
                </div>
            </div>
            <asp:HiddenField ID="HFUsrId" runat="server" />  
            </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
