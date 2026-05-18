<%@ Page Title="Sign Up" Language="C#" MasterPageFile="~/Auth.master" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="EzBiology.Pages.Signup" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

  <div class="auth-page">

    <!-- ── Left: DNA hero ── -->
    <div class="auth-hero">
      <div class="auth-hero-bg">
        <img src="https://www.figma.com/api/mcp/asset/b9a5048a-e375-4368-9a11-0283f1f52398" alt="" />
        <div class="auth-hero-overlay"></div>
      </div>
      <div class="auth-hero-text">
        <h1>Sign Up</h1>
        <p>Start your<br />learning/teaching journey!</p>
      </div>
    </div>

    <!-- ── Right: form ── -->
    <div class="auth-form-panel">

      <asp:Label ID="errMessage" runat="server" CssClass="auth-message" Visible="false" ForeColor="Red" Text="Another account with the same name exists, please change your username" />

      <!-- Full Name -->
      <div class="auth-field">
        <label for="txtFullName">Full Name</label>
        <asp:TextBox ID="txtFullName" runat="server" placeholder="John Doe" />
        <asp:RequiredFieldValidator runat="server"
          ControlToValidate="txtFullName"
          ErrorMessage="Full name is required."
          CssClass="auth-validator" Display="Dynamic" />
      </div>

      <!-- Username -->
      <div class="auth-field">
        <label for="txtUsername">Username</label>
        <asp:TextBox ID="txtUsername" runat="server" placeholder="JohnDoe7489" />
        <asp:RequiredFieldValidator runat="server"
          ControlToValidate="txtUsername"
          ErrorMessage="Username is required."
          CssClass="auth-validator" Display="Dynamic" />
      </div>

        <div class="auth-field">
          <label for="rolePick">Role</label>
          <asp:DropDownList ID="rolePick" runat="server" CssClass="auth-select">
              <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>
              <asp:ListItem Text="Student" Value="Student"></asp:ListItem>
              <asp:ListItem Text="Teacher" Value="Teacher"></asp:ListItem>
          </asp:DropDownList>
          <asp:RequiredFieldValidator runat="server"
            ControlToValidate="rolePick"
            ErrorMessage="Role is required."
            CssClass="auth-validator" Display="Dynamic" />
        </div>

      <!-- Password -->
      <div class="auth-field">
        <label for="txtPassword">Password</label>
        <asp:TextBox ID="txtPassword" runat="server"
          TextMode="Password" placeholder="" />
        <asp:RequiredFieldValidator runat="server"
          ControlToValidate="txtPassword"
          ErrorMessage="Password is required."
          CssClass="auth-validator" Display="Dynamic" />
      </div>

      <!-- Confirm Password -->
      <div class="auth-field">
        <label for="txtConfirmPassword">Confirm Password</label>
        <asp:TextBox ID="txtConfirmPassword" runat="server"
          TextMode="Password" placeholder="" />
        <asp:RequiredFieldValidator runat="server"
          ControlToValidate="txtConfirmPassword"
          ErrorMessage="Please confirm your password."
          CssClass="auth-validator" Display="Dynamic" />
        <asp:CompareValidator runat="server"
          ControlToValidate="txtConfirmPassword"
          ControlToCompare="txtPassword"
          ErrorMessage="Passwords do not match."
          CssClass="auth-validator" Display="Dynamic" />
      </div>

      <div class="auth-btns">
        <asp:Button ID="btnSubmit" runat="server" Text="Submit"
          CssClass="btn btn-green" OnClick="btnSubmit_Click" />
        <asp:Button ID="btnClear"   runat="server" Text="Clear"
          CssClass="btn btn-red"   OnClick="btnClear_Click"
          CausesValidation="false" />
      </div>

    </div>
  </div>

</asp:Content>
