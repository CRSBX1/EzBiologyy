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
        <div class="uid">User ID: <asp:Label runat="server" ID="lblUserId" /></div>
        <div class="uname"><asp:Label runat="server" ID="lblUserName" /></div>
        <span class="urole"><asp:Label runat="server" ID="lblUserRole" /></span>
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
            <asp:TextBox runat="server" ID="fullName" CssClass="prefilled" ClientIDMode="Static" />
        </div>
        <div class="field-group">
            <label>Username</label>
            <asp:TextBox runat="server" ID="username" CssClass="prefilled" ClientIDMode="Static" />
            <span class="hint">Must be unique. No spaces allowed.</span>
        </div>
        <div class="field-group">
            <label>New Password</label>
            <asp:TextBox runat="server" ID="password" TextMode="Password" ClientIDMode="Static" />
            <span class="hint">Only fill if you want to change the password.</span>
        </div>
        <div class="field-group">
            <label>Confirm New Password</label>
            <asp:TextBox runat="server" ID="confirmPassword" TextMode="Password" ClientIDMode="Static" />
        </div>
        </div>

        <hr class="divider" />

        <!-- Personal Info -->
        <div class="section-label">Personal Information</div>
        <div class="form-grid">
        <div class="field-group">
            <label>Age</label>
            <asp:TextBox runat="server" TextMode="Number" CssClass="prefilled" ID="age" ClientIDMode="Static" />
        </div>
        <div class="field-group">
            <label>Gender</label>
            <asp:DropDownList runat="server" ID="gender" ClientIDMode="Static">
                <asp:ListItem Text="Male" Value="Male" />
                <asp:ListItem Text="Female" Value="Female" />
                <asp:ListItem Text="Prefer not to say" Value="Other" />
            </asp:DropDownList>
        </div>
        </div>

        <hr class="divider" />

        <!-- Role -->
        <div class="section-label">Role</div>
        <div class="role-options">
        <div class="role-option" data-role="Student" onclick="selectRole(this)">
            <div class="role-icon">🎓</div>
            <span>Student</span>
        </div>
        <div class="role-option" data-role="Teacher" onclick="selectRole(this)">
            <div class="role-icon">📖</div>
            <span>Teacher</span>
        </div>
        <div class="role-option" data-role="Admin" onclick="selectRole(this)">
            <div class="role-icon">⚙️</div>
            <span>Administrator</span>
        </div>
        </div>
        <asp:HiddenField runat="server" ID="hfRole" ClientIDMode="Static" />

        <hr class="divider" />

        <!-- Status -->
        <div class="section-label">Account Status</div>
        <div class="status-row">
        <div class="status-option" data-active="1" onclick="selectStatus(this, 'active-sel')">
            <div style="font-size:20px; margin-bottom:6px;">✅</div>
            <span>Active</span>
        </div>
        <div class="status-option" data-active="0" onclick="selectStatus(this, 'disabled-sel')">
            <div style="font-size:20px; margin-bottom:6px;">🚫</div>
            <span>Disabled</span>
        </div>
        </div>
        <asp:HiddenField runat="server" ID="hfActive" ClientIDMode="Static" />

        <!-- Message -->
        <asp:Label runat="server" ID="lblMessage" ClientIDMode="Static"
            style="display:block; font-size:13px; font-family:'Inter',sans-serif; margin-top:16px; text-align:center;" />

        <div class="btn-row">
            <a href="AdminManage.aspx" class="btn btn-red" style="text-decoration:none;">Cancel</a>
            <asp:Button runat="server" ID="btnSave" CssClass="btn btn-green" Text="Save Changes" OnClick="btnSave_Click" />
        </div>
    </div>

    </div>
</div>

<script>
    function selectRole(el) {
        document.querySelectorAll('.role-option').forEach(r => r.classList.remove('selected'));
        el.classList.add('selected');
        document.getElementById('hfRole').value = el.getAttribute('data-role');
    }

    function selectStatus(el, cls) {
        document.querySelectorAll('.status-option').forEach(s => {
            s.classList.remove('active-sel', 'disabled-sel');
            s.querySelector('span').style.color = '#555';
        });
        el.classList.add(cls);
        el.querySelector('span').style.color = cls === 'active-sel' ? '#3B6D11' : '#A32D2D';
        document.getElementById('hfActive').value = el.getAttribute('data-active');
    }

    // Initial render: sync visuals to hidden field values that were populated server-side.
    document.addEventListener('DOMContentLoaded', function() {
        var role = document.getElementById('hfRole').value;
        document.querySelectorAll('.role-option').forEach(r => {
            r.classList.toggle('selected', r.getAttribute('data-role') === role);
        });

        var active = document.getElementById('hfActive').value;
        document.querySelectorAll('.status-option').forEach(s => {
            s.classList.remove('active-sel', 'disabled-sel');
            s.querySelector('span').style.color = '#555';
        });
        var sel = document.querySelector('.status-option[data-active="' + active + '"]');
        if (sel) {
            var cls = active === '1' ? 'active-sel' : 'disabled-sel';
            sel.classList.add(cls);
            sel.querySelector('span').style.color = active === '1' ? '#3B6D11' : '#A32D2D';
        }
    });
</script>
</asp:Content>
