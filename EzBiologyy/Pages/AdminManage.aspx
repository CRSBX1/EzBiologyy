<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminManage.aspx.cs" Inherits="EzBiologyy.Pages.AdminManage" MasterPageFile="~/Admin.Master"%>


<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server" >
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
        <div class="frame">
        <div style="background:#fff;">

        <!-- HERO -->
        <div class="hero">
            <h1>Manage Users</h1>
            <p>View, search, and manage all registered users on EzBiology</p>
        </div>

        <!-- CONTENT -->
        <div class="content">

            <!-- TOOLBAR -->
            <div class="toolbar">
            <div class="search-wrap">
                <label>ID or Name</label>
                <asp:TextBox runat="server" TextMode="Search" CssClass="search-input" ID="searchInput" placeholder="e.g. U00001 / Richard Lionheart" />
            </div>
            <div class="filter-wrap">
                <label>Role</label>
                <asp:DropDownList runat="server" ID="roleFilter" CssClass="filter-select">
                    <asp:ListItem>All Roles</asp:ListItem>
                    <asp:ListItem>Student</asp:ListItem>
                    <asp:ListItem>Teacher</asp:ListItem>
                    <asp:ListItem>Admin</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="filter-wrap">
                <label>Status</label>
                <asp:DropDownList runat="server" ID="statusFilter" CssClass="filter-select">
                    <asp:ListItem>All Status</asp:ListItem>
                    <asp:ListItem>Active</asp:ListItem>
                    <asp:ListItem>Disabled</asp:ListItem>
                </asp:DropDownList>
            </div>
            <asp:Button runat="server" ID="btnSearch" CssClass="btn-search" Text="🔍 Search" OnClick="btnSearch_Click" />
            </div>

            <!-- TABLE -->
            <div class="table-wrap">
            <table>
                <thead>
                <tr>
                    <th>User ID</th>
                    <th>Full Name</th>
                    <th>Username</th>
                    <th>Age</th>
                    <th>Gender</th>
                    <th>Role</th>
                    <th>Status</th>
                    <th>Actions</th>
                </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="lstUsers" runat="server" OnItemCommand="lstUsers_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td><%# string.Format("U{0:D5}", Eval("UserID")) %></td>
                                <td><%# Eval("FullName") %></td>
                                <td><%# Eval("Username") %></td>
                                <td><%# Eval("Age") %></td>
                                <td><%# Eval("Gender") %></td>
                                <td><span class='<%# "badge badge-" + ((string)Eval("Role")).ToLower() %>'><%# Eval("Role") %></span></td>
                                <td>
                                    <span class='<%# Convert.ToBoolean(Eval("IsActive")) ? "badge badge-active" : "badge badge-disabled" %>'>
                                        <%# Convert.ToBoolean(Eval("IsActive")) ? "Active" : "Disabled" %>
                                    </span>
                                </td>
                                <td>
                                    <a class="btn-sm btn-edit" href='<%# "AdminEdit.aspx?UserID=" + Eval("UserID") %>'>Edit</a>
                                    <asp:LinkButton runat="server"
                                        Visible='<%# Convert.ToInt32(Eval("UserID")) != CurrentAdminId %>'
                                        CssClass='<%# Convert.ToBoolean(Eval("IsActive")) ? "btn-sm btn-disable" : "btn-sm btn-enable" %>'
                                        Text='<%# Convert.ToBoolean(Eval("IsActive")) ? "Disable" : "Enable" %>'
                                        CommandName="Toggle"
                                        CommandArgument='<%# Eval("UserID") %>'
                                        OnClientClick="return confirm('Toggle this user’s active status?');" />
                                    <asp:LinkButton runat="server"
                                        Visible='<%# Convert.ToInt32(Eval("UserID")) != CurrentAdminId %>'
                                        CssClass="btn-sm btn-delete"
                                        Text="Delete"
                                        CommandName="Delete"
                                        CommandArgument='<%# Eval("UserID") %>'
                                        OnClientClick="return confirm('Delete this user? They will be hidden but recoverable in the database.');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblEmpty" runat="server" Visible="false" />
                        </FooterTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            </div>

        </div>

        <!-- BOTTOM BAR -->
        <div class="bottom-bar">
            <asp:Label runat="server" ID="lblShowing" CssClass="total-label" Text="" />
            <asp:Button runat="server" CssClass="btn btn-green" Text="+ Add New User" OnClick="Unnamed7_Click" />
        </div>

        </div>
    </div>
    </asp:Content>
