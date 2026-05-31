<%@ Page Title="Materials" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Materials.aspx.cs" Inherits="EzBiology.Pages.Materials" ValidateRequest="false" Async="true"%>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="https://cdn.quilljs.com/1.3.7/quill.snow.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="hero materials-hero">
    <div class="hero-bg">
      <img src="https://images.unsplash.com/photo-1524178232363-1fb2b075b655?w=1400&q=80" alt="" />
      <div class="hero-overlay"></div>
    </div>
    <div class="hero-content">
      <h1>Learning Materials</h1>
      <p class="sub">Publish Knowledge</p>
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

    <div style="margin-bottom:20px;">
      <label class="field-label">Material Category</label>
      <asp:TextBox ID="txtCategory" runat="server" CssClass="field-input"
        placeholder="Neuroscience" style="max-width:340px;" />
    </div>

    <div>
      <label class="field-label">Content</label>
      <div id="quillEditor" style="height:300px; border-radius: 0 0 8px 8px;"></div>
      <asp:HiddenField ID="hfRteContent" runat="server" />
    </div>

    <div class="materials-action-row">
      <asp:Button ID="btnContentClear"  runat="server" Text="Clear"
        CssClass="btn btn-red"   OnClick="btnContentClear_Click" />
      <asp:Button ID="btnContentUpload" runat="server" Text="Upload"
        CssClass="btn btn-green" OnClick="btnContentUpload_Click" />
    </div>

    <div style="margin-top: 5%">
        <h2>Generate your learning material content with AI</h2>
        <label class="field-label">Prompt</label>
        <asp:TextBox ID="aiPromptTxt" runat="server" TextMode="MultiLine" CssClass="aiPromptTxt"></asp:TextBox>
        <div class="materials-action-row">
            <asp:Button runat="server" ID="generateBtn" Text="Generate Content" CssClass="btn btn-green"
                OnClick="generateBtn_Click" style="margin-top:12px;" />
        </div>
    </div>
  </div>

    <script src="https://cdn.quilljs.com/1.3.7/quill.min.js"></script>
    <script>
        var quill = new Quill('#quillEditor', {
            theme: 'snow',
            modules: {
                toolbar: [
                    ['bold', 'italic', 'underline'],
                    [{ 'align': [] }],
                    [{ 'list': 'ordered' }, { 'list': 'bullet' }],
                    [{ 'header': [1, 2, 3, false] }],
                    ['clean']
                ]
            }
        });

        // Restore content if PostBack brought data back
        var hf = document.getElementById('<%= hfRteContent.ClientID %>');
        if (hf && hf.value) {
            quill.root.innerHTML = hf.value;
        }

        // Sync Quill HTML into hidden field before every PostBack
        function syncRTE() {
            if (hf) hf.value = quill.root.innerHTML;
        }

        // Also wire the Upload button's OnClientClick
        var uploadBtn = document.getElementById('<%= btnContentUpload.ClientID %>');
        if (uploadBtn) uploadBtn.addEventListener('click', syncRTE);

        var clearBtn = document.getElementById('<%= btnContentClear.ClientID %>');
        if (clearBtn) clearBtn.addEventListener('click', function () {
            quill.setText('');
        });
    </script>
</asp:Content>
