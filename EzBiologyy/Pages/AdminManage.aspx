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
                <asp:TextBox runat="server" TextMode="Search" CssClass="search-input" ID="searchInput" Text="e.g. U00001 / Richard Lionheart"/>
            </div>
            <div class="filter-wrap">
                <label>Role</label>
                <asp:DropDownList runat="server" ID="roleFilter" CssClass="filter-select">
                    <asp:ListItem>All Roles</asp:ListItem>
                    <asp:ListItem>Student</asp:ListItem>
                    <asp:ListItem>Teacher</asp:ListItem>
                    <asp:ListItem>Administrator</asp:ListItem>
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
            <asp:Button runat="server" CssClass="btn-search" Text="🔍 Search"/>
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
                <tbody id="userTable">
                <tr data-name="Herbert Smith" data-id="U00001" data-role="Teacher" data-status="Active">
                    <td>U00001</td><td>Herbert Smith</td><td>herbsmith</td><td>34</td><td>Male</td>
                    <td><span class="badge badge-teacher">Teacher</span></td>
                    <td><span class="badge badge-active">Active</span></td>
                    <td>
                    <button class="btn-sm btn-edit" onclick="window.location='admin-edit-user.html'">Edit</button>
                    <button class="btn-sm btn-disable" onclick="openDisable('U00001','Herbert Smith','Teacher')">Disable</button>
                    <button class="btn-sm btn-delete" onclick="openDelete('U00001','Herbert Smith','Teacher')">Delete</button>
                    </td>
                </tr>
                <tr data-name="Richard Lionheart" data-id="U00002" data-role="Student" data-status="Active">
                    <td>U00002</td><td>Richard Lionheart</td><td>richard04</td><td>16</td><td>Male</td>
                    <td><span class="badge badge-student">Student</span></td>
                    <td><span class="badge badge-active">Active</span></td>
                    <td>
                    <button class="btn-sm btn-edit" onclick="window.location='admin-edit-user.html'">Edit</button>
                    <button class="btn-sm btn-disable" onclick="openDisable('U00002','Richard Lionheart','Student')">Disable</button>
                    <button class="btn-sm btn-delete" onclick="openDelete('U00002','Richard Lionheart','Student')">Delete</button>
                    </td>
                </tr>
                <tr data-name="Naoya Inoue" data-id="U00003" data-role="Student" data-status="Disabled">
                    <td>U00003</td><td>Naoya Inoue</td><td>naoya99</td><td>15</td><td>Male</td>
                    <td><span class="badge badge-student">Student</span></td>
                    <td><span class="badge badge-disabled">Disabled</span></td>
                    <td>
                    <button class="btn-sm btn-edit" onclick="window.location='admin-edit-user.html'">Edit</button>
                    <button class="btn-sm btn-enable">Enable</button>
                    <button class="btn-sm btn-delete" onclick="openDelete('U00003','Naoya Inoue','Student')">Delete</button>
                    </td>
                </tr>
                <tr data-name="Thia Gonzales" data-id="U00004" data-role="Student" data-status="Active">
                    <td>U00004</td><td>Thia Gonzales</td><td>thia_g</td><td>15</td><td>Female</td>
                    <td><span class="badge badge-student">Student</span></td>
                    <td><span class="badge badge-active">Active</span></td>
                    <td>
                    <button class="btn-sm btn-edit" onclick="window.location='admin-edit-user.html'">Edit</button>
                    <button class="btn-sm btn-disable" onclick="openDisable('U00004','Thia Gonzales','Student')">Disable</button>
                    <button class="btn-sm btn-delete" onclick="openDelete('U00004','Thia Gonzales','Student')">Delete</button>
                    </td>
                </tr>
                <tr data-name="Saint Germaine" data-id="U00005" data-role="Student" data-status="Active">
                    <td>U00005</td><td>Saint Germaine</td><td>saintg</td><td>17</td><td>Female</td>
                    <td><span class="badge badge-student">Student</span></td>
                    <td><span class="badge badge-active">Active</span></td>
                    <td>
                    <button class="btn-sm btn-edit" onclick="window.location='admin-edit-user.html'">Edit</button>
                    <button class="btn-sm btn-disable" onclick="openDisable('U00005','Saint Germaine','Student')">Disable</button>
                    <button class="btn-sm btn-delete" onclick="openDelete('U00005','Saint Germaine','Student')">Delete</button>
                    </td>
                </tr>
                <tr data-name="Gilbert Cruz" data-id="U00006" data-role="Teacher" data-status="Active">
                    <td>U00006</td><td>Gilbert Cruz</td><td>gilcruz</td><td>32</td><td>Male</td>
                    <td><span class="badge badge-teacher">Teacher</span></td>
                    <td><span class="badge badge-active">Active</span></td>
                    <td>
                    <button class="btn-sm btn-edit" onclick="window.location='admin-edit-user.html'">Edit</button>
                    <button class="btn-sm btn-disable" onclick="openDisable('U00006','Gilbert Cruz','Teacher')">Disable</button>
                    <button class="btn-sm btn-delete" onclick="openDelete('U00006','Gilbert Cruz','Teacher')">Delete</button>
                    </td>
                </tr>
                <tr data-name="Alucard Tepes" data-id="U00007" data-role="Student" data-status="Disabled">
                    <td>U00007</td><td>Alucard Tepes</td><td>alucard_t</td><td>16</td><td>Male</td>
                    <td><span class="badge badge-student">Student</span></td>
                    <td><span class="badge badge-disabled">Disabled</span></td>
                    <td>
                    <button class="btn-sm btn-edit" onclick="window.location='admin-edit-user.html'">Edit</button>
                    <button class="btn-sm btn-enable">Enable</button>
                    <button class="btn-sm btn-delete" onclick="openDelete('U00007','Alucard Tepes','Student')">Delete</button>
                    </td>
                </tr>
                </tbody>
            </table>
            </div>

            <!-- PAGINATION -->
            <div class="pagination">
            <asp:Button runat="server" CssClass="page-btn" Text=&#8592; />
            <asp:Button runat="server" CssClass="page-btn active" Text="1" />
            <asp:Button runat="server" CssClass="page-btn" Text="2" />
            <asp:Button runat="server" CssClass="page-btn" Text="3" />
            <asp:Button runat="server" CssClass="page-btn" Text=&#8594; />
            </div>

        </div>

        <!-- BOTTOM BAR -->
        <div class="bottom-bar">
            <span class="total-label" id="totalLabel">Showing 7 of 124 users</span>
            <asp:Button runat="server" CssClass="btn btn-green" Text="+ Add New User" OnClick="Unnamed7_Click" />
        </div>

        </div>
    </div>

    <!-- DISABLE MODAL -->
    <div class="modal-overlay-bg" id="disableModal">
        <div class="modal">
        <div class="modal-icon">🚫 <div class="modal-title">Disable User Account?</div>
        <div class="modal-subtitle">You are about to disable the following user:</div>
        <div class="modal-user">
            <div class="mu-name" id="disableName"></div>
            <div class="mu-id" id="disableId"></div>
        </div>
        <div class="warning-box warn">⚠️ This user will no longer be able to log in. Their data will be preserved and the account can be re-enabled at any time.</div>
        <div class="modal-btn-row">
            <button class="btn btn-outline" onclick="closeModal('disableModal')">Cancel</button>
            <button class="btn btn-orange" onclick="closeModal('disableModal')">Disable</button>
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
            <button class="btn btn-outline" onclick="closeModal('deleteModal')">Cancel</button>
            <button class="btn btn-red" onclick="closeModal('deleteModal')">Delete</button>
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
        function closeModal(id) { document.getElementById(id).classList.remove('show'); }

        function filterTable() {
        const search = document.getElementById('searchInput').value.toLowerCase();
        const role   = document.getElementById('roleFilter').value;
        const status = document.getElementById('statusFilter').value;
        const rows   = document.querySelectorAll('#userTable tr');
        let visible  = 0;
        rows.forEach(row => {
            const name   = row.dataset.name   || '';
            const id     = row.dataset.id     || '';
            const rRole  = row.dataset.role   || '';
            const rStat  = row.dataset.status || '';
            const matchSearch = name.toLowerCase().includes(search) || id.toLowerCase().includes(search);
            const matchRole   = !role   || rRole   === role;
            const matchStatus = !status || rStat   === status;
            if (matchSearch && matchRole && matchStatus) { row.style.display = ''; visible++; }
            else { row.style.display = 'none'; }
        });
        document.getElementById('totalLabel').textContent = 'Showing ' + visible + ' of 124 users';
        }
    </script>
    </asp:Content>