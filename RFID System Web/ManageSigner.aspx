<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ManageSigner.aspx.cs" Inherits="RFID_System_Web.ManageSigner" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">

        <div class="row">
            <h1>Blockchain Signer</h1>
       <div class="col-md-6">
            <div class="card border-secondary mb-3" style="max-width: 40rem;">
                <div class="card-header">Identities</div>
                <div class="card-body">
                    <h5 class="card-title">Signer Name List</h5>
                    <div class="form-group">
                        <asp:Button ID="Button1" runat="server" Text="Identities" OnClick="Button1_Click" CssClass="btn btn-primary" /><br />
                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                        <asp:Label ID="Label1" runat="server" Text=" " CssClass="mt-2"></asp:Label>
                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" OnRowCommand="GridViewData_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="name" HeaderText="Name" ItemStyle-Width="10%" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
            <br />

        <div class="row">
            <div class="col-md-6">
                <div class="card border-secondary mb-3" style="max-width: 40rem;">
                    <div class="card-header">Signer Name Registration</div>
                    <div class="card-body">
                        <h5 class="card-title">Register</h5>
                        <div class="form-group">
                            <label for="TextBox1">Name:</label>
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                            <br />
                            <asp:Button ID="Button2" runat="server" Text="Register" OnClick="Button2_Click" CssClass="btn btn-primary" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
        <br />
        
            <div class="row">
    <div class="col-md-6">
        <div class="card border-secondary mb-3" style="max-width: 40rem;">
            <div class="card-header">Signer Account Administration</div>
            <div class="card-body">
                <h4 class="card-title">Assign Signer</h4>
                <div class="form-group">
                    <label for="lblUsername">Username:</label>
                    <asp:TextBox ID="txtNewUsername" runat="server" CssClass="form-control"></asp:TextBox><br />
                    <label for="lblSigner">Signer:</label>
                    <asp:TextBox ID="txtNewSigner" runat="server" CssClass="form-control"></asp:TextBox><br />
                    <br />
                    <asp:Button ID="btnSaveData" runat="server" Text="Save Data" OnClick="btnSaveData_Click" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </div>
</div>

        </div>


            <div class="container">
            <h1>Assigned Signers List</h1>
                            <div class="row mb-3">
                                <div class="col-sm-2">
                    <asp:Label ID="Label2" runat="server" Text="Filter By:" CssClass="form-label"></asp:Label>
                </div>
                <div class="col-sm-2">
                    <asp:DropDownList ID="DropDownListFilterBy" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="DropDownListFilterBy_SelectedIndexChanged">
                        <asp:ListItem Text="All" Value="All"></asp:ListItem>
                        <asp:ListItem Text="Username" Value="Username"></asp:ListItem>
                        <asp:ListItem Text="Signer" Value="Signer"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-sm-2" id="usernameContainer" runat="server" visible="false">
                    <asp:Label ID="LabelUsername" runat="server" Text="Username:" CssClass="form-label"></asp:Label>
                </div>
                <div class="col-sm-2" id="usernameFilterContainer" runat="server" visible="false">
                    <asp:TextBox ID="TextBoxUsername" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-sm-2" id="signerContainer" runat="server" visible="false">
                    <asp:Label ID="LabelSigner" runat="server" Text="Signer:" CssClass="form-label"></asp:Label>
                </div>
                <div class="col-sm-2" id="signerFilterContainer" runat="server" visible="false">
                    <asp:TextBox ID="TextBoxSigner" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <asp:Button ID="btnShowData" runat="server" Text="Show Data" OnClick="btnShowData_Click" CssClass="btn btn-primary" /><br /><br />
            <div class="table-responsive smaller-gridview">
                <asp:GridView ID="GridViewData" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" OnRowCommand="GridViewData_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="UserId" HeaderText="ID" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="Username" HeaderText="Name" ItemStyle-Width="40%" />
                        <asp:BoundField DataField="Signer" HeaderText="Signer" ItemStyle-Width="40%" />
                        <asp:TemplateField HeaderText="Actions" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="DeleteRow" CommandArgument='<%# Eval("UserId") %>' CssClass="btn btn-danger" OnClientClick="return confirm('Are you sure you want to delete this row?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <asp:Label ID="lblNoData" runat="server" Text="No data found." Visible="False"></asp:Label>
        </div>
    </form>
</asp:Content>
