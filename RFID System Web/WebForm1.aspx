<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="RFID_System_Web.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <form runat="server">
    <div class="row">
        <div class="col-md-6">
            <h1>Identities</h1>
            <div class="form-group">
                <asp:Button ID="Button1" runat="server" Text="Identities" OnClick="Button1_Click" CssClass="btn btn-primary" />
                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                <asp:Label ID="Label1" runat="server" Text=" " CssClass="mt-2"></asp:Label>
            </div>
        </div>

        <br />

        <div class="col-md-6">
            <h1>Register</h1>
            <div class="form-group">
                <label for="TextBox1">Name</label>
                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                <br />
                <asp:Button ID="Button2" runat="server" Text="Register" OnClick="Button2_Click" CssClass="btn btn-primary" />
            </div>
        </div>
    </div>

        <div class="row">
            <div class="col-md-6">
                <h1>Manage Data</h1>
                <div class="form-group">
                    <asp:Label ID="lblUsername" runat="server" Text="" CssClass="mb-2"></asp:Label><br />
                    <asp:Label ID="lblSigner" runat="server" Text="" CssClass="mb-2"></asp:Label><br />
                    <label for="txtNewUsername">Username:</label>
                    <asp:TextBox ID="txtNewUsername" runat="server" CssClass="form-control"></asp:TextBox><br />
                    <label for="txtNewSigner">Signer:</label>
                    <asp:TextBox ID="txtNewSigner" runat="server" CssClass="form-control"></asp:TextBox><br />
                    <br />
                    <asp:Button ID="btnShowData" runat="server" Text="Show Data" OnClick="btnShowData_Click" CssClass="btn btn-primary" /><br /><br />
                    <asp:Button ID="btnSaveData" runat="server" Text="Save Data" OnClick="btnSaveData_Click" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>


            <div class="container">
            <h1>Table Page</h1>
            <div class="table-responsive smaller-gridview">
                <asp:GridView ID="GridViewData" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" OnRowCommand="GridViewData_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="UserId" HeaderText="ID" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="Username" HeaderText="Name" ItemStyle-Width="40%" />
                        <asp:BoundField DataField="Signer" HeaderText="Signer" ItemStyle-Width="40%" />
                        <asp:TemplateField HeaderText="Actions" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="DeleteRow" CommandArgument='<%# Eval("UserId") %>' CssClass="btn btn-danger" OnClientClick="return confirm('Are you sure you want to delete this row?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <asp:Label ID="lblNoData" runat="server" Text="No data found." Visible="False"></asp:Label>
        </div>

        <h2>Admin Form</h2>
        <div class="form-group">
            <label for="txtRequestId">Request ID:</label>
            <input type="text" id="txtRequestId" runat="server" class="form-control col-sm-4" placeholder="Enter request ID" />
            <asp:Button ID="Button3" runat="server" Text="Button" OnClick="btnRequestID_Click" />
        </div>
        <div class="form-group">
            <label for="txtDescription">Description:</label>
            <textarea id="txtDescription" runat="server" class="form-control col-sm-6" rows="4" readonly cols="20" name="S1"></textarea>
        </div>
        <div class="form-group">
            <asp:Button ID="btnAccept1" runat="server" OnClick="btnAccept_Click" CssClass="btn btn-success" Text="Accept" />
            <asp:Button ID="btnDecline1" runat="server" OnClick="btnDecline_Click" CssClass="btn btn-danger" Text="Decline" />
        </div>

         <br />
         <asp:Label ID="Label3" runat="server" Text=" "></asp:Label>

    </form>
</asp:Content>
