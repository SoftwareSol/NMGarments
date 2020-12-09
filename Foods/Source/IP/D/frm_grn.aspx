<%@ Page Title="GRN" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
     CodeBehind="frm_grn.aspx.cs" Inherits="Foods.frm_grn" %>
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
      /* Modal GRNpUp Start */

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
        .modalGRNpup{
                border: 3px solid #000000;
                background-color: #FFFFFF;
                margin-top: 0px;
                color: #000000;
                margin-right: -3px;
                margin-bottom: 0px;
        }

        .modalGRNpup1{
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

        /* Modal GRNpUp End */
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
            <a href="#">Purchase</a>
            <i class="icon-angle-right"></i>
        </li>
        <li><a href="frm_grn.aspx">GRN</a></li>
    </ul>

    
    <!-- imageLoader - START -->

    <img id='HiddenLoadingImage' src="../../img/page-loader.gif" class="LoadingProgress" />

    <!-- imageLoader - END -->
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
        
<div class="row-fluid">
    <div class="box  span12">
        <div class="box-header" data-original-title>
            <h2><i class="halflings-icon edit"></i><span class="break"></span> Create GRN </h2>
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
                                <label class="control-label" for="TBSearchGRNNum">GRN Num</label>
                            </div>
                        </div>
                        <div class="span10">
                            <div class="controls">
                                <div class="input-append">
                                    <asp:TextBox runat="server" class="span12" ID="TBSearchGRNNum" ></asp:TextBox><asp:LinkButton runat="server" ID="LinkButton1" CssClass="add-on" ><i class="icon-search"></i></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="span12">
                            <div class="controls">
                                <div class="scrollit">
                                    <asp:GridView ID="GVScrhrMGRN" runat="server" class="table table-striped table-bordered" AllowPaging="True" PageSize="4" AutoGenerateColumns="False" DataKeyNames="mgrn_id" OnPageIndexChanging="GVScrhrMGRN_PageIndexChanging" OnRowDeleting="GVScrhrMGRN_RowDeleting" OnRowCommand="GVScrhrMGRN_RowCommand" OnSelectedIndexChanging="GVScrhrMGRN_SelectedIndexChanging">
                                        <Columns>
                                            <asp:BoundField DataField="mgrn_sono" HeaderText="ID" SortExpression="mgrn_sono" ReadOnly="True" />
                                            <asp:BoundField DataField="mgrn_dat" HeaderText="Date" SortExpression="mgrn_dat" />
                                            <asp:BoundField DataField="ven_nam" HeaderText="Vender" SortExpression="ven_nam" />                                            
                                            <asp:BoundField DataField="CreatedBy" HeaderText="Created By" SortExpression="CreatedBy" />                                            
                                            <asp:BoundField DataField="CreatedAt" HeaderText="Created At" SortExpression="CreatedAt" />                                            
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LBtnScrhMPur" runat="server" class="btn btn-success" CommandName="Select" ><i class="icon-eye-open"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LBtnDel" runat="server" class="btn btn-danger" CommandName="Delete" ><i class="icon-file"></i></asp:LinkButton>
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
                        <h1><span style="color:black;"> GRN</span></h1>                                    
                    </div>
                    <div class="span5"></div>
                        <div class="span2">
                            <div class="box span12">
                                <div class="box-header" data-original-title>
                                    <h2><i class="halflings-icon calendar"></i><span class="break"></span>Date</h2>
                                </div>
                                <div class="box-content">
                                    <asp:TextBox runat="server" class="span10 datepicker" ID="TBGRNDat" ></asp:TextBox>
                                </div>
                            </div>
                        </div>                               
                    <div  class="span2">
                        <div class="box  span12">
                            <div class="box-header" data-original-title>
                                <h2><i class="halflings-icon edit"></i><span class="break"></span>GRN. No.</h2>
                            </div>
                            <div class="box-content">
                                <asp:Label runat="server" class="span2" ID="TBGRNNum" ></asp:Label>
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
                                 <div class="span12" >
                                    <div class="control-group">
                                            <label style="color:black" for="DDL_PO" >P.O.</label>
                                        <div class="controls">
                                            <asp:DropDownList ID="DDL_PO" runat="server" data-rel="chosen" CssClass="span12" OnSelectedIndexChanged="DDL_PO_SelectedIndexChanged" AutoPostBack="true">                                               
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="span12">
                                    <div class="control-group">
                                        <label style="color:black" for="TBVenNam" >Vendor</label>
                                        <div class="controls">
                                            <asp:DropDownList id="ddlVenNam" runat="server" data-rel="chosen" CssClass="span12" OnSelectedIndexChanged="ddlVenNam_SelectedIndexChanged" ></asp:DropDownList>                                           
                                        </div>
                                    </div>
                                </div>                                
                                <div class="span12">
                                    <div class="control-group">
                                            <label style="color:black" for="TBRmk" >Remarks</label>
                                        <div class="controls">
                                            <asp:TextBox runat="server" class="span12" ID="TBRmk" placeholder="Remarks..."></asp:TextBox>                                    
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
<h2><i class="halflings-icon th-list"></i><span class="break"></span> GRN Items</h2>
</div>
<div class="box-content">
<div class="scrollit">
<asp:Panel ID="PnlCrtPurItem" runat="server">
<div class="row-fluid">	
<div class="span12">
<div class="box-content span12">
                            
<asp:GridView ID="GVGRNItems" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Exits!!"  class="table table-striped table-bordered" OnRowDeleting="GVGRNItems_RowDeleting" >
    <Columns>
        <asp:TemplateField HeaderText="ITEMS">  
            <ItemTemplate>
                <asp:DropDownList ID="ddlPurItm" runat="server" AutoGRNstBack="true" data-rel="chosen" OnSelectedIndexChanged="ddlPurItm_SelectedIndexChanged"></asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="DESCRIPTIONS">
            <ItemTemplate>
                <asp:TextBox ID="ItmDscptin" runat="server" Text='<%# Eval("dgrn_ItmDes")%>' placeholder="Description..."  style="width:120px; height:20px; background:none; border:none;"   ></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="QTY">
            <ItemTemplate>
                <asp:TextBox ID="ItmQty" runat="server"  Text='<%# Eval("dgrn_ItmQty")%>' placeholder="0.00" style="width:50px; height:20px; background:none; border:none;"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="INSPECTIONS">
            <ItemTemplate>
                <asp:TextBox ID="TbInsp" runat="server" Text='<%# Eval("dgrn_ItmInsp")%>'  style="width:50px; height:20px; background:none; border:none;" ></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="REJECTIONS">
            <ItemTemplate>
                <asp:TextBox ID="Tbrej" runat="server" Text='<%# Eval("dgrn_ItmReg")%>'  style="width:50px; height:20px; background:none; border:none;" ></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="BALANCE"> 
            <ItemTemplate>      
                <asp:TextBox ID="Tbbal" runat="server" Text='<%# Eval("dgrn_ItmBal")%>'  style="width:50px; height:20px; background:none; border:none;" ></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="REMARKS">
            <ItemTemplate>    
                <asp:TextBox ID="Tbrmk" runat="server"  Text='<%# Eval("dgrn_Rmk")%>' style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>
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
<div class="box-content">
<div class="row-fluid">	
<div class="span12">
<div class="span3">
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
                                   
<asp:Button runat="server"  ID="btnSaveClose" Text="Save"  CssClass="btn btn-info"  ValidationGroup="Pay Order" OnClick="btnSaveClose_Click" />   
<asp:Button runat="server"  ID="btnRevert" Text="Cancel" CssClass="btn" OnClick="btnRevert_Click"  />       
</div>     
                              
</div>
</div>
</div>
</div>
    <asp:HiddenField ID="HFMGRN" runat="server"  />
    <asp:HiddenField ID="HFDGRN" runat="server" />
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
