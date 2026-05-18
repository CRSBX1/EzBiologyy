<%@ Page Title="Courses" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Courses.aspx.cs" Inherits="EzBiology.Pages.Courses" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="hero">
    <div class="hero-bg">
      <img src="https://images.unsplash.com/photo-1580582932707-520aed937b7b?w=1400&q=80" alt="" />
      <div class="hero-overlay"></div>
    </div>
    <div class="hero-content">
      <div class="courses-hero-header">
        <h1>Create New Courses</h1>
        <asp:Button ID="btnHeroClear" runat="server" Text="Clear"
          CssClass="btn btn-red" OnClick="btnClear_Click" />
      </div>
      <div class="course-panels">
        <div class="course-panel">
          <h3>Course Materials</h3>
          <asp:Repeater ID="rptCourseMaterials" runat="server">
            <ItemTemplate><p><%# Container.DataItem %></p></ItemTemplate>
          </asp:Repeater>
        </div>
        <div class="course-panel">
          <h3>Quizzes and Assessments</h3>
          <asp:Literal ID="litQA" runat="server" />
        </div>
      </div>
      <div class="create-row">
        <asp:Button ID="btnCreate" runat="server" Text="Create"
          CssClass="btn btn-green" OnClick="btnCreate_Click" />
      </div>
    </div>
  </div>

  <div class="add-components">
    <h2>Add Course Components</h2>
    <asp:Label ID="lblMessage" runat="server" ForeColor="Green"
      style="display:block;margin-bottom:12px;font-weight:600;" />
    <div class="fields-row">
      <div class="field-group">
        <label class="field-label">ID or Name</label>
        <asp:TextBox ID="txtSearch" runat="server" CssClass="field-input field-id"
          placeholder="LM0023/Characteristics of living organisms" />
      </div>
      <div class="field-group">
        <label class="field-label">Category</label>
        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="field-select field-cat">
          <asp:ListItem Text="Course Materials" Value="CourseMaterials" />
          <asp:ListItem Text="Quiz"             Value="Quiz" />
          <asp:ListItem Text="Assessment"       Value="Assessment" />
        </asp:DropDownList>
      </div>
      <asp:Button ID="btnSearch" runat="server" Text="" CssClass="btn-search"
        OnClick="btnSearch_Click" ToolTip="Search" />
    </div>
    <div class="comp-table-wrap">
      <asp:GridView ID="gvComponents" runat="server"
        CssClass="data-table comp-table"
        AutoGenerateColumns="false"
        GridLines="None"
        OnRowDataBound="gvComponents_RowDataBound"
        DataKeyNames="Id">
        <Columns>
          <asp:BoundField DataField="Id"   HeaderText="ID" />
          <asp:BoundField DataField="Name" HeaderText="Name" />
        </Columns>
      </asp:GridView>
    </div>
    <div class="action-row">
      <asp:Button ID="btnClear" runat="server" Text="Clear"
        CssClass="btn btn-red"   OnClick="btnClear_Click" />
      <asp:Button ID="btnAdd"   runat="server" Text="Add"
        CssClass="btn btn-green" OnClick="btnAdd_Click" />
    </div>
  </div>
</asp:Content>
