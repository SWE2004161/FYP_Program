<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="UserRequestSigner.aspx.cs" Inherits="RFID_System_Web.UserRequestSigner" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form runat="server">

        <div class="card mb-3" id="pnlNoSigner" runat="server"  style="max-width: 60rem;" visible="false">
                <h3 class="card-header">No Signer Assigned</h3>
            <div class="card-body">
                <p>Name: <asp:Label ID="lblUserName" runat="server"></asp:Label></p>
                <p>Email: <asp:Label ID="lblUserEmail" runat="server"></asp:Label></p>
                <p>Company: <asp:Label ID="lblUserCompany" runat="server"></asp:Label></p>
                <p>No signer has been assigned to this user.</p>
                     
                <asp:Button ID="SendRequest" runat="server" Text="Send Request"  class="btn btn-primary" OnClick="btnSendRequest_Click" />
            </div>
        </div>

        <div class="card border-secondary mb-3" style="max-width: 60rem; " runat="server" id="pnlSigner">
        <h2 class="card-header">Signer Account Validation</h2>
        <div class="card-body">
        <asp:GridView ID="GridViewData" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" OnRowCommand="GridViewData_RowCommand">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-Width="10%" />
                <asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-Width="20%" />
                <asp:BoundField DataField="Company" HeaderText="Company" ItemStyle-Width="20%" />
                <asp:BoundField DataField="Validated" HeaderText="Validated" ItemStyle-Width="10%" />
            </Columns>
        </asp:GridView>
    </div>
            </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

</asp:Content>
