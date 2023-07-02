<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="RFID_System_Web.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <h1>Transaction</h1>
        <div class="row mb-3">
            <h1>RFID Tag</h1>
            <div class="col-sm-2">
                <asp:Label ID="Label2" runat="server" Text="TagID:" CssClass="form-label"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

            <div class="row ">
            <div class="col-sm-2">
                <asp:Label ID="Label3" runat="server" Text="PreHash:" CssClass="form-label"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

            <div class="row mt-4">
            <div class="col-sm-6">
                <asp:Button ID="Button3" runat="server" Text="Submit" OnClick="Button3_Click" CssClass="btn btn-primary" />
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-sm-2">
                <asp:Label ID="TagIDLabel" runat="server" Text="TagID:" CssClass="form-label"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:TextBox ID="TagID" runat="server" CssClass="form-control disabled" Enabled="false"></asp:TextBox>
            </div>
        </div>
        <div class="row mb-3">
            <div class="col-sm-2">
                <asp:Label ID="ReaderIPLabel" runat="server" Text="ReaderIP:" CssClass="form-label"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:TextBox ID="ReaderIP" runat="server" CssClass="form-control disabled" Enabled="false"></asp:TextBox>
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-sm-2">
                <asp:Label ID="ProductNameLabel" runat="server" Text="Product Name:" CssClass="form-label"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:TextBox ID="ProductName" runat="server" CssClass="form-control disabled" Enabled="false"></asp:TextBox>
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-sm-2">
                <asp:Label ID="ProductIDLabel" runat="server" Text="Product ID:" CssClass="form-label"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:TextBox ID="ProductID" runat="server" CssClass="form-control disabled" Enabled="false"></asp:TextBox>
            </div>
        </div>
        <div class="row mb-3">
            <div class="col-sm-2">
                <asp:Label ID="LocationLabel" runat="server" Text="Location:" CssClass="form-label"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:TextBox ID="Location" runat="server" CssClass="form-control disabled" Enabled="false"></asp:TextBox>
            </div>
        </div>
        <div class="row mb-3">
            <div class="col-sm-2">
                <asp:Label ID="CompanyLabel" runat="server" Text="Company:" CssClass="form-label"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:TextBox ID="Company" runat="server" CssClass="form-control disabled" Enabled="false"></asp:TextBox>
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-sm-2">
                <asp:Label ID="JobLabel" runat="server" Text="Job:" CssClass="form-label"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:TextBox ID="Job" runat="server" CssClass="form-control disabled" Enabled="false"></asp:TextBox>
            </div>
        </div>
        <div class="row mb-3">
            <div class="col-sm-2">
                <asp:Label ID="SensorLabel" runat="server" Text="Sensor:" CssClass="form-label"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:TextBox ID="Sensor" runat="server" CssClass="form-control disabled" Enabled="false"></asp:TextBox>
            </div>
        </div>
        <div class="row mb-3">
            <div class="col-sm-2">
                <asp:Label ID="SensorDataLabel" runat="server" Text="Sensor Data:" CssClass="form-label"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:TextBox ID="SensorData" runat="server" CssClass="form-control disabled" Enabled="false"></asp:TextBox>
            </div>
        </div>

        <div class="row mt-4">
            <div class="col-sm-6">
                <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Transactions_Click" CssClass="btn btn-primary" />
            </div>
        </div>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>

    <div class="container">
    <h1>Table Page</h1>
        <asp:Button ID="Button2" runat="server" Text="Submit" OnClick="Button2_Click" CssClass="btn btn-primary" />
    <asp:GridView ID="GridViewData" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" OnRowCommand="GridViewData_RowCommand">
        <Columns>
            <asp:BoundField DataField="Company" HeaderText="Company" />
            <asp:BoundField DataField="Job" HeaderText="Job" />
            <asp:BoundField DataField="Location" HeaderText="Location" />
            <asp:BoundField DataField="ProductID" HeaderText="Product ID" />
            <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
            <asp:BoundField DataField="ReaderIP" HeaderText="Reader IP" />
            <asp:BoundField DataField="Sensor" HeaderText="Sensor" />
            <asp:BoundField DataField="SensorData" HeaderText="Sensor Data" />
            <asp:BoundField DataField="TagID" HeaderText="Tag ID" />
            <asp:BoundField DataField="UniID" HeaderText="Uni ID" />
        </Columns>
    </asp:GridView>
    <asp:Label ID="lblNoData" runat="server" Text="No data found." Visible="False"></asp:Label>
</div>

        <div class="container">
            <h2>Send Request</h2>
            <div class="form-group">
                <label for="txtDescription">Description:</label>
                <textarea id="txtDescription" runat="server" class="form-control" rows="4" placeholder="Enter request description"></textarea>
            </div>
            <div class="form-group">
                <asp:Button ID="btnSubmit" runat="server" OnClick="btnRequest_Click" Text="Submit" CssClass="btn btn-primary" />
            </div>
        </div>

        </form>
</asp:Content>
