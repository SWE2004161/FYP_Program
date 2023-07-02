<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User Login.aspx.cs" Inherits="RFID_System_Web.WebForm5" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>User Login</title>
    <link href="css/my.css" rel="stylesheet" />  
    <link rel="stylesheet" href="css/StyleSheet1.css" />
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
   <div class="container mt-5">
    <div class="row justify-content-center">
        <div class="col-md-6">
            <form id="form1" runat="server">
                <div class="card">
                    <div class="card-body">
                        <h2 class="text-center">Login</h2>

                        <div class="form-group">
                            <label for="txtUsername">Username:</label>
                            <input type="text" class="form-control" id="txtUsername" runat="server" />
                        </div>

                        <div class="form-group">
                            <label for="txtPassword">Password:</label>
                            <input type="password" class="form-control" id="txtPassword" runat="server" />
                        </div>

                        <div class="text-center">
                            <asp:Button ID="Button1" runat="server" Text="Login" OnClick="btnLogin_Click" class="btn btn-primary" />
                        </div>

                        <label id="lblMessage" runat="server" Visible="false"></label>

                        <div class="text-center mt-3">
                            <span>Not a user? <a href="User Register.aspx">Register here</a></span>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    </div>
</div>
     <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ENjdO4Dr2bkBIFxQpeoTz1HIcje39Wm4jDKdf19U8gI4ddQ3GYNS7NTKfAdVQSZe" crossorigin="anonymous"></script>
</body>
</html>