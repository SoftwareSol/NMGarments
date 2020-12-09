<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GenerateBarCode.aspx.cs" Inherits="Foods.Source.IP.D.Global_Test.GenerateBarCode" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Generate Bar Code</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" />  
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>  
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>  
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="container">  
            <h3 class="text-center text-uppercase">How to create barcode in asp.net</h3>  
            <div class="row">  
                <div class="form-group">  
                    <label>Enter Number:</label>  
                    <div class="input-group">  
                        <asp:TextBox ID="txtBarcode" CssClass="form-control" runat="server"></asp:TextBox>  
                        <div class="input-group-append">  
                            <asp:Button ID="btnGenrate" CssClass="btn btn-outline-primary" runat="server" Text="Genrate" OnClick="btnGenrate_Click" />                             
                        </div>  
                    </div>  
                </div>                  
            </div>  
            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>  
        </div>    
    </div>
    </form>
</body>
</html>
