<%@ Page Title="Reports" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
     CodeBehind="Rpt_PurchaseForm.aspx.cs" Inherits="Foods.Rpt_PurchaseForm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
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
           <div class="row-fluid">	
               <asp:LinkButton ID="lnkbtn_inv" CssClass="quick-button metro yellow span3" runat="server" OnClick="lnkbtn_inv_Click">
                   <i class="icon-group"></i>
				   <p>Inventory</p>
               </asp:LinkButton>
               <asp:LinkButton ID="lnkbtn_sal" CssClass="quick-button metro red span3" runat="server" OnClick="lnkbtn_sal_Click">
					<i class="icon-comments-alt"></i>
					<p>Sales</p>
               </asp:LinkButton>
               <asp:LinkButton ID="lnkbtn_Ral" CssClass="quick-button metro red span3" runat="server" OnClick="lnkbtn_Ral_Click">
					<i class="icon-comments-alt"></i>
					<p>Retail Sales</p>
               </asp:LinkButton>				
               <asp:LinkButton ID="lnkbtn_pur" CssClass="quick-button metro blue span3" runat="server" OnClick="lnkbtn_pur_Click">
					<i class="icon-shopping-cart"></i>
					<p>Purchase</p>
               </asp:LinkButton>	
            </div> 
            <div class="span12"></div>
            <div class="row-fluid">			
               <asp:LinkButton ID="lnkbtn_pro" CssClass="quick-button metro green span3" runat="server" OnClick="lnkbtn_pro_Click">
					<i class="icon-barcode"></i>
					<p>Credit Reports</p>
               </asp:LinkButton>              				    		  
                <asp:LinkButton ID="lnkprof" CssClass="quick-button metro green span3" runat="server" OnClick="lnkprof_Click">
                    <i class="icon-calendar"></i>
                    <p>Profit Sheet</p>
                </asp:LinkButton>
         	
                <asp:LinkButton ID="lnkexp" CssClass="quick-button metro yellow span3" runat="server" OnClick="lnkexp_Click">
                    <i class="icon-calendar"></i>
                    <p>Expences</p>
                </asp:LinkButton>
                 <asp:LinkButton ID="lnk_acc" CssClass="quick-button metro yellow span3" runat="server" OnClick="lnk_acc_Click" >
                    <i class="icon-calendar"></i>
                    <p>Accounts</p>
                </asp:LinkButton>
                </div> 
            <div class="span12"></div>
            <div class="row-fluid">		
                <asp:LinkButton ID="lnk_Transc" CssClass="quick-button metro yellow span3" runat="server" OnClick="lnk_Transc_Click" >
                    <i class="icon-calendar"></i>
                    <p>Transactions</p>
                </asp:LinkButton>		
                <div class="clearfix"></div>
            </div>
            <div class="row-fluid span12"></div>
<div class="row-fluid">	
    <div class="box span12">
        <div class="box-content">                                 
            <div class="span10">
                <div class="control-group">
                    <label class="control-label"  runat="server"> please Select Report Type... </label>
                </div>
            </div>
            <div class="span10">
                <div class="controls">
                    <div class="input-append">
                            <asp:DropDownList ID="ddl_rpttyp" data-rel="chosen"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_rpttyp_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="span10">
                <asp:Label ID="lbl_error" ForeColor="Red" runat="server"></asp:Label>
            </div>

            <!-- Account -->
            <asp:Panel ID="pnl_acc" runat="server">
                 <div class="span12">
                    <label class="span3">
                        <span>Date Wise</span>
                    </label>
                    <div class="span4">
                        <asp:TextBox ID="TBFLed" runat="server" placeholder="ex. 12/10/2015..." />
                        <asp:CalendarExtender ID="CalendarExtender10" PopupButtonID="imgPopup" runat="server" TargetControlID="TBFLed" Format="dd/MM/yyyy" />
                    </div>
                    <div class="span4">
                        <asp:TextBox ID="TBTLed" runat="server" placeholder="ex. 12/10/2015..." />
                        <asp:CalendarExtender ID="CalendarExtender11" PopupButtonID="imgPopup" runat="server" TargetControlID="TBTLed" Format="dd/MM/yyyy" />
                    </div>
                     <div class="span12"></div>
                      <label class="span3">
                        <span>Accounts.</span>
                    </label>
                    <div class="span8">
                        <asp:DropDownList ID="DDL_LedgerAcc" runat="server"></asp:DropDownList>
                    </div>
                </div>
            </asp:Panel>
            <!-- Panel Ware House -->

            <asp:Panel ID="pnl_Expence" runat="server">
                 <div class="span12">
                    <label class="span3">
                        <span>Date Wise</span>
                    </label>
                    <div class="span4">
                        <asp:TextBox ID="TBSExp" runat="server" placeholder="ex. 12/10/2015..." />
                        <asp:CalendarExtender ID="CalendarExtender8" PopupButtonID="imgPopup" runat="server" TargetControlID="TBSExp" Format="MM/dd/yyyy" />
                    </div>
                    <div class="span4">
                        <asp:TextBox ID="TBFExp" runat="server" placeholder="ex. 12/10/2015..." />
                        <asp:CalendarExtender ID="CalendarExtender9" PopupButtonID="imgPopup" runat="server" TargetControlID="TBFExp" Format="MM/dd/yyyy" />
                    </div>
                     <div class="span12"></div>
                      <label class="span3">
                        <span>Account.</span>
                    </label>
                    <div class="span8">
                        <asp:DropDownList ID="ddlexpence" runat="server"></asp:DropDownList>
                    </div>
                </div>
            </asp:Panel>


            <asp:Panel ID="pnl_wh" runat="server">
                <div class="span3">
                    <div class="control-group">
                        <label class="control-label" for="ddl_wh">Ware House</label>
                    </div>
                </div>
                <div class="span10">
                    <div class="controls">
                        <div class="input-append">
                                <asp:DropDownList ID="ddl_wh" data-rel="chosen"  runat="server"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <!-- Panel PayRoll -->

            <asp:Panel ID="pnl_payroll" runat="server">
                <div class="span6">
                    <div class="span3">
                        <div class="control-group">
                            <label class="control-label" for="DDLEmp">Employee</label>
                        </div>
                    </div>
                    <div class="span4">
                        <div class="controls">
                            <div class="input-append">
                                    <asp:DropDownList ID="DDLEmp" data-rel="chosen"  runat="server"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="span6">
                    <div class="span3">
                        <div class="control-group">
                            <label class="control-label" for="DDLEmp">Select Month</label>
                        </div>
                    </div>
                    <div class="span3">
                        <div class="controls">
                            <div class="input-append">
                                    <asp:TextBox ID="TBCal" CssClass="datepicker" runat="server" ></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>                
            </asp:Panel>

            <!-- Panel Quotations -->

            <asp:Panel ID="pnl_Quot" runat="server">
                <div class="span12">
                   <div class="span6">
                    <div class="span3">
                        <div class="control-group">
                            <label class="control-label" for="DDLEmp">Quotation</label>
                        </div>
                    </div>
                    <div class="span4">
                        <div class="controls">
                            <div class="input-append">
                                    <asp:DropDownList ID="DDLQuot" data-rel="chosen"  runat="server"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="span6">
                    <div class="span3">
                        <div class="control-group">
                            <label class="control-label" for="DDLEmp">Select Month</label>
                        </div>
                    </div>
                    <div class="span3">
                        <div class="controls">
                            <div class="input-append">
                                    <asp:TextBox ID="TBDate" CssClass="datepicker" runat="server" ></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
                <div class="span12">
                <div class="span6">
                    
                    <div class="span4">
                        <div class="control-group">
                            <label class="control-label" for="DDLCust">Select Customers</label>
                        </div>
                    </div>
                    <div class="span3">
                        <div class="controls">
                            <div class="input-append">
                                <asp:DropDownList ID="DDLCust" data-rel="chosen"  runat="server"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                    </div>
            </asp:Panel>
            
            <!-- Panel Load Sheet --> 

            <asp:Panel ID="LoadSheet" runat="server" >
                <div class="span12">
                   <div class="span6">
                       <div class="span3">
                           <div class="control-group">
                                <label class="control-label" for="DDLLS">User</label>
                                <asp:DropDownList ID="DDLLS" data-rel="chosen"  runat="server"></asp:DropDownList>
                           </div>
                       </div>
                    </div>
                <div class="span6">
                    <div class="span3">
                        <div class="control-group">
                            <label class="control-label" for="TBDatLS">Select Date</label>
                        </div>
                    </div>
                    <div class="span3">
                        <div class="controls">
                            <div class="input-append">
                                    <asp:TextBox ID="TBDatLS" CssClass="datepicker" runat="server" ></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </asp:Panel>

            <!-- Panel Requisitions -->

            <asp:Panel ID="pnl_Req" runat="server">
                <div class="span12">                            
                            <div class="span6">
						            <label class="span4">
							            <span>Requisition NO</span>                                                        
                                    </label>
                                    <div class="span8">
                                        <asp:DropDownList ID="DDLReq" runat="server" CssClass="span10" data-rel="chosen"></asp:DropDownList>
                                    </div>						            
						        </div>
                        </div>  
                        <div class="span12">                            
                            <div class="span6">
						            <label class="span4">
							            <span>Date</span>                                                        
                                    </label>
                                    <div class="span8">
                                        <asp:TextBox ID="TBReq_dat" runat="server" placeholder="ex. 12/10/2015..." class="datepicker" />
                                        <asp:RequiredFieldValidator ID="RFVReqDat" runat="server" ForeColor="Red" ControlToValidate="TBReq_dat" ValidationGroup="Req" ErrorMessage="Please Enter the Requisitions Date!"></asp:RequiredFieldValidator>
                                        <%--<asp:CompareValidator ID="CVReqDat" Type="Date" Operator="DataTypeCheck" ControlToValidate="TBReq_dat" runat="server" ValidationGroup="Req" ForeColor="Red" ErrorMessage="Please Enter the Valid Date!"></asp:CompareValidator>--%>
                                    </div>						            
						    </div>
                            <div class="span6">
						            					            
						    </div>
                        </div>
                        <div class="span12">                                                        
						    <label class="span3">
							    <span>Vendor</span>                                                        
                            </label>
                            <div class="span10">
                                <asp:DropDownList ID="DDL_Ven" runat="server" CssClass="span10" data-rel="chosen"></asp:DropDownList>
                            </div>						            
                        </div>
                         <div class="span12">
						    <label class="span3">
							    <span>Local Requisition</span>                                                        
                            </label>
                            <div class="span3">
                                <asp:CheckBox ID="chk_req" AutoPostBack="true" runat="server" OnCheckedChanged="chk_req_CheckedChanged" />
                            </div>						            
						</div>

                        <!-- Panel Local -->

                        <asp:Panel runat="server" ID="pnl_local">
                            <div class="span12">                                                        
						        <label class="span3">
							        <span>Requested By</span>                                                        
                                </label>
                                <div class="span10">
                                    <asp:DropDownList ID="DDL_emp" runat="server" CssClass="span10" data-rel="chosen"></asp:DropDownList>
                                </div>						            
                            </div>
                            <div class="span12">                                                        
						        <label class="span3">
							        <span>Department</span>                                                        
                                </label>
                                <div class="span10">
                                    <asp:DropDownList ID="DDL_Dpt" runat="server" CssClass="span10" data-rel="chosen"></asp:DropDownList>
                                </div>						            
                            </div>
                        </asp:Panel>
            </asp:Panel>      

            <!-- Panel Purchase -->

                        <asp:Panel ID="pnl_pur" runat="server">
                 <div class="span12">

                </div>
                <div class="span12">
                    <label class="span3">
                        <span>Date Wise</span>
                    </label>
                    <div class="span10">
                        <div class="span5">
                            <label class="span3">
                                <span>From Date</span>
                            </label>
                        <asp:TextBox ID="TBFDWise" runat="server" placeholder="ex. 12/10/2015..." class="datepicker" />
                        <asp:CalendarExtender ID="CETBFDWise" PopupButtonID="imgPopup" runat="server" TargetControlID="TBFDWise" Format="yyyy-MM-dd" />
                        </div>
                        <div class="span5">
                            <label class="span3">
                                <span>To Date</span>
                            </label>
                        <asp:TextBox ID="TBTDWise" runat="server" placeholder="ex. 12/10/2015..." class="datepicker" />
                        <asp:CalendarExtender ID="CETBTDWise" PopupButtonID="imgPopup" runat="server" TargetControlID="TBTDWise" Format="yyyy-MM-dd" />
                        </div>
                    </div>
                </div>
                 <div class="span12">                                                        
                    <label class="span3">
                        <span>Month Wise</span>                                                        
                    </label>
                    <div class="span10">
                        <asp:DropDownList ID="DDLMnthwis" runat="server" CssClass="span3" data-rel="chosen" OnSelectedIndexChanged="DDLMnthwis_SelectedIndexChanged"></asp:DropDownList>                        
                        <asp:DropDownList ID="DDLYrwis" runat="server"  CssClass="span3" data-rel="chosen"></asp:DropDownList>                        
                    </div>						            
                </div>
               <div class="span12">                                                        
                    <label class="span3">
                        <span>Product Wise</span>                                                        
                    </label>
                    <div class="span10">
                        <asp:DropDownList ID="DDLPNowis" runat="server" CssClass="span3" data-rel="chosen" OnSelectedIndexChanged="DDLPNowis_SelectedIndexChanged"></asp:DropDownList>
                    </div>						            
                </div>
                <div class="span12">                                                        
                    <label class="span3">
                        <span>Vendor Wise</span>                                                        
                    </label>
                    <div class="span10">
                        <asp:DropDownList ID="DDLVenWis" runat="server" CssClass="span3" data-rel="chosen" AutoPostBack="True" OnSelectedIndexChanged="DDLVenWis_SelectedIndexChanged"></asp:DropDownList>
                    </div>						            
                </div>
            </asp:Panel>

            
            <!-- Panel Sales -->

            <asp:Panel ID="pnlSal" runat="server">
                 <div class="span12">

                </div>
                <div class="span12">
                    <label class="span3">
                        <span>Date Wise</span>
                    </label>
                    <div class="span10">
                        <div class="span5">
                            <label class="span3">
                                <span>From Date</span>
                            </label>
                              <asp:TextBox ID="TBFDatWisSal" runat="server" placeholder="ex. 12/10/2015..." class="datepicker" />
                            <asp:CalendarExtender ID="CETFDatWisSal" PopupButtonID="imgPopup" runat="server"
                                 TargetControlID="TBFDatWisSal" Format="yyyy-MM-dd" />
                        </div>
                        <div class="span5">
                            <label class="span3">
                                <span>To Date</span>
                            </label>
                            <asp:TextBox ID="TBTDatWisSal" runat="server" placeholder="ex. 12/10/2015..." class="datepicker" />
                            <asp:CalendarExtender ID="CETTDatWisSal" PopupButtonID="imgPopup" runat="server" 
                                TargetControlID="TBTDatWisSal" Format="yyyy-MM-dd" />
                        </div>
                    </div>
                </div>
                <div class="span12">                                                        
                    <label class="span3">
                        <span>Month Wise</span>                                                        
                    </label>
                    <div class="span10">
                        <asp:DropDownList ID="DDLMonWisSal" runat="server" CssClass="span3" data-rel="chosen"></asp:DropDownList>
                        <asp:DropDownList ID="DDLyr" runat="server" CssClass="span3" data-rel="chosen"></asp:DropDownList>
                    </div>						            
                </div>
                <asp:Panel ID="pnl_salemp" runat="server">
                    <div class="span12">                                                        
                    <label class="span3">
                        <span>Employee</span>                                                        
                    </label>
                    <div class="span10">
                        <asp:DropDownList ID="DDLSalUsr" runat="server" CssClass="span3" data-rel="chosen"></asp:DropDownList>
                    </div>						            
                </div>
                </asp:Panel>
                <asp:Panel ID="pnl_salarea" runat="server">
                    <div class="span12">                                                        
                        <label class="span3">
                            <span>Area Wise</span>                                                        
                        </label>
                        <div class="span10">
                            <asp:DropDownList ID="DDL_areawis" runat="server" CssClass="span3" data-rel="chosen" AutoPostBack="True" OnSelectedIndexChanged="DDL_areawis_SelectedIndexChanged"></asp:DropDownList>
                        </div>						            
                    </div>
                </asp:Panel>
                <div class="span12">                                                        
                    <label class="span3">
                        <span>Customer Wise</span>                                                        
                    </label>
                    <div class="span10">
                        <asp:DropDownList ID="DDLCustWis" runat="server" CssClass="span3" data-rel="chosen"></asp:DropDownList>
                    </div>						            
                </div>
                <div class="span12">                                                        
                    <label class="span3">
                        <span>Product Wise</span>                                                        
                    </label>
                    <div class="span10">
                        <asp:DropDownList ID="DDLProWis" runat="server" CssClass="span3" data-rel="chosen"></asp:DropDownList>
                    </div>					            
                </div>
            </asp:Panel>
            <!-- Retail Daily Sales Report -->
            <asp:Panel ID="pnl_dsr" runat="server">                      
                <label class="span3">
                    <span>Daily Retail Sales Report</span>                                                        
                </label>
                <div class="span12">
                    From:<asp:TextBox ID="TBDSR" runat="server" placeholder="ex. 12/10/2015..." class="datepicker" />
                        <asp:CalendarExtender ID="CalendarExtender12" PopupButtonID="imgPopup" runat="server" 
                            TargetControlID="TBDSR" Format="yyyy-MM-dd" />
                    To:<asp:TextBox ID="TBTDSR" runat="server" placeholder="ex. 12/10/2015..." class="datepicker" />
                        <asp:CalendarExtender ID="CalendarExtender18" PopupButtonID="imgPopup" runat="server" 
                            TargetControlID="TBTDSR" Format="yyyy-MM-dd" />
                </div>
                <div class="span12">                                                        
                    <label class="span3">
                        Customer:
                    </label>
                    <div class="span10">
                        <asp:DropDownList ID="DDL_RCust" runat="server" CssClass="span3" data-rel="chosen"></asp:DropDownList>
                    </div>
                </div>
                
            </asp:Panel>
            <!-- Panel Stocks -->

            <asp:Panel ID="pnlstk" runat="server">
                 <div class="span12">
                     <div class="span6">
                          <div class="span12">
                            <asp:Panel ID="pnl_stkdat" runat="server">
                                <label class="span3">
                                    <span>Date Wise</span>
                                </label>
                                <div class="span10">
                                    <asp:TextBox ID="TBStckDat" runat="server" placeholder="ex. 12/10/2015..." />
                                    <asp:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" runat="server"
                                         TargetControlID="TBStckDat" Format="yyyy/MM/dd" />
                                </div>
                            </asp:Panel>
                            <label class="span3">
                                <span>Stock Type</span>                                                        
                            </label>
                            <div class="span10">
                                <asp:DropDownList ID="DDL_stktyp" runat="server" CssClass="span12" data-rel="chosen"
                                     AutoPostBack="true" OnSelectedIndexChanged="DDL_stktyp_SelectedIndexChanged">
                                    <asp:ListItem Value="0">-- Select Stock Type --</asp:ListItem>
                                    <asp:ListItem Value="StckSumm">Stock Summary</asp:ListItem>
                                    <asp:ListItem Value="StckRpt">Stock Reports</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <asp:Panel ID="pnl_stck" runat="server">
                                <label class="span3">
                                    <span>Product Type</span>                                                        
                                </label>
                                <div class="span10">
                                    <asp:DropDownList ID="DDLProtyp" runat="server" CssClass="span12" data-rel="chosen"
                                         AutoPostBack="true" OnSelectedIndexChanged="DDLProtyp_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                  <label class="span3">
                                    <span>Items/Products</span>                                                        
                                </label>
                                <div class="span10">
                                    <asp:DropDownList ID="DDL_StkItm" runat="server" CssClass="span12" data-rel="chosen"
                                         AutoPostBack="true" OnSelectedIndexChanged="DDL_stkItm_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                 <label class="span3">
                                    <span>Sizes</span>                                                        
                                </label>
                                <div class="span10">
                                    <asp:DropDownList ID="DDL_Itemsiz" runat="server" CssClass="span12" data-rel="chosen" AutoPostBack="true" OnSelectedIndexChanged="DDL_Itemsiz_SelectedIndexChanged"></asp:DropDownList>
                                </div>						            
                            </asp:Panel>
                            <asp:Panel ID="pnl_stcksumm" runat="server">
                                <label class="span3">
                                    <span>Product Type</span>                                                        
                                </label>
                                <div class="span10">
                                    <asp:DropDownList ID="DDl_Itmtyp" runat="server" CssClass="span12" data-rel="chosen"
                                         AutoPostBack="true" OnSelectedIndexChanged="DDl_Itmtyp_SelectedIndexChanged" ></asp:DropDownList>
                                </div>
                                <label class="span3">
                                    <span>Items/Products</span>                                                        
                                </label>
                                <div class="span10">
                                    <asp:DropDownList ID="DDL_stkItms" runat="server" CssClass="span12" data-rel="chosen"
                                         AutoPostBack="true" OnSelectedIndexChanged="DDL_stkItms_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                 <label class="span3">
                                    <span>Sizes</span>                                                        
                                </label>
                                <div class="span10">
                                    <asp:DropDownList ID="DDL_Itmsiz" runat="server" CssClass="span12" data-rel="chosen" AutoPostBack="true" OnSelectedIndexChanged="DDL_Itmsiz_SelectedIndexChanged" ></asp:DropDownList>
                                </div>		
                            </asp:Panel>
                        </div>
                     </div>
                     <div class="span5">
                     <asp:Panel ID="pnl_rej" runat="server">
                         <div class="span12">                                                        
                            <label class="span3">
                                <span>Rejected</span>                                                        
                            </label>
                            <div class="span10">
                                <asp:DropDownList ID="DDL_Rej" runat="server" CssClass="span12" data-rel="chosen" 
                                    AutoPostBack="true" OnSelectedIndexChanged="DDL_Rej_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="span12">
                                <label class="span3">
                                <span>Sizes</span>                                                        
                                </label>
                                <div class="span10">
                                    <asp:DropDownList ID="DDL_RejItmsiz" runat="server" CssClass="span12" 
                                        data-rel="chosen" AutoPostBack="true" OnSelectedIndexChanged="DDL_Rej_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>                    			            
                        </div>
                    </asp:Panel>
                         <div class="span12">                                                        
                            <label class="span3">
                                <span>Defected</span>                                                        
                            </label>
                            <div class="span10">
                                <asp:DropDownList ID="ddl_defected" runat="server" CssClass="span12" data-rel="chosen"
                                     AutoPostBack="true" OnSelectedIndexChanged="ddl_defected_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="span12">
                                <label class="span3">
                                <span>Sizes</span>                                                        
                                </label>
                                <div class="span10">
                                    <asp:DropDownList ID="DDL_defectsiz" runat="server" CssClass="span12" data-rel="chosen"
                                         AutoPostBack="true" OnSelectedIndexChanged="DDL_defectsiz_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>                    			            
                        </div>
                    </div>
                </div> 
            </asp:Panel>


            <!-- Credit Sheets -->

            <asp:Panel ID="Pnlpro" runat="server">
                <div class="span12">
                    <label class="span3">
                        <span>Date Wise</span>
                    </label>
                    <div class="span4">
                        <asp:TextBox ID="TBFrmDat" runat="server" placeholder="ex. 12/10/2015..." />
                        <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" runat="server" TargetControlID="TBStckDat" Format="dd/MM/yyyy" />--%>
                        <asp:CalendarExtender ID="CalendarExtender4" PopupButtonID="imgPopup" runat="server" 
                            TargetControlID="TBFrmDat" Format="MM/dd/yyyy" />
                    </div>
                    <div class="span4">
                        <asp:TextBox ID="TBtDat" runat="server" placeholder="ex. 12/10/2015..." />
                        <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" runat="server" TargetControlID="TBStckDat" Format="dd/MM/yyyy" />--%>
                        <asp:CalendarExtender ID="CalendarExtender5" PopupButtonID="imgPopup" runat="server"
                             TargetControlID="TBtDat" Format="MM/dd/yyyy" />
                    </div>
                </div>
                <asp:Panel ID="pnl_cre" runat="server">
                    <div class="span5">                                                        
                        <label class="span3">
                            <span>Booker</span>                                                        
                        </label>
                        <div class="span10">
                            <asp:DropDownList ID="DDL_CreBkr" runat="server" CssClass="span12" data-rel="chosen" ></asp:DropDownList>
                        </div>						            
                    </div>
                    <div class="span6">                                                        
                        <label class="span3">
                            <span>Sales Man</span>                                                        
                        </label>
                        <div class="span10">
                            <asp:DropDownList ID="DDL_CreSal" runat="server" CssClass="span12" data-rel="chosen" ></asp:DropDownList>
                        </div>						            
                    </div>
                </asp:Panel>
                <div class="span12">                                                        
                    <label class="span3">
                        <span>Customer Wise</span>                                                        
                    </label>
                    <div class="span10">
                        <asp:DropDownList ID="DDL_Cust" runat="server" CssClass="span3" data-rel="chosen"></asp:DropDownList>
                    </div>						            
                </div>

                <div class="span12">                                                        
                    <label class="span3">
                        <span>Vendor Wise</span>                                                        
                    </label>
                    <div class="span10">
                        <asp:DropDownList ID="DDl_Ara" runat="server" CssClass="span3" data-rel="chosen"></asp:DropDownList>
                    </div>						            
                </div>
                <asp:Panel ID="pnl_bilno" runat="server">
                <div class="span12">                                                        
                    <label class="span3">
                        <span>Bill NO.</span>                                                        
                    </label>
                    <div class="span10">
                        <asp:TextBox ID="TBCreBill" runat="server" placeholder="Enter Bill No.."></asp:TextBox>                        						            
                    </div>
                </div>
                    </asp:Panel>
                <div class="span12">                                                        
                    <label class="span3">
                        <span>Employee Loan</span>                                                        
                    </label>
                    <div class="span10">
                        <asp:TextBox ID="TBEmpLoan" runat="server" placeholder="Enter Employee Name..."></asp:TextBox> 
                          <asp:AutoCompleteExtender ServiceMethod="GetEmpAcc" CompletionListCssClass="completionList"
                            CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                            MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10"
                            TargetControlID="TBEmpLoan" ID="AutoCompleteExtender2"  
                            runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>                       						            
                    </div>
                </div>
            </asp:Panel>

            <!-- Employee -->

            <asp:Panel ID="PnlEmp" runat="server">
                <div class="span12">
                    <label class="span3">
                        <span>Date Wise</span>
                    </label>
                    <div class="span4">
                        <asp:TextBox ID="TBEFdat" runat="server" placeholder="ex. 12/10/2015..." />
                        <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" runat="server" TargetControlID="TBStckDat" Format="dd/MM/yyyy" />--%>
                        <asp:CalendarExtender ID="CalendarExtender2" PopupButtonID="imgPopup" runat="server" TargetControlID="TBEFdat" Format="MM/dd/yyyy" />
                    </div>
                    <div class="span4">
                        <asp:TextBox ID="TBELdat" runat="server" placeholder="ex. 12/10/2015..." />
                        <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" runat="server" TargetControlID="TBStckDat" Format="dd/MM/yyyy" />--%>
                        <asp:CalendarExtender ID="CalendarExtender3" PopupButtonID="imgPopup" runat="server" TargetControlID="TBELdat" Format="MM/dd/yyyy" />
                    </div>
                </div>
                <div class="span5">                                                        
                    <label class="span3">
                        <span>Booker</span>                                                        
                    </label>
                    <div class="span10">
                        <asp:DropDownList ID="DDL_BOOk" runat="server" CssClass="span12" data-rel="chosen" AutoPostBack="true" OnSelectedIndexChanged="DDL_BOOk_SelectedIndexChanged" ></asp:DropDownList>
                    </div>						            
                </div>
                <div class="span6">                                                        
                    <label class="span3">
                        <span>Sales Man</span>                                                        
                    </label>
                    <div class="span10">
                        <asp:DropDownList ID="DDL_SalMan" runat="server" CssClass="span12" data-rel="chosen" AutoPostBack="true" OnSelectedIndexChanged="DDL_SalMan_SelectedIndexChanged" ></asp:DropDownList>
                    </div>						            
                </div>
            </asp:Panel>

            
            <asp:Panel ID="pnl_prof" runat="server">
                <div class="span12">
                    <fieldset>
                        <legend>Net Profit</legend>
                        <label class="span3">
                            <span>Date Wise</span>
                        </label>
                        <div class="span4">
                            <asp:TextBox ID="TNFNetProf" runat="server" placeholder="ex. 12/10/2015..." />
                            <asp:CalendarExtender ID="CalendarExtender16" PopupButtonID="imgPopup" runat="server"
                                 TargetControlID="TNFNetProf" Format="MM/dd/yyyy" />
                        </div>
                        <div class="span4">
                            <asp:TextBox ID="TBTNetProf" runat="server" placeholder="ex. 12/10/2015..." />
                            <asp:CalendarExtender ID="CalendarExtender17" PopupButtonID="imgPopup" runat="server"
                                 TargetControlID="TBTNetProf" Format="MM/dd/yyyy" />
                        </div>
                        <label class="span3">
                            <span>Month Wise</span>                                                        
                        </label>
                        <div class="span10">
                            <asp:DropDownList ID="DDL_NetMonwis" runat="server" CssClass="span3" data-rel="chosen" OnSelectedIndexChanged="DDLMnthwis_SelectedIndexChanged"></asp:DropDownList>                        
                            <asp:DropDownList ID="DDL_NetYrwis" runat="server"  CssClass="span3" data-rel="chosen"></asp:DropDownList>                        
                        </div>						            
                    </fieldset>
                    <fieldset>
                        <legend>Profit</legend>
                        <label class="span3">
                            <span>Date Wise</span>
                        </label>
                        <div class="span4">
                            <asp:TextBox ID="TBFProf" runat="server" placeholder="ex. 12/10/2015..." />
                            <asp:CalendarExtender ID="CalendarExtender6" PopupButtonID="imgPopup" runat="server" TargetControlID="TBFProf" Format="MM/dd/yyyy" />
                        </div>
                        <div class="span4">
                            <asp:TextBox ID="TBTProf" runat="server" placeholder="ex. 12/10/2015..." />
                            <asp:CalendarExtender ID="CalendarExtender7" PopupButtonID="imgPopup" runat="server" TargetControlID="TBTProf" Format="MM/dd/yyyy" />
                        </div>
                    </fieldset>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnl_Transc" runat="server">
                <fieldset>
                    <legend>Cash</legend>
                    <label class="span3">
                        <span>Date Wise</span>
                    </label>
                    <div class="span4">
                        <asp:TextBox ID="TBCashFDat" runat="server" placeholder="ex. 12/10/2015..." />
                        <asp:CalendarExtender ID="CalendarExtender13" PopupButtonID="imgPopup" runat="server"
                             TargetControlID="TBCashFDat" Format="yyyy-MM-dd" />
                    </div>
                    <div class="span4">
                        <asp:TextBox ID="TBCashTDat" runat="server" placeholder="ex. 12/10/2015..." />
                        <asp:CalendarExtender ID="CalendarExtender14" PopupButtonID="imgPopup" runat="server"
                             TargetControlID="TBCashTDat" Format="yyyy-MM-dd" />
                    </div>
                    <label class="span3">
                        <span>Customer</span>
                    </label>
                    <div class="span4">
                        <asp:DropDownList ID="DDL_CashCust" runat="server"></asp:DropDownList>
                    </div>
                    <label class="span3">
                        <span>Employee</span>
                    </label>
                    <div class="span4">
                        <asp:DropDownList ID="DDL_CashEmp" runat="server"></asp:DropDownList>
                    </div>
                    <label class="span3">
                        <span>Supplier</span>
                    </label>
                    <div class="span4">
                        <asp:DropDownList ID="DDL_CashSup" runat="server"></asp:DropDownList>
                    </div>
                    <label class="span3">
                        <span>Expence</span>
                    </label>
                    <div class="span4">
                        <asp:DropDownList ID="DDL_CashExp" runat="server"></asp:DropDownList>
                    </div>
                </fieldset>
                <fieldset>
                    <legend>Cheque</legend>
                    <label class="span3">
                        <span>Bank</span>
                    </label>
                    <div class="span4">
                        <asp:DropDownList ID="DDL_bnk" runat="server"></asp:DropDownList>
                    </div>
                    <label class="span3">
                        <span>Cheque Date</span>
                    </label>
                    <div class="span4">
                        <asp:TextBox ID="TBCheqdat" runat="server" placeholder="ex. 12/10/2015..." />
                        <asp:CalendarExtender ID="CalendarExtender15" PopupButtonID="imgPopup" runat="server"
                             TargetControlID="TBCheqdat" Format="yyyy-MM-dd" />
                    </div>
                    <label class="span3">
                        <span>Customer</span>
                    </label>
                    <div class="span4">
                        <asp:DropDownList ID="DDL_BnkCust" runat="server"></asp:DropDownList>
                    </div>
                    <label class="span3">
                        <span>Labour</span>
                    </label>
                    <div class="span4">
                        <asp:DropDownList ID="DDL_Bnklabour" runat="server"></asp:DropDownList>
                    </div>
                    <label class="span3">
                        <span>Supplier</span>
                    </label>
                    <div class="span4">
                        <asp:DropDownList ID="DDL_BnkSup" runat="server"></asp:DropDownList>
                    </div>
                    <label class="span3">
                        <span>Expence</span>
                    </label>
                    <div class="span4">
                        <asp:DropDownList ID="DDL_BnkExp" runat="server"></asp:DropDownList>
                    </div>
                </fieldset>

                
            </asp:Panel>
        </div>
    </div>
</div>
<div class="span4">
    <asp:Button ID="btn_shwrpt" runat="server" Text="Show" class="btn btn-info" OnClick="btn_shwrpt_Click" />       
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
</asp:Content>
