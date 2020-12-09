<%@ Page Title="Sales" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="frm_Sal.aspx.cs" Inherits="Foods.frm_Sal" %>
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

        .itemHighlighted {
            background-color: #ADD6FF;               
        }


      /* Scroller End */
      /* Modal SalespUp Start */

        .modalBackground{
                background-color: #000000;
                filter: alpha(opacity=10);
                opacity: 0.7;
                z-index:1000 !important;
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
     <asp:UpdatePanel  ID="UpdatePanel1" runat="server">
        <ContentTemplate>  
           
        <ul class="breadcrumb">
        <li>
            <i class="icon-home"></i>
                <a href="WellCome.aspx">Home</a> 
            <i class="icon-angle-right"></i>
        </li>
        <li>
            <a href="#">Sales</a>
            <i class="icon-angle-right"></i>
        </li>
            <li><a href="frm_Sal.aspx">Sales</a></li>
    </ul>

    
    <!-- imageLoader - START -->

    <img id='HiddenLoadingImage' src="../../img/page-loader.gif" class="LoadingProgress" />

    <!-- imageLoader - END -->
        <div class="row-fluid">
    <div class="box  span12">
        <div class="box-header" data-original-title>
            <h2><i class="halflings-icon edit"></i><span class="break"></span> Create Sales </h2>
        </div>
        <div class="box-content">
            <asp:Panel ID="PanelShowClosed" runat="server" Visible="false" style="visibility:hidden;">
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
            <div class="span12">
                        <div style="text-align:center">
                            <asp:Label ID="lbl_err" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                     </div>
            <div class="row-fluid">	
                <div class="box span12">
                    <div class="box-content">                                 
                        <div class="span1">
                            <div class="control-group">
                                <label class="control-label" for="TBSearchSales">Sales</label>
                            </div>
                        </div>
                        <div class="span10">
                            <div class="controls">
                                <div class="input-append">
                                    <asp:TextBox runat="server" class="span12" ID="TBSearchSales" AutoPostBack="true" OnTextChanged="TBSearchSalesNum_TextChanged"   ></asp:TextBox><asp:LinkButton runat="server" ID="LinkButton1" CssClass="add-on" ><i class="icon-search"></i></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="span12">
                            <div class="controls">
                                <div class="scrollit">
                                    <asp:GridView ID="GVScrhMSal" runat="server" class="table table-striped table-bordered" AllowPaging="True" PageSize="5" AutoGenerateColumns="False" DataKeyNames="MSal_id,CustomerID" OnPageIndexChanging="GVScrhMSal_PageIndexChanging" OnRowDeleting="GVScrhMSal_RowDeleting" OnRowCommand="GVScrhMSal_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="MSal_sono" HeaderText="ID" SortExpression="MSal_sono" ReadOnly="True" />                                            
                                            <asp:BoundField DataField="CustomerName" HeaderText="Customer" SortExpression="CustomerName" />
                                            <asp:BoundField DataField="MSal_dat" HeaderText="Date" SortExpression="MSal_dat" />
                                            <asp:BoundField DataField="CreatedBy" HeaderText="Created By" SortExpression="CreatedBy" />                                            
                                            <asp:BoundField DataField="CreatedAt" HeaderText="Created At" SortExpression="CreatedAt" />                                            
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LBtnScrhMSal" runat="server" CommandName="Select" > Select </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LBtnDel" runat="server" Text="Delete" CommandName="Delete" > Delete </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LBtn" runat="server" Text="Invoice" CommandName="Show" > Invoice </asp:LinkButton>
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
            <div class="span12"></div>               
            <div class="row-fluid">	
                <div class="span12">
                    <div class="span3">
                        <h1><span style="color:black;"> Sales</span></h1>                                    
                    </div>
                    <div class="span3"></div>
                    <div class="span3">
                        <div class="span12">
                            <div class="box-content">
                                <h2>Date:</h2>
                                <asp:TextBox runat="server" class="span10" ID="TBSalDat" ></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" CssClass="calender" PopupButtonID="imgPopup" runat="server" 
                                    TargetControlID="TBSalDat" Format="yyyy-MM-dd"> </asp:CalendarExtender>
                            </div>
                        </div>
                    </div>                               
                    <div class="span1"></div>
                    <div  class="span3">
                        <div class="span12">                            
                            <h2>Voucher No.</h2>
                            <div class="box-content">
                                <asp:Label runat="server" class="span12" ID="TBSalesNum" ForeColor="Red" ></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="box-content">
                            <div class="span1">&nbsp;</div>
                            <div class="span3">
                                <div class="control-group">                                            
                                    <div class="controls">
                                        <asp:CheckBox ID="chk_Act" runat="server" Text="Active" />
                                    </div>
                                </div>
                            </div>
                            <div class="span3">
                                <div class="control-group">                                            
                                    <div class="controls">
                                        <asp:CheckBox ID="chk_prtd" runat="server" Text="Printed" />
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pnl_crdcsh" runat="server">
                                <div class="span3">
                                    <div class="control-group">                                            
                                        <div class="controls">
                                            <asp:RadioButton id="ckcrdt" GroupName="CreCsh" Text="Credit" runat="server" Checked="true" />
                                        </div>
                                    </div>
                                </div>
                                <div class="span3">
                                <div class="control-group">                                            
                                    <div class="controls">
                                        <asp:RadioButton id="ckcsh" GroupName="CreCsh" Text="Cash" runat="server"/>
                                    </div>
                                </div>
                            </div>
                            </asp:Panel>
                            <div class="span3">
                                <div class="span6">
                                    <asp:Button ID="btn_ImportDSR" Visible="false" Text="Import DSR" CssClass="btn btn-primary" runat="server" />
                                </div>						            
						    </div>      
                            <asp:Panel ID="pnlgpno" runat="server">
                                <div class="span4">
                                    <div class="control-group">    
                                        Gate Pass No.                              
                                        <div class="controls">
                                            <asp:TextBox runat="server" class="span12" ID="TBGPNo" placeholder="Gate Pass No..." ></asp:TextBox>
                                        </div>
                                    </div>
                                </div>  
                            </asp:Panel>
                            <div class="span12">
                                <div class="control-group">
                                    Customer
                                    <div class="controls">
                                        <div class="span12">
                                            <asp:DropDownList id="DDL_CustAcc" runat="server" data-rel="chosen" CssClass="span4" AutoPostBack="true" OnSelectedIndexChanged="DDL_CustAcc_SelectedIndexChanged"></asp:DropDownList>                                                                                   
                                             <asp:DropDownList id="DDL_Cust" runat="server" data-rel="chosen" CssClass="span8" AutoPostBack="true" OnSelectedIndexChanged="ddl_Cust_SelectedIndexChanged" ></asp:DropDownList>
                                            <asp:Label ID="v_cname" runat="server" ForeColor="Red" Font-Bold="true" Text="Please Select Customer Name"></asp:Label>                                          
                                        </div>
                                    </div>
                                </div>
                            </div>
                        <asp:Panel ID="salmanbook" runat="server">
                            <div class="span5">
                                <div class="control-group">
                                    Booker
                                    <div class="controls">
                                        <asp:DropDownList id="DDL_Book" runat="server" data-rel="chosen" CssClass="span12" AutoPostBack="true" OnSelectedIndexChanged="ddl_Cust_SelectedIndexChanged" ></asp:DropDownList>                                           
                                    </div>
                                </div>
                            </div><div class="span6">
                                <div class="control-group">
                                    Sales Man
                                    <div class="controls">
                                        <asp:DropDownList id="DDL_SalMan" runat="server" data-rel="chosen" CssClass="span12" AutoPostBack="true" OnSelectedIndexChanged="ddl_Cust_SelectedIndexChanged" ></asp:DropDownList>                                           
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                            <div class="span12">
                                <div class="control-group">
                                        <label style="color:black" for="TBRmk" >Remarks</label>
                                    <div class="controls">
                                        <asp:TextBox runat="server" class="span12" ID="TBRmk" TextMode="MultiLine"></asp:TextBox>                                    
                                    </div>
                                </div>
                            </div>
                        </div>
                    <div class="scrollit">
                    <asp:Panel ID="PnlCrtSalItem" runat="server">
                        <div class="row-fluid">	
                            <div class="span12">
                                <div class="box-content span12">   
                                      <table  style="text-align:center; font-weight:bold;" class="table table-striped table-bordered">
                                            <thead>
                                                <tr>
                                                    <td>Category</td>
                                                    <td>Items</td>
                                                    <td>Rate</td>
                                                    <td>Qty</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </thead>
                                           
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="TBCat" CssClass="span10" runat="server" placeholder="Category..." 
                                                            AutoPostBack="true" OnTextChanged="TBCat_TextChanged"></asp:TextBox>
                                                        <asp:AutoCompleteExtender ServiceMethod="GetCat" CompletionListCssClass="completionList"
                                                            CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                            MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10"
                                                            TargetControlID="TBCat" ID="AutoCompleteExtender1"  
                                                            runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
                                           <asp:Label ID="v_category" Font-Bold="true" Text="" ForeColor="Red" runat="server"></asp:Label>            
                                                    </td>
                                                   
                                                    <td>
                                                        <asp:TextBox ID="TBItms" CssClass="span10" runat="server" AutoPostBack="true" 
                                                            placeholder="Items..." OnTextChanged="TBItms_TextChanged"></asp:TextBox>
                                                        <asp:AutoCompleteExtender ServiceMethod="GetPro" CompletionListCssClass="completionList"
                                                            CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                            MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10"
                                                            TargetControlID="TBItms" ID="AutoCompleteExtender2"  
                                                            runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
                                                            <asp:Label ID="v_items" Font-Bold="true" Text="" ForeColor="Red" runat="server"></asp:Label>                                                       
                                                                 </td>
                                                    <td>
                                                        <asp:TextBox ID="TB_Rat" runat="server" palceholder="Rate..." AutoPostBack="true" OnTextChanged="TB_Rat_TextChanged" ></asp:TextBox>
                                                        <asp:Label ID="v_rate" Font-Bold="true" Text="Please Change Rate.." ForeColor="Red" runat="server"></asp:Label>                                                       
                                                                
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:GridView ID="GVStkItems" runat="server" class="table table-striped table-bordered" 
                                                            AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" 
                                                            EmptyDataText="No Record Exits!!" ShowHeader="true"   
                                                             OnRowDeleting="GVStkItems_RowDeleting" OnRowDataBound="GVStkItems_RowDataBound" >
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Size">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="itmsiz" runat="server" Text='<%# Eval("SIZE")%>'   ></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Avail Qty">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblQty" runat="server"  Text='<%# Eval("QTY")%>' placeholder="0.00"
                                                                             style="width:30px; height:20px; background:none; border:none;" ></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Qty">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="ItReqQty" runat="server"  Text='<%# Eval("QUANTY")%>' placeholder="0.00"
                                                                             style="width:30px; height:20px; background:none; border:none;" AutoPostBack="true" 
                                                                            OnTextChanged="ItQty_TextChanged"></asp:TextBox>
                                                                        
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="v_qty" runat="server" ForeColor="Red" Text=""></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btn_copy" runat="server" Text="ADD" CssClass="btn btn-info" OnClick="btn_copy_Click" />
                                                    </td>
                                                    </tr>
                                                </tbody>
                                          
                                            </table>
                                        <asp:GridView ID="GVItms" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" 
                                           EmptyDataText="No Record Exits!!"  class="table table-striped table-bordered"  OnSelectedIndexChanged="GVItms_SelectedIndexChanged1" >
                                            <Columns>
                                                <asp:TemplateField HeaderText="PRODUCT" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Pro" runat="server" Text='<%# Eval("PRODUCT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Size">
                                                    <ItemTemplate>
                                                        <asp:Label ID="itmsiz" runat="server" Text='<%# Eval("SIZE")%>' ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="RATE">
                                                    <ItemTemplate>    
                                                        <asp:TextBox ID="lblrat" runat="server"  AutoPostBack="true"  style="width:80px; height:20px; background:none; border:none;" OnTextChanged="lblrat_TextChanged" ></asp:TextBox>                       
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qty">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="ItmQty" runat="server"  AutoPostBack="true" Text='<%# Eval("QTY")%>' placeholder="0.00" style="width:30px; height:20px; background:none; border:none;" OnTextChanged="ItmQty_TextChanged1"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="AMOUNT">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Flag"  runat="server" Text="0" Visible="false" />
                                                        <asp:TextBox ID="TBamt" runat="server"  Text='<%# Eval("AMT")%>' style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RFVAmt" ForeColor="Red" ValidationGroup="gvItems" runat="server" ErrorMessage="Please Write Some in Amount" ControlToValidate="TBamt"></asp:RequiredFieldValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>  
                                    <%--<asp:GridView ID="GVItms" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Exits!!"  class="table table-striped table-bordered" OnRowDeleting="GVItms_RowDeleting" OnRowDataBound="GVItms_RowDataBound" OnRowCommand="GVItms_RowCommand" OnSelectedIndexChanged="GVItms_SelectedIndexChanged" >
                                        <Columns>
                                            <asp:TemplateField HeaderText="PARTICULARS" Visible="false">  
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="DDL_Par" runat="server" OnSelectedIndexChanged="DDL_Par_SelectedIndexChanged"  AutoPostBack="True" ></asp:DropDownList>
                                                    <asp:Label ID="lbl_Par" runat="server" Visible="false" Text='<%# Eval("PARTICULARS") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="INVENTORY TYPE">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="DDL_invtyp" runat="server"  data-rel="chosen" AutoPostBack="true" OnSelectedIndexChanged="DDL_invtyp_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">-- Select Inventory Type --</asp:ListItem>
                                                        <asp:ListItem Value="NORM">Normal</asp:ListItem>
                                                        <asp:ListItem Value="DEFEC">Defective</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lbl_invtyp" runat="server" Visible="false" Text='<%# Eval("INVENTORYTYP") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="PRODUCT">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="DDL_Prorefcde" runat="server"  data-rel="chosen" AutoPostBack="true" OnSelectedIndexChanged="DDL_Prorefcde_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">--Select Products--</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lbl_Pro" runat="server" Visible="false" Text='<%# Eval("PRODUCT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Details">
                                                <ItemTemplate>
                                                    <asp:GridView ID="GVStkItems" runat="server" class="table table-striped table-bordered" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Exits!!" ShowHeader="true" OnRowDataBound="GVStkItems_RowDataBound" >
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Size">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="itmsiz" runat="server" Text='<%# Eval("SIZE")%>' placeholder="Size..."  style="width:30px; height:20px; background:none; border:none;"   ></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Qty">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="ItmQty" runat="server"  Text='<%# Eval("QTY")%>' placeholder="0.00" style="width:30px; height:20px; background:none; border:none;" AutoPostBack="true" OnTextChanged="ItmQty_TextChanged" ></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:Label ID="lbl_Details" runat="server" Visible="false" Text='<%# Eval("Details") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Percentage" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TBDIS" runat="server" Text='<%# Eval("DIS")%>' style="width:50px; height:20px; background:none; border:none;" AutoPostBack="true" OnTextChanged="TBDIS_TextChanged"></asp:TextBox>                                                        
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="QTY Available" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItmQty" runat="server" Text='<%# Eval("QTYAVAIL")%>' />                                                        
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="RATE">
                                                <ItemTemplate>    
                                                    <asp:TextBox ID="TBrat" runat="server"  Text='<%# Eval("RATE")%>'  style="width:80px; height:20px; background:none; border:none;" AutoPostBack="true" OnTextChanged="TBrat_TextChanged" ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RFVRat" ForeColor="Red" ValidationGroup="gvItems" runat="server" ErrorMessage="Please Write Some in Rate" ControlToValidate="TBrat"></asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
  
                                            <asp:TemplateField HeaderText="QTY">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbshw" runat="server" />                                                        
                                                    <asp:TextBox ID="TBItmQty" runat="server" Text='<%# Eval("QTY")%>' style="width:50px; height:20px; background:none; border:none;" AutoPostBack="true" OnTextChanged="TBItmQty_TextChanged" ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RFVQty" ForeColor="Red" ValidationGroup="gvItems" runat="server" ErrorMessage="Please Write Some in Quantity" ControlToValidate="TBItmQty"></asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UNIT" Visible="false">
                                                <ItemTemplate>       
                                                    <asp:TextBox ID="TbItmunt"  runat="server" Text='<%# Eval("UNIT")%>' style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="AMOUNT">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Flag"  runat="server" Text="0" Visible="false" />
                                                    <asp:TextBox ID="TBamt" runat="server"  Text='<%# Eval("AMT")%>' style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RFVAmt" ForeColor="Red" ValidationGroup="gvItems" runat="server" ErrorMessage="Please Write Some in Amount" ControlToValidate="TBamt"></asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>        
                                            <asp:TemplateField>                                                
                                                <ItemTemplate>
                                                    <asp:Label ID="Isupdat" runat="server" Text="" Visible="false"></asp:Label>
                                                    <asp:LinkButton ID="lnkbtnadd" ValidationGroup="gvItems" CommandName="Add"  OnClick="linkbtnadd_Click" runat="server"><i class="halflings-icon plus-sign" >+</i></asp:LinkButton>
                                                    <asp:HiddenField ID="HFDSal" runat="server" Value='<%# Eval("DSal_id")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>                                     
                                            <asp:CommandField ShowDeleteButton="True" DeleteText="-"  >
                                                <ControlStyle CssClass="halflings-icon minus-sign" />
                                            </asp:CommandField>

                                        </Columns>
                                    </asp:GridView>--%>
                                </div>
                            </div>
                        </div>     
                    </asp:Panel>
                </div>
            <div class="row-fluid">
                <div class="box span12">
                    <div class="box-content">
                        <div class="row-fluid">	
                            <div class="span12">
                                <asp:Panel ID="pnl_recov" runat="server">
                                    <div  class="span3">
                                        <h5><span class="break"></span>Recovery</h5>
                                        <asp:TextBox ID="TBRecov" runat="server" AutoPostBack="true"  OnTextChanged="TBRecov_TextChanged" ></asp:TextBox>                                     
                                    </div>
                                    <div  class="span3">
                                        <h5><span class="break"></span>Out Standing</h5>
                                        <asp:TextBox runat="server" class="span12" ID="TBoutstan"  AutoPostBack="true" OnTextChanged="TBoutstan_TextChanged"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="VGSal" ForeColor="Red" runat="server" ErrorMessage="Recovery Can not be Null" ControlToValidate="TBoutstan"></asp:RequiredFieldValidator>  
                                    </div>
                                </asp:Panel>
                                <div  class="span3">
                                    <h5><span class="break"></span>Previous Balance</h5>
                                    <asp:Label ID="lbl_outstan" runat="server"  ></asp:Label>                                     
                                </div>
                                <div class="span3">
                                 <h5><span class="break"></span>Discount Percentage:</h5>                                    
                                    <div class="controls">
                                        <asp:TextBox ID="TBDISC" runat="server"  AutoPostBack="true" OnTextChanged="TBDISC_TextChanged"></asp:TextBox>
                                    </div>
                                </div> 
                                <div class="span3">
                                 <h5><span class="break"></span>Discount Amount:</h5>                                    
                                    <div class="controls">
                                        <asp:TextBox ID="TBDISAMT" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div  class="span3" >
                                 <h5><span class="break"></span>Total</h5>
                                    <asp:TextBox runat="server" class="span12" ID="TBTtl" Text="0.00"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="VGSal" ForeColor="Red" runat="server" ErrorMessage="Total Amount Can not be Null" ControlToValidate="TBTtl"></asp:RequiredFieldValidator>
                                     <asp:Label ID="v_ttl" runat="server" ForeColor="Red" Font-Bold="true" Text=""></asp:Label>
                                </div>
                                <div class="span12"></div>
                                <div  class="span8"></div>
                                <div  class="span3">
                                    <h5><span class="break"></span> Grand Total:</h5>
                                    <asp:TextBox runat="server" class="span12" ID="TBGTtl" Text="0.00"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RFVGTtl" ValidationGroup="VGSal"  runat="server" ErrorMessage="GST Can not be Null" ControlToValidate="TBGST" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                     <asp:Label ID="v_grand" runat="server" ForeColor="Red" Font-Bold="true" Text=""></asp:Label>
                                  </div>
                            </div>                        
                            <div class="span12" style="display:none;">
                                <div class="span6"></div>
                                <div  class="span4"></div>
                                <div  class="span3">
                                    <h5><span class="break"></span>GST%</h5>
                                    <asp:TextBox runat="server" class="span12" ID="TBGST" Text="17"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFVGST" ValidationGroup="VGSal" ForeColor="Red" runat="server" ErrorMessage="GST Can not be Null" ControlToValidate="TBGST"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row-fluid">	
                            <div class="span12">
                                <div class="span3" style="visibility:hidden;">
                                    <asp:CheckBox runat="server" ID="ckSch" AutoPostBack="true" Text="Scheme" TextAlign="Left" OnCheckedChanged="ckSch_CheckedChanged" /> 
                                </div>
                                <div class="span1"></div>
                                <asp:Panel ID="pnl_sch" runat="server" >    
                                    <div class="row-fluid">	
                                        <div class="box-content span12">  
                                            <div class="span12">                                            
                                                <div class="control-group">
                                                    Scheme
                                                    <div class="controls">
                                                        <asp:TextBox runat="server" class="span12" ID="TBSchm"  placeholder="Scheme..." ></asp:TextBox>
                                                    </div>
                                                </div>
                                            <%--</div>
                                            <div class="span12">--%>
                                                <div class="control-group">
                                                    Bonus
                                                    <div class="controls">
                                                        <asp:TextBox runat="server" class="span12" ID="TBBns" placeholder="Bonus..." ></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>                                                             
                                </asp:Panel>
                                <div  class="span3"></div>
                            </div>
                        </div>
                        <div class="row-fluid">	
                            <div class="span12">
                                    <div class="span4"></div>
                                <div class="span4">
                                    <asp:Button runat="server"  ID="btnSave" Text="Save" ValidationGroup="VGSal"  CssClass="btn btn-info"  OnClick="btnSave_Click" />   
                                    <asp:Button runat="server"  ID="btnRevert" Text="Cancel" CssClass="btn" OnClick="btnRevert_Click"  />       
                                </div>                                   
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="HFMSal" runat="server" />                
            </div>
        </div>
            <asp:Panel ID="Panel1"  CssClass="modalPopup"  runat="server" HorizontalAlign="Center"  Style=" Width:495px; Height:600px; display: none;">
            <div class="modal" >
                <div class="modal-header">
                    <!--<button type="button" class="close" data-dismiss="modal">×</button>-->
                    <asp:Button ID="closebtn" Text="x"  CssClass="close" data-dismiss="modal" runat="server"   />
                    <h3>Import DSR</h3>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <table style="text-align:left; height:600px; width:500px;">
                                <tr>
                                    <td valign="top">
                                        <table>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="lblerr" runat="server" ForeColor="Red" ></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:HiddenField ID="HFDSRID" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                 <td>
                                                    Customer
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tb_cust" runat="server"  placeholder="-- Select Customer--" ></asp:TextBox>
                                                    <asp:TextBox ID="TB_CustNam" runat="server" Visible="false" Height="18px" EnableViewState="true" Width="142px"></asp:TextBox>                                                    
                                                    <asp:AutoCompleteExtender ServiceMethod="GetCust" CompletionListCssClass="completionList"
                                                    CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                    MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10"
                                                    TargetControlID="tb_cust" ID="AutoCompleteExtender6"  
                                                    runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
                                                </td>
                                                 <td>
                                                    Date
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TBDSRDat" runat="server" placeholder="Date.." Width="100" ></asp:TextBox>
                                                    <asp:CalendarExtender ID="Calendar1" CssClass="calender" PopupButtonID="imgPopup" runat="server" 
                                        TargetControlID="TBDSRDat" Format="yyyy-MM-dd"> </asp:CalendarExtender>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btn_searchdsr" runat="server"  CssClass="btn btn-primary" Text="Search" OnClick="btn_searchdsr_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:GridView ID="GV_DSR" CssClass="table table-striped table-bordered" DataKeyNames="Voucher" ShowHeaderWhenEmpty="true" ShowHeader="true" runat="server" PageSize="5" AutoGenerateColumns="false" OnRowCommand="GV_DSR_RowCommand" OnPageIndexChanging="GV_DSR_PageIndexChanging">
                                                        <Columns>
                                                            <asp:BoundField DataField="Voucher" HeaderText="Voucher" SortExpression="Voucher" />
                                                            <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDSRSelect" CommandName="Select" runat="server" Text="Select" ></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </asp:Panel>

        <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" 
        PopupControlID="Panel1" TargetControlID="btn_ImportDSR"
        CancelControlID="closebtn" BackgroundCssClass="modalBackground1">
        </asp:ModalPopupExtender>

             </ContentTemplate>
    </asp:UpdatePanel>
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

        <div class="span12" >
            <asp:Panel ID="pnlSO" runat="server">
                <div class="control-group">
                    Sale Order
                    <div class="controls">
                        <asp:DropDownList id="DDL_SO" runat="server" data-rel="chosen" CssClass="span12" ></asp:DropDownList>                                           
                    </div>
                </div>
            </asp:Panel>
        </div>
        <div class="span12" style="visibility:hidden">
            <div class="control-group">                                            
                <div class="controls">
                    <asp:CheckBox ID="chk_SO" runat="server" Text="Sales Order" AutoPostBack="true" OnCheckedChanged="chk_SO_CheckedChanged" />
                </div>
            </div>
        </div>      
        <div class="span12">
            <div class="span6"></div>
            <div  class="span4"></div>
            <div  class="span3" style="visibility:hidden;">
                <h5><span class="break"></span>Total:</h5>
                <asp:TextBox runat="server" class="span12" ID="TBTotal" Text="0.00"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RFVTtl" ValidationGroup="VGSal" ForeColor="Red" runat="server" ErrorMessage="Total Can not be Null" ControlToValidate="TBTotal"></asp:RequiredFieldValidator>
            </div>
        </div>


<script>

    function PopupShown(sender, args) {
        sender._popupBehavior._element.style.zIndex = 99999999;
    }

</script>
                        
</asp:Content>
