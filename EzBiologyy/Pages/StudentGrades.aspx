<%@ Page Language="C#" AutoEventWireup="true"
CodeBehind="StudentGrades.aspx.cs"
Inherits="EzBiologyy.Pages.StudentGrades"
MasterPageFile="~/Student.Master" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<div class="content">

<h1>My Grades</h1>

<br /><br />

<asp:GridView
ID="gradesGrid"
runat="server"
AutoGenerateColumns="false"
Width="800px"
GridLines="None"
CellPadding="10">

<HeaderStyle
BackColor="#1B4332"
ForeColor="White"
Font-Bold="true"/>

<RowStyle
BackColor="White"
ForeColor="Black"/>

<AlternatingRowStyle
BackColor="#F2F2F2"
ForeColor="Black"/>

<Columns>

<asp:BoundField
DataField="AssessmentName"
HeaderText="Assessment"/>

<asp:BoundField
DataField="Grade"
HeaderText="Grade"/>

<asp:BoundField
DataField="GradePoints"
HeaderText="Points"/>

</Columns>

</asp:GridView>

</div>

</asp:Content>