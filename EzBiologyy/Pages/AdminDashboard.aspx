<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="EzBiologyy.Pages.AdminDashboard" MasterPageFile="~/Admin.Master"%>


<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server" >
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

    <!-- HERO -->
    <div class="hero">
      <h1><asp:Label runat="server" ID="greetingLbl"/></h1>
      <p>System Overview — Manage and monitor all users on EzBiology</p>
      <div class="stat-row">
        <div class="stat-card">
          <div class="num"><asp:Label runat="server" ID="lblStatTotal" Text="0" /></div>
          <div class="lbl">Total Users</div>
        </div>
        <div class="stat-card">
          <div class="num"><asp:Label runat="server" ID="lblStatStudents" Text="0" /></div>
          <div class="lbl">Total Students</div>
        </div>
        <div class="stat-card">
          <div class="num"><asp:Label runat="server" ID="lblStatTeachers" Text="0" /></div>
          <div class="lbl">Total Teachers</div>
        </div>
        <div class="stat-card">
          <div class="num"><asp:Label runat="server" ID="lblStatDisabled" Text="0" /></div>
          <div class="lbl">Disabled Accounts</div>
        </div>
      </div>
    </div>

    <!-- CONTENT -->
    <div class="content">

      <div class="section-label">Quick Actions</div>
      <div class="quick-actions">
        <a href="AdminCreate.aspx" class="action-card">
          <div class="action-icon">👤</div>
          <span>Create New User</span>
        </a>
        <a href="AdminManage.aspx" class="action-card">
          <div class="action-icon">📋</div>
          <span>View All Users</span>
        </a>
        <a href="AdminProfile.aspx" class="action-card">
          <div class="action-icon">⚙️</div>
          <span>Edit Profile</span>
        </a>
      </div>

      <div class="section-label">Recent Users</div>
      <div class="table-wrap">
        <table>
          <thead>
            <tr>
              <th>User ID</th>
              <th>Full Name</th>
              <th>Username</th>
              <th>Role</th>
              <th>Status</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            <asp:Repeater ID="lstRecent" runat="server" OnItemCommand="lstRecent_ItemCommand">
              <ItemTemplate>
                <tr>
                  <td><%# string.Format("U{0:D5}", Eval("UserID")) %></td>
                  <td><%# Eval("FullName") %></td>
                  <td><%# Eval("Username") %></td>
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
            </asp:Repeater>
          </tbody>
        </table>
      </div>
    </div>

    <!-- BOTTOM BAR -->
    <div class="bottom-bar">
      <asp:Label runat="server" ID="lblShowing" CssClass="total-label" Text="" />
      <asp:Button runat="server" CssClass="btn btn-green" Text="+ Add New User" OnClick="Unnamed5_Click" />
    </div>
</asp:Content>
