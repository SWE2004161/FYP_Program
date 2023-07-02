<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/Site3.Master" AutoEventWireup="true" CodeBehind="ClientQuery.aspx.cs" Inherits="RFID_System_Web.ClientQuery" %>
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
            <h1>Query</h1><br />
            <div class="col-12">
                <div class="custom-line"></div>
            </div>
        </div>
          

            <div class="row mb-3">
            <h1>RFID Tag Form</h1><br />
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
                <asp:Button ID="Button2" runat="server" Text="Submit" OnClick="Button2_Click" CssClass="btn btn-primary" />
            </div>
        </div>
         <br />

        <div class="container">


             <div class="card mb-3" id="pnlCardTag" runat="server"  style="max-width: 60rem;" visible="false">
                <h3 class="card-header">RFID Tag Information</h3>
            <div class="card-body">
                <p>Tag ID: <asp:Label ID="lblTagID" runat="server"></asp:Label></p>
                <p>Product Name: <asp:Label ID="lblProductName" runat="server"></asp:Label></p>
                <p>Product ID: <asp:Label ID="lblProductID" runat="server"></asp:Label></p>
                
               
            </div>
        </div>


        <div id="detail" runat="server"  style="max-width: 60rem;" visible="false">
        <h1>Detail Process</h1>
        <div class="table-responsive">
            <div class="small-table">
                <asp:GridView ID="GridViewData" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" OnRowCommand="GridViewData_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="Company" HeaderText="Company" />
                        <asp:BoundField DataField="Job" HeaderText="Job" />
                        <asp:BoundField DataField="Location" HeaderText="Location" />
                        <asp:BoundField DataField="ReaderIP" HeaderText="Reader IP" />
                        <asp:BoundField DataField="Sensor" HeaderText="Sensor" />
                        <asp:BoundField DataField="SensorData" HeaderText="Sensor Data" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <asp:Label ID="lblNoData" runat="server" Text="No data found." Visible="False"></asp:Label>
        </div>

            </div>

         </form>
</asp:Content>
