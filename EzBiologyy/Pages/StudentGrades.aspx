<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentGrades.aspx.cs" Inherits="EzBiologyy.Pages.StudentGrades" MasterPageFile="~/Student.Master"%>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server" />
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <h1>Grades</h1>
        <p>View your assessment results.</p>

        <div class="table-card">

            <table>
                <tr>
                    <th>Assessment</th>
                    <th>Grade</th>
                    <th>Percentage</th>
                </tr>

                <tr>
                    <td>Cell Structure Quiz</td>
                    <td>A</td>
                    <td>92%</td>
                </tr>

                <tr>
                    <td>Plant Growth Lab Report</td>
                    <td>B+</td>
                    <td>85%</td>
                </tr>

                <tr>
                    <td>Photosynthesis Assignment</td>
                    <td>A-</td>
                    <td>88%</td>
                </tr>

            </table>

        </div>

    </div>
</asp:Content>