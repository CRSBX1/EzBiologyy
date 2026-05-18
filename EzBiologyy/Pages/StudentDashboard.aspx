<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentDashboard.aspx.cs" Inherits="EzBiologyy.Pages.StudentDashboard" MasterPageFile="~/Student.Master"%>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server" >
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="content">
        <h1>Welcome back! <asp:Label ID="nameLbl" runat="server" /></h1>
        <p>Continue your learning journey.</p>

        <h3 style="margin-top:40px;">My Enrolled Courses</h3>

        <div class="courses">

            <div class="course-card">
                <img src="images/img1.png" class="course-img">
                <div class="course-bottom">
                    <h3>Course 1</h3>
                    <span class="status in-progress">In Progress</span>
                </div>
            </div>

            <div class="course-card">
                <img src="images/img2.png" class="course-img">
                <div class="course-bottom">
                    <h3>Course 2</h3>
                    <span class="status in-progress">In Progress</span>
                </div>
            </div>

            <div class="course-card">
                <img src="images/img3.png" class="course-img">
                <div class="course-bottom">
                    <h3>Course 3</h3>
                    <span class="status completed">Completed</span>
                </div>
            </div>

        </div>
    </div>
</asp:Content>