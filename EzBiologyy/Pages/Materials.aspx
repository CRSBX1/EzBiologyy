<%@ Page Title="Materials" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Materials.aspx.cs" Inherits="EzBiology.Pages.Materials" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="hero materials-hero">
    <div class="hero-bg">
      <img src="https://images.unsplash.com/photo-1524178232363-1fb2b075b655?w=1400&q=80" alt="" />
      <div class="hero-overlay"></div>
    </div>
    <div class="hero-content">
      <h1>Learning Materials</h1>
      <p class="sub">Create and Publish Knowledge</p>
      <div class="upload-zone">
        <p id="uploadPlaceholder" runat="server">Attach your files here</p>
        <asp:FileUpload ID="fuMaterial" runat="server" />
      </div>
      <div class="upload-btns">
        <asp:Button ID="btnFileClear"  runat="server" Text="Clear"
          CssClass="btn btn-red"   OnClick="btnFileClear_Click" />
        <asp:Button ID="btnFileUpload" runat="server" Text="Upload"
          CssClass="btn btn-green" OnClick="btnFileUpload_Click" />
      </div>
    </div>
  </div>

  <div class="or-divider">Or..</div>

  <div class="create-new-section">
    <h2>Create Something New</h2>
    <asp:Label ID="lblMessage" runat="server" ForeColor="Green"
      style="display:block;text-align:center;margin-bottom:16px;font-weight:600;" />

    <div style="margin-bottom:20px;">
      <label class="field-label">Learning Material Name</label>
      <asp:TextBox ID="txtMaterialName" runat="server" CssClass="field-input"
        placeholder="Neuroscience overview" style="max-width:340px;" />
    </div>

    <div>
      <label class="field-label">Content</label>
      <div class="rte-toolbar">
        <button type="button" class="rte-btn bold"      onclick="document.execCommand('bold')">B</button>
        <button type="button" class="rte-btn italic"    onclick="document.execCommand('italic')">I</button>
        <button type="button" class="rte-btn underline" onclick="document.execCommand('underline')">U</button>
        <div class="rte-divider"></div>
        <button type="button" class="rte-btn" onclick="document.execCommand('justifyLeft')">&#8676;</button>
        <button type="button" class="rte-btn" onclick="document.execCommand('justifyCenter')">&#8677;</button>
        <button type="button" class="rte-btn" onclick="document.execCommand('justifyFull')">&#8644;</button>
        <div class="rte-divider"></div>
        <button type="button" class="rte-btn" onclick="document.execCommand('insertUnorderedList')">&#8226;&#8801;</button>
        <button type="button" class="rte-btn" onclick="document.execCommand('insertOrderedList')">1&#8801;</button>
      </div>
      <div id="rteContent" class="rte-area" contenteditable="true" oninput="syncRTE()"></div>
      <asp:HiddenField ID="hfRteContent" runat="server" />
    </div>

    <div class="materials-action-row">
      <asp:Button ID="btnContentClear"  runat="server" Text="Clear"
        CssClass="btn btn-red"   OnClick="btnContentClear_Click" />
      <asp:Button ID="btnContentUpload" runat="server" Text="Upload"
        CssClass="btn btn-green" OnClick="btnContentUpload_Click"
        OnClientClick="syncRTE()" />
    </div>
  </div>

  <script>
    function syncRTE() {
      var hf = document.getElementById('<%= hfRteContent.ClientID %>');
      var ed = document.getElementById('rteContent');
      if (hf && ed) hf.value = ed.innerHTML;
    }
  </script>
</asp:Content>
