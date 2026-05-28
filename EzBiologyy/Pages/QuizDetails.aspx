<%@ Page Language="C#" AutoEventWireup="true"
CodeBehind="QuizDetails.aspx.cs"
Inherits="EzBiologyy.Pages.QuizDetails"
MasterPageFile="~/Student.Master"%>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<div class="content">

<h1>Quiz Details</h1>

<p style="color:white;">
Review the quiz information below before you begin.
</p>

<br/>

<div class="course-card"
style="padding:40px;max-width:900px;">

<div style="margin-bottom:30px;">

<h3>Quiz Title</h3>

<h2>
<asp:Label
ID="quizTitleLbl"
runat="server">
</asp:Label>
</h2>

</div>

<div style="margin-bottom:30px;">

<h3>Course</h3>

<h2>
<asp:Label
ID="courseLbl"
runat="server">
</asp:Label>
</h2>

</div>

<asp:Button
ID="startBtn"
runat="server"
Text="Start Quiz"
OnClick="startBtn_Click"
CssClass="status open"/>

</div>

</div>

</asp:Content>