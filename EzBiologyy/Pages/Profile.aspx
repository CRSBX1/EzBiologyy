<%@ Page Title="Profile" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="EzBiology.Pages.Profile" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="profile-page">
    <h1>Profile Credentials</h1>

    <asp:Label ID="lblMessage" runat="server"
      style="display:block;text-align:center;margin-bottom:20px;font-weight:600;" />

    <div class="profile-field">
      <label class="field-label">Username</label>
      <asp:TextBox ID="txtUsername" runat="server" CssClass="field-input" placeholder="JohnDoe7489" />
      <asp:RequiredFieldValidator runat="server" ControlToValidate="txtUsername"
        ErrorMessage="Username is required." ForeColor="Red" Display="Dynamic" />
    </div>

    <div class="profile-field">
      <label class="field-label">Password</label>
      <asp:TextBox ID="txtPassword" runat="server" CssClass="field-input"
        TextMode="Password" placeholder="Enter new password" />
    </div>

    <div class="inline-fields">
      <div class="field-group">
        <label class="field-label">Age</label>
        <asp:TextBox ID="txtAge" runat="server" CssClass="age-input" TextMode="Number" />
        <asp:RangeValidator runat="server" ControlToValidate="txtAge"
          MinimumValue="18" MaximumValue="100" Type="Integer"
          ErrorMessage="Age must be 18–100." ForeColor="Red" Display="Dynamic" />
      </div>
      <div class="field-group">
        <label class="field-label">Gender</label>
        <asp:DropDownList ID="ddlGender" runat="server" CssClass="gender-select">
          <asp:ListItem Text="Male"   Value="Male" />
          <asp:ListItem Text="Female" Value="Female" />
        </asp:DropDownList>
      </div>
        <div class="field-group">
            <label class="field-label">Change Password?</label>
           <asp:DropDownList ID="ddlPwd" runat="server" CssClass="gender-select">
              <asp:ListItem Text="Yes"   Value="Yes" />
              <asp:ListItem Text="No" Value="No" />
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlPwd"
                ErrorMessage="This field is required" ForeColor="Red" Display="Dynamic" />
        </div>
    </div>

    <div class="profile-btns">
      <asp:Button ID="btnCancel" runat="server" Text="Cancel"
        CssClass="btn btn-red"   OnClick="btnCancel_Click" CausesValidation="false" />
      <asp:Button ID="btnSave"   runat="server" Text="Save"
        CssClass="btn btn-green" OnClick="btnSave_Click" />
    </div>
  </div>
</asp:Content>
