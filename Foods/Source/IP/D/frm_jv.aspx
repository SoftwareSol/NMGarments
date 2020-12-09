<%@ Page Title="Journel Voucher" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
     CodeBehind="frm_jv.aspx.cs" Inherits="Foods.frm_jv" %>
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
        <li><a href="frm_jv.aspx">Journal Voucher</a></li>
    </ul>

    
    <!-- imageLoader - START -->

    <img id='HiddenLoadingImage' src="../../img/page-loader.gif" class="LoadingProgress" />

    <!-- imageLoader - END -->
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="row-fluid">
    <div class="box  span12">
        <div class="box-header" data-original-title>
            <h3><i class="halflings-icon edit"></i><span class="break"></span> Journal Voucher </h3>
        </div>
        <div class="box-content">
            <div class="span12">
                <div style="text-align:center">
                    <asp:Label ID="lbl_err" runat="server" ForeColor="Red"></asp:Label>
                </div>
            </div>
                <%--<div class="row-fluid">	
                    <div class="box span12">
                        <div class="box-content">                                 
                            <div class="span1">
                                <div class="control-group">
                                    <label class="control-label" for="TBSrhJV">Search</label>
                                </div>
                            </div>
                            <div class="span10">
                                <div class="controls">
                                    <div class="input-append">
                                        <asp:TextBox runat="server" class="span12" ID="TBSrhJV" AutoPostBack="true" ></asp:TextBox><asp:LinkButton runat="server" ID="LnkBtn_MDN" CssClass="add-on" ><i class="icon-search"></i></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="span12">
                                <div class="controls">
                                    <div class="scrollit">
                                        <asp:GridView ID="GVScrhJV" runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found!"  class="table table-striped table-bordered" AllowPaging="True" PageSize="4" AutoGenerateColumns="False" DataKeyNames="mjv_id">
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
                </div>--%>               

            <div class="row-fluid">	
                <div class="span12">
                    <div class="span5">
                        <h1><span style="color:black;"> Journal Voucher</span></h1>                                    
                    </div>
                    <div class="span12"></div>
                    <div class="span5">
                        <div class="control-group">    
                            Date
                            <div class="controls">
                                <asp:TextBox runat="server" class="span10 datepicker" ID="TBjvdat" placeholder="Date..." ></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="span12"></div>
                    <div class="row-fluid">	
                        <div class="span12">
                            <div class="box-content span12">                            
                                <asp:GridView ID="GVJV" runat="server" AutoGenerateColumns="False"  class="table table-striped table-bordered" OnRowDeleting="GVJV_RowDeleting" >
                                    <Columns>
                                         <asp:TemplateField HeaderText="PARTICULARS">
                                             <ItemTemplate>
                                                 <asp:DropDownList ID="DDL_Par" runat="server" data-rel="chosen" AutopostBack="true" style="width:200px; height:20px; background:none; border:none;"></asp:DropDownList>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DEBIT">
                                             <ItemTemplate>
                                                 <asp:TextBox ID="TBDbt" runat="server" Text='<%# Eval("DEBIT")%>' placeholder="Debit..." style="width:50px; height:20px; background:none; border:none;" ></asp:TextBox>
                                             </ItemTemplate>                                        
                                         </asp:TemplateField>
                                             <asp:TemplateField HeaderText="CREDIT">
                                             <ItemTemplate>
                                                 <asp:TextBox ID="TBCrd" runat="server" Text='<%# Eval("CREDIT")%>' placeholder="Credit..." style="width:50px; height:20px; background:none; border:none;" ></asp:TextBox>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NARRATIONS">
                                             <ItemTemplate>
                                                 <asp:TextBox ID="TBnarr" runat="server"  Text='<%# Eval("NARR")%>' placeholder="Narration..." style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>
                                             </ItemTemplate>                                      
                                         </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="HF_JVSNo" runat="server" Value='<%# Eval("JVSNO")%>' /> 
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="linkbtnadd" OnClick="linkbtnadd_Click"  runat="server" ><i class="halflings-icon plus-sign"  ></i></asp:LinkButton>
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
                    <div class="span12">
                        <asp:Button runat="server"  ID="btnSave" Text="Save"  CssClass="btn btn-info" OnClick="btnSave_Click"  />
                        <asp:Button runat="server"  ID="btnCancl" Text="Cancel" CssClass="btn" />       
                        <asp:HiddenField ID="HFjv" runat="server" />
                    </div>  
                </div>
            </div>
        </div>
    </div>
</div>
</div>
</div>
</div>
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

</asp:Content>
