<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="RFID_System_Web.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <form runat="server">
            <h1>Query</h1><br />

            <div class="row mb-3">
            <div class="col-sm-2">
                <asp:Label ID="Label1" runat="server" Text="TagID:" CssClass="form-label"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

            <div class="row ">
            <div class="col-sm-2">
                <asp:Label ID="Label2" runat="server" Text="PreHash:" CssClass="form-label"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

            <div class="row mt-4">
            <div class="col-sm-6">
                <asp:Button ID="Button2" runat="server" Text="Submit" OnClick="Button2_Click" CssClass="btn btn-primary" />
            </div>
        </div>

        <div class="container">
        <h1>Table Page</h1>
        <div class="table-responsive">
            <div class="small-table">
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
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <asp:Label ID="lblNoData" runat="server" Text="No data found." Visible="False"></asp:Label>
        </div>


            </form>
</asp:Content>
