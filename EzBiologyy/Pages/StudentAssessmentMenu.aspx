<%@ Page Language="C#" AutoEventWireup="true"
CodeBehind="StudentAssessmentMenu.aspx.cs"
Inherits="EzBiologyy.Pages.StudentAssessmentMenu"
MasterPageFile="~/Student.Master"%>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<div class="content">

<h1>Assessments</h1>

<div class="table-card">

<table>

<tr>
<th>Title</th>
<th>Course</th>
<th>Type</th>
<th>Status</th>
</tr>

<asp:Repeater
ID="assessmentRepeater"
runat="server">

<ItemTemplate>

<tr

onclick='<%#

Eval("Status").ToString()=="Submitted"

?

"window.location.href=\"StudentGrades.aspx\""

:

(Eval("Status").ToString()=="Upcoming"

?

""

:

"window.location.href=\"QuizDetails.aspx?AssessmentID="+
Eval("AssessmentID")+"\"")

%>'

style='<%#

Eval("Status").ToString()=="Upcoming"

?

"cursor:default;"

:

"cursor:pointer;"

%>'>

<td>
<%# Eval("AssessmentName") %>
</td>

<td>
<%# Eval("CourseName") %>
</td>

<td>
<%# Eval("AssessmentType") %>
</td>

<td>

<span class='status

<%#

Eval("Status").ToString()=="Submitted"

?

"completed"

:

(Eval("Status").ToString()=="Upcoming"

?

"upcoming"

:

"open")

%>'>

<%#

Eval("Status").ToString()=="Submitted"

?

"View Results"

:

(Eval("Status").ToString()=="Upcoming"

?

"Upcoming"

:

"Open")

%>

</span>

</td>

</tr>

</ItemTemplate>

</asp:Repeater>

</table>

</div>

</div>

<style>

.completed
{
background-color:gray !important;
color:white;
}

.upcoming
{
background-color:#ffc107 !important;
color:black !important;
}

</style>

</asp:Content>