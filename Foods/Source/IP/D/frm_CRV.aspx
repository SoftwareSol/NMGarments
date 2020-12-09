<%@ Page Title="Cash Reciept Voucher" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
     CodeBehind="frm_CRV.aspx.cs" Inherits="Foods.frm_CRV" %>
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

    <ul class="breadcrumb">
        <li>
            <i class="icon-home"></i>
            <a href="WellCome.aspx">Home</a> 
            <i class="icon-angle-right"></i>
        </li>
        <li>
            <a href="#">Accounts</a>
            <i class="icon-angle-right"></i>
        </li>
        <li><a href="frm_CRV.aspx">Cash Reciept Voucher</a></li>
    </ul>

    
    <!-- imageLoader - START -->

    <img id='HiddenLoadingImage' src="../../img/page-loader.gif" class="LoadingProgress" />

    <!-- imageLoader - END -->

        <div class="row-fluid">
    <div class="box  span12">
        <div class="box-header" data-original-title>
            <h3><i class="halflings-icon edit"></i><span class="break"></span> Cash Reciept Voucher </h3>
        </div>
        <div class="box-content">
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
                                    <label class="control-label" for="TBSrhCRV">Search</label>
                                </div>
                            </div>
                            <div class="span10">
                                <div class="controls">
                                    <div class="input-append">
                                        <asp:TextBox runat="server" class="span12" ID="TBSrhCRV" OnTextChanged="TBSrhCRV_TextChanged" AutoPostBack="true" ></asp:TextBox><asp:LinkButton runat="server" ID="LnkBtn_MDN" CssClass="add-on" ><i class="icon-search"></i></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="span12">
                                <div class="controls">
                                    <div class="scrollit">
                                        <asp:GridView ID="GVScrhCRV" runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found!"  class="table table-striped table-bordered" AllowPaging="True" PageSize="4" AutoGenerateColumns="False" DataKeyNames="mjv_id" OnRowDeleting="GVScrhCRV_RowDeleting" OnSelectedIndexChanging="GVScrhCRV_SelectedIndexChanging">
                                            <Columns>
                                                <asp:BoundField DataField="mjv_sono" HeaderText="ID" SortExpression="mjv_sono" />
                                                <asp:BoundField DataField="mjv_dat" HeaderText="Date" SortExpression="mjv_dat" />
                                                <asp:BoundField DataField="createdby" HeaderText="Created By" SortExpression="createdby" />
                                                <asp:BoundField DataField="CreatedAt" HeaderText="Created At" SortExpression="CreatedAt" />                                            
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LnkSlect" runat="server"  CommandName="Select" >Select</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LBtnDel" runat="server"  CommandName="Delete" >Delete</asp:LinkButton>
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
                    <div class="span5">
                        <h1><span style="color:black;"> Cash Reciept Voucher</span></h1>                                    
                    </div>
                    <div class="span2"></div>
                        <div class="span2">
                            <div class="box span12">
                                <div class="box-header" data-original-title>
                                    <h2><i class="halflings-icon calendar"></i><span class="break"></span>Date</h2>
                                </div>
                                <div class="box-content">
                                    <asp:TextBox runat="server" class="span10 datepicker" ID="TBCRVDat" ></asp:TextBox>
                                </div>
                            </div>
                        </div>                               
                    <div  class="span3">
                        <div class="box  span12">
                            <div class="box-header" data-original-title>
                                <h2>Serial. NO</h2>
                            </div>
                            <div class="box-content">
                                <asp:Label runat="server" class="span12" ID="lbl_CRVSNo" ></asp:Label>
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
                                           <asp:CheckBox ID="chk_prtd" runat="server" Text="Printed" />
                                        </div>
                                    </div>
                                </div>
                            <%--<div class="span12">
                                    <div class="control-group">    
                                        Cash/Bank
                                        <div class="controls">
                                            <asp:DropDownList ID="DDLCshBnk" runat="server" data-rel="chosen" CssClass="span12"></asp:DropDownList>                                            
                                        </div>
                                    </div>
                                </div>  --%>
                                 <div class="span12">
                                    <div class="control-group">                                            
                                        <div class="controls">
                                           <asp:CheckBox ID="chk_incm" runat="server" AutoPostBack="true" Text="Income Tax Challan Recieved" OnCheckedChanged="chk_incm_CheckedChanged"/>
                                        </div>
                                    </div>
                                </div>              
                                <asp:Panel ID="pnlitno" runat="server">
                                    <div class="span12">
                                        <div class="control-group">
                                            Income Tax No
                                            <div class="controls">
                                                <asp:TextBox runat="server" ID="TBITNo"  placeholder="Income Tax Number..."></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>                               
                                <%--<div class="span12">
                                    <div class="control-group">
                                        Recieved By
                                        <div class="controls">
                                            <asp:DropDownList id="DDL_RcdBy" runat="server" data-rel="chosen" CssClass="span12"></asp:DropDownList>                                           
                                        </div>
                                    </div>
                                </div>--%>
                                <div class="span12">
                                    <div class="control-group">
                                        Account Code
                                        <div class="controls">
                                            <asp:DropDownList id="DDL_AccCde" runat="server" data-rel="chosen" CssClass="span12"></asp:DropDownList>                                           
                                        </div>
                                    </div>
                                </div>
                                <div class="span12">
                                    <div class="control-group">
                                            <label style="color:black" for="TBNarr" >Narrations</label>
                                        <div class="controls">
                                            <asp:TextBox runat="server" class="span12" ID="TBNarr"></asp:TextBox>                                    
                                        </div>
                                    </div>
                                </div>
                                <div class="span3">
                                    <div class="control-group">
                                        <label style="color:black" for="TBTotal" >Cash Amount</label>
                                        <div class="controls">
                                            <asp:TextBox runat="server" class="span12" ID="TBTotal" Text="0.00"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFVTtl" ValidationGroup="VGCRV" runat="server" ErrorMessage="Total Can not be Null" ControlToValidate="TBTotal"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="span3">
                                    <div class="control-group">
                                        <label style="color:black" for="TBITAX" >I.Tax %</label>
                                        <div class="controls">
                                            <asp:TextBox runat="server" class="span12" ID="TBITAX" Text="0.00"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFVITAX" ValidationGroup="VGCRV" runat="server" ErrorMessage="ITax % Can not be Null" ControlToValidate="TBITAX" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="span3">
                                    <div class="control-group">
                                        <label style="color:black" for="TBItaxamt" >I.Tax Amount</label>
                                        <div class="controls">
                                            <asp:TextBox runat="server" class="span12" ID="TBItaxamt" Text="0.00"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFVGTtl" ValidationGroup="VGCRV" runat="server" ErrorMessage="ITax Amount Can not be Null" ControlToValidate="TBItaxamt" ForeColor="Red"></asp:RequiredFieldValidator>                                        
                                        </div>
                                    </div>
                                </div>
                                <div class="span12">
                                    <div class="control-group">
                                        <label style="color:black" for="TBTotal" >Total Amount</label>
                                        <div class="controls">
                                            <asp:TextBox runat="server" class="span12" ID="TBttlAmt" Text="0.00"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFVTtlAmt" ValidationGroup="VGCRV" runat="server" ErrorMessage="Total Amount Can not be Null" ControlToValidate="TBttlAmt" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="span12">
                                    <asp:Button runat="server"  ID="btnSave" Text="Save"  CssClass="btn btn-info" OnClick="btnSave_Click"  />
                                    <asp:Button runat="server"  ID="btnCancl" Text="Cancel" CssClass="btn" />  
                                    <asp:HiddenField ID="HFmjv" runat="server" />
                                         
                                </div>  
                            </div>
                        </div>
                    </div>
                </div>
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
        <script src="Controller/Common.js"></script>

</asp:Content>
