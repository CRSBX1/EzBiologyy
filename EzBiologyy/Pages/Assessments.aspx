<%@ Page Title="Assessments" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Assessments.aspx.cs" Inherits="EzBiology.Pages.Assessments" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="hero assess-hero hero-solid">
    <div class="hero-content">
      <h1><asp:Literal ID="litPageTitle" runat="server" Text="Create Quiz" /></h1>
      <p class="sub">Build tests and track student progress</p>
      <div class="type-tabs">
        <asp:Button ID="btnTabQuiz"   runat="server" Text="Quiz"
          CssClass="tab-btn active-tab"   OnClick="btnTabQuiz_Click" />
        <asp:Button ID="btnTabAssess" runat="server" Text="Assessment"
          CssClass="tab-btn inactive-tab" OnClick="btnTabAssess_Click" />
      </div>
    </div>
  </div>

  <div class="assess-form">
    <asp:Label ID="lblMessage" runat="server" ForeColor="Green"
      style="display:block;margin-bottom:16px;font-weight:600;" />

    <div class="title-field">
      <asp:Label ID="lblTitleField" runat="server" CssClass="field-label"
        Text="Quiz Title" AssociatedControlID="txtTitle" />
      <asp:TextBox ID="txtTitle" runat="server" CssClass="field-input"
        placeholder="All about biology" />
    </div>

    <div class="questions-section">
      <h2>Questions</h2>
      <asp:Repeater ID="rptQuestions" runat="server"
        OnItemDataBound="rptQuestions_ItemDataBound">
        <ItemTemplate>
          <div class="question-card">
            <h3>Question <%# ((EzBiology.Pages.QuestionModel)Container.DataItem).Number %></h3>

            <div class="question-field-row">
              <label class="field-label">Question Type</label>
              <asp:DropDownList ID="ddlType" runat="server" CssClass="field-select">
                <asp:ListItem Text="Multiple Choice" Value="mc" />
                <asp:ListItem Text="Short Answer"    Value="sa" />
                <asp:ListItem Text="True / False"    Value="tf" />
              </asp:DropDownList>
            </div>

            <div class="question-field-row">
              <label class="field-label">Question Text</label>
              <asp:TextBox ID="txtQuestion" runat="server" CssClass="field-textarea"
                TextMode="MultiLine" Rows="3"
                Text='<%# ((EzBiology.Pages.QuestionModel)Container.DataItem).Text %>' />
            </div>

            <div class="answer-options">
              <label class="field-label">Answer Options</label>
              <asp:Repeater ID="rptOptions" runat="server">
                <ItemTemplate>
                  <div class="option-row">
                    <span class="option-label"><%# ((EzBiology.Pages.AnswerOption)Container.DataItem).Label %>.</span>
                    <asp:TextBox runat="server" CssClass="field-input"
                      Text='<%# ((EzBiology.Pages.AnswerOption)Container.DataItem).Text %>' />
                  </div>
                </ItemTemplate>
              </asp:Repeater>
            </div>

            <div>
              <label class="field-label">Correct Answer</label>
              <asp:DropDownList ID="ddlCorrect" runat="server"
                CssClass="field-select" style="max-width:180px;">
                <asp:ListItem Text="Option A" Value="A" />
                <asp:ListItem Text="Option B" Value="B" />
                <asp:ListItem Text="Option C" Value="C" />
                <asp:ListItem Text="Option D" Value="D" />
              </asp:DropDownList>
            </div>
          </div>
        </ItemTemplate>
      </asp:Repeater>
    </div>

    <asp:Button ID="btnAddQuestion" runat="server" Text="+ Add Question"
      CssClass="add-question-btn" OnClick="btnAddQuestion_Click" />

    <div class="assess-action-row">
      <asp:Button ID="btnClear"   runat="server" Text="Clear"
        CssClass="btn btn-red"   OnClick="btnClear_Click" />
      <asp:Button ID="btnPublish" runat="server" Text="Publish"
        CssClass="btn btn-green" OnClick="btnPublish_Click" />
    </div>
  </div>
</asp:Content>
