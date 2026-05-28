<%@ Page Language="C#" AutoEventWireup="true"
CodeBehind="StudentDashboard.aspx.cs"
Inherits="EzBiologyy.Pages.StudentDashboard"
MasterPageFile="~/Student.Master"%>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<div class="content">

    <h1>
        Welcome back!
        <asp:Label ID="nameLbl" runat="server"></asp:Label>
    </h1>

    <p>Continue your learning journey.</p>

    <div style="margin-top:25px;">
        <h3>
            Enrolled Courses:
            <asp:Label ID="courseCountLbl" runat="server"></asp:Label>
        </h3>
    </div>

    <h3 style="margin-top:40px;">
        My Enrolled Courses
    </h3>

    <div class="courses">

        <asp:Repeater ID="courseRepeater" runat="server">

<ItemTemplate>

<div class="course-card">

<img src='<%# "../images/img" + ((Container.ItemIndex % 3) + 1) + ".png" %>' class="course-img"/>

<div class="course-bottom">

<h3>
<%# Eval("CourseName") %>
</h3>

<span class='<%# Eval("Status").ToString()=="Completed" ? "status completed" : "status in-progress" %>'>

<%# Eval("Status") %>

</span>

</div>

</div>

</ItemTemplate>

</asp:Repeater>

    </div>

</div>

</asp:Content>