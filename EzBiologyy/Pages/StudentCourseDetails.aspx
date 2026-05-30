<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentCourseDetails.aspx.cs" Inherits="EzBiologyy.Pages.StudentCourseDetails" MasterPageFile="~/Student.Master" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-wrapper">
        <div class="page-inner">

            <div class="breadcrumb">
                <a href="StudentCourses.aspx">Courses</a> › 
                <asp:Label ID="lblBreadcrumbCourseName" runat="server" Text="Course"></asp:Label>
            </div>

            <div class="page-title">
                <asp:Label ID="lblCourseName" runat="server" Text="Course Name"></asp:Label>
            </div>

            <div class="page-status">In Progress</div>

            <div class="course-detail-card">
                <div class="course-detail-header">
                    <h3>Course materials</h3>
                    <p>Select a section to begin</p>
                </div>

                <!-- MATERIALS -->
                <div class="acc-section">
                    <div class="acc-header" onclick="toggleAcc('mat')">
                        <div class="acc-left">
                            <div class="acc-icon">
                                <svg width="15" height="15" viewBox="0 0 16 16" fill="none" stroke="rgba(255,255,255,0.7)" stroke-width="1.5">
                                    <rect x="3" y="2" width="10" height="12" rx="1.5"/>
                                    <line x1="5.5" y1="5.5" x2="10.5" y2="5.5"/>
                                    <line x1="5.5" y1="8" x2="10.5" y2="8"/>
                                    <line x1="5.5" y1="10.5" x2="8.5" y2="10.5"/>
                                </svg>
                            </div>
                            <div>
                                <div class="acc-title">Materials</div>
                                <div class="acc-sub">Lecture notes and readings</div>
                            </div>
                        </div>
                        <span class="acc-chev" id="mat-chev">›</span>
                    </div>

                    <div class="acc-body" id="mat-body">
                        <asp:Repeater ID="rptMaterials" runat="server">
                            <ItemTemplate>
                                <div class="acc-item">
                                    <div class="acc-item-left">
                                        <svg width="13" height="13" viewBox="0 0 16 16" fill="none" stroke="rgba(255,255,255,0.4)" stroke-width="1.3">
                                            <rect x="3" y="2" width="10" height="12" rx="1"/>
                                            <line x1="5.5" y1="6" x2="10.5" y2="6"/>
                                            <line x1="5.5" y1="9" x2="8.5" y2="9"/>
                                        </svg>
                                        <div>
                                            <div class="acc-item-name"><%# Eval("MaterialName") %></div>
                                            <div class="acc-item-meta"><%# Eval("MaterialType") %></div>
                                            <div class="acc-item-meta">
                                                <asp:HiddenField ID="hfFilePath" runat="server" Value='<%# Eval("FilePath") %>'/>
                                                <asp:Button runat="server" Text="Open material" ID="OpenMaterialBtn" OnClick="openMaterial" CssClass="mark-btn"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                    <button type="button" class="mark-btn" onclick="toggleDone(this)">Mark as done</button>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                        <asp:Label ID="lblNoMaterials" runat="server" Text="No materials available for this course." Visible="false"></asp:Label>
                    </div>
                </div>

                <!-- QUIZ -->
                <div class="acc-section">
                    <div class="acc-header" onclick="toggleAcc('quiz')">
                        <div class="acc-left">
                            <div class="acc-icon">
                                <svg width="15" height="15" viewBox="0 0 16 16" fill="none" stroke="rgba(255,255,255,0.7)" stroke-width="1.5">
                                    <circle cx="8" cy="8" r="5.5"/>
                                    <path d="M8 5.5v.5a1.5 1.5 0 010 3"/>
                                    <circle cx="8" cy="11" r=".4" fill="rgba(255,255,255,0.7)" stroke="none"/>
                                </svg>
                            </div>
                            <div>
                                <div class="acc-title">Quiz</div>
                                <div class="acc-sub">Test your knowledge</div>
                            </div>
                        </div>
                        <span class="acc-chev" id="quiz-chev">›</span>
                    </div>

                    <div class="acc-body" id="quiz-body">
                        <asp:Repeater ID="rptQuizzes" runat="server">
                            <ItemTemplate>
                                <div class="acc-item">
                                    <div class="acc-item-left">
                                        <svg width="13" height="13" viewBox="0 0 16 16" fill="none" stroke="rgba(255,255,255,0.4)" stroke-width="1.3">
                                            <circle cx="8" cy="8" r="5.5"/>
                                            <path d="M8 5.5v.5a1.5 1.5 0 010 3"/>
                                        </svg>
                                        <div>
                                            <div class="acc-item-name"><%# Eval("AssessmentName") %></div>
                                            <div class="acc-item-meta"><%# Eval("AssessmentType") %> · Not started</div>
                                        </div>
                                    </div>
                                    <button type="button" class="mark-btn" onclick="toggleDone(this)">Mark as done</button>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                        <asp:Label ID="lblNoQuizzes" runat="server" Text="No quizzes available for this course." Visible="false"></asp:Label>
                    </div>
                </div>

                <!-- ASSESSMENTS -->
                <div class="acc-section">
                    <div class="acc-header" onclick="toggleAcc('assess')">
                        <div class="acc-left">
                            <div class="acc-icon">
                                <svg width="15" height="15" viewBox="0 0 16 16" fill="none" stroke="rgba(255,255,255,0.7)" stroke-width="1.5">
                                    <rect x="2.5" y="2.5" width="11" height="11" rx="1.5"/>
                                    <path d="M5 8l2 2 4-4"/>
                                </svg>
                            </div>
                            <div>
                                <div class="acc-title">Assessments</div>
                                <div class="acc-sub">Graded submissions</div>
                            </div>
                        </div>
                        <span class="acc-chev" id="assess-chev">›</span>
                    </div>

                    <div class="acc-body" id="assess-body">
                        <asp:Repeater ID="rptAssessments" runat="server">
                            <ItemTemplate>
                                <div class="acc-item">
                                    <div class="acc-item-left">
                                        <svg width="13" height="13" viewBox="0 0 16 16" fill="none" stroke="rgba(255,255,255,0.4)" stroke-width="1.3">
                                            <path d="M4 2h8a1 1 0 011 1v10a1 1 0 01-1 1H4a1 1 0 01-1-1V3a1 1 0 011-1z"/>
                                            <path d="M6 6h4M6 9h2"/>
                                        </svg>
                                        <div>
                                            <div class="acc-item-name"><%# Eval("AssessmentName") %></div>
                                            <div class="acc-item-meta"><%# Eval("AssessmentType") %></div>
                                        </div>
                                    </div>
                                    <button type="button" class="mark-btn" onclick="toggleDone(this)">Mark as done</button>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                        <asp:Label ID="lblNoAssessments" runat="server" Text="No assessments available for this course." Visible="false"></asp:Label>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <script>
        function toggleAcc(id) {
            var body = document.getElementById(id + "-body");
            var chev = document.getElementById(id + "-chev");

            if (body.style.display === "block") {
                body.style.display = "none";
                chev.innerHTML = "›";
            } else {
                body.style.display = "block";
                chev.innerHTML = "⌄";
            }
        }

        function toggleDone(button) {
            if (button.innerHTML === "Done") {
                button.innerHTML = "Mark as done";
                button.classList.remove("done");
            } else {
                button.innerHTML = "Done";
                button.classList.add("done");
            }
        }
    </script>
</asp:Content>