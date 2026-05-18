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
          <div class="num">124</div>
          <div class="lbl">Total Users</div>
        </div>
        <div class="stat-card">
          <div class="num">98</div>
          <div class="lbl">Total Students</div>
        </div>
        <div class="stat-card">
          <div class="num">24</div>
          <div class="lbl">Total Teachers</div>
        </div>
        <div class="stat-card">
          <div class="num">6</div>
          <div class="lbl">Disabled Accounts</div>
        </div>
      </div>
    </div>

    <!-- CONTENT -->
    <div class="content">

      <div class="section-label">Quick Actions</div>
      <div class="quick-actions">
        <a href="AdminCreate" class="action-card">
          <div class="action-icon">👤</div>
          <span>Create New User</span>
        </a>
        <a href="AdminManage" class="action-card">
          <div class="action-icon">📋</div>
          <span>View All Users</span>
        </a>
        <a href="AdminEdit" class="action-card">
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
            <tr>
              <td>U00001</td><td>Herbert Smith</td><td>herbsmith</td>
              <td><span class="badge badge-teacher">Teacher</span></td>
              <td><span class="badge badge-active">Active</span></td>
              <td>
                <button class="btn-sm btn-edit" onclick="window.location='admin-edit-user.html'">Edit</button>
                <button class="btn-sm btn-disable" onclick="openDisable('U00001','Herbert Smith','Teacher')">Disable</button>
                <button class="btn-sm btn-delete" onclick="openDelete('U00001','Herbert Smith','Teacher')">Delete</button>
              </td>
            </tr>
            <tr>
              <td>U00002</td><td>Richard Lionheart</td><td>richard04</td>
              <td><span class="badge badge-student">Student</span></td>
              <td><span class="badge badge-active">Active</span></td>
              <td>
                <button class="btn-sm btn-edit" onclick="window.location='admin-edit-user.html'">Edit</button>
                <button class="btn-sm btn-disable" onclick="openDisable('U00002','Richard Lionheart','Student')">Disable</button>
                <button class="btn-sm btn-delete" onclick="openDelete('U00002','Richard Lionheart','Student')">Delete</button>
              </td>
            </tr>
            <tr>
              <td>U00003</td><td>Naoya Inoue</td><td>naoya99</td>
              <td><span class="badge badge-student">Student</span></td>
              <td><span class="badge badge-disabled">Disabled</span></td>
              <td>
                <button class="btn-sm btn-edit" onclick="window.location='admin-edit-user.html'">Edit</button>
                <button class="btn-sm btn-enable">Enable</button>
                <button class="btn-sm btn-delete" onclick="openDelete('U00003','Naoya Inoue','Student')">Delete</button>
              </td>
            </tr>
            <tr>
              <td>U00004</td><td>Thia Gonzales</td><td>thia_g</td>
              <td><span class="badge badge-student">Student</span></td>
              <td><span class="badge badge-active">Active</span></td>
              <td>
                <button class="btn-sm btn-edit" onclick="window.location='admin-edit-user.html'">Edit</button>
                <button class="btn-sm btn-disable" onclick="openDisable('U00004','Thia Gonzales','Student')">Disable</button>
                <button class="btn-sm btn-delete" onclick="openDelete('U00004','Thia Gonzales','Student')">Delete</button>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>

    <!-- BOTTOM BAR -->
    <div class="bottom-bar">
      <span class="total-label">Showing 4 of 124 users</span>
      <asp:Button runat="server" CssClass="btn btn-green" Text="+ Add New User" OnClick="Unnamed5_Click" />
    </div>

<!-- DISABLE MODAL -->
<div class="modal-overlay-bg" id="disableModal">
  <div class="modal">
    <div class="modal-icon">🚫</div>
    <div class="modal-title">Disable User Account?</div>
    <div class="modal-subtitle">You are about to disable the following user:</div>
    <div class="modal-user">
      <div class="mu-name" id="disableName"></div>
      <div class="mu-id" id="disableId"></div>
    </div>
    <div class="warning-box warn">⚠️ This user will no longer be able to log in. Their data will be preserved and the account can be re-enabled at any time.</div>
    <div class="modal-btn-row">
        <asp:Button runat="server" CssClass="btn btn-outline" Text="Cancel" />
        <asp:Button runat="server" CssClass="btn btn-orange" Text="Disable" />
    </div>
  </div>
</div>

<!-- DELETE MODAL -->
<div class="modal-overlay-bg" id="deleteModal">
  <div class="modal">
    <div class="modal-icon">🗑️</div>
    <div class="modal-title">Delete User Account?</div>
    <div class="modal-subtitle">You are about to permanently delete:</div>
    <div class="modal-user">
      <div class="mu-name" id="deleteName"></div>
      <div class="mu-id" id="deleteId"></div>
    </div>
    <div class="warning-box danger">⛔ This action is permanent and cannot be undone. All data associated with this user will be removed from the system.</div>
    <div class="modal-btn-row">
        <asp:Button runat="server" CssClass="btn btn-outline" Text="Cancel" />
        <asp:Button runat="server" CssClass="btn btn-red" Text="Delete" />
    </div>
  </div>
</div>

<script>
  function openDisable(id, name, role) {
    document.getElementById('disableName').textContent = name;
    document.getElementById('disableId').textContent = 'ID: ' + id + ' · ' + role;
    document.getElementById('disableModal').classList.add('show');
  }
  function openDelete(id, name, role) {
    document.getElementById('deleteName').textContent = name;
    document.getElementById('deleteId').textContent = 'ID: ' + id + ' · ' + role;
    document.getElementById('deleteModal').classList.add('show');
  }
  function closeModal(id) {
    document.getElementById(id).classList.remove('show');
  }
</script>
</asp:Content>
