<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentAssessmentMenu.aspx.cs" Inherits="EzBiologyy.Pages.StudentAssessmentMenu" MasterPageFile="~/Student.Master"%>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server" />
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

                <!-- QUIZ -->
                <tr onclick="window.location.href='quiz-details.html'" style="cursor:pointer;">
                    <td>Cell Structure Quiz</td>
                    <td>Course 1</td>
                    <td>Quiz</td>
                    <td>
                        <span class="status open">Open</span>
                    </td>
                </tr>

                <!-- ASSESSMENT -->
                <tr onclick="window.location.href='assessment-details.html'" style="cursor:pointer;">
                    <td>Plant Growth Lab Report</td>
                    <td>Course 2</td>
                    <td>Assessment</td>
                    <td>
                        <span class="status open">Open</span>
                    </td>
                </tr>

                <!-- QUIZ -->
                <tr onclick="window.location.href='quiz-details.html'" style="cursor:pointer;">
                    <td>Genetics Worksheet</td>
                    <td>Course 3</td>
                    <td>Quiz</td>
                    <td>
                        <span class="status upcoming">Upcoming</span>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>