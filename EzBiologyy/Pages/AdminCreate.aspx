<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminCreate.aspx.cs" Inherits="EzBiologyy.Pages.AdminCreate" MasterPageFile="~/Admin.Master"%>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
    <asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
        <div class="frame">
          <div style="background:#fff;">


            <!-- HERO -->
            <div class="hero">
              <h1>Create New User</h1>
              <p>Add a new student, teacher, or administrator to EzBiology</p>
            </div>

            <!-- CONTENT -->
            <div class="content">
              <div class="form-title">New User Details</div>

              <!-- Account Info -->
              <div class="section-label">Account Information</div>
              <div class="form-grid">
                <div class="field-group">
                  <label>Full Name</label>
                  <asp:TextBox runat="server" ID="fullName" ClientIDMode="Static" />
                </div>
                <div class="field-group">
                  <label>Username</label>
                  <asp:TextBox runat="server" ID="username" ClientIDMode="Static" />
                  <span class="hint">Must be unique. No spaces allowed.</span>
                </div>
                <div class="field-group">
                  <label>Password</label>
                  <asp:TextBox runat="server" ID="password" TextMode="Password" ClientIDMode="Static" />
                </div>
                <div class="field-group">
                  <label>Confirm Password</label>
                  <asp:TextBox runat="server" ID="confirmPassword" TextMode="Password" ClientIDMode="Static" />
                </div>
              </div>

              <hr class="divider" />

              <!-- Personal Info -->
              <div class="section-label">Personal Information</div>
              <div class="form-grid">
                <div class="field-group">
                  <label>Age</label>
                  <asp:TextBox runat="server" ID="age" TextMode="Number" ClientIDMode="Static" />
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
              <div class="section-label">Assign Role</div>
              <div class="role-options">
                <div class="role-option selected" data-role="Student" onclick="selectRole(this)">
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
              <asp:HiddenField runat="server" ID="hfRole" ClientIDMode="Static" Value="Student" />

              <!-- Message -->
              <asp:Label runat="server" ID="lblMessage" ClientIDMode="Static"
                style="display:block; font-size:13px; font-family:'Inter',sans-serif; margin-top:16px; text-align:center;" />

              <div class="btn-row">
                <button type="button" class="btn btn-red" onclick="clearForm()">Clear</button>
                <asp:Button runat="server" ID="btnCreate" CssClass="btn btn-green" Text="Create User" OnClick="btnCreate_Click" />
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

          function clearForm() {
            document.getElementById('fullName').value = '';
            document.getElementById('username').value = '';
            document.getElementById('password').value = '';
            document.getElementById('confirmPassword').value = '';
            document.getElementById('age').value = '';
            document.getElementById('gender').selectedIndex = 0;
            document.querySelectorAll('.role-option').forEach(r => {
              r.classList.toggle('selected', r.getAttribute('data-role') === 'Student');
            });
            document.getElementById('hfRole').value = 'Student';
            var msg = document.getElementById('lblMessage');
            if (msg) msg.innerHTML = '';
          }
        </script>
    </asp:Content>
