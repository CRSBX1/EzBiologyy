<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="EzBiology.Pages.Home"%>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

  <div class="hero home-hero">
    <div class="hero-bg">
      <img src="https://images.unsplash.com/photo-1509062522246-3755977927d7?w=1400&q=80" alt="" />
      <div class="hero-overlay"></div>
    </div>
    <div class="hero-content">
      <h1>Hello, <asp:Label ID="labelTeacherName" runat="server" Text="" /></h1>
      <h2>Student Performances Overview</h2>
      <div class="perf-table-wrap">
        <asp:GridView ID="gvStudentPerformance" runat="server"
          CssClass="data-table perf-table"
          AutoGenerateColumns="false"
          GridLines="None"
          OnRowDataBound="gvStudentPerformance_RowDataBound">
          <Columns>
            <asp:BoundField DataField="StudentId" HeaderText="Student ID" />
            <asp:BoundField DataField="Name"      HeaderText="Student Name" />
            <asp:BoundField DataField="AvgQuiz"   HeaderText="Avg. Quiz Score" />
            <asp:BoundField DataField="AvgAssess" HeaderText="Avg. Assessment Score" />
          </Columns>
        </asp:GridView>
      </div>
    </div>
  </div>

  <div class="create-section">
    <h2>Create and Publish</h2>
    <div class="create-cards">
      <a class="create-card" href="~/Pages/Materials.aspx" runat="server">
        <img src="../Assets/studying.jpg" alt="Learning Materials" />
        <span>Learning<br />Materials</span>
      </a>
      <a class="create-card" href="~/Pages/Courses.aspx" runat="server">
        <img src="../Assets/users.jpeg" alt="Courses" />
        <span>Courses</span>
      </a>
    </div>
  </div>

</asp:Content>
