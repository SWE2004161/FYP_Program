<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="UserQuery.aspx.cs" Inherits="RFID_System_Web.UserQuery" %>
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
    <h1>QUERY</h1>
           <br />
    <div class="col-12">
        <div class="custom-line"></div>
    </div>
</div>

         <div class="row mb-3">
            <h1>Blockchain RFID Data Viewer</h1>
            </div>


                <div class="row mb-3">
                <div class="col-sm-2">
                    <asp:Label ID="Label2" runat="server" Text="Filter By:" CssClass="form-label"></asp:Label>
                </div>
                <div class="col-sm-2">
                    <asp:DropDownList ID="DropDownListFilterBy" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="DropDownListFilterBy_SelectedIndexChanged">
                        <asp:ListItem Text="All" Value="All"></asp:ListItem>
                        <asp:ListItem Text="TagID" Value="TagID"></asp:ListItem>
                        <asp:ListItem Text="Location" Value="Location"></asp:ListItem>
                        <asp:ListItem Text="ReaderIP" Value="ReaderIP"></asp:ListItem>
                        <asp:ListItem Text="Sensor" Value="Sensor"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-sm-2" id="tagIdContainer" runat="server" visible="false">
                    <asp:Label ID="Label3" runat="server" Text="TagID:" CssClass="form-label"></asp:Label>
                </div>
                <div class="col-sm-2" id="tagIdFilterContainer" runat="server" visible="false">
                    <asp:TextBox ID="TextBoxTagID" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-sm-2" id="locationContainer" runat="server" visible="false">
                    <asp:Label ID="Label4" runat="server" Text="Location:" CssClass="form-label"></asp:Label>
                </div>
                <div class="col-sm-2" id="locationFilterContainer" runat="server" visible="false">
                    <asp:TextBox ID="TextBoxLocation" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-sm-2" id="readerIpContainer" runat="server" visible="false">
                    <asp:Label ID="Label5" runat="server" Text="ReaderIP:" CssClass="form-label"></asp:Label>
                </div>
                <div class="col-sm-2" id="readerIpFilterContainer" runat="server" visible="false">
                    <asp:TextBox ID="TextBoxReaderIP" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-sm-2" id="sensorContainer" runat="server" visible="false">
                    <asp:Label ID="Label6" runat="server" Text="Sensor:" CssClass="form-label"></asp:Label>
                </div>
                <div class="col-sm-2" id="sensorFilterContainer" runat="server" visible="false">
                    <asp:TextBox ID="TextBoxSensor" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-sm-2">
                    <asp:Button ID="Button2" runat="server" Text="Submit" OnClick="Button2_Click" CssClass="btn btn-primary" /><br />
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                </div>
            </div>

         <div class="container">
            <asp:GridView ID="GridViewData" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" OnRowCommand="GridViewData_RowCommand">
                <Columns>
                    
                    <asp:BoundField DataField="TagID" HeaderText="Tag ID" />
                    <asp:BoundField DataField="Company" HeaderText="Company" />
                    <asp:BoundField DataField="Job" HeaderText="Job" />
                    <asp:BoundField DataField="Location" HeaderText="Location" />
                    <asp:BoundField DataField="ProductID" HeaderText="Product ID" />
                    <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                    <asp:BoundField DataField="ReaderIP" HeaderText="Reader IP" />
                    <asp:BoundField DataField="Sensor" HeaderText="Sensor" />
                    <asp:BoundField DataField="SensorData" HeaderText="Sensor Data" />
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblNoData" runat="server" Text="No data found." Visible="False"></asp:Label>
        </div>

         </form>
</asp:Content>
