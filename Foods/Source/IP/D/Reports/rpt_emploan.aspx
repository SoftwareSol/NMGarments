<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt_emploan.aspx.cs" Inherits="Foods.rpt_emploan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customer Wise Credit Report</title>
          <style type="text/css">
        #container{
	        width:100%;
	        height:100%;
	        font-family:CordiaUPC;
            font-size:18px; 
        }
        .uppper{
	        width:45%;
	        height:auto;
	        margin:0px auto;
	        text-align:center;
        }
        .left{
	        width:45%;
	        height:auto;
	        float:left;
	        margin-left:20px;
        }
        .right{
	        width:49%;
	        height:auto;
	        float:right;
	        text-align:center;
        }
        .gv{
	        width:100%;
	        height: auto;
            text-align:center;
        }
        .clear{
        clear: both;
        }
        h1 {
        margin:0px;
        padding: 0px;
        }
        h2 {
        margin:0px;
        padding: 0px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div>
            <div id="container">
            <div class="uppper">
                <h1>Silver Moon</h1>
                <h2>Employee Loan Report</h2>
            </div>
            <div class="left">
                Employee Name: <asp:Label ID="lbl_Emp" runat="server" ></asp:Label><br />
            </div>
            <div class="right">
                <asp:LinkButton ID="LinkBtnExportExcel" runat="server" OnClick="LinkBtnExportExcel_Click">Export to Excel</asp:LinkButton>       
            </div>
            <div class="clear"></div>
                <fieldset>
                    <br />
                    <br />
                    <div class="gv">
                        <asp:GridView ID="GV_EmpCre" runat="server" EmptyDataText="No Record Found!" ShowHeader="true"  ShowFooter="true" ShowHeaderWhenEmpty="true"                             
                            AutoGenerateColumns="False" style="width:100%; height:auto;" >
                            <Columns>
                                <asp:BoundField DataField="loanid" HeaderText="ID" SortExpression="loanid" />
                                <asp:TemplateField HeaderText="Loan Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_lonamt" runat="server" Text='<%# Eval("loanamt")%>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="LoanDat" HeaderText="Loan Date" SortExpression="LoanDat" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="gv">
                            Total Amount: <asp:Label ID="lbl_ttl" runat="server" Text="--"></asp:Label>
                    </div>
                </fieldset>
        </div>
    </div>
    </form>
</body>
</html>
