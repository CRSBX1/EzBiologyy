<%@ Page Title="Login" Language="C#" MasterPageFile="~/Auth.master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EzBiology.Pages.Login" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

  <div class="auth-page">

    <!-- ── Left: DNA hero ── -->
    <div class="auth-hero">
      <div class="auth-hero-bg">
        <img src="~/Assets/dna.jpeg" alt="" runat="server"/>
        <div class="auth-hero-overlay"></div>
      </div>
      <div class="auth-hero-text">
        <h1>Login</h1>
        <p>Continue your<br />learning/teaching journey!</p>
      </div>
    </div>

    <!-- ── Right: form ── -->
    <div class="auth-form-panel">

        <asp:Label Visible="false" ID="errorMessage" runat="server" ForeColor="Red" Text="Username or password is invalid, try again" />


      <!-- Username -->
      <div class="auth-field">
        <label for="txtUsername">Username</label>
        <asp:TextBox ID="txtUsername" runat="server" placeholder="JohnDoe7489" />
        <asp:RequiredFieldValidator runat="server"
          ControlToValidate="txtUsername"
          ErrorMessage="Username is required."
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
