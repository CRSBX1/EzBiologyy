<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminProfile.aspx.cs" Inherits="EzBiologyy.Pages.AdminProfile" MasterPageFile="~/Admin.Master"%>


<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server" >
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="frame">
<div style="background:#fff;">

        <!-- HERO -->
        <div class="hero">
            <h1>Profile Credentials</h1>
            <p>Update your administrator account details</p>
        </div>

        <!-- CONTENT -->
        <div class="content" style="max-width: 560px; margin: 0 auto;">
            <div class="form-title">Admin Profile</div>

            <asp:Label ID="lblMessage" runat="server"
            style="display:block;text-align:center;margin-bottom:20px;font-weight:600;" />
            <div class="field-group" style="margin-bottom:20px;">
            <label>Full Name</label>
            <asp:TextBox runat="server" ID="fullName" />
            </div>

            <div class="field-group" style="margin-bottom:20px;">
            <label>Username</label>
            <asp:TextBox runat="server" ID="username" />
            </div>

            <div class="field-group" style="margin-bottom:20px;">
            <label>New Password</label>
            <asp:TextBox runat="server" ID="password" TextMode="Password"/>
            <span class="hint">Only fill if you want to change the password.</span>
            </div>

            <div class="field-group" style="margin-bottom:20px;">
            <label>Confirm New Password</label>
            <asp:TextBox runat="server" ID="confirmPassword" TextMode="Password"/>
            </div>

            <div style="display:grid; grid-template-columns:1fr 1fr; gap: 20px 28px; margin-bottom:20px;">
            <div class="field-group">
                <label>Age</label>
                <asp:TextBox runat="server" ID="age" TextMode="Number" />
            </div>
            <div class="field-group">
                <label>Gender</label>
                <asp:DropDownList runat="server" ID="gender">
                    <asp:ListItem Text="Male" Value="Male" />
                    <asp:ListItem Text="Female" Value="Female" />
                    <asp:ListItem Text="Prefer not to say" Value="Other" />
                </asp:DropDownList>
            </div>
            <div class="field-group">
                <label>Change Password?</label>
               <asp:DropDownList ID="ddlPwd" runat="server">
                  <asp:ListItem Text="Yes"   Value="Yes" />
                  <asp:ListItem Text="No" Value="No" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlPwd"
                    ErrorMessage="This field is required" ForeColor="Red" Display="Dynamic" />
            </div>
            </div>

            <div class="btn-row">
            <asp:Button runat="server" Text="Cancel" CssClass="btn btn-red" OnClick="Unnamed2_Click" />
            <asp:Button runat="server" Text="Save" CssClass="btn btn-green" OnClick="Unnamed3_Click" />
            </div>
        </div>

        </div>
    </div>
</asp:Content>
