<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User Register.aspx.cs" Inherits="RFID_System_Web.WebForm4" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Registration</title>
    
    <link href="css/my.css" rel="stylesheet" />  
    <link rel="stylesheet" href="css/StyleSheet1.css" />
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
    <div class="row justify-content-center">
        <div class="col-md-6">
            <div class="card">
                <div class="card-body">
                    <h2 class="text-center">Registration</h2>
                    <div class="mb-3">
                        <label for="txtUsername" class="form-label mt-4">Username</label>
                        <input type="text" class="form-control" id="txtUsername" runat="server" placeholder="Name" />
                    </div>
                    <div class="mb-3">
                        <label for="txtPassword" class="form-label">Password</label>
                        <input type="password" class="form-control" id="txtPassword" runat="server" placeholder="Password" />
                    </div>
                    <div class="mb-3">
                        <label for="txtEmail" class="form-label">Email</label>
                        <input type="text" class="form-control" id="txtEmail" runat="server" placeholder="name@example.com" />
                    </div>
                    <div class="mb-3">
                        <label for="txtCompany" class="form-label">Company</label>
                        <input type="text" class="form-control" id="txtCompany" runat="server" placeholder="" />
                        <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="text-center">
                        <asp:Button ID="Button1" runat="server" Text="Register" OnClick="btnRegister_Click" class="btn btn-primary" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ENjdO4Dr2bkBIFxQpeoTz1HIcje39Wm4jDKdf19U8gI4ddQ3GYNS7NTKfAdVQSZe" crossorigin="anonymous"></script>
    
</body>
</html>