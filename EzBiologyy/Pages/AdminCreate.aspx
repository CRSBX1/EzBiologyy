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
                  <asp:TextBox runat="server" ID="fullName"  />
                </div>
                <div class="field-group">
                  <label>Username</label>
                  <asp:TextBox runat="server" ID="username"  />
                  <span class="hint">Must be unique. No spaces allowed.</span>
                </div>
                <div class="field-group">
                  <label>Password</label>
                  <asp:TextBox runat="server" ID="password" TextMode="Password" />
                </div>
                <div class="field-group">
                  <label>Confirm Password</label>
                  <asp:TextBox runat="server" ID="confirmPassword" TextMode="Password" />
                </div>
              </div>

              <hr class="divider" />

              <!-- Personal Info -->
              <div class="section-label">Personal Information</div>
              <div class="form-grid">
                <div class="field-group">
                  <label>Age</label>
                  <asp:TextBox runat="server" ID="age" TextMode="Number" />
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
              <div class="section-label">Assign Role</div>
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

              <!-- Error message -->
              <div id="errorMsg" style="color:#E53935; font-size:13px; font-family:'Inter',sans-serif; margin-top:16px; text-align:center; display:none;"></div>

              <div class="btn-row">
                <button class="btn btn-red" onclick="clearForm()">Clear</button>
                <button class="btn btn-green" onclick="submitForm()">Create User</button>
              </div>
            </div>

          </div>
        </div>

        <script>
          function selectRole(el) {
            document.querySelectorAll('.role-option').forEach(r => r.classList.remove('selected'));
            el.classList.add('selected');
          }

          function clearForm() {
            document.getElementById('fullName').value = '';
            document.getElementById('username').value = '';
            document.getElementById('password').value = '';
            document.getElementById('confirmPassword').value = '';
            document.getElementById('age').value = '';
            document.getElementById('gender').value = '';
            document.getElementById('errorMsg').style.display = 'none';
            document.querySelectorAll('.role-option').forEach((r,i) => {
              r.classList.toggle('selected', i === 0);
            });
          }

          function submitForm() {
            const name = document.getElementById('fullName').value.trim();
            const user = document.getElementById('username').value.trim();
            const pass = document.getElementById('password').value;
            const conf = document.getElementById('confirmPassword').value;
            const age  = document.getElementById('age').value;
            const gen  = document.getElementById('gender').value;
            const err  = document.getElementById('errorMsg');

            if (!name || !user || !pass || !conf || !age || !gen) {
              err.textContent = '⚠️ Please fill in all fields before creating a user.';
              err.style.display = 'block'; return;
            }
            if (pass !== conf) {
              err.textContent = '⚠️ Passwords do not match. Please try again.';
              err.style.display = 'block'; return;
            }
            err.style.display = 'none';
            alert('User created successfully! Redirecting to Manage Users...');
            window.location = 'admin-manage-users.html';
          }
        </script>
    </asp:Content>
