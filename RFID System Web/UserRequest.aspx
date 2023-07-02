<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="UserRequest.aspx.cs" Inherits="RFID_System_Web.UserRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
    .custom-line {
        height: 1px;
        background-color: black;
        width: 99%;
        margin-left: auto;
        margin-right: auto;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">

       <div class="row mb-3">
            
            <h1>Requesting Administrator Assistance</h1>
           <br />
            <div class="col-12">
                <div class="custom-line"></div>
            </div>
            </div>
        <div class="container">
        <div class="form-group row mb-3">
            <h2>Contact Support</h2>
            <label for="txtTitle">Subject:</label>
            <input type="text" id="txtTitle" runat="server" class="form-control" placeholder="Enter request subject" />
        </div>
        <div class="form-group row mb-3">
            <label for="txtDescription">Description:</label>
            <textarea id="txtDescription" runat="server" class="form-control" rows="4" placeholder="Enter request description"></textarea><br />
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
        </div>
        <div class="form-group row mb-3">
            <asp:Button ID="btnSubmit" runat="server" OnClick="btnRequest_Click" Text="Submit" CssClass="btn btn-primary" />
        </div>
            </div>

        <div class="container" runat="server" id="talbe" visible="true">
        <h2>Requests table</h2>
        <asp:GridView ID="GridViewData" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" OnRowCommand="GridViewData_RowCommand">
            <Columns>
                <asp:BoundField DataField="Title" HeaderText="Subject" ItemStyle-Width="10%" />
                <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-Width="20%" />
                <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Width="20%" />
            </Columns>
        </asp:GridView>
    </div>

    </form>
</asp:Content>
