<%@ Page Title="Attendance" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
     CodeBehind="frm_attn.aspx.cs" Inherits="Foods.frm_attn" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <style type="text/css">

        .scrollit {
            overflow:scroll;
            height:100%;
	        width:100%;           
	        margin:0px auto;
        }

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
            <a href="#">Security</a>
            <i class="icon-angle-right"></i>
        </li>
        <li><a href="frm_attn.aspx">Attendance</a></li>
    </ul>

    <!-- imageLoader - START -->

    <img id='HiddenLoadingImage' src="../../img/page-loader.gif" class="LoadingProgress" />

    <!-- imageLoader - END -->
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row-fluid">
                <div class="box  span12">
                    <div class="box-header" data-original-title>
                        <h2><i class="halflings-icon edit"></i><span class="break"></span>Attendance</h2>
                    </div>
                    <div class="box-content">
                            <div class="row-fluid">	
                                <div class="span12">
                                    <div class="span5"></div>
                                    <div class="row-fluid">	
                                        <div class="box span12">
                                            <div class="box-content">
                                                <div class="span1">&nbsp;</div>
                                                <div class="span12">
                                                    <div class="control-group">
                                                        <label style="color:black" for="TBVenNam" >EmployeeID</label>
                                                        <div class="controls">
                                                            <asp:Label runat="server" ID="lbl_empid"></asp:Label>                                                                                    
                                                        </div>
                                                    </div>
                                                </div>                                
                                                <div class="span12" >
                                                    <div class="control-group">
                                                            <label style="color:black" for="DDL_Emp" >Employee</label>
                                                        <div class="controls">
                                                            <asp:DropDownList ID="DDL_Emp" runat="server" data-rel="chosen" CssClass="span12" AutoPostBack="True" OnSelectedIndexChanged="DDL_Emp_SelectedIndexChanged">                                               
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="span12">
                                                    <div class="control-group">
                                                            <label style="color:black" for="TBTim" >Time Entry</label>
                                                        <div class="controls">
                                                            <asp:TextBox runat="server" class="span10" ID="TBTim" Visible="false" ></asp:TextBox>  
                                                            
                                                            <iframe src="http://free.timeanddate.com/clock/i5wvar1b/n757/th1" frameborder="0" width="58" height="19"></iframe>
                                  
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="form-actions">
                                                        <div class="span11">&nbsp;</div>
                                                        <div class="span6">
                                                            <asp:Button ID="btnSubmit" runat="server" Text="Mark" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="Controller/Common.js"></script>
</asp:Content>
