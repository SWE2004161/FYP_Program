<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RequestSigner.aspx.cs" Inherits="RFID_System_Web.RequestSigner" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">

    <h2>
        Admin Form</h2>
        <br />

        <div class="container">
    <h2>Pending Requests Signer</h2>
    <asp:GridView ID="GridViewSigner" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" OnRowCommand="GridViewSigner_RowCommand">
        <Columns>
            <asp:BoundField DataField="RequestId" HeaderText="Request ID" ItemStyle-Width="10%" />
            <asp:BoundField DataField="Username" HeaderText="Username" ItemStyle-Width="20%" />
            <asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-Width="20%" />
            <asp:BoundField DataField="Company" HeaderText="Company" ItemStyle-Width="20%" />
            <asp:TemplateField HeaderText="Actions" ItemStyle-Width="30%">
                <ItemTemplate>
                    <asp:Button ID="btnAccept" runat="server" CssClass="btn btn-success" Text="Accept" CommandName="Accept" CommandArgument='<%# Container.DataItemIndex %>' />
                    <asp:Button ID="btnDecline" runat="server" CssClass="btn btn-danger" Text="Decline" CommandName="Decline" CommandArgument='<%# Container.DataItemIndex %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>


        </form>
</asp:Content>
