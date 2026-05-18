<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentCourses.aspx.cs" Inherits="EzBiologyy.Pages.StudentCourses" MasterPageFile="~/Student.Master" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server"> </asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
      <div class="page-wrapper">
      <div class="page-inner">

        <div class="page-title">Courses</div>
        <div class="page-desc">Browse and manage your biology courses.</div>

        <div class="tab-bar">
          <button class="tab-btn active" onclick="switchTab('enrolled', this)">My enrolled courses</button>
          <button class="tab-btn" onclick="switchTab('available', this)">Available courses</button>
        </div>

        <!-- ENROLLED -->
        <div id="tab-enrolled">
          <div class="section-label">Enrolled</div>
          <div class="courses-grid">

            <a class="course-card" href="course-detail.html">
              <div class="course-card-top">
                <svg width="48" height="48" viewBox="0 0 48 48" fill="none" stroke="white" stroke-width="1.5">
                  <circle cx="24" cy="24" r="16"/><circle cx="24" cy="24" r="6"/>
                  <line x1="24" y1="8" x2="24" y2="18"/><line x1="24" y1="30" x2="24" y2="40"/>
                  <line x1="8" y1="24" x2="18" y2="24"/><line x1="30" y1="24" x2="40" y2="24"/>
                </svg>
              </div>
              <div class="course-card-body">
                <div class="course-card-title">Cell Biology</div>
                <div class="course-card-desc">Explore cell structure, organelles, and biological processes in depth.</div>
                <span class="status-badge inprogress">● In Progress</span>
              </div>
            </a>

            <a class="course-card" href="course-detail.html">
              <div class="course-card-top">
                <svg width="48" height="48" viewBox="0 0 48 48" fill="none" stroke="white" stroke-width="1.5">
                  <path d="M24 40V20"/>
                  <path d="M24 24 C18 18 10 20 8 14 C14 14 20 16 24 24"/>
                  <path d="M24 30 C30 24 38 26 40 20 C34 20 28 22 24 30"/>
                </svg>
              </div>
              <div class="course-card-body">
                <div class="course-card-title">Plant Biology</div>
                <div class="course-card-desc">Study plant anatomy, photosynthesis, growth, and reproduction.</div>
                <span class="status-badge inprogress">● In Progress</span>
              </div>
            </a>

            <a class="course-card" href="course-detail.html">
              <div class="course-card-top">
                <svg width="48" height="48" viewBox="0 0 48 48" fill="none" stroke="white" stroke-width="1.5">
                  <path d="M18 8 C18 20 30 28 30 40"/><path d="M30 8 C30 20 18 28 18 40"/>
                  <line x1="18" y1="18" x2="30" y2="22"/><line x1="18" y1="26" x2="30" y2="30"/>
                </svg>
              </div>
              <div class="course-card-body">
                <div class="course-card-title">Genetics</div>
                <div class="course-card-desc">Understand inheritance, DNA, gene expression, and genetic variation.</div>
                <span class="status-badge completed">✓ Completed</span>
              </div>
            </a>

          </div>
        </div>

        <!-- AVAILABLE -->
        <div id="tab-available" style="display:none">
          <div class="section-label">Available to enroll</div>
          <div class="courses-grid">

            <div class="course-card">
              <div class="course-card-top">
                <svg width="48" height="48" viewBox="0 0 48 48" fill="none" stroke="white" stroke-width="1.5">
                  <circle cx="24" cy="18" r="8"/><path d="M10 40 C10 30 38 30 38 40"/>
                </svg>
              </div>
              <div class="course-card-body">
                <div class="course-card-title">Ecology</div>
                <div class="course-card-desc">Discover ecosystems, food webs, and environmental conservation strategies.</div>
                <button class="enroll-btn">Enroll</button>
              </div>
            </div>

            <div class="course-card">
              <div class="course-card-top">
                <svg width="48" height="48" viewBox="0 0 48 48" fill="none" stroke="white" stroke-width="1.5">
                  <rect x="12" y="10" width="24" height="28" rx="3"/>
                  <line x1="18" y1="18" x2="30" y2="18"/><line x1="18" y1="24" x2="30" y2="24"/><line x1="18" y1="30" x2="24" y2="30"/>
                </svg>
              </div>
              <div class="course-card-body">
                <div class="course-card-title">Microbiology</div>
                <div class="course-card-desc">Study bacteria, viruses, fungi, and their roles in health and environment.</div>
                <button class="enroll-btn">Enroll</button>
              </div>
            </div>

            <div class="course-card">
              <div class="course-card-top">
                <svg width="48" height="48" viewBox="0 0 48 48" fill="none" stroke="white" stroke-width="1.5">
                  <circle cx="24" cy="24" r="14"/><circle cx="24" cy="24" r="6"/>
                  <line x1="10" y1="24" x2="18" y2="24"/><line x1="30" y1="24" x2="38" y2="24"/>
                  <line x1="24" y1="10" x2="24" y2="18"/><line x1="24" y1="30" x2="24" y2="38"/>
                </svg>
              </div>
              <div class="course-card-body">
                <div class="course-card-title">Human Anatomy</div>
                <div class="course-card-desc">Learn about human body systems, organs, tissues, and their functions.</div>
                <button class="enroll-btn">Enroll</button>
              </div>
            </div>

          </div>
        </div>

      </div>
    </div>
</asp:Content>