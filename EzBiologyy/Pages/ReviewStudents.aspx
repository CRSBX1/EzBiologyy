<%@ Page Title="Review Students" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ReviewStudents.aspx.cs" Inherits="EzBiology.Pages.ReviewStudents" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="hero review-hero">
    <div class="hero-bg">
      <img src="https://images.unsplash.com/photo-1522202176988-66273c2fd55f?w=1400&q=80" alt="" />
      <div class="hero-overlay"></div>
    </div>
    <div class="hero-content">
      <h1>Review Student Performance</h1>
      <p class="sub">To see if they're studying properly</p>
      <div class="student-selector">
        <label>Select Student:</label>
        <asp:DropDownList ID="ddlStudent" runat="server"
          AutoPostBack="true"
          OnSelectedIndexChanged="ddlStudent_SelectedIndexChanged" />
      </div>
      <div class="score-cards">
        <div class="score-card">
          <div class="score-val grade"><asp:Literal ID="litGrade"     runat="server" Text="A+" /></div>
          <div class="score-label">Overall Grade</div>
        </div>
        <div class="score-card">
          <div class="score-val"><asp:Literal ID="litAvgQuiz"   runat="server" Text="0" /></div>
          <div class="score-label">Average Quiz Score</div>
        </div>
        <div class="score-card">
          <div class="score-val"><asp:Literal ID="litAvgAssess" runat="server" Text="0" /></div>
          <div class="score-label">Average Assessment Score</div>
        </div>
      </div>
    </div>
  </div>

  <div class="grades-section">
    <h2>Student Grades</h2>
    <div class="category-row">
      <label>Category:</label>
      <asp:DropDownList ID="ddlCategory" runat="server"
        AutoPostBack="true"
        OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
        <asp:ListItem Text="Quiz"       Value="quiz" />
        <asp:ListItem Text="Assessment" Value="assess" />
      </asp:DropDownList>
    </div>
    <div class="grades-table-wrap">
      <asp:GridView ID="gvGrades" runat="server"
        CssClass="data-table" AutoGenerateColumns="false" GridLines="None">
        <Columns>
          <asp:BoundField DataField="Name"        HeaderText="Name" />
          <asp:BoundField DataField="Grade"       HeaderText="Grade" />
          <asp:BoundField DataField="GradePoints" HeaderText="Grade Points" />
        </Columns>
      </asp:GridView>
    </div>
  </div>
</asp:Content>
