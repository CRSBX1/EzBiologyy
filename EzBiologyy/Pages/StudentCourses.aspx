<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentCourses.aspx.cs" Inherits="EzBiologyy.Pages.StudentCourses" MasterPageFile="~/Student.Master" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server"> </asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
      <div class="page-wrapper">
      <div class="page-inner">

        <div class="page-title">Courses</div>
        <div class="page-desc">Browse and manage your biology courses.</div>

        <div class="tab-bar">
          <button type="button" class="tab-btn active" onclick="switchTab('enrolled', this)">My enrolled courses</button>
          <button type="button" class="tab-btn" onclick="switchTab('available', this)">Available courses</button>
        </div>

        <!-- ENROLLED -->
        <div id="tab-enrolled">
          <div class="section-label">Enrolled</div>
          <div class="courses-grid">

<asp:Repeater ID="rptEnrolledCourses" runat="server">
    <ItemTemplate>
        <a class="course-card" href='StudentCourseDetails.aspx?CourseID=<%# Eval("CourseID") %>'>
            <div class="course-card-top">
                <svg width="48" height="48" viewBox="0 0 48 48" fill="none" stroke="white" stroke-width="1.5">
                    <circle cx="24" cy="24" r="16"/>
                    <circle cx="24" cy="24" r="6"/>
                    <line x1="24" y1="8" x2="24" y2="18"/>
                    <line x1="24" y1="30" x2="24" y2="40"/>
                    <line x1="8" y1="24" x2="18" y2="24"/>
                    <line x1="30" y1="24" x2="40" y2="24"/>
                </svg>
            </div>
            <div class="course-card-body">
                <div class="course-card-title"><%# Eval("CourseName") %></div>
                <div class="course-card-desc">View course materials, quizzes, and assessments.</div>
                <span class="status-badge inprogress">● Enrolled</span>
            </div>
        </a>
    </ItemTemplate>
</asp:Repeater>

          </div>
        </div>

        <!-- AVAILABLE -->
        <div id="tab-available" style="display:none">
          <div class="section-label">Available to enroll</div>
          <div class="courses-grid">

<asp:Repeater ID="rptAvailableCourses" runat="server" OnItemCommand="rptAvailableCourses_ItemCommand">
    <ItemTemplate>
        <div class="course-card">
            <div class="course-card-top">
                <svg width="48" height="48" viewBox="0 0 48 48" fill="none" stroke="white" stroke-width="1.5">
                    <circle cx="24" cy="18" r="8"/>
                    <path d="M10 40 C10 30 38 30 38 40"/>
                </svg>
            </div>

            <div class="course-card-body">
                <div class="course-card-title"><%# Eval("CourseName") %></div>
                <div class="course-card-desc">Enroll to access this biology course.</div>

                <asp:LinkButton 
                    ID="btnEnroll" 
                    runat="server" 
                    CssClass="enroll-btn"
                    CommandName="Enroll"
                    CommandArgument='<%# Eval("CourseID") %>'>
                    Enroll
                </asp:LinkButton>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
              </div>
            </div>

          </div>
        </div>
    <script>
    function switchTab(tabName, button) {
        document.getElementById("tab-enrolled").style.display = "none";
        document.getElementById("tab-available").style.display = "none";

        document.getElementById("tab-" + tabName).style.display = "block";

        var buttons = document.querySelectorAll(".tab-btn");
        buttons.forEach(function (btn) {
            btn.classList.remove("active");
        });

      </div>
    </div>
</asp:Content>
<asp:Repeater ID="courseRepeater" runat="server">

    <ItemTemplate>

        <div class="course-card">

            <div class="course-bottom">
                <h3>
                    <%# Eval("CourseName") %>
                </h3>

                <span class="status in-progress">
                    Enrolled
                </span>
            </div>

        </div>

    </ItemTemplate>

</asp:Repeater>
