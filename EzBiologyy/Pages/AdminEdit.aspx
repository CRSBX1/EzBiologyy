<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminEdit.aspx.cs" Inherits="EzBiologyy.Pages.AdminEdit" MasterPageFile="~/Admin.Master"%>


<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
<style>
    .hero { display: flex; align-items: center; justify-content: space-between; }
    .hero-left h1 { color: #fff; font-size: 26px; font-weight: 700; font-family: 'Inter', sans-serif; }
    .hero-left p  { color: rgba(255,255,255,0.85); font-size: 14px; font-family: 'Inter', sans-serif; margin-top: 4px; }
    .user-badge { background: #3D6B4F; border-radius: 10px; padding: 12px 20px; text-align: center; }
    .user-badge .uid   { font-size: 11px; color: rgba(255,255,255,0.7); font-family: 'Inter', sans-serif; margin-bottom: 2px; }
    .user-badge .uname { font-size: 15px; font-weight: 700; color: #fff; font-family: 'Inter', sans-serif; }
    .user-badge .urole { display: inline-block; margin-top: 6px; background: #EAF3DE; color: #3B6D11; font-size: 11px; font-weight: 600; font-family: 'Inter', sans-serif; padding: 2px 10px; border-radius: 12px; }
</style>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="frame">
    <div style="background:#fff;">

    <!-- HERO -->
    <div class="hero">
        <div class="hero-left">
        <h1>Edit User</h1>
        <p>Update the details of an existing EzBiology user</p>
        </div>
        <div class="user-badge" style="margin-right:36px;">
        <div class="uid">User ID: U00002</div>
        <div class="uname">Richard Lionheart</div>
        <span class="urole">Student</span>
        </div>
    </div>

    <!-- CONTENT -->
    <div class="content">
        <div class="form-title">Edit User Details</div>

        <!-- Account Info -->
        <div class="section-label">Account Information</div>
        <div class="form-grid">
        <div class="field-group">
            <label>Full Name</label>
            <asp:TextBox runat="server" ID="fullName" CssClass="prefilled" />
        </div>
        <div class="field-group">
            <label>Username</label>
            <asp:TextBox runat="server" ID="username" CssClass="prefilled" />
            <span class="hint">Must be unique. No spaces allowed.</span>
        </div>
        <div class="field-group">
            <label>New Password</label>
            <asp:TextBox runat="server" ID="password" TextMode="Password" />
            <span class="hint">Only fill if you want to change the password.</span>
        </div>
        <div class="field-group">
            <label>Confirm New Password</label>
            <asp:TextBox runat="server" ID="confirmPassword" TextMode="Password" />
        </div>
        </div>

        <hr class="divider" />

        <!-- Personal Info -->
        <div class="section-label">Personal Information</div>
        <div class="form-grid">
        <div class="field-group">
            <label>Age</label>
            <asp:TextBox runat="server" TextMode="Number" CssClass="prefilled" ID="age" />
        </div>
        <div class="field-group">
            <label>Gender</label>
            <asp:DropDownList runat="server" ID="gender">
                <asp:ListItem>Male</asp:ListItem>
                <asp:ListItem>Female</asp:ListItem>
                <asp:ListItem>Prefer not to say</asp:ListItem>
            </asp:DropDownList>
        </div>
        </div>

        <hr class="divider" />

        <!-- Role -->
        <div class="section-label">Role</div>
        <div class="role-options">
        <div class="role-option selected" onclick="selectRole(this)">
            <div class="role-icon">🎓</div>
            <span>Student</span>
        </div>
        <div class="role-option" onclick="selectRole(this)">
            <div class="role-icon">📖</div>
            <span>Teacher</span>
        </div>
        <div class="role-option" onclick="selectRole(this)">
            <div class="role-icon">⚙️</div>
            <span>Administrator</span>
        </div>
        </div>

        <hr class="divider" />

        <!-- Status -->
        <div class="section-label">Account Status</div>
        <div class="status-row">
        <div class="status-option active-sel" onclick="selectStatus(this, 'active-sel')">
            <div style="font-size:20px; margin-bottom:6px;">✅</div>
            <span>Active</span>
        </div>
        <div class="status-option" onclick="selectStatus(this, 'disabled-sel')">
            <div style="font-size:20px; margin-bottom:6px;">🚫</div>
            <span style="color:#555;">Disabled</span>
        </div>
        </div>

        <!-- Error -->
        <div id="errorMsg" style="color:#E53935; font-size:13px; font-family:'Inter',sans-serif; margin-top:16px; text-align:center; display:none;"></div>

        <div class="btn-row">
        <button class="btn btn-red" onclick="window.location='admin-manage-users.html'">Cancel</button>
        <button class="btn btn-green" onclick="saveChanges()">Save Changes</button>
        </div>
    </div>

    </div>
</div>

<script>
    function selectRole(el) {
    document.querySelectorAll('.role-option').forEach(r => r.classList.remove('selected'));
    el.classList.add('selected');
    }

    function selectStatus(el, cls) {
    document.querySelectorAll('.status-option').forEach(s => {
        s.classList.remove('active-sel', 'disabled-sel');
        s.querySelector('span').style.color = '#555';
    });
    el.classList.add(cls);
    el.querySelector('span').style.color = cls === 'active-sel' ? '#3B6D11' : '#A32D2D';
    }

    function saveChanges() {
    const pass = document.getElementById('password').value;
    const conf = document.getElementById('confirmPassword').value;
    const err  = document.getElementById('errorMsg');
    if (pass && pass !== conf) {
        err.textContent = '⚠️ Passwords do not match. Please try again.';
        err.style.display = 'block'; return;
    }
    err.style.display = 'none';
    alert('Changes saved successfully! Redirecting to Manage Users...');
    window.location = 'admin-manage-users.html';
    }
</script>
</asp:Content>