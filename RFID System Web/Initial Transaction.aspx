<%@ Page Title="" Language="C#" Async="true"  MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="Initial Transaction.aspx.cs" Inherits="RFID_System_Web.Initial_Transaction" %>
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
            <h1>Initial RFID Transaction Form</h1><br />
            <hr/>
        </div>

        <div class="row mb-3">
            <div class="col-sm-2">
                <asp:Label ID="TagIDLabel" runat="server" Text="TagID:" CssClass="form-label"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:TextBox ID="TagID" runat="server" CssClass="form-control disabled"></asp:TextBox>
            </div>
        </div>
        <div class="row mb-3">
            <div class="col-sm-2">
                <asp:Label ID="ReaderIPLabel" runat="server" Text="ReaderIP:" CssClass="form-label"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:TextBox ID="ReaderIP" runat="server" CssClass="form-control disabled"></asp:TextBox>
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-sm-2">
                <asp:Label ID="ProductNameLabel" runat="server" Text="Product Name:" CssClass="form-label"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:TextBox ID="ProductName" runat="server" CssClass="form-control disabled"></asp:TextBox>
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-sm-2">
                <asp:Label ID="ProductIDLabel" runat="server" Text="Product ID:" CssClass="form-label"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:TextBox ID="ProductID" runat="server" CssClass="form-control disabled"></asp:TextBox>
            </div>
        </div>
        <div class="row mb-3">
            <div class="col-sm-2">
                <asp:Label ID="LocationLabel" runat="server" Text="Location:" CssClass="form-label"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:TextBox ID="Location" runat="server" CssClass="form-control disabled"></asp:TextBox>
            </div>
        </div>
        <div class="row mb-3">
            <div class="col-sm-2">
                <asp:Label ID="CompanyLabel" runat="server" Text="Company:" CssClass="form-label"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:TextBox ID="Company" runat="server" CssClass="form-control disabled"></asp:TextBox>
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-sm-2">
                <asp:Label ID="JobLabel" runat="server" Text="Job:" CssClass="form-label"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:TextBox ID="Job" runat="server" CssClass="form-control disabled"></asp:TextBox>
            </div>
        </div>
        <div class="row mb-3">
            <div class="col-sm-2">
                <asp:Label ID="SensorLabel" runat="server" Text="Sensor:" CssClass="form-label"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:TextBox ID="Sensor" runat="server" CssClass="form-control disabled" ></asp:TextBox>
            </div>
        </div>
        <div class="row mb-3">
            <div class="col-sm-2">
                <asp:Label ID="SensorDataLabel" runat="server" Text="Sensor Data:" CssClass="form-label"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:TextBox ID="SensorData" runat="server" CssClass="form-control disabled" ></asp:TextBox>
            </div>
        </div>

        <div class="row mt-4">
            <div class="col-sm-6">
                <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Transactions_Click" CssClass="btn btn-primary" />
            </div>
            <br />
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        </div>
       
        <br />
       </form>
</asp:Content>
