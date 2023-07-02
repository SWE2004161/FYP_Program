<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ManageRequest.aspx.cs" Inherits="RFID_System_Web.ManageRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
    <h2>Admin Form</h2>
    <br />

    <!-- Panel for displaying selected request details --><div class="container">
    <asp:Panel ID="pnlRequestDetails" runat="server" Visible="false" CssClass="panel panel-default">
    <div class="panel-heading">
        <h4 class="panel-title">Selected Request Details</h4>
    </div>
    <div class="panel-body">

         <div class="form-group">
            <label for="lblSelectedRequest">Request:</label>
            <asp:Label ID="lblSelectedRequest" runat="server" CssClass="form-control-static" Visible="false"></asp:Label>
        </div>

        <div class="form-group">
            <label for="lblSelectedTitle">Title:</label>
            <asp:Label ID="lblSelectedTitle" runat="server" CssClass="form-control-static"></asp:Label>
        </div>
        <div class="form-group">
            <label for="lblSelectedDescription">Description:</label>
            <asp:Label ID="lblSelectedDescription" runat="server" CssClass="form-control-static"></asp:Label>
        </div>
        <div class="form-group">
            <label for="lblSelectedStatus">Status:</label>
            <asp:Label ID="lblSelectedStatus" runat="server" CssClass="form-control-static"></asp:Label>
        </div>
        <div class="form-group">
            <label for="ddlSelectedStatus">Update Status:</label>
            <asp:DropDownList ID="ddlSelectedStatus" runat="server" CssClass="form-control">
                <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                <asp:ListItem Text="Accepted" Value="Accepted"></asp:ListItem>
                <asp:ListItem Text="Declined" Value="Declined"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <asp:Button ID="btnUpdateStatus" runat="server" Text="Update Status" OnClick="btnUpdateStatus_Click" CssClass="btn btn-primary" />
    </div>
</asp:Panel>

                                                          </div>

    <div class="container">
        <h2>Pending Requests</h2>
        <asp:GridView ID="GridViewData" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" OnRowCommand="GridViewData_RowCommand">
    <Columns>
        <asp:BoundField DataField="RequestId" HeaderText="Request ID" ItemStyle-Width="10%" />
        <asp:BoundField DataField="Username" HeaderText="Username" ItemStyle-Width="20%" />
        <asp:BoundField DataField="Title" HeaderText="Title" ItemStyle-Width="20%" />
        <asp:TemplateField HeaderText="Actions" ItemStyle-Width="10%">
            <ItemTemplate>
                <asp:Button ID="btnShowDetails" runat="server" Text="Show Details" CommandName="ShowDetails" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-primary" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
    </div>
</form>

</asp:Content>
