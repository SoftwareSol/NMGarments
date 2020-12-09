<%@ Page Title="Credit Note" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
CodeBehind="mcn.aspx.cs" Inherits="Foods.mcn" %>
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
        <li>
            <i class="icon-angle-right"></i>
            <a href="#">Purchase</a>
        </li>
        <li>
            <i class="icon-angle-right"></i>
            <a href="mcn.aspx">Credit Note</a>
        </li>
    </ul>    
    <!-- imageLoader - START -->

    <img id='HiddenLoadingImage' src="../../img/page-loader.gif" class="LoadingProgress" />

    <!-- imageLoader - END -->

    <div class="row-fluid">
        <div class="box  span12">
            <div class="box-header" data-original-title>
                <h2><i class="halflings-icon edit"></i><span class="break"></span> Create Credit Note</h2>
            </div>
            <div class="box-content">
                <div class="row-fluid">	
                    <div class="box span12">
                        <div class="box-content">                                 
                            <div class="span1">
                                <div class="control-group">
                                    <label class="control-label" for="TBSearchCredtNot">Credit Note</label>
                                </div>
                            </div>
                            <div class="span10">
                                <div class="controls">
                                    <div class="input-append">
                                        <asp:TextBox runat="server" class="span12" ID="TBSearchCredtNot" AutoPostBack="true" OnTextChanged="TBSearchCredtNot_TextChanged" ></asp:TextBox><asp:LinkButton runat="server" ID="LnkBtn_MCN" CssClass="add-on" ><i class="icon-search"></i></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="span12">
                                <div class="controls">
                                    <div class="scrollit">
                                        <asp:GridView ID="GVScrhMcn" runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found!"  class="table table-striped table-bordered" AllowPaging="True" PageSize="4" AutoGenerateColumns="False" DataKeyNames="Mcn_id,Mcn_sono" OnPageIndexChanging="GVScrhMcn_PageIndexChanging" OnRowCommand="GVScrhMcn_RowCommand" OnRowDeleting="GVScrhMcn_RowDeleting" >
                                            <Columns>
                                                <asp:BoundField DataField="Mcn_sono" HeaderText="ID" SortExpression="Mcn_sono" />
                                                <asp:BoundField DataField="ven_nam" HeaderText="Vender" SortExpression="ven_nam" />
                                                <asp:BoundField DataField="Mcn_dat" HeaderText="Date" SortExpression="Mcn_dat" />
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
                            <h1><span style="color:black;"> Credit Note</span></h1>                                    
                        </div>
                    <div class="span6"></div>
                    <div class="span3">
                        <div class="box span12">
                            <div class="box-header" data-original-title>
                                <h2><i class="halflings-icon calendar"></i><span class="break"></span>Date</h2>
                            </div>
                            <div class="box-content">
                                <asp:TextBox runat="server" class="span10 datepicker" ID="TBCNDat" placeholder="02/16/12" ></asp:TextBox>
                                <asp:Label runat="server" class="span12" ID="TBCredtNot" Visible="false" ></asp:Label>
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
                                <div class="span12">
                                    <div class="control-group">
                                      <label style="color:black" for="DDL_Pur" >Purchase</label>
                                        <div class="controls">
                                            <asp:DropDownList id="DDL_Pur" runat="server" data-rel="chosen" CssClass="span12" AutoPostBack="True" OnSelectedIndexChanged="DDL_Pur_SelectedIndexChanged"></asp:DropDownList>                                           
                                        </div>
                                    </div>
                                </div>
                                <div class="span9"></div> 
                                <div class="span3">
                                    <div class="span12">
                                       <div class="control-group">
                                            <label style="color:black" for="TBPurDat" >Purchase Date</label>
                                            <div class="controls">
                                                <asp:TextBox ID="TBPurDat" runat="server" CssClass="span12"></asp:TextBox>
                                            </div>
                                       </div>
                                    </div>
                                </div>                   
                                <div class="span12">
                                    <div class="control-group">
                                      <label style="color:black" for="TBVenNam" >Vendor</label>
                                        <div class="controls">
                                            <asp:DropDownList id="ddl_Ven" runat="server" data-rel="chosen" CssClass="span12"></asp:DropDownList>                                           
                                        </div>
                                    </div>
                                </div>
                                <div class="span12">
                                    <div class="control-group">
                                      <label style="color:black" for="TBVenNam" >Supplier</label>
                                        <div class="controls">
                                            <asp:DropDownList id="ddl_sup" runat="server" data-rel="chosen" CssClass="span12"></asp:DropDownList>                                           
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
                    <asp:Panel ID="PnlCrtPurItem" runat="server">
                        <div class="row-fluid">	
                            <div class="span12">
                                <div class="box-content span12">                            
                                <asp:GridView ID="GVCNItm" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="true" EmptyDataText="No Record Exits!!"  class="table table-striped table-bordered" OnRowDeleting="GVCNItm_RowDeleting" >
                                    <Columns>
                                        <asp:TemplateField HeaderText="ITEMS">  
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddl_Itm" runat="server"  data-rel="chosen" ></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DESCRIPTIONS">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TbaddPurItmDscptin" runat="server" Text='<%# Eval("DESCRIPTIONS")%>' style="width:120px; height:20px; background:none; border:none;" ></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QTY">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TbAddPurItmQty" runat="server" Text='<%# Eval("QTY")%>'  style="width:50px; height:20px; background:none; border:none;"  ></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UNIT">
                                            <ItemTemplate>                                              
                                                <asp:TextBox ID="TbAddPurUnit" runat="server" Text='<%# Eval("UNIT")%>'  style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>                    
                                            </ItemTemplate>
                                        </asp:TemplateField>                                        
                                         <asp:TemplateField HeaderText="REMARKS">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TBDCNRmk" runat="server" Text='<%# Eval("REMARKS")%>' style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>
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
                <asp:HiddenField ID="HFMcn_id" runat="server" Value='<%# Eval("Mcn_id")%>' />                                           
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
                <div class="span12">
                    <asp:Button runat="server"  ID="btnSave" Text="Save"  CssClass="btn btn-info" OnClick="btnSave_Click" />   
                    <asp:Button runat="server"  ID="btnCancl" Text="Cancel" CssClass="btn" OnClick="btnCancl_Click"  />       
                </div>  
            </div>
        </div>
    </div>
    <asp:HiddenField ID="HFMCN" runat="server" />
    <asp:HiddenField ID="HFDCN" runat="server" />
    <asp:HiddenField ID="HFCredtNot" runat="server" />
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
