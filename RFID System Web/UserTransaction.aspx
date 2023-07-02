<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="UserTransaction.aspx.cs" Inherits="RFID_System_Web.UserTransaction" %>
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
    <h1>TRANSACTION</h1> 
           <br />
    <div class="col-12">
        <div class="custom-line"></div>
    </div>
</div>
        <div class="row mb-3">
            <h1>RFID Transaction Form</h1><br />
            <hr/>
            <div class="col-sm-2">
                <asp:Label ID="Label2" runat="server" Text="TagID:" CssClass="form-label"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

            <div class="row ">
            <div class="col-sm-2">
                <asp:Label ID="Label3" runat="server" Text="Current Block Hash:" CssClass="form-label"></asp:Label>
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
       <br />
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
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        </div>
       
        <br />

       <div class="col-sm-6">
       <span> <a href="Initial Transaction.aspx">Initial First RFID Block</a></span>
       </div>

       </form>
</asp:Content>
